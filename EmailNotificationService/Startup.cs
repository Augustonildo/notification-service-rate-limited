using EmailNotificationService.Domain.Interfaces;
using EmailNotificationService.Extensions;
using EmailNotificationService.Handlers;
using EmailNotificationService.Integration;
using EmailNotificationService.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

[assembly: FunctionsStartup(typeof(EmailNotificationService.Startup))]
namespace EmailNotificationService
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var context = builder.GetContext();
            var configuration = context.Configuration;

            builder.Services.AddSingleton(configuration)
                .AddScoped<ISendEmailHandler, SendEmailHandler>()
                .AddTransient<INotificationService, NotificationService>()
                .AddTransient<IEmailSender, EmailSender>();

            // Add logging
            builder.Services.AddLogging();
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var context = builder.GetContext();

            if (IsLocalEnvironment(context))
            {
                builder.ConfigurationBuilder.AddJsonFile(
                    Path.Combine(context.ApplicationRootPath, "appsettings.Local.json"), optional: false, reloadOnChange: true);
            }
            else
            {
                builder.ConfigurationBuilder.RegisterAppConfiguration();
            }

            builder.ConfigurationBuilder.AddJsonFile(
                Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: false, reloadOnChange: true);
        }

        private bool IsLocalEnvironment(FunctionsHostBuilderContext context)
            => (Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") ?? context.EnvironmentName) is "Development";
    }
}
