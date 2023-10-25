using Microsoft.AspNetCore.Mvc;
using RunGroup_WebApp__learning_MVC_.Interfaces;
using RunGroup_WebApp__learning_MVC_.Repository;
using RunGroup_WebApp__learning_MVC_.ViewModel;

namespace RunGroup_WebApp__learning_MVC_.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository) 
        {
            _dashboardRepository = dashboardRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userClubs = await _dashboardRepository.GetUserClubs();
            var userRaces = await _dashboardRepository.GetUserRaces();

            var dashboardVM = new DashboardViewModel
            {
                Clubs = userClubs,
                Races = userRaces,
            };
            return View(dashboardVM);
        }
    }
}
