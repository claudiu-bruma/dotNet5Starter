using AutoMapper;
using dotNet5Starter.Services.CompanyServices;
using dotNet5Starter.Webapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace dotNet5Starter.Webapp.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ILogger<CompaniesController> _logger;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        public CompaniesController(ILogger<CompaniesController> logger,
            ICompanyService companyService,
            IMapper mapper)
        {
            _logger = logger;
            _companyService = companyService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var companyList = _companyService.GetCompanies();
            var model = _mapper.Map<IEnumerable<CompanyViewModel>>(companyList);
            return View("Index", model);
        }
        public IActionResult CreateNewCompany( CompanyViewModel model)
        {
            var companyToAdd = _mapper.Map<CompanyDto>(model);
            _companyService.Add(companyToAdd);
            return Index();
        }
    }
}
