using System.Net.Mail;

namespace EmailNotificationService.Domain.Models
{
    public class SendEmailEvent
    {
        public Guid EventId { get; set; }
        public Guid SessionId { get; set; }
        public string To { get; set; } = string.Empty;
        public List<string> Cc { get; set; } = new List<string>();
        public List<string> Bcc { get; set; } = new List<string>();
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        // Attachments
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
