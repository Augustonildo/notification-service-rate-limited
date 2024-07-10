using EmailNotificationService.Domain.Interfaces;
using EmailNotificationService.Domain.Models;
using EmailNotificationService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EmailNotificationService.UnitTests.Services
{
    public class NotificationServiceTest
    {
        private Mock<ILogger<NotificationService>> _loggerMock;
        private Mock<IEmailSender> _emailSenderMock;
        private Mock<IConfiguration> _configurationMock;

        public NotificationServiceTest()
        {
            _loggerMock = new Mock<ILogger<NotificationService>>();
            _emailSenderMock = new Mock<IEmailSender>();
            _configurationMock = new Mock<IConfiguration>();
        }

        [Fact]
        public async Task Send_Should_CreateScenarios()
        {
            // TODO: create test scenarios

            var notificationService = new NotificationService(_loggerMock.Object, _emailSenderMock.Object, _configurationMock.Object);
            notificationService.Send(new Notification());
        }
    }
}
