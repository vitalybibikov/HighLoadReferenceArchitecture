using System.Linq;
using System.Threading.Tasks;
using Core.NuGet.Contracts;
using Core.Sources;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using SyncFunctionApp.Extensions;

namespace SyncFunctionApp
{
    public class SyncCompetitionsFunction
    {
        private readonly SourcesFactory factory;

        public SyncCompetitionsFunction(SourcesFactory factory)
        {
            this.factory = factory;
        }

        [FunctionName("SyncCompetitionsFunction")]
        
        public async Task Run(
            [ServiceBusTrigger("%ServiceBusQueueName%", Connection = "ServiceBusConnectionString")]
            SyncMessage message,
            [ServiceBus("normal-competitions", EntityType.Topic, Connection = "ServiceBusConnectionString")] IAsyncCollector<Message> output)
        {
            var source = factory.GetSource(message.ConnectorType);
            var retriever = source.GetRetriever(message);
            var competitions = await retriever.GetAllAsync();

            var tasks = competitions
                .Select(competition => output.AddAsync(competition.ToBrokeredMessage(competition.UniqueId.ToString())))
                .ToList();

            await Task.WhenAll(tasks);
        }
    }
}
