using Api.Infrastructure.Settings.ServiceBus;
using Microsoft.Azure.ServiceBus;

namespace Api.Infrastructure.Settings
{
    public class LiveCoordinationQueueClient: ILiveCoordinationQueueClient
    {
        public IQueueClient Client { get; }

        public LiveCoordinationQueueClient(IQueueClient client)
        {
            Client = client;
        }
    }
}
