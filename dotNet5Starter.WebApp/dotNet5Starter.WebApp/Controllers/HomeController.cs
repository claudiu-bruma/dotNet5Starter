using dotNet5Starter.Services.CompanyServices;
using dotNet5Starter.Webapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Diagnostics;
using System.Collections.Generic;

namespace dotNet5Starter.Webapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public HomeController(
            ILogger<HomeController> logger,
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
            var model = _mapper.Map<IEnumerable <CompanyViewModel>>(companyList);
            return View("Index",model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
