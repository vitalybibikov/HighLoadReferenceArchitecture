using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace Api.Hosted.Handler
{
    public interface INotificationsMessageHandler
    {
        Task Handle(Message brokeredMessage, CancellationToken cancellationToken);
    }
}