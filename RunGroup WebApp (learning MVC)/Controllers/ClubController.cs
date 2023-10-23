using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using RunGroup_WebApp__learning_MVC_.Data;
using RunGroup_WebApp__learning_MVC_.Interfaces;
using RunGroup_WebApp__learning_MVC_.Models;
using RunGroup_WebApp__learning_MVC_.Repository;
using RunGroup_WebApp__learning_MVC_.ViewModel;

namespace RunGroup_WebApp__learning_MVC_.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository repo;
        private readonly IPhotoService _photoService;

        public ClubController(IClubRepository clubRepo, IPhotoService photoService)
        {
            repo = clubRepo;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await repo.GetAll();
            return View(clubs); 
        }

        public async Task<IActionResult> Details(int id)
        {
            Club club = await repo.GetByIdAsync(id);
            return View(club);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
               var result = await _photoService.AddPhotoAsync(clubVM.Image);
                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State
                    }
                };
                repo.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(" ", "Complete all required fields");
                return View(clubVM);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var club = await repo.GetByIdAsync(id);
            if (club == null)
                return View("Error");

            var clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                URL = club.Image,
                ClubCategory = club.ClubCategory,
            };
            return View(clubVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditClubViewModel clubVM, int id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to save edit");
                return View("Edit", clubVM);
            }

            var club = await repo.GetByIdAsyncNoTracking(id);

            if(club != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(club.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to delete photo");
                    return View(clubVM);
                }
                var image = await _photoService.AddPhotoAsync(clubVM.Image);

                var newClub = new Club
                {
                    Id = id,
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = image.Url.ToString(),
                    Address = clubVM.Address,
                };

                repo.Update(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "failed to save");
                return View(clubVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var club = await repo.GetByIdAsync(id);
            if (club == null)
                return View("Error");

            return View(club);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var club = await repo.GetByIdAsync(id);
            if (club == null)
                return View("Error");

            repo.Delete(club);
            return RedirectToAction("Index");
        }
    }
}
