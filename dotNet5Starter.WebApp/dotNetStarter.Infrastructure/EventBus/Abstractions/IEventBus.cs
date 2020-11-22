using System.Threading;
using System.Threading.Tasks;

namespace dotNet5Starter.Infrastructure.EventBus.Abstractions
{
    public interface IEventBus
    {
        void PublishEvent(EventHubMessage message,
             string routing,
             CancellationToken cancelationToken);
    }
}
