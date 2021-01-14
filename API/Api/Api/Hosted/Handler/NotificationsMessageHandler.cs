using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Application.Commands;
using Api.Core.Shared.Enums;
using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.InteropExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NuGets.NuGets.Dtos;

namespace Api.Hosted.Handler
{
    public class NotificationsMessageHandler : INotificationsMessageHandler
    {
        private readonly string handleMessageTemplate = "CorrelationId: {CorrelationId}. The {Type} is handling the {@Message}.";
        private readonly ILogger<NotificationsMessageHandler> logger;
        private readonly IServiceProvider serviceProvider;

        public NotificationsMessageHandler(IServiceProvider serviceProvider, ILogger<NotificationsMessageHandler> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        public async Task Handle(Message brokeredMessage, CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await Handle(mediator, brokeredMessage, cancellationToken);
        }

        private async Task Handle(IMediator mediator, Message brokeredMessage, CancellationToken cancellationToken)
        {
            switch (brokeredMessage.ContentType)
            {
                case nameof(CompetitionMessage):
                    {
                        var message = brokeredMessage.GetBody<CompetitionMessage>();

                        logger.LogInformation(
                            handleMessageTemplate,
                            brokeredMessage.CorrelationId,
                            GetType().Name,
                            message);

                        await mediator.Send(
                            new CreateCompetitionCommand
                            {
                                Name = message.Name,
                                Place = message.Place,
                                SportType = (SportType)message.SportType,
                                StartDate = message.StartDate,
                                Teams = message.Teams.Select(x => new CreateTeamCommand { Name = x.Name }),
                                UniqueId = message.UniqueId,
                                LiveUri = message.LiveUri
                            }, 
                            cancellationToken);

                        break;
                    }

                default:
                    {
                        logger.LogDebug(
                            $"{GetType().Name} does not handle the {brokeredMessage.ContentType}");
                        break;
                    }
            }
        }
    }
}
