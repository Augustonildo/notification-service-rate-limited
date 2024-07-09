using EmailNotificationService.Domain.Interfaces;
using EmailNotificationService.Integration;
using EmailNotificationService.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace EmailNotificationService.Handlers
{
    public class SendEmailHandler : ISendEmailHandler
    {
        private readonly ILogger<SendEmailHandler> _logger;

        public SendEmailHandler(ILogger<SendEmailHandler> logger)
        {
            _logger = logger;
        }

        /// TODO: Document email handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myQueueItem"></param>
        /// <param name="log"></param>
        [FunctionName("SendEmailFunction")]
        public void Run([ServiceBusTrigger("mail-queue", Connection = "")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

            INotificationService service = new NotificationService(new EmailSender());

            service.Send("news", "user", "news 1");
            service.Send("news", "user", "news 2");
            service.Send("news", "user", "news 3");
            service.Send("news", "another user", "news 1");
            service.Send("update", "user", "update 1");
        }
    }
}