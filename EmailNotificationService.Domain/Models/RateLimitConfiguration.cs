namespace EmailNotificationService.Domain.Models
{
    public class RateLimitConfiguration
    {
        public int TimeRangeInMinutes { get; set; }
        public int NotificationLimit { get; set; }
    }
}
