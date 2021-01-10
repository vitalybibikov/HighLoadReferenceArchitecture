using System;
using System.Threading;
using System.Threading.Tasks;
using Core.NuGet.Contracts;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace LiveSyncFunctionApp
{
    public class LiveDataFunction
    {
        public LiveDataFunction()
        {

        }

        [FunctionName("ScheduledLiveMonitoring")]
        public static async Task<string> Run(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var message = context.GetInput<LiveSyncMessage>();
            //string result = await context.CallActivityAsync<string>("SayHello", name);

            int jobId = context.GetInput<int>();
            int pollingInterval = message.PollingIntervalInSec;
            DateTime expiryTime = message.FinishTime;

            while (context.CurrentUtcDateTime < expiryTime)
            {
                var jobStatus = await context.CallActivityAsync<string>("GetJobStatus", jobId);
                if (jobStatus == "Completed")
                {
                    // Perform an action when a condition is met.
                    await context.CallActivityAsync("SendAlert", machineId);
                    break;
                }

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

    }
}