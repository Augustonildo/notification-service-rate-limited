using EmailNotificationService.Domain.Interfaces;
using EmailNotificationService.Domain.Models;
using EmailNotificationService.Handlers;
using Microsoft.Extensions.Logging;

namespace EmailNotificationService.UnitTests.Handlers
{
    public class SendEmailHandlerTest
    {
        private Mock<ILogger<SendEmailHandler>> _loggerMock;
        private Mock<INotificationService> _notificationServiceMock;
        private SendEmailHandler _handler;

        public SendEmailHandlerTest()
        {
            _loggerMock = new Mock<ILogger<SendEmailHandler>>();
            _notificationServiceMock = new Mock<INotificationService>();
            _handler = new SendEmailHandler(_loggerMock.Object, _notificationServiceMock.Object);
        }

        [Fact]
        public async Task Run_Should_CreateScenarios()
        {
            // TODO: create test scenarios

            _handler.Run(new SendEmailEvent());
        }
    }
}