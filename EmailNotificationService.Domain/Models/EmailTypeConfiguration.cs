namespace EmailNotificationService.Domain.Models
{
    public class EmailTypeConfiguration
    {
        public string Type { get; set; } = string.Empty;
        public RateLimitConfiguration? RateLimit { get; set; }
    }
}
