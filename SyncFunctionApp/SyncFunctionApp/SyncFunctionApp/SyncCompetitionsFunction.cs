using System.Linq;
using System.Threading.Tasks;
using Core.NuGets.Contracts;
using Core.NuGets.Dtos;
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
            [ServiceBus("%ServiceBusNormalCompetitionsQueueName%", EntityType.Topic, Connection = "ServiceBusConnectionString")] IAsyncCollector<Message> output)
        {
            var source = factory.GetSource(message.ConnectorType);
            var retriever = source.GetRetriever(message);
            var competitions = await retriever.GetAllAsync();

            var tasks = competitions
                .Select(x=> new CompetitionDto(x))
                .Select(competition => output.AddAsync(competition.ToBrokeredMessage(competition.UniqueId.ToString())))
                .ToList();

            await Task.WhenAll(tasks);
        }
    }
}
