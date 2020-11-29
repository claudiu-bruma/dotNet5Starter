using AutoMapper;
using dotNet5Starter.Infrastructure.Data.DataModels;
using dotNet5Starter.ReactWebApp.Models;
using dotNet5Starter.Services.CompanyServices; 

namespace dotNet5Starter.ReactWebApp.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, CompanyViewModel>();
            CreateMap<CompanyViewModel, CompanyDto>();
            CreateMap<CompanyDto, Company>();
        }
    }
}
