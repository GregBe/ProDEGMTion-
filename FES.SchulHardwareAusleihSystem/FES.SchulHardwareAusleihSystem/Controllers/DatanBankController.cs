
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FES.SchulHardwareAusleihSystem.Controllers
{
    public class DatenBankController : Controller
    {
        public IActionResult DatenBank()
        {
            return View();
        }
    }
}
