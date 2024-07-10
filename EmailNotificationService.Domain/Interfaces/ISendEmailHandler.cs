using EmailNotificationService.Domain.Models;

namespace EmailNotificationService.Domain.Interfaces
{
    public interface ISendEmailHandler
    {
        Task RunAsync(SendEmailEvent sendEmailEvent);
    }
}
