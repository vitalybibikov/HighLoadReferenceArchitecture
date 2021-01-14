using Api.Infrastructure.Settings.ServiceBus;
using Microsoft.Azure.ServiceBus;

namespace Api.Infrastructure.Settings
{
    public class NormalCoordinationQueueClient: INormalCoordinationQueueClient
    {
        public IQueueClient Client { get; }

        public NormalCoordinationQueueClient(IQueueClient client)
        {
            Client = client;
        }
    }
}
