using EmailNotificationService.Domain.Exceptions;
using EmailNotificationService.Domain.Interfaces;
using EmailNotificationService.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailNotificationService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailSender _emailSender;
        private readonly IEmailSentRepository _emailSentRepository;
        private readonly Dictionary<string, EmailTypeConfiguration> _emailTypeDictionary;

        public NotificationService(IEmailSender emailSender, IEmailSentRepository emailSentRepository, IConfiguration configuration)
        {
            this._emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            this._emailSentRepository = emailSentRepository ?? throw new ArgumentNullException(nameof(emailSentRepository));

            var emailTypeConfigurationList = new List<EmailTypeConfiguration>();
            configuration.GetSection("EmailTypeList").Bind(emailTypeConfigurationList);
            _emailTypeDictionary = new Dictionary<string, EmailTypeConfiguration>();
            foreach (var item in emailTypeConfigurationList)
            {
                _emailTypeDictionary[item.Type] = item;
            }
        }

        public async Task SendAsync(Notification notification)
        {
            ValidateEmailInformation(notification);

            EmailTypeConfiguration emailType = GetEmailType(notification);
            await ValidateRateLimitAsync(emailType, notification);

            // This implementations assumes that, after validating e-mail information, all exceptions from emailSender are unexpected.
            // In this case, any exceptions thrown in SendAsync should stop the execution and return the message to the queue to be retried.
            await _emailSender.SendAsync(notification);
            await _emailSentRepository.RegisterEmailSentAsync(notification.To, emailType.Type);
        }

        private static void ValidateEmailInformation(Notification notification)
        {
            if (notification == null) throw new MissingEmailInformationException("Invalid Email Information: notification is null");
            if (string.IsNullOrEmpty(notification.To)) throw new MissingEmailInformationException("Invalid Email Information: missing 'To' information");
            if (string.IsNullOrEmpty(notification.Subject)) throw new MissingEmailInformationException("Invalid Email Information: missing 'Subject' information");
            if (string.IsNullOrEmpty(notification.Body)) throw new MissingEmailInformationException("Invalid Email Information: missing 'Body' information");
        }

        private EmailTypeConfiguration GetEmailType(Notification notification)
        {
            EmailTypeConfiguration emailType = _emailTypeDictionary.GetValueOrDefault(notification.Type)
                ?? throw new UnsupportedNotificationTypeException("Unsupported Notification Type: type is empty or invalid");

            return emailType;
        }

        private async Task ValidateRateLimitAsync(EmailTypeConfiguration emailTypeConfiguration, Notification notification)
        {
            if (emailTypeConfiguration.RateLimit == null) return;

            int emailsSentCount = await _emailSentRepository.CountEmailSentInTimeRangeByTypeAsync(notification.To, emailTypeConfiguration.Type, emailTypeConfiguration.RateLimit.TimeRangeInMinutes);
            if (emailsSentCount >= emailTypeConfiguration.RateLimit.NotificationLimit)
                throw new NotificationOverTheLimitException($"Notification over the Limit: User {notification.To} has reached notification rate limit for type {emailTypeConfiguration.Type}");
        }

    }
}
