using System;
using Api.Configuration.Extensions;
using Api.Hosted;
using Api.Hosted.Data;
using Api.Hosted.Handler;
using Api.Settings;
using Api.Settings.Extensions;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(v => v.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddControllersAsServices();

            services.Configure<ServiceBusSettings>(Configuration.GetSection(nameof(ServiceBusSettings)))
                .AddOptions<ServiceBusSettings>()
                .ValidateDataAnnotations();

            services.AddMediatR(
                typeof(NotificationsMessageHandler).Assembly);

            AddHostedServices(services);
            AddHealthChecks(services);

            services.AddApplicationInsightsTelemetry();
            services.AddApplicationInsightsKubernetesEnricher();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use365ExceptionHandler();

            app.UseRouting();
            app.UseCors("AllowAllOrigins");

            if (env.IsEnvironment("dev"))
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self", StringComparison.OrdinalIgnoreCase)
                });
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }

        protected virtual void AddHealthChecks(IServiceCollection services)
        {
            var serviceBusSettings = Configuration.GetServiceBusSettings();

            services.AddHealthChecks()
                .AddSelf()
                .AddNormalCompetitionsTopic(serviceBusSettings)
                .AddLiveCompetitionsTopic(serviceBusSettings)
                .AddLiveCoordinationQueue(serviceBusSettings)
                .AddNormalCompetitionsQueue(serviceBusSettings);
        }

        protected virtual void AddHostedServices(IServiceCollection services)
        {
            var serviceBusSettings = Configuration.GetServiceBusSettings();

            services.Configure<ServiceBusSettings>(options =>
            {
                options.ConnectionString = serviceBusSettings.ConnectionString;

                options.ServiceBusLiveCompetitionsTopicName = serviceBusSettings.ServiceBusLiveCompetitionsTopicName;
                options.ServiceBusNormalCompetitionsTopicName = serviceBusSettings.ServiceBusNormalCompetitionsTopicName;

                options.ServiceBusLiveQueueName = serviceBusSettings.ServiceBusLiveQueueName;
                options.ServiceBusQueueName = serviceBusSettings.ServiceBusQueueName;
            });

            services.AddTransient<INotificationsMessageHandler, NotificationsMessageHandler>();
            services.AddSingleton<LiveHostedServiceData>();
            services.AddSingleton<NormalHostedServiceData>();

            //todo: this 2 hosted services and an api should belong to different applications, as normal and live data flow differ in > 10000 times.
            services.AddHostedService<LiveCompetitionHostedService>();
            services.AddHostedService<NormalCompetitionHostedService>();
        }
    }
}
