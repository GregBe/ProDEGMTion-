using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FES.AusleihSystem.ViewModel;

namespace FES.AusleihSystem.Controllers
{
    public class HomeController : Controller
    {
        //private UserManager<ApplicationUser> _nutzer;
        //private SignInManager<ApplicationUser> _signInManager;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
