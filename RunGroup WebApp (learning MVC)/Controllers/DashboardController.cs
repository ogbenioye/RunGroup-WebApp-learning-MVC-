using Microsoft.AspNetCore.Mvc;
using RunGroup_WebApp__learning_MVC_.Interfaces;
using RunGroup_WebApp__learning_MVC_.Repository;
using RunGroup_WebApp__learning_MVC_.ViewModel;

namespace RunGroup_WebApp__learning_MVC_.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContext;

        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContext) 
        {
            _dashboardRepository = dashboardRepository;
            _httpContext = httpContext;
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

        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _httpContext.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(curUserId);
            if (user == null) return View("Error");

            var userVM = new EditUserProfileViewModel
            {
                Id = curUserId,
                Pace = user.Pace,
                Mielage = user.Mielage,
                City = user.City,
                State = user.State,
                ProfileImageUrl = user.ProfileImageUrl,
            };
            return View(userVM);
        }
    }
}
