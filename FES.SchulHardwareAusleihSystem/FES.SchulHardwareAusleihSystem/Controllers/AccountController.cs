using FES.SchulHardwareAusleihSystem.Interfaces.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FES.SchulHardwareAusleihSystem.Interfaces.Model;
using Microsoft.AspNetCore.Mvc;
using FES.SchulHardwareAusleihSystem.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace FES.SchulHardwareAusleihSystem.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _nutzer;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        public AccountController(UserManager<ApplicationUser> nutzer, SignInManager<ApplicationUser> signInManager)
        {
            _nutzer = nutzer;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email};
                
                IdentityResult result = await _nutzer.CreateAsync(user, model.Passwort);
                if (result.Succeeded)
                {
                    await _nutzer.AddToRoleAsync(user, model.Role);
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
           
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Passwort,false, false);
                if (result.Succeeded)
                {
                   // _logger.LogInformation("User logged in.");
                    return RedirectToAction("Profil","Profil");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //_logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
