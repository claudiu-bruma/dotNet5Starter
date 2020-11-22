using dotNet5Starter.Infrastructure.EventBus.Configuration;
using RabbitMQ.Client;

namespace dotNet5Starter.Infrastructure.EventBus.Abstractions
{
    public class RabbitMqChannelFactory
    {
       public static IModel CreateRabbitMqChannel(RabbitMqSettings rabbitMqSettings)
        {
            var factory = new ConnectionFactory()
            {
                HostName = rabbitMqSettings.Hostname,
                Port = rabbitMqSettings.Port,
                UserName = rabbitMqSettings.UserName,
                Password = rabbitMqSettings.Password,
                VirtualHost = rabbitMqSettings.VirtualHost,
                ClientProvidedName = rabbitMqSettings.ClientProvidedName
            };
            var connection = factory.CreateConnection();
            return connection.CreateModel();
          }
    }
}
