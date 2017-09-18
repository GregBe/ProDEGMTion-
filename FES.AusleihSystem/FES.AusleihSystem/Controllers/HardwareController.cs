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
            /// <summary>
            /// Dieser Controller handhabt die Geräte Verwaltung
            /// </summary>

            private readonly ApplicationDbContext _context;

            /// <summary>
            /// Der Konstruktor wird zur Laufzeit aufgerufen, sobald der User auf .../Hardware/... gelangt 
            /// </summary>
            /// <param name="context">DBContext</param>
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
            /// <summary>
            /// Fügt Geräte hinzu .../Hardware/AddGeraet
            /// </summary>
            /// <returns></returns>
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

            /// <summary>
            /// .../Hardware/GeraeteAnsicht
            /// Zeigt alle Geraete an
            /// </summary>
            /// <returns></returns>
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