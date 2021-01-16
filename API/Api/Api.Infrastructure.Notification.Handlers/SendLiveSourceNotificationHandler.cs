using System.Threading;
using System.Threading.Tasks;
using Api.Application.Notifications;
using Api.Infrastructure.Notification.Handlers.Extensions;
using Api.Infrastructure.Settings.ServiceBus;
using MediatR;
using NuGets.NuGets.Contracts;

namespace Api.Infrastructure.Notification.Handlers
{
    public class SendLiveSourceNotificationHandler : INotificationHandler<SendLiveSourceNotification>
    {
        private readonly ILiveCoordinationQueueClient queueClient;

        public SendLiveSourceNotificationHandler(ILiveCoordinationQueueClient queueClient)
        {
            this.queueClient = queueClient;
        }

        public async Task Handle(SendLiveSourceNotification notification, CancellationToken cancellationToken)
        {
            var syncMessage = new LiveSyncMessage
            {
                ConnectorType = notification.ConnectorType,
                SportType = notification.SportType,
                When = notification.When,
                FinishTime = notification.FinishTime,
                PollingIntervalInSec = notification.PollingIntervalInSec,
                CompetitionUniqueId = notification.CompetitionUniqueId,
                StartTime = notification.StartTime,
                Uri = notification.Uri
            };

            var brokeredMessage = syncMessage.ToBrokeredMessage();
            await queueClient.Client.SendAsync(brokeredMessage);
        }
    }
}
