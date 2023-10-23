using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroup_WebApp__learning_MVC_.Data;
using RunGroup_WebApp__learning_MVC_.Models;
using RunGroup_WebApp__learning_MVC_.ViewModel;

namespace RunGroup_WebApp__learning_MVC_.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var correct = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (correct)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }
                }
                TempData["Error"] = "Wrong Credentials, Please try again";
                return View(loginVM);
            }
            TempData["Error"] = "Wrong Credentials, try again";
            return View(loginVM);
        }
    }
}
