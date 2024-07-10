using EmailNotificationService.Domain.Models;

namespace EmailNotificationService.Domain.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(Notification notification);
    }
}
