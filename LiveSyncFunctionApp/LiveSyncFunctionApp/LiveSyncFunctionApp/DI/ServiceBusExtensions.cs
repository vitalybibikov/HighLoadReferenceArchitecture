using Core.Sources;
using Core.Sources.Implementations.LifeScores.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SyncFunctionApp.DI
{
    public static class LoyaltyServiceBusExtensions
    {
        public static void Setup(this IServiceCollection services)
        {
            services.AddTransient<SourcesFactory>(x => new SourcesFactory(new LiveScoresPathBuilder()));
        }
    }
}