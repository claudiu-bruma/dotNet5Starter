using dotNet5Starter.Infrastructure.Data.DataAccessAbtractions;
using dotNet5Starter.Services.CompanyServices; 
using dotNet5Starter.Services.CustomerServices;
using dotNet5Starter.Webapp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace dotNet5Starter.Services
{
   public class ServicesDependencyInjectionSetup
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(ICompanyService), typeof(CompanyService));
            services.AddScoped(typeof(ICustomerService), typeof(CustomerService));
        } 
    }
}
