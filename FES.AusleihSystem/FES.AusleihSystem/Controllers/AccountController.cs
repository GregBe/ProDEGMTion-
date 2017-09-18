using FES.AusleihSystem.Models;
using FES.AusleihSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FES.AusleihSystem.Controllers
{
    public class AccountController: Controller
    {
        /// <summary>
        /// Der Controller welcher den User händelt:
        /// Register
        /// Login
        /// Logout
        /// </summary>
        private UserManager<ApplicationUser> _nutzer;
        private SignInManager<ApplicationUser> _signInManager;
        private ILogger _logger;
       
        /// <summary>
        /// Der Konstruktor wird zur Laufzeit aufgerufen, sobald der User auf .../Account/... gelangt 
        /// </summary>
        /// <param name="nutzer">Übergibt den momentanen User und dessen Cookies</param>
        /// <param name="signInManager">Handelt alles rund ums Ein/Ausloggen</param>
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

        /// <summary>
        /// Prüft ob der das RegisterViewModel Valide ist und versucht dann den Nutzer anzulegen und gleichzeitig
        /// einzuloggen.
        /// .../Account/Register
        /// </summary>
        /// <param name="model">Bekommt von der View ein Objekt von RegisterViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };
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

        /// <summary>
        /// Versucht den User anhand seiner Eingaben anzumelden.
        /// .../Account/Login
        /// </summary>
        /// <param name="model">Wird mit deinem Obejkt von LoginViewModel befüllt</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Passwort, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View();
        }
        /// <summary>
        /// Meldet den aktuellen Nutzer ab.
        /// .../Account/Logout
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //_logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Home");
        }
    }
}
