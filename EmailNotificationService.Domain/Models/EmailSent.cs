namespace EmailNotificationService.Domain.Models
{
    public class EmailSent
    {
        public string To { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTime Time { get; set; }
    }
}
