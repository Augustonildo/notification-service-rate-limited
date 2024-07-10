using EmailNotificationService.Domain.Models;

namespace EmailNotificationService.Domain.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Notification notification);
    }
}
