
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FES.SchulHardwareAusleihSystem.Controllers
{
    public class ReservierungController : Controller
    {
        public IActionResult Ausleihe()
        {
            return View();
        }
    }
}
