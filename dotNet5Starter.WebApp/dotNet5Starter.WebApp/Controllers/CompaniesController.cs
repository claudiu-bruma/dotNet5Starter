using AutoMapper;
using dotNet5Starter.Infrastructure.EventBus.Abstractions;
using dotNet5Starter.Services.CompanyServices;
using dotNet5Starter.Webapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Text.Json;

namespace dotNet5Starter.Webapp.Controllers
{
    public class CompaniesController : Controller
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
        public IActionResult Index()
        {
            var companyList = _companyService.GetCompanies();
            var model = _mapper.Map<IEnumerable<CompanyViewModel>>(companyList);
            return View("Index", model);
        }
        public IActionResult CreateNewCompany(CompanyViewModel model, CancellationToken cancelationToken)
        {
            _eventBus.PublishEvent(new EventHubMessage()
            {
                Message = JsonSerializer.Serialize(model)
            }, "company", cancelationToken);

            return Index();
        }
    }
}
