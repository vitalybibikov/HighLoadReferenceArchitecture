using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Sources;
using LiveSyncFunctionApp.Extensions;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using NuGets.NuGets.Contracts;
using NuGets.NuGets.Dtos;

namespace LiveSyncFunctionApp
{
    public class LiveDataFunction
    {
        private readonly SourcesFactory factory;

        public LiveDataFunction(SourcesFactory factory)
        {
            this.factory = factory;
        }

        [FunctionName("ScheduledLiveMonitoring")]
        public async Task Run([OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var message = context.GetInput<LiveSyncMessage>();
            int pollingInterval = message.PollingIntervalInSec;
            DateTime expiryTime = message.FinishTime;

            while (context.CurrentUtcDateTime < expiryTime)
            {
                //todo: if match has ended or was cancelled - skip.
                var source = factory.GetSource(message.ConnectorType);
                var retriever = source.GetRetriever(message);

                //todo: currently only all is supported for the demo

                DurableHttpResponse response =
                    await context.CallHttpAsync(System.Net.Http.HttpMethod.Get, message.Uri);
                var competitions = await retriever.GetAllByContent(response.Content);
                var competition = competitions.First();

                var result = new CompetitionMessage
                {
                    Name = competition.Name,
                    Place = competition.Place,
                    SportType = competition.SportType,
                    StartDate = competition.StartDate,
                    Teams = competition.Teams.Select(m => new TeamMessage { Name = m.Name }).ToList(),
                    UniqueId = competition.UniqueId
                };

                await context.CallActivityAsync("LiveDataFunctionResultServiceBus", result);

                // Orchestration sleeps until this time.
                var nextCheck = context.CurrentUtcDateTime.AddSeconds(pollingInterval);
                await context.CreateTimer(nextCheck, CancellationToken.None);
            }
        }

        [FunctionName("LiveDataFunctionServiceBus")]
        public async Task LiveDataFunctionServiceBus(
            [ServiceBusTrigger("%ServiceBusQueueName%", Connection = "ServiceBusConnectionString")] LiveSyncMessage message,
            [DurableClient] IDurableOrchestrationClient client,
            ILogger log)
        {
            await client.StartNewAsync("ScheduledLiveMonitoring", message);
            log.LogInformation($"{nameof(LiveDataFunction)} was triggered.");
        }

        [FunctionName("LiveDataFunctionResultServiceBus")]
        public async Task LiveDataFunctionResultServiceBus(
            [ActivityTrigger] IDurableActivityContext context,
            [ServiceBus("%ServiceBusLiveCompetitionsQueueName%", EntityType.Topic, Connection = "ServiceBusConnectionString")] IAsyncCollector<Message> output,
            ILogger log)
        {
            var message = context.GetInput<CompetitionMessage>();
            var result = message.ToBrokeredMessage(message.UniqueId.ToString());
            log.LogInformation($"{nameof(LiveDataFunctionResultServiceBus)} was triggered.");
            await output.AddAsync(result);
        }
    }
}