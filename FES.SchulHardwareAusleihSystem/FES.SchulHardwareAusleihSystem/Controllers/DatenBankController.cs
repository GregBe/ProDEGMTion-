using FES.SchulHardwareAusleihSystem.Interfaces.Controller;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FES.SchulHardwareAusleihSystem.Controllers
{
    public class DatenBankController : Controller, IDatenBankController
    {
        public IActionResult DatenBank()
        {
            return View();
        }
    }
}
