using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Domain.Identity;
using EIMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;

        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(
                    loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
                    if (!User.IsInRole("Admin") && !user.EmailConfirmed)
                        return RedirectToAction("ChangePassword", "Profile");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(loginViewModel);
                }
            }
            return View(loginViewModel);
        }


        public async Task<IActionResult> SignOut()
		{
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
		}
    }
}