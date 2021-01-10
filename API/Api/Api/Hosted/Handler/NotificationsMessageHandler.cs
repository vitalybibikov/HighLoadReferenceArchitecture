using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Core;
using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.InteropExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

            if (brokeredMessage != null)
            {
                await Handle(mediator, brokeredMessage, cancellationToken);
            }
        }

        private async Task Handle(IMediator mediator, Message brokeredMessage, CancellationToken cancellationToken)
        {
            switch (brokeredMessage.ContentType)
            {
                case nameof(Competition):
                    {
                        var message = brokeredMessage.GetBody<Competition>();

                        logger.LogInformation(
                            handleMessageTemplate,
                            brokeredMessage.CorrelationId,
                            GetType().Name,
                            message);
                        await mediator.Publish(null, cancellationToken);

                        //todo: here I stopped.
                        throw new NotImplementedException();
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
