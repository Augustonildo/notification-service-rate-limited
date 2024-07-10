using EmailNotificationService.Domain.Interfaces;
using EmailNotificationService.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailNotificationService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IEmailSentRepository _emailSentRepository;
        private readonly Dictionary<string, RateLimitConfiguration> _rateLimitDictionary;

        public NotificationService(ILogger<NotificationService> logger, IEmailSender emailSender, IEmailSentRepository emailSentRepository, IConfiguration configuration)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            // 1st: Variable checking: check every interesting info. Ex: Throw exception if To is null, if Subject null, Content null, etc.

            // 2nd: Access dictionary and check if type is supported

            // 3rd: Access an repository to return how many notifications from that type an user has received
            // If type HasRateLimit false, skip this

            // 4th: Verify based on dictionary rule if a new notification can be sent. 
            // If don't, send message back to the queue.

            // 5th: If all set, call emailSender and send e-mail.

            // 6th: Register emailSent in emailSentRepository
        }

    }
}
