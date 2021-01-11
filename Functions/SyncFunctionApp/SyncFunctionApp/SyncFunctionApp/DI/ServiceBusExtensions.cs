using Common.Sources;
using Common.Sources.Implementations.LifeScores.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SyncFunctionApp.DI
{
    public static class ServiceBusExtensions
    {
        public static void Setup(this IServiceCollection services)
        {
            services.AddTransient(x => new SourcesFactory(new LiveScoresPathBuilder()));
        }
    }
}