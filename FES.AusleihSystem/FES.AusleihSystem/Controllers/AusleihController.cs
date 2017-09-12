using FES.AusleihSystem.Data;
using FES.AusleihSystem.Models;
using FES.AusleihSystem.ViewModels;
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
        public AusleihController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
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
        public IActionResult ReservierungAnlegen()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ReservierungAnlegen(GeraeteReservierungModel model)
        {
            var VM = new ReservierungViewModel()
            {
                Nutzer = GetNutzer(),
                GeraeteListe = GetGeraet(model.GeraeteEan),
                ReservierungsBeginn = model.ReservierungsBeginn,
                ReservierungsEnde = model.ReservierungsEnde,
                ReservierungsZeitpunkt = DateTime.Now
            };



            _context.Reservierungen.Add(VM);
            _context.SaveChanges();


            return RedirectToAction("Ubersicht");
        }

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


        private NutzerViewModel GetNutzer()
        {

            if (_context.Nutzer.Count() > 0)
            {
                return _context.Nutzer.First();
            }
            else
            {
                return new NutzerViewModel()
                {
                    Email = "t@t.de",
                    Passwort = "..Aa12",
                    NutzerRolle = new Rolle(),
                };
            }


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
