using AutoMapper;
using dotNet5Starter.Infrastructure.Data.DataModels;
//using dotNet5Starter.ReactWebApp.Models;
using dotNet5Starter.Services.CompanyServices;
using dotNet5Starter.Services.CustomerServices;
using dotNet5Starter.Webapp.Infrastructure.DataModels;

namespace dotNet5Starter.VueJsWebApp.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Company, CompanyDto>();
            //CreateMap<CompanyDto, CompanyViewModel>();
            //CreateMap<CompanyViewModel, CompanyDto>();
            CreateMap<CompanyDto, Company>();

            CreateMap<Customer, CustomerDto>(); 
            CreateMap<CustomerDto, Customer>();
        }
    }
}
