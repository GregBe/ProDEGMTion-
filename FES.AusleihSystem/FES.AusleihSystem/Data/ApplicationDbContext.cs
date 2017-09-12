using FES.AusleihSystem.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FES.AusleihSystem.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {
        }
        public DbSet<NutzerViewModel> Nutzer { get; set; }
        public DbSet<GeraetViewModel> Geraete{ get; set; }
        public DbSet<ReservierungViewModel> Reservierungen { get; set; }
    }
}
//namespace FES.AusleihSystem.Controllers
//{
//    public class HardwareController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Hinzufuegen(GeraetViewModel geraet)
//        {
//            using (var ctx = new ApplicationDbContext())
//            {
//                ctx.Geraete.Add(geraet);
//                ctx.SaveChanges();
//            }
//                return View();
//        }

//        public IActionResult GeraeteAnsicht()
//        {
//            List<GeraetViewModel> ger = new List<GeraetViewModel>();
//            using (var ctx = new ApplicationDbContext())
//            {
//                ger = ctx.Geraete.ToList();
//            }
//            return View(ger);
//        }
//    }
//}