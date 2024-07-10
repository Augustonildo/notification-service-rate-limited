using EmailNotificationService.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace EmailNotificationService.Integration
{
    public class EmailSender : IEmailSender
    {
        public EmailSender() { }

        public async Task SendAsync(string email, string message)
        {
            Console.WriteLine(" to user: " + email + "\nMessage:\"" + message + "\"");
        }
    }
}
