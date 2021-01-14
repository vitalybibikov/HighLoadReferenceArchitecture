using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Sources;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using NuGets.NuGets.Contracts;
using NuGets.NuGets.Dtos;
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
                .Select(x => new CompetitionMessage
                {
                    Name = x.Name,
                    Place = x.Place,
                    SportType = x.SportType,
                    StartDate = x.StartDate,
                    Teams = x.Teams.Select(m => new TeamMessage { Name = m.Name }).ToList(),
                    UniqueId = x.UniqueId,
                    LiveUri = x.LiveUri
                })
                .Select(competition => output.AddAsync(competition.ToBrokeredMessage(competition.UniqueId.ToString())))
                .ToList();

            await Task.WhenAll(tasks);
        }
    }
}
