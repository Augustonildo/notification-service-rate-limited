using EmailNotificationService.Domain.Interfaces;
using EmailNotificationService.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace EmailNotificationService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly IEmailSender _emailSender;
        private readonly Dictionary<string, RateLimitConfiguration> _rateLimitDictionary;

        public NotificationService(ILogger<NotificationService> logger, IEmailSender emailSender, IConfiguration configuration)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));

            var infoList = new List<RateLimitConfiguration>();
            configuration.GetSection("RateLimitTypeList").Bind(infoList);
            _rateLimitDictionary = new Dictionary<string, RateLimitConfiguration>();
            foreach (var info in infoList)
            {
                _rateLimitDictionary[info.Type] = info;
            }

        }

        // TODO: TASK: IMPLEMENT this
        public void Send(Notification notification)
        {
            throw new Exception("not implemented - fix this");
        }

    }
}
