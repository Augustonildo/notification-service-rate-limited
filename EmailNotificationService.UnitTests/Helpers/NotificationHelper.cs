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
            var rateLimitConfigurations = new List<RateLimitConfiguration>
            {
                new RateLimitConfiguration {
                  Type = "Status",
                  HasRateLimit = true,
                  TimeRangeInMinutes= 1,
                  NotificationLimit= 2
                },
                new RateLimitConfiguration {
                  Type= "News",
                  HasRateLimit= true,
                  TimeRangeInMinutes= 1440,
                  NotificationLimit= 1
                },
                new RateLimitConfiguration {
                  Type= "Marketing",
                  HasRateLimit= true,
                  TimeRangeInMinutes= 60,
                  NotificationLimit= 3
                },
                new RateLimitConfiguration {
                  Type= "Security Breach",
                  HasRateLimit= false
                }
            };

            var configDictionary = new Dictionary<string, string>();

            for (int index = 0; index < rateLimitConfigurations.Count; index++)
            {
                var config = rateLimitConfigurations[index];
                configDictionary[$"RateLimitTypeList:{index}:Type"] = config.Type.ToString();
                configDictionary[$"RateLimitTypeList:{index}:HasRateLimit"] = config.HasRateLimit.ToString();
                configDictionary[$"RateLimitTypeList:{index}:TimeRangeInMinutes"] = config.TimeRangeInMinutes.ToString();
                configDictionary[$"RateLimitTypeList:{index}:NotificationLimit"] = config.NotificationLimit.ToString();
            }

            return configDictionary;
        }
    }
}
