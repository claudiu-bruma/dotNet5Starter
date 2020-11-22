using dotNet5Starter.Infrastructure.Data.DataModels;
using dotNet5Starter.Services.CompanyServices;
using AutoMapper;
using dotNet5Starter.Infrastructure.EventBus.Events;

namespace dotNet5Starter.EventProcessor.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, Company>();
            CreateMap<CompanyCreateEvent, CompanyDto>();
            CreateMap<CompanyDto, CompanyCreateEvent>();
        }
    }
}
