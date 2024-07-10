using EmailNotificationService.Domain.Interfaces;
using EmailNotificationService.Domain.Models;
using EmailNotificationService.Handlers;
using Microsoft.Extensions.Logging;

namespace EmailNotificationService.UnitTests.Handlers
{
    public class SendEmailHandlerTest
    {
        private readonly Mock<ILogger<SendEmailHandler>> _loggerMock;
        private readonly Mock<INotificationService> _notificationServiceMock;
        private SendEmailHandler? _handler;

        public SendEmailHandlerTest()
        {
            _loggerMock = new Mock<ILogger<SendEmailHandler>>();
            _notificationServiceMock = new Mock<INotificationService>();
        }

        [Fact]
        public async Task RunAsync_NotificationServiceException_ThrowsException()
        {
            SendEmailEvent emailEvent = new SendEmailEvent { EventId = Guid.NewGuid() };

            _notificationServiceMock.Setup(esr => esr.SendAsync(It.IsAny<Notification>())).ThrowsAsync(new Exception());

            _handler = new SendEmailHandler(_loggerMock.Object, _notificationServiceMock.Object);
            await Assert.ThrowsAsync<Exception>(async () => await _handler.RunAsync(emailEvent));

            VerifyLogError(Times.Once(), emailEvent.EventId);
        }

        [Fact]
        public async Task RunAsync_Success()
        {
            SendEmailEvent emailEvent = new SendEmailEvent { EventId = Guid.NewGuid() };

            _handler = new SendEmailHandler(_loggerMock.Object, _notificationServiceMock.Object);
            await _handler.RunAsync(emailEvent);

            VerifyLogError(Times.Never(), emailEvent.EventId);
        }

        private void VerifyLogError(Times times, Guid eventId)
        {
            _loggerMock.Verify(logger => logger.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((@object, @type) => @object != null && @object.ToString()!.Equals($"Error on trying to process sendEmailEvent#{eventId}")),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
            times);
        }
    }
}