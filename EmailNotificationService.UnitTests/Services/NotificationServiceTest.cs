using EmailNotificationService.Domain.Exceptions;
using EmailNotificationService.Domain.Interfaces;
using EmailNotificationService.Domain.Models;
using EmailNotificationService.Services;
using EmailNotificationService.UnitTests.Helpers;
using Microsoft.Extensions.Configuration;

namespace EmailNotificationService.UnitTests.Services
{
    public class NotificationServiceTest
    {
        private readonly Mock<IEmailSender> _emailSenderMock;
        private readonly Mock<IEmailSentRepository> _emailSentRepositoryMock;
        private readonly IConfiguration _configuration;

        public NotificationServiceTest()
        {
            _emailSenderMock = new Mock<IEmailSender>();
            _emailSentRepositoryMock = new Mock<IEmailSentRepository>();

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(NotificationHelper.GetRateLimitConfigurationDictionary())
                .Build();
        }

        [Fact]
        public async Task SendAsync_EmptyToField_ShouldThrowMissingEmailInformationException()
        {
            Notification emptyNotification = new Notification();

            var notificationService = new NotificationService(_emailSenderMock.Object, _emailSentRepositoryMock.Object, _configuration);

            await Assert.ThrowsAsync<MissingEmailInformationException>(async () => await notificationService.SendAsync(emptyNotification));
        }

        [Fact]
        public async Task SendAsync_EmptySubjectField_ShouldThrowMissingEmailInformationException()
        {
            Notification semiemptyNotification = new Notification
            {
                To = "userEmail@domain.com"
            };

            var notificationService = new NotificationService(_emailSenderMock.Object, _emailSentRepositoryMock.Object, _configuration);

            await Assert.ThrowsAsync<MissingEmailInformationException>(async () => await notificationService.SendAsync(semiemptyNotification));
        }

        [Fact]
        public async Task SendAsync_NotificationOfUnsupportedType_ShouldThrowUnsupportedNotificationTypeException()
        {
            Notification notification = NotificationHelper.GetNotification(type: "InvalidType");

            var notificationService = new NotificationService(_emailSenderMock.Object, _emailSentRepositoryMock.Object, _configuration);

            await Assert.ThrowsAsync<UnsupportedNotificationTypeException>(async () => await notificationService.SendAsync(notification));
        }

        [Fact]
        public async Task SendAsync_ExternalErrorOnEmailSentRepository_ShouldThrowEmailSentRepositoryException()
        {
            Notification notification = NotificationHelper.GetNotification();

            _emailSentRepositoryMock.Setup(esr => esr.CountEmailSentInTimeRangeByTypeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ThrowsAsync(new EmailSentRepositoryException());

            var notificationService = new NotificationService(_emailSenderMock.Object, _emailSentRepositoryMock.Object, _configuration);

            await Assert.ThrowsAsync<EmailSentRepositoryException>(async () => await notificationService.SendAsync(notification));
        }

        [Fact]
        public async Task SendAsync_NotificationOverRateLimit_ShouldNotSendEmailAndThrowNotificationOverTheLimitException()
        {
            Notification notification = NotificationHelper.GetNotification();

            _emailSentRepositoryMock.Setup(esr => esr.CountEmailSentInTimeRangeByTypeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(99999);

            var notificationService = new NotificationService(_emailSenderMock.Object, _emailSentRepositoryMock.Object, _configuration);

            await Assert.ThrowsAsync<NotificationOverTheLimitException>(async () => await notificationService.SendAsync(notification));
            _emailSentRepositoryMock.Verify(esr => esr.CountEmailSentInTimeRangeByTypeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Once());
            _emailSentRepositoryMock.Verify(esr => esr.RegisterEmailSentAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
            _emailSenderMock.Verify(es => es.SendAsync(It.IsAny<Notification>()), Times.Never());
        }

        [Fact]
        public async Task SendAsync_NotificationUnderLimit_EmailSent()
        {
            Notification notification = NotificationHelper.GetNotification();

            _emailSentRepositoryMock.Setup(esr => esr.CountEmailSentInTimeRangeByTypeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(0);

            var notificationService = new NotificationService(_emailSenderMock.Object, _emailSentRepositoryMock.Object, _configuration);

            await notificationService.SendAsync(notification);

            _emailSentRepositoryMock.Verify(esr => esr.CountEmailSentInTimeRangeByTypeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Once());
            _emailSentRepositoryMock.Verify(esr => esr.RegisterEmailSentAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            _emailSenderMock.Verify(es => es.SendAsync(It.IsAny<Notification>()), Times.Once());
        }

        [Fact]
        public async Task SendAsync_NotificationHasNoRateLimit_EmailSent_DoesntCheckCount()
        {
            Notification notification = NotificationHelper.GetNotification(type: "Security Breach");

            var notificationService = new NotificationService(_emailSenderMock.Object, _emailSentRepositoryMock.Object, _configuration);

            await notificationService.SendAsync(notification);

            _emailSentRepositoryMock.Verify(esr => esr.CountEmailSentInTimeRangeByTypeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never());
            _emailSentRepositoryMock.Verify(esr => esr.RegisterEmailSentAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            _emailSenderMock.Verify(es => es.SendAsync(It.IsAny<Notification>()), Times.Once());
        }
    }
}
