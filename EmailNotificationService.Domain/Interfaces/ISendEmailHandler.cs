using Microsoft.Extensions.Logging;

namespace EmailNotificationService.Domain.Interfaces
{
    public interface ISendEmailHandler
    {
        void Run(string myQueueItem, ILogger log);
    }
}
