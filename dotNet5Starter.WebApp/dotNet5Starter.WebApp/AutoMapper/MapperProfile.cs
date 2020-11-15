using dotNet5Starter.Infrastructure.Data.DataModels;
using dotNet5Starter.Services.CompanyServices;
using dotNet5Starter.Webapp.Models;
using AutoMapper;

namespace dotNet5Starter.Webapp.AutoMapper
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
