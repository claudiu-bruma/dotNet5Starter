namespace dotNet5Starter.Infrastructure.EventBus.Configuration
{
    public class RabbitMqSettings
    {
        public string Hostname { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ClientProvidedName { get; set; }
        public string VirtualHost { get; set; }
        public string ExchangeName { get; set; }
    }
}
