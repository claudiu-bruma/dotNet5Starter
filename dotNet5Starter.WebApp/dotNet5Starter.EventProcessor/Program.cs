using AutoMapper;
using dotNet5Starter.EventProcessor.AutoMapper;
using dotNet5Starter.Infrastructure.Data.DataAccessAbtractions;
using dotNet5Starter.Infrastructure.EventBus.Abstractions;
using dotNet5Starter.Infrastructure.EventBus.Configuration;
using dotNet5Starter.Services.CompanyServices;
using dotNet5Starter.Services.EventProcessor;
using dotNet5Starter.Webapp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 

namespace dotNet5Starter.EventProcessor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    var startup = new Startup(configuration);
                    startup.ConfigureServices(services);
                    services.AddHostedService<Worker>();
                });
    }

    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureInjectionForServices(IServiceCollection services)
        {
            RegisterEventBus(services);
            RegisterDAL(services);
            RegisterServices(services);
        }

        private void RegisterEventBus(IServiceCollection services)
        {
            services.AddScoped(typeof(IEventBus), typeof(RabbitMqEventBus));
        }
        
        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(ICompanyService), typeof(CompanyService));
            services.AddScoped(typeof(ICompanyCreateEventProcessor), typeof(CompanyCreateEventProcessor));
        }

        private void RegisterDAL(IServiceCollection services)
        {
            services.AddDbContext<Dotnet5StarterDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString(nameof(Dotnet5StarterDbContext)))
            );
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            ConfigureInjectionForServices(services);

            var config = new RabbitMqSettings();
            Configuration.Bind("RabbitMqSettings", config);
            services.AddSingleton(config);
        }
    }
}
