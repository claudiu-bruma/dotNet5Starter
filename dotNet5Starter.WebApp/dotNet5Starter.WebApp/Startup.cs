using AutoMapper;
using dotNet5Starter.Infrastructure.Data.DataAccessAbtractions;
using dotNet5Starter.Services.CompanyServices;
using dotNet5Starter.Webapp.AutoMapper;
using dotNet5Starter.Webapp.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet5Starter.Webapp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void AddDependencyInjection(IServiceCollection services)
        {
            RegisterDAL(services);
            RegisterServices(services);

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
            AddDependencyInjection(services);
            
            services.AddControllersWithViews();
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
