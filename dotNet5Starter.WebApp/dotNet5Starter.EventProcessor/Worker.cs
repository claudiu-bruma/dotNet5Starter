using dotNet5Starter.Services.EventProcessor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;  

namespace dotNet5Starter.EventProcessor
{
    public partial class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger; 
        private readonly IServiceProvider _services;
        public Worker(IServiceProvider services, ILogger<Worker> logger)
        {
            _logger = logger;
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _services.CreateScope();
            var companyCreateEventProcessor = scope.ServiceProvider
                    .GetRequiredService<ICompanyCreateEventProcessor>();
            companyCreateEventProcessor.SetupConsumerForCompanyCreatedEvent();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(100000, stoppingToken);
            }
        }
    }
}
