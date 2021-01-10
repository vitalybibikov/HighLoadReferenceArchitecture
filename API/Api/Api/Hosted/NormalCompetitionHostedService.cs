using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Hosted.Data;
using Api.Hosted.Handler;
using Core.NuGets.HostedBase.Abstracts;
using Core.NuGets.HostedBase.Interface;
using Microsoft.Azure.ServiceBus;

namespace Api.Hosted
{
    public class NormalCompetitionHostedService : SubscriptionHostedServiceBase
    {
        private readonly INotificationsMessageHandler handler;

        public NormalCompetitionHostedService(
            NormalHostedServiceData data,
            IServiceProvider serviceProvider,
            INotificationsMessageHandler handler)
            : base(data, serviceProvider)
        {
            this.handler = handler;
        }

        protected override async Task OnMessageAsync(Message message, CancellationToken cancellationToken)
        {
            await handler.Handle(message, cancellationToken);
        }
    }
}
