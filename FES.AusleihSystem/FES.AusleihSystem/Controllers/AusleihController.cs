using FES.AusleihSystem.Data;
using FES.AusleihSystem.Models;
using FES.AusleihSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FES.AusleihSystem.Controllers
{
    public class AusleihController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _user;
        public AusleihController(ApplicationDbContext context, UserManager<ApplicationUser> user)
        {
            _context = context;
            _user = user;
        }


        [HttpGet]
        [Authorize]
        public IActionResult Ubersicht()
        {

            List<ReservierungViewModel> res = new List<ReservierungViewModel>();
            GeraetViewModel ger = new GeraetViewModel();
            IQueryable<GeraetViewModel> gerList;
            using (var ctx = _context)
            {

                res = ctx.Reservierungen.ToList();
                foreach (var reservierung in res)
                {
                    gerList = ctx.Geraete.Where(g => g.Reservierung.ReservierungsNummer == reservierung.ReservierungsNummer);
                    reservierung.GeraeteListe = gerList.ToList();
                }


            }
            return View(res);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ReservierungAnlegen()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReservierungAnlegen(GeraeteReservierungModel model)
        {
            var VM = new ReservierungViewModel()
            {
                Nutzer = await _user.GetUserAsync(User),
                GeraeteListe = GetGeraet(model.GeraeteEan),
                ReservierungsBeginn = model.ReservierungsBeginn,
                ReservierungsEnde = model.ReservierungsEnde,
                ReservierungsZeitpunkt = DateTime.Now
            };



            _context.Reservierungen.Add(VM);
            _context.SaveChanges();


            return RedirectToAction("Ubersicht");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Loeschen(ReservierungViewModel model)
        {
            if (model.ReservierungsNummer == 0)
            {
                Console.WriteLine("Kein Model gefunden");
            }
            else
            {
                var res = _context.Reservierungen.First(g => g.ReservierungsNummer == model.ReservierungsNummer);
                foreach (var gerat in res.GeraeteListe)
                {
                    gerat.GeraeteStatus = GeraetViewModel.Status.isVerfugbar;
                    gerat.Reservierung = null;
                    //_context.Geraete.Update(g => g.ID == gerat.ID);
                    //_context.Reservierungen.Remove(res);


                }
                _context.Reservierungen.Remove(res);
                 _context.SaveChanges();
            }

            return RedirectToAction("Ubersicht");
        }




        private List<GeraetViewModel> GetGeraet(int id)
        {
            var result = new List<GeraetViewModel>();
            IQueryable<GeraetViewModel> temp;


            temp = _context.Geraete.Where(g => (g.ID == id && g.GeraeteStatus == GeraetViewModel.Status.isVerfugbar));
            if (temp.Count() > 0)
            {
                foreach (var geraet in temp)
                {
                    result.Add(geraet);
                    geraet.GeraeteStatus = GeraetViewModel.Status.isReserviert;
                }
            }

            return result;
        }
    }
}
