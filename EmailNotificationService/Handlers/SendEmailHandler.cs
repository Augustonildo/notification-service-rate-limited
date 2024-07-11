using EmailNotificationService.Domain.Interfaces;
using EmailNotificationService.Domain.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EmailNotificationService.Handlers
{
    public class SendEmailHandler : ISendEmailHandler
    {
        private readonly ILogger<SendEmailHandler> _logger;
        private readonly INotificationService _notificationService;

        public SendEmailHandler(ILogger<SendEmailHandler> logger, INotificationService notificationService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        /// <summary>
        /// This handler function consumes events from a service-bus queue and sends an e-mail if all validations pass.
        /// </summary>
        /// <param name="sendEmailEvent">A queue event representing 1 e-mail to be sent</param>
        /// <returns></returns>
        [FunctionName("SendEmailFunction")]
        public async Task RunAsync([ServiceBusTrigger("mail-queue", IsSessionsEnabled = true, Connection = "ServiceBusConnection")] SendEmailEvent sendEmailEvent)
        {
            _logger.LogInformation($"Received sendEmailEvent#{sendEmailEvent.EventId} to user {sendEmailEvent.To} with subject: {sendEmailEvent.Subject}");

            try
            {
                Notification notification = new Notification(sendEmailEvent);
                await _notificationService.SendAsync(notification);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error on trying to process sendEmailEvent#{sendEmailEvent.EventId}");
                throw;
            }
        }
    }
}