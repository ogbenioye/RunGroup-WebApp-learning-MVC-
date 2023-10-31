using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RunGroup_WebApp__learning_MVC_.Helper;
using RunGroup_WebApp__learning_MVC_.Interfaces;
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
        private readonly IClubRepository _clubRepository;

        public HomeController(ILogger<HomeController> logger, IClubRepository clubRepository)
        {
            _logger = logger;
            _clubRepository = clubRepository;
        }

        public async Task<IActionResult> Index()
        {
            var ipInfo = new IpInfo();
            var homeVM = new HomeViewModel();

            string url = "https://ipinfo.io?token=[TOKEN]";
            try
            {
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);

                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;

                homeVM.City = ipInfo.City;
                homeVM.State = ipInfo.Region;
                if (homeVM.City != null)
                {
                    homeVM.Clubs = await _clubRepository.GetClubByCity(ipInfo.City);

                }

                return View(homeVM);
            }
            catch (Exception ex)
            {
                return View(homeVM);
            }
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