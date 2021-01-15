using System;
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
            //todo: when message is received, the function can be scheduled till time T,
            //e.g. till 3-5 minutes prior to match start, after that it can change it's polling behavior from 
            // 60 days (e.g. time from the match info is parsed) - simply it waits, without being billed till the moment
            // to 3 every 2 secs while the match is finished and exits.

            //Multiple sources can be queried from this 1 function, for instance if we've specified them in a message, that scheduled the function.

            var message = context.GetInput<LiveSyncMessage>();
            int pollingInterval = message.PollingIntervalInSec;

            //to make the demo work;
            DateTime expiryTime = message.FinishTime; 

           // while (context.CurrentUtcDateTime <= expiryTime.ToUniversalTime())
            {
                //todo: if match has ended or was cancelled - skip.
                var source = factory.GetSource(message.ConnectorType);
                var retriever = source.GetRetriever(message);

                //todo: currently only all is supported for the demo
                //todo: can support almost unlimited amount of calls to source apis, that can be aggregated in this point.
                DurableHttpResponse response =
                    await context.CallHttpAsync(
                        System.Net.Http.HttpMethod.Get,
                        new Uri("https://www.livescores.com/soccer/holland/eredivisie/fc-twente-vs-ajax/247557/")); //message.Uri

                var stats = await retriever.GetLiveAsync(response.Content);

                var result = new CompetitionStatsMessage
                {
                    CompetitionId = message.CompetitionUniqueId,
                    Score =  stats.Score
                };

                if (!String.IsNullOrEmpty(result.CompetitionId))
                {
                    await context.CallActivityAsync("LiveDataFunctionResultServiceBus", result);

                    // Orchestration sleeps until this time.
                    var nextCheck = context.CurrentUtcDateTime.AddSeconds(pollingInterval); 
                    //await context.CreateTimer(nextCheck, CancellationToken.None);
                }
            }
        }

        [FunctionName("LiveDataFunctionServiceBus")]
        public async Task LiveDataFunctionServiceBus(
            [ServiceBusTrigger("%ServiceBusQueueName%", Connection = "ServiceBusConnectionString")] LiveSyncMessage message,
            [DurableClient] IDurableOrchestrationClient client,
            ILogger log)
        {
            //todo: UniqueId might be used here if we need some kind of identification, but should include connectorType then.
            if (message.Uri != null)
            {
                var instanceId = message.Uri.ToBase64OrNull();

                var existingInstance = await client.GetStatusAsync(instanceId);

                if (existingInstance == null
                    || existingInstance.RuntimeStatus == OrchestrationRuntimeStatus.Completed
                    || existingInstance.RuntimeStatus == OrchestrationRuntimeStatus.Failed
                    || existingInstance.RuntimeStatus == OrchestrationRuntimeStatus.Terminated)
                {
                    log.LogInformation($"{nameof(LiveDataFunction)} was triggered.");
                    await client.StartNewAsync("ScheduledLiveMonitoring", instanceId, message);
                }
            }
        }

        [FunctionName("LiveDataFunctionResultServiceBus")]
        public async Task LiveDataFunctionResultServiceBus(
            [ActivityTrigger] IDurableActivityContext context,
            [ServiceBus("%ServiceBusLiveCompetitionsQueueName%", EntityType.Topic, Connection = "ServiceBusConnectionString")]
            IAsyncCollector<Message> output,
            ILogger log)
        {

            log.LogInformation($"{nameof(LiveDataFunctionResultServiceBus)} was triggered.");

            var message = context.GetInput<CompetitionStatsMessage>();

            //if required, can be deduped as well with the service bus later on.
            var result = message.ToBrokeredMessage();

            await output.AddAsync(result);
        }
    }
}