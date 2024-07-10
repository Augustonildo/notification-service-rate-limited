using System.Net.Mail;

namespace EmailNotificationService.Domain.Models
{
    public class Notification
    {
        public string To { get; set; } = string.Empty;
        public List<string> Cc { get; set; } = new List<string>();
        public List<string> Bcc { get; set; } = new List<string>();
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        // Attachments
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();

        public Notification() { }

        public Notification(SendEmailEvent e)
        {
            To = e.To;
            Cc = e.Cc;
            Bcc = e.Bcc;
            Subject = e.Subject;
            Body = e.Body;
            Type = e.Type;
            Attachments = e.Attachments;
        }
    }
}
