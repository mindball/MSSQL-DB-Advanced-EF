using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealEstate.Services;
using RealEstate.Web.Models;

namespace RealEstate.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDistrictsService districtsService;

        public HomeController(ILogger<HomeController> logger, IDistrictsService districtsService)
        {
            _logger = logger;
            this.districtsService = districtsService;
        }

        public IActionResult Index()
        {
            var result = this.districtsService.GetTopDistrictByAveragePrice(1000);
            return View(result);
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
