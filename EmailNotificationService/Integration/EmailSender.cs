using EmailNotificationService.Domain.Interfaces;
using EmailNotificationService.Domain.Models;
using System;
using System.Threading.Tasks;

namespace EmailNotificationService.Integration
{
    public class EmailSender : IEmailSender
    {
        public EmailSender() { }

        public async Task SendAsync(Notification notification)
        {
            // This service would be integrated to a SMTP structure that authenticates and sends the e-mail from an official source.

            Console.WriteLine($@"To: {notification.To} 
                                \n Cc: {notification.Cc}
                                \n Bcc: {notification.Bcc}
                                \n Subject: {notification.Subject}
                                \n Body: {notification.Body}
                                \n Attachments: {notification.Attachments}");

        }
    }
}
