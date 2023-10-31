using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using RunGroup_WebApp__learning_MVC_.Interfaces;
using RunGroup_WebApp__learning_MVC_.Models;
using RunGroup_WebApp__learning_MVC_.Repository;
using RunGroup_WebApp__learning_MVC_.ViewModel;

namespace RunGroup_WebApp__learning_MVC_.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContext, IPhotoService photoService) 
        {
            _dashboardRepository = dashboardRepository;
            _httpContext = httpContext;
            _photoService = photoService;
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
                Pace = user.Pace,
                Mielage = user.Mielage,
                City = user.City,
                State = user.State,
                ProfileImageUrl = user.ProfileImageUrl,
            };
            return View(userVM);
        }

        private void MapUserUpdate(AppUser user, EditUserProfileViewModel editVM, ImageUploadResult photoResult)
        {
            user.Pace = editVM.Pace;
            user.Mielage = editVM.Mielage;
            user.City = editVM.City;
            user.State = editVM.State;
            user.ProfileImageUrl = photoResult.Url.ToString();
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserProfileViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to save changes");
                return View("EditUserProfile", editVM);
            }

            var curUserId = _httpContext.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(curUserId);

            if (user.ProfileImageUrl == null || user.ProfileImageUrl == "")
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserUpdate(user, editVM, photoResult);

                _dashboardRepository.Update(user);

                return RedirectToAction("Index");
            }

            if (editVM.Image != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to replace old image");
                    return View(editVM);
                }

                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserUpdate(user, editVM, photoResult);
            } 
            else
            {
                user.Pace = editVM.Pace;
                user.Mielage = editVM.Mielage;
                user.City = editVM.City;
                user.State = editVM.State;
            }

            _dashboardRepository.Update(user);

            return RedirectToAction("Index");
        }
    }
}
