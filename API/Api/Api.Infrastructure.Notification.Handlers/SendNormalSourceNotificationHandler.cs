using System.Threading;
using System.Threading.Tasks;
using Api.Application.Notifications;
using Api.Infrastructure.Notification.Handlers.Extensions;
using Api.Infrastructure.Settings.ServiceBus;
using MediatR;
using NuGets.NuGets.Contracts;

namespace Api.Infrastructure.Notification.Handlers
{
    public class SendNormalSourceNotificationHandler : INotificationHandler<SendNormalSourceNotification>
    {
        private readonly INormalCoordinationQueueClient queueClient;

        public SendNormalSourceNotificationHandler(INormalCoordinationQueueClient queueClient)
        {
            this.queueClient = queueClient;
        }

        public async Task Handle(SendNormalSourceNotification notification, CancellationToken cancellationToken)
        {
            var syncMessage = new SyncMessage
            {
                ConnectorType = notification.ConnectorType,
                SportType = notification.SportType,
                When = notification.When
            };

            var brokeredMessage = syncMessage.ToBrokeredMessage();
            await queueClient.Client.SendAsync(brokeredMessage);
        }
    }
}
