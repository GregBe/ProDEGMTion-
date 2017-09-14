using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FES.AusleihSystem.ViewModels;
using FES.AusleihSystem.Data;
using Microsoft.AspNetCore.Authorization;

namespace FES.AusleihSystem.Controllers
{
    namespace FES.AusleihSystem.Controllers
    {
        public class HardwareController : Controller
        {
            private readonly ApplicationDbContext _context;
            public HardwareController(ApplicationDbContext context)
            {
                _context = context;
            }
            [Authorize]
            public IActionResult Index()
            {
                return View();
            }

            [HttpGet]
            [Authorize(Roles ="Admin")]
            public IActionResult AddGeraet()
            {
                return View();
            }

            [HttpPost]
            [Authorize(Roles = "Admin")]
            public IActionResult AddGeraet(GeraetViewModel geraet)
            {
                using (var ctx = _context)
                {
                    ctx.Geraete.Add(geraet);
                    ctx.SaveChanges();
                }
                return RedirectToAction("GeraeteAnsicht");
            }
            [Authorize]
            public IActionResult GeraeteAnsicht()
            {
                List<GeraetViewModel> ger = new List<GeraetViewModel>();
                using (var ctx = _context)
                {
                    var list = ctx.Geraete;
                    ger = list.ToList();
                }

                return View(ger);
            }
        }
    }
}