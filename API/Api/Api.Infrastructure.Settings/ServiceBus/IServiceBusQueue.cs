using System;
using Microsoft.Azure.ServiceBus;

namespace Api.Infrastructure.Settings.ServiceBus
{
    public interface IServiceBusQueueClient
    {
        public IQueueClient Client { get; }
    }
}
