using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Hosted.Data;
using Api.Hosted.Handler;
using Microsoft.Azure.ServiceBus;
using NuGets.NuGets.HostedBase.Abstracts;

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
            //todo: separate handlers
            await handler.Handle(message, cancellationToken);
        }
    }
}
