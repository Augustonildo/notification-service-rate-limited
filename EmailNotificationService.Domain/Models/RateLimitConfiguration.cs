namespace EmailNotificationService.Domain.Models
{
    public class RateLimitConfiguration
    {
        public string Type { get; set; } = string.Empty;
        public bool HasRateLimit { get; set; }
        public int TimeRangeInMinutes { get; set; }
        public int NotificationLimit { get; set; }
    }
}
