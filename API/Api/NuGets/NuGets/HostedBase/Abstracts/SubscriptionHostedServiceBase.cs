using System;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using NuGets.NuGets.HostedBase.Abstracts.Base;
using NuGets.NuGets.HostedBase.Interface;

namespace NuGets.NuGets.HostedBase.Abstracts
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
