using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Application.Notifications;
using Api.Application.Queries.Sources;
using MediatR;
using NuGets.NuGets.Dtos.Enums;
using NuGets.NuGets.HostedBase.Abstracts;

namespace Api.Hosted
{
    /// <summary>
    /// Sends the data for the specified source for specified sports (1 or many).
    /// </summary>
    public class NormalSourceSchedulingHostedService : TimerHostedServiceBase
    {
        private readonly IMediator mediator;

        //todo: set timespan value from settings
        //later on can be moved to functions, if we desire agile scheduling here
        public NormalSourceSchedulingHostedService(IMediator mediator)
            : base(TimeSpan.FromMinutes(1))
        {
            this.mediator = mediator;
        }

        protected override async Task OnTimerAsync(object? state, CancellationToken cancellationToken)
        {
            var sources = await mediator.Send(new GetSourcesQuery(), cancellationToken);

            //todo: Extension logic should be here, like for every source, which supports X sports, we should have a scheduled call to it's source.
            // also we don't care about duplication in hosted services, as the volumes are low,
            // while ServiceBus can natively de-dupe them, so we get the agility for free.
            foreach (var source in sources.Sources)
            {
                await mediator.Publish(
                    new SendNormalSourceNotification
                    {
                        ConnectorType = source.ConnectorType,
                        SourceType = source.SourceType,
                        When = DateTime.Now.AddDays(-47),
                        SportType = SportType.Soccer
                    }, 
                    cancellationToken);
            }
        }
    }
}
