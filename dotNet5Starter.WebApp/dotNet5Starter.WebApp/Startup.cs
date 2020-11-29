using AutoMapper;
using dotNet5Starter.Infrastructure.Data.DataAccessAbtractions;
using dotNet5Starter.Infrastructure.EventBus.Abstractions;
using dotNet5Starter.Services.CompanyServices;
using dotNet5Starter.Webapp.AutoMapper;
using dotNet5Starter.Webapp.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RawRabbit.Configuration;
using System;
using System.Linq;
using RabbitMQ.Client.Core.DependencyInjection;
using dotNet5Starter.Infrastructure.EventBus.Configuration;

namespace dotNet5Starter.Webapp
{
    public class Startup
    {
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

        public IConfiguration Configuration { get; }
        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(ICompanyService), typeof(CompanyService));
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

            ConfigureRabbitMq(services);
            ConfigureInjectionForServices(services);

            var config = new RabbitMqSettings();
            Configuration.Bind("RabbitMqSettings", config);
            services.AddSingleton(config);

            services.AddControllersWithViews();
        }
        public void ConfigureRabbitMq(IServiceCollection services)
        {
            var rabbitMqSection = Configuration.GetSection("RabbitMq");
            var exchangeSection = Configuration.GetSection("RabbitMqExchange");
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
