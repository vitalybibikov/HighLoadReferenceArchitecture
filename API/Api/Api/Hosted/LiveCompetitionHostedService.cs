using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Hosted.Data;
using Api.Hosted.Handler;
using Core.NuGets.HostedBase.Abstracts;
using Microsoft.Azure.ServiceBus;

namespace Api.Hosted
{
    public class LiveCompetitionHostedService : SubscriptionHostedServiceBase
    {
        private readonly INotificationsMessageHandler handler;

        public LiveCompetitionHostedService(
            LiveHostedServiceData data,
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
