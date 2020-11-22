using AutoMapper;
using dotNet5Starter.Infrastructure.Constants;
using dotNet5Starter.Infrastructure.EventBus.Abstractions;
using dotNet5Starter.Infrastructure.EventBus.Configuration;
using dotNet5Starter.Infrastructure.EventBus.Events;
using dotNet5Starter.Services.CompanyServices;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5Starter.Services.EventProcessor
{
    public class CompanyCreateEventProcessor : ICompanyCreateEventProcessor
    {
        private readonly ILogger<CompanyCreateEventProcessor> _logger;
        private readonly IMapper _mapper;
        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly ICompanyService _companyService;

        public CompanyCreateEventProcessor(
        RabbitMqSettings rabbitMqSettings,
        ILogger<CompanyCreateEventProcessor> logger,
        ICompanyService companyService,
        IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _rabbitMqSettings = rabbitMqSettings;
            _companyService = companyService;
        }
        public void SetupConsumerForCompanyCreatedEvent()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqSettings.Hostname,
                Port = _rabbitMqSettings.Port,
                UserName = _rabbitMqSettings.UserName,
                Password = _rabbitMqSettings.Password,
                VirtualHost = _rabbitMqSettings.VirtualHost,
                ClientProvidedName = _rabbitMqSettings.ClientProvidedName
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                await ProcessCompanyCreatedEvent(ea, channel);
            };
            channel.BasicConsume(queue: EventBusConstants.CompanyQueue,
                                 autoAck: false,
                                 consumer: consumer);
        }

        private async Task ProcessCompanyCreatedEvent(BasicDeliverEventArgs ea, IModel channel)
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation("[x] Received {0}", message);

            try
            {
                var companyCreateEventPayload = JsonConvert.DeserializeObject<CompanyCreateEvent>(message);
                var companyDto = _mapper.Map<CompanyDto>(companyCreateEventPayload);

                await _companyService.Add(companyDto);
                channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[x] Received {message} and was unable to process it with exception message {ex} ");
                channel.BasicNack(ea.DeliveryTag, false, false);
            }
        }
    }
}
