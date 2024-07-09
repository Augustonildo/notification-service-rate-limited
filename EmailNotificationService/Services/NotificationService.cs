using EmailNotificationService.Domain.Interfaces;
using System;

namespace EmailNotificationService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailSender _emailSender;

        public NotificationService(IEmailSender emailSender)
        {
            this._emailSender = emailSender;

        }

        // TODO: TASK: IMPLEMENT this
        public void Send(string type, string userId, string message)
        {
            throw new Exception("not implemented - fix this");
        }

    }
}
