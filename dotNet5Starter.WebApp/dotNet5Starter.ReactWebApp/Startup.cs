using AutoMapper;
using dotNet5Starter.Infrastructure.Data.DataAccessAbtractions;
using dotNet5Starter.Infrastructure.EventBus.Abstractions;
using dotNet5Starter.Infrastructure.EventBus.Configuration;
using dotNet5Starter.Services.CompanyServices;
using dotNet5Starter.Webapp.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using dotNet5Starter.ReactWebApp.AutoMapper;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace dotNet5Starter.ReactWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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
        public void ConfigureInjectionForServices(IServiceCollection services)
        {
            RegisterEventBus(services);
            RegisterDAL(services);
            RegisterServices(services);
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

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client-app-black-dashboard";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");                    
                }
            });
        }
    }
}
