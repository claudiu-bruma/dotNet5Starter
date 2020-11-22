using dotNet5Starter.Infrastructure.Constants;
using dotNet5Starter.Infrastructure.EventBus.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotNet5Starter.Infrastructure.EventBus.Abstractions
{
    public class RabbitMqEventBus : IEventBus
    {
        private RabbitMqSettings _rabbitMqSettings;

        public RabbitMqEventBus(RabbitMqSettings rabbitMqSettings)
        {
            _rabbitMqSettings = rabbitMqSettings;
        }
        public void PublishEvent(EventHubMessage message,
            string routing,
            CancellationToken cancelationToken)
        {
            using var channel = RabbitMqChannelFactory.CreateRabbitMqChannel(_rabbitMqSettings);
            var body = Encoding.UTF8.GetBytes(message.Message);

            channel.BasicPublish(exchange: EventBusConstants.CompanyExchangeName,
                                 routingKey: routing,
                                 basicProperties: null,
                                 body: body);
            
        } 
    }
}
