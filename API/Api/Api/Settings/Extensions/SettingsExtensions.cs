using Microsoft.Extensions.Configuration;

namespace Api.Settings.Extensions
{
    public static class SettingsExtensions
    {
        public static AppSettings GetAppSettings(this IConfiguration configuration)
        {
            return configuration
                .GetSection(nameof(AppSettings))
                .Get<AppSettings>()
                .ValidateDataAnnotations();
        }

        public static ServiceBusSettings GetServiceBusSettings(this IConfiguration configuration)
        {
            return configuration
                .GetSection(nameof(ServiceBusSettings))
                .Get<ServiceBusSettings>()
                .ValidateDataAnnotations();
        }
    }
}
