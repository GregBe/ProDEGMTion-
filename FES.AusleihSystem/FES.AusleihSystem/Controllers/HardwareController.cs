using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FES.AusleihSystem.ViewModels;
using FES.AusleihSystem.Data;

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

            public IActionResult Index()
            {
                return View();
            }
            [HttpGet]
            public IActionResult AddGeraet()
            {

                return View();
            }
            [HttpPost]
            public IActionResult AddGeraet(GeraetViewModel geraet)
            {
                using (var ctx = _context)
                {
                    ctx.Geraete.Add(geraet);
                    ctx.SaveChanges();
                }
                return View();
            }

            public IActionResult GeraeteAnsicht()
            {
                List<GeraetViewModel> ger = new List<GeraetViewModel>();
                using (var ctx = _context)
                {
                    ger = ctx.Geraete.ToList();
                }

                return View(ger);
            }
        }
}
}