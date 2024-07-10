using EmailNotificationService.Domain.Models;

namespace EmailNotificationService.Domain.Interfaces
{
    public interface ISendEmailHandler
    {
        void Run(SendEmailEvent sendEmailEvent);
    }
}
