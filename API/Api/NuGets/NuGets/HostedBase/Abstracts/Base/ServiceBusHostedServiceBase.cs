using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.ServiceBus.Diagnostics;

namespace NuGets.NuGets.HostedBase.Abstracts.Base
{
    public abstract class ServiceBusHostedServiceBase : HostedServiceBase
    {
        protected IServiceProvider ServiceProvider { get; }

        protected abstract Lazy<IReceiverClient> Client { get; }

        protected ServiceBusHostedServiceBase(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public override void Dispose()
        {
            base.Dispose();
            Client?.Value?.CloseAsync().GetAwaiter();
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Client.Value.RegisterMessageHandler(
                MessageHandler,
                new MessageHandlerOptions((token) => Task.CompletedTask) { MaxConcurrentCalls = 10, AutoComplete = true });

            return Task.CompletedTask;
        }

        protected abstract Task OnMessageAsync(Message message, CancellationToken cancellationToken);

        private Task MessageHandler(Message message, CancellationToken cancellationToken)
        {
            var activity = message.ExtractActivity();
            return OnMessageAsync(message, cancellationToken);
        }
    }
}
