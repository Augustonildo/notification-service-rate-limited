using EmailNotificationService.Domain.Models;

namespace EmailNotificationService.Domain.Interfaces
{
    public interface INotificationService
    {
        void Send(Notification notification);
    }
}
