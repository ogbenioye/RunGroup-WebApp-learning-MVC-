using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RunGroup_WebApp__learning_MVC_.Helper;
using RunGroup_WebApp__learning_MVC_.Models;
using RunGroup_WebApp__learning_MVC_.ViewModel;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace RunGroup_WebApp__learning_MVC_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View();
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