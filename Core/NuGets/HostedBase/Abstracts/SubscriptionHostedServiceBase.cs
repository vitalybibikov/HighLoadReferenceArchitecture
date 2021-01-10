using System;
using Core.NuGets.HostedBase.Abstracts.Base;
using Core.NuGets.HostedBase.Interface;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;

namespace Core.NuGets.HostedBase.Abstracts
{
    public abstract class SubscriptionHostedServiceBase : ServiceBusHostedServiceBase
    {
        private readonly Lazy<IReceiverClient> client;

        protected SubscriptionHostedServiceBase(ISubscriptionClientData data, IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            client = new Lazy<IReceiverClient>(() => new SubscriptionClient(
                data.ConnectionString,
                data.StorageName,
                data.SubscriptionName,
                ReceiveMode.PeekLock,
                RetryPolicy.Default));
        }

        protected override Lazy<IReceiverClient> Client => client;
    }
}
