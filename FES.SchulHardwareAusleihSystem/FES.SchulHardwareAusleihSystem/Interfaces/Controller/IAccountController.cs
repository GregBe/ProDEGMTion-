using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using FES.SchulHardwareAusleihSystem.Interfaces.Model;
using System.Threading.Tasks;

namespace FES.SchulHardwareAusleihSystem.Interfaces.Controller
{
    public interface IAccountController
    {
        [HttpGet]
        IActionResult Login();

        [HttpGet]
        Task<IActionResult> Login(ILoginModel model);

        [HttpGet]
        Task<IActionResult> Logout();
    }
}
