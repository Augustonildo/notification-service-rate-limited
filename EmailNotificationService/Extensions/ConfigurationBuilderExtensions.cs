using Microsoft.Extensions.Configuration;
using System;

namespace EmailNotificationService.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder RegisterAppConfiguration(this IConfigurationBuilder builder)
        {
            throw new NotImplementedException();

            // Add a call to populate "RateLimitTypeList" with values from an independent solution (e.g. AppConfig, API Endpoint, etc.)
        }
    }
}
