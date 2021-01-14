using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Sources;
using Common.Sources.Core;
using Dynamitey.DynamicObjects;
using LiveSyncFunctionApp.Extensions;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using NuGets.NuGets.Contracts;
using NuGets.NuGets.Dtos;
using NuGets.NuGets.Dtos.Enums;

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

            while (context.CurrentUtcDateTime < expiryTime)
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

                //currently not implemented, thus will fail
                //var competition = await retriever.GetLiveAsync(response.Content);

                var competition = new Competition(
                    "Test",
                    "Test",
                    new List<Team>()
                {
                    new Team("Test1"),
                    new Team("Test2")
                },
                    DateTime.Now,
                    SportType.Soccer,
                    message.Uri);


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
            //uri is pretty unique, but UniqueId might be used here as well;
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
            var message = context.GetInput<CompetitionMessage>();
            var result = message.ToBrokeredMessage(message.UniqueId.ToString());
            log.LogInformation($"{nameof(LiveDataFunctionResultServiceBus)} was triggered.");
            await output.AddAsync(result);
        }
    }
}