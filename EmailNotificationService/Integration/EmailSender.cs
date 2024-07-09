using EmailNotificationService.Domain.Interfaces;
using System;

namespace EmailNotificationService.Integration
{
    public class EmailSender : IEmailSender
    {
        public EmailSender() { }

        public void Send(string email, string message)
        {
            Console.WriteLine(" to user: " + email + "\nMessage:\"" + message + "\"");
        }
    }
}
