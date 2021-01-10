using System;
using Api.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Api.Configuration.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection ConfigurateCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAllOrigins",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetPreflightMaxAge(TimeSpan.FromSeconds(2520))
                        .SetIsOriginAllowedToAllowWildcardSubdomains());
            });
            return services;
        }

        public static IHealthChecksBuilder AddSelf(this IHealthChecksBuilder hcBuilder)
        {
            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            return hcBuilder;
        }

        public static IHealthChecksBuilder AddNormalCompetitionsQueue(this IHealthChecksBuilder hcBuilder, ServiceBusSettings serviceBusSettings)
        {
            hcBuilder.AddAzureServiceBusQueue(
                serviceBusSettings.ConnectionString,
                serviceBusSettings.ServiceBusNormalCompetitionsTopicName,
                serviceBusSettings.ServiceBusNormalCompetitionsTopicName,
                tags: new[] { "servicebus" });

            return hcBuilder;
        }

        public static IHealthChecksBuilder AddLiveCoordinationQueue(this IHealthChecksBuilder hcBuilder, ServiceBusSettings serviceBusSettings)
        {
            hcBuilder.AddAzureServiceBusQueue(
                serviceBusSettings.ConnectionString,
                serviceBusSettings.ServiceBusNormalCompetitionsTopicName,
                serviceBusSettings.ServiceBusNormalCompetitionsTopicName,
                tags: new[] { "servicebus" });

            return hcBuilder;
        }

        public static IHealthChecksBuilder AddNormalCompetitionsTopic(this IHealthChecksBuilder hcBuilder, ServiceBusSettings serviceBusSettings)
        {
            hcBuilder.AddAzureServiceBusTopic(
                serviceBusSettings.ConnectionString,
                serviceBusSettings.ServiceBusNormalCompetitionsTopicName,
                serviceBusSettings.ServiceBusNormalCompetitionsTopicName,
                tags: new[] { "servicebus" });

            return hcBuilder;
        }

        public static IHealthChecksBuilder AddLiveCompetitionsTopic(this IHealthChecksBuilder hcBuilder, ServiceBusSettings serviceBusSettings)
        {
            hcBuilder.AddAzureServiceBusQueue(
                serviceBusSettings.ConnectionString,
                serviceBusSettings.ServiceBusLiveCompetitionsTopicName,
                serviceBusSettings.ServiceBusLiveCompetitionsTopicName,
                tags: new[] { "servicebus" });

            return hcBuilder;
        }
    }
}
