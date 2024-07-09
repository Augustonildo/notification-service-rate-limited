using EmailNotificationService.Handlers;
using Microsoft.Extensions.Logging;

namespace EmailNotificationService.UnitTests.Handlers
{
    public class SendEmailHandlerTest
    {
        private Mock<ILogger<SendEmailHandler>> _loggerMock;
        private SendEmailHandler _handler;

        public SendEmailHandlerTest()
        {
            _loggerMock = new Mock<ILogger<SendEmailHandler>>();

            _handler = new SendEmailHandler(_loggerMock.Object);
        }

        [Fact]
        public async Task Run_Should_CreateScenarios()
        {

        }
    }
}