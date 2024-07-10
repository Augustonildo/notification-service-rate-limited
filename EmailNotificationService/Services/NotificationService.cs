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
        private readonly Dictionary<string, RateLimitConfiguration> _rateLimitDictionary;

        public NotificationService(IEmailSender emailSender, IEmailSentRepository emailSentRepository, IConfiguration configuration)
        {
            this._emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            this._emailSentRepository = emailSentRepository ?? throw new ArgumentNullException(nameof(emailSentRepository));

            var rateLimitConfigurationList = new List<RateLimitConfiguration>();
            configuration.GetSection("RateLimitTypeList").Bind(rateLimitConfigurationList);
            _rateLimitDictionary = new Dictionary<string, RateLimitConfiguration>();
            foreach (var item in rateLimitConfigurationList)
            {
                _rateLimitDictionary[item.Type] = item;
            }
        }

        public async Task SendAsync(Notification notification)
        {
            ValidateEmailInformation(notification);

            RateLimitConfiguration rateLimit = _rateLimitDictionary.GetValueOrDefault(notification.Type);
            if (rateLimit == null) throw new UnsupportedNotificationTypeException("Unsupported Notification Type: type is empty or invalid");

            if (rateLimit.HasRateLimit)
            {
                int emailsSentCount = await _emailSentRepository.CountEmailSentInTimeRangeByTypeAsync(notification.To, rateLimit.Type, rateLimit.TimeRangeInMinutes);
                if (emailsSentCount >= rateLimit.NotificationLimit) throw new NotificationOverTheLimitException($"Notification over the Limit: User {notification.To} has reached notification rate limit for type {rateLimit.Type}");
            }

            await _emailSender.SendAsync(notification);
            await _emailSentRepository.RegisterEmailSentAsync(notification.To, rateLimit.Type);
        }

        private void ValidateEmailInformation(Notification notification)
        {
            if (notification == null) throw new MissingEmailInformationException("Invalid Email Information: notification is null");
            if (string.IsNullOrEmpty(notification.To)) throw new MissingEmailInformationException("Invalid Email Information: missing 'To' information");
            if (string.IsNullOrEmpty(notification.Subject)) throw new MissingEmailInformationException("Invalid Email Information: missing 'Subject' information");
            if (string.IsNullOrEmpty(notification.Body)) throw new MissingEmailInformationException("Invalid Email Information: missing 'Body' information");
        }

    }
}
