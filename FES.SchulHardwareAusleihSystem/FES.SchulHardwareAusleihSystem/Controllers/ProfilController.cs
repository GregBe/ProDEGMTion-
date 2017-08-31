using FES.SchulHardwareAusleihSystem.Interfaces.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FES.SchulHardwareAusleihSystem.Controllers
{
    public class ProfilController : Controller, IProfilController
    {
        public IActionResult Profil()
        {
            return View();
        }
        public IActionResult Admin()
        {
            return View();
        }
    }
}
