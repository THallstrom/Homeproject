﻿using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Silicon.Models;

namespace Silicon.Controllers
{
    public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly SignInManager<UserEntity> _signInManager = signInManager;

        #region SignIn
        [HttpGet]
        [Route("/signin")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [Route("/signin")]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Home", "Default");
                    }
                }
            }
            ViewData["StatusMessage"] = "Incorrect email or password";
            return View();
        }
        #endregion

        #region SignUp
        [HttpGet]
        [Route("/signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [Route("/signup")]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (!await _userManager.Users.AnyAsync(x => x.Email == viewModel.Email))
                {
                    var userEntity = new UserEntity
                    {
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName,
                        Email = viewModel.Email,
                        UserName = viewModel.Email
                    };

                    var result = await _userManager.CreateAsync(userEntity, viewModel.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("SignIn", "Auth");
                    }
                    else
                    {
                        ViewData["StatusMessage"] = "Something went wrong, please try again!";
                    }
                }
                else
                    ViewData["StatusMessage"] = "User with the same email address already exists";
                {
                }
            }
            return View(viewModel);
        }
        #endregion

        #region SignOut

        [HttpGet]
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Home", "Default");
        }
        #endregion
    }
}
