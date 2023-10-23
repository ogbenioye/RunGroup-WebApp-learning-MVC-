using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroup_WebApp__learning_MVC_.Data;
using RunGroup_WebApp__learning_MVC_.Interfaces;
using RunGroup_WebApp__learning_MVC_.Models;
using RunGroup_WebApp__learning_MVC_.ViewModel;

namespace RunGroup_WebApp__learning_MVC_.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository repo;
        private readonly IPhotoService _photoService;
        public RaceController(IRaceRepository raceRepo, IPhotoService photoService) {
            repo = raceRepo;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await repo.GetAll();
            return View(races);
        }

        public async Task<IActionResult> Details(int id)
        {
            Race race = await repo.GetByIdAsync(id);
            return View(race);
        } 

       public IActionResult Create()
       {
           return View();
       }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var image = await _photoService.AddPhotoAsync(raceVM.Image);

                var race = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = image.Url.ToString(),
                    Address = new Address
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        State = raceVM.Address.State,
                    }
                };
                repo.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to upload image");
                return View(raceVM);
            }
        } 

        public async Task<IActionResult> Edit(int id)
        {
            var race = await repo.GetByIdAsync(id);
            if (race == null)
            {
                return View("Error");
            }

            var raceVM = new EditRaceViewModel
            {
                Title = race.Title,
                Description = race.Description,
                URL = race.Image,
                Address = race.Address,
                RaceCategory = race.RaceCategory,
            };

            return View(raceVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRaceViewModel raceVM, int id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid inputs");
                return View("Edit", raceVM);
            }

            var race = await repo.GetByIdAsync(id);
            if (race != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(race.Image);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Failed to delete photo");
                    return View(raceVM);
                }
                var image = await _photoService.AddPhotoAsync(raceVM.Image);

                //doing it this way to avoid using ASNOTracking in GetById.
                race.Title = raceVM.Title;
                race.Description = raceVM.Description;
                race.Image = image.Url.ToString();
                race.Address = raceVM.Address;
                race.RaceCategory = raceVM.RaceCategory;

                repo.Update(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Error: your changes were not saved");
                return View(raceVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var race = await repo.GetByIdAsync(id);
            if (race == null)
                return View("Error");

            return View(race);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteRace(int id)
        {
            var race = await repo.GetByIdAsync(id);
            if (race == null)
                return View("Error");

            repo.Delete(race);
            return RedirectToAction("Index");
        }
    }
}