using EmailNotificationService.Domain.Models;
using System.Net.Mail;

namespace EmailNotificationService.UnitTests.Helpers
{
    public static class NotificationHelper
    {
        public static Notification GetNotification(
            string to = "userEmail@domain.com",
            List<string>? cc = null,
            List<string>? bcc = null,
            string subject = "Subject",
            string body = "Body",
            string type = "Status",
            List<Attachment>? attachments = null)
        {
            return new Notification
            {
                To = to,
                Cc = cc ?? new List<string>(),
                Bcc = bcc ?? new List<string>(),
                Subject = subject,
                Body = body,
                Type = type,
                Attachments = attachments ?? new List<Attachment>()
            };
        }

        public static Dictionary<string, string> GetRateLimitConfigurationDictionary()
        {
            var rateLimitConfigurations = new List<EmailTypeConfiguration>
            {
                new EmailTypeConfiguration {
                  Type = "Status",
                  RateLimit = new RateLimitConfiguration {
                      TimeRangeInMinutes= 1,
                      NotificationLimit= 2
                  }
                },
                new EmailTypeConfiguration {
                  Type= "News",
                  RateLimit = new RateLimitConfiguration {
                      TimeRangeInMinutes= 1440,
                      NotificationLimit= 1
                  }
                },
                new EmailTypeConfiguration {
                  Type= "Marketing",
                  RateLimit = new RateLimitConfiguration {
                      TimeRangeInMinutes= 60,
                      NotificationLimit= 3
                  }
                },
                new EmailTypeConfiguration {
                  Type= "Security Breach"
                }
            };

            var configDictionary = new Dictionary<string, string>();

            for (int index = 0; index < rateLimitConfigurations.Count; index++)
            {
                var config = rateLimitConfigurations[index];
                configDictionary[$"EmailTypeList:{index}:Type"] = config.Type.ToString();
                if (config.RateLimit != null)
                {
                    configDictionary[$"EmailTypeList:{index}:RateLimit:TimeRangeInMinutes"] = config.RateLimit.TimeRangeInMinutes.ToString();
                    configDictionary[$"EmailTypeList:{index}:RateLimit:NotificationLimit"] = config.RateLimit.NotificationLimit.ToString();
                }
            }

            return configDictionary;
        }
    }
}
