using AutoMapper;
using dotNet5Starter.Infrastructure.EventBus.Abstractions;
using dotNet5Starter.Services.CompanyServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic; 
using System.Threading;
using System.Text.Json;
using dotNet5Starter.ReactWebApp.Models;

namespace dotNet5Starter.ReactWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ILogger<CompaniesController> _logger;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;
        public CompaniesController(ILogger<CompaniesController> logger,
            ICompanyService companyService,
            IMapper mapper,
            IEventBus eventBus)
        {
            _logger = logger;
            _companyService = companyService;
            _mapper = mapper;
            _eventBus = eventBus;
        }
        [HttpGet]
        public  IEnumerable<CompanyViewModel> Get()
        {
            var companyList = _companyService.GetCompanies();
            return _mapper.Map<IEnumerable<CompanyViewModel>>(companyList);
        }

        [HttpPost]
        public IEnumerable<CompanyViewModel> Post(CompanyViewModel model, CancellationToken cancelationToken)
        {
            _eventBus.PublishEvent(new EventHubMessage()
            {
                Message = JsonSerializer.Serialize(model)
            }, "company", cancelationToken);

            return Get();
        }
    }
}