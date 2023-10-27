﻿using Microsoft.AspNetCore.Mvc;
using RunGroup_WebApp__learning_MVC_.Interfaces;
using RunGroup_WebApp__learning_MVC_.ViewModel;

namespace RunGroup_WebApp__learning_MVC_.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();

            foreach (var user in users)
            {
                var userVM = new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Pace = user.Pace,
                    Mielage = user.Mielage,
                };
                result.Add(userVM);
            }
            return View(result);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _userRepository.GetUserById(id);
            var userVM = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Pace = user.Pace,
                Mielage = user.Mielage,
            };

            return View(userVM);
        }
    }
}
