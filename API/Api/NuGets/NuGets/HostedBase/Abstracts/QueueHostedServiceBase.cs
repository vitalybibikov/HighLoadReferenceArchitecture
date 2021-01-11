using System;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using NuGets.NuGets.HostedBase.Abstracts.Base;
using NuGets.NuGets.HostedBase.Interface;

namespace NuGets.NuGets.HostedBase.Abstracts
{
    public abstract class QueueHostedServiceBase : ServiceBusHostedServiceBase
    {
        private readonly Lazy<IReceiverClient> client;

        protected QueueHostedServiceBase(IQueueClientData data, IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            client = new Lazy<IReceiverClient>(() => new QueueClient(
            data.ConnectionString,
            data.StorageName,
            ReceiveMode.PeekLock,
            RetryPolicy.Default));
        }

        protected override Lazy<IReceiverClient> Client => client;
    }
}
