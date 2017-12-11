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
        /// <summary>
        /// Dieser Controller welcher die Ausleih und Reserverierungen händelt
        /// </summary>
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _user;

        /// <summary>
        /// Der Konstruktor wird zur Laufzeit aufgerufen, sobald der User auf .../Ausleih/... gelangt 
        /// </summary>
        /// <param name="context">Bekommt vom Service den aktuellen DbContext</param>
        /// <param name="user">Bekommt vom Service den aktuellen User</param>
        public AusleihController(ApplicationDbContext context, UserManager<ApplicationUser> user)
        {
            _context = context;
            _user = user;
        }

        /// <summary>
        /// Zeigt einfach nur eine View mit allen Reservierungen an
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Ubersicht(string errormsg = null)
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
            //List<GeraeteKategorie> kat = new List<GeraeteKategorie>();
            //kat = _context.Kategorien.ToList();
            //ViewBag.kategorien = kat;

            GeraeteReservierungModel model = new GeraeteReservierungModel();
            model.KategorieList= _context.Kategorien.ToList();
//            model.ReservierungsDauer = model.ReservierungsEnde - model.ReservierungsBeginn;
            model.ReservierungsBeginn = DateTime.Now;
            model.ReservierungsEnde = DateTime.Now;
            return View(model);
        }

        /// <summary>
        /// Prüft ob das eingegebene Model Valide ist und legt anschließend eine Reservierung an
        /// </summary>
        /// <param name="model">Nutzer Eingeben bei /Ausleih/ReservierungAnlegen</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ReservierungAnlegen(GeraeteReservierungModel model)
        {
            if (ModelState.IsValid)
            {
                var nutzer = await _user.GetUserAsync(User);
                var VM = new ReservierungViewModel()
                {

                    Nutzer = await _user.GetUserAsync(User),
                    GeraeteListe = GetGeraet(model.GeraeteEan),
                    ReservierungsBeginn = model.ReservierungsBeginn.Date + model.ReservierungsBeginnZeit.TimeOfDay,
                    ReservierungsEnde = model.ReservierungsEnde.Date + model.ReservierungsEndeZeit.TimeOfDay,
                    ReservierungsDauerTage = model.ReservierungsEnde.Date-model.ReservierungsBeginn.Date,
                    ReservierungsDauerStunden = model.ReservierungsEndeZeit - model.ReservierungsBeginnZeit,
                    ReservierungsZeitpunkt = DateTime.Now
                };



                _context.Reservierungen.Add(VM);
                _context.SaveChanges();


                return RedirectToAction("Ubersicht");
            }
            return View();
        }

        /// <summary>
        /// Nur Admins dürfen Löschen
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public IActionResult Loeschen(ReservierungViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ReservierungsNummer == 0)
                {
                    Console.WriteLine("Kein Model gefunden");
                }
                else
                {
                    var res = _context.Reservierungen.First(g => g.ReservierungsNummer == model.ReservierungsNummer);
                    var geraete = _context.Geraete.Where(g => g.Reservierung.ReservierungsNummer == res.ReservierungsNummer);
                    foreach (var gerat in geraete)
                    {
                        //_context.Geraete.Remove(gerat);
                        gerat.GeraeteStatus = GeraetViewModel.Status.isVerfugbar;
                        gerat.Reservierung = null;
                        //_context.Geraete.Update(g => g.ID == gerat.ID);
                        //_context.Reservierungen.Remove(res);
                        // _context.Geraete.Add(gerat);
                        _context.Geraete.Update(gerat);

                    }

                    _context.Reservierungen.Remove(res);
                    _context.SaveChanges();
                }

                return RedirectToAction("Ubersicht");
            }
            return RedirectToAction("Ubersicht");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult IstReserviert(ReservierungViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = _context.Reservierungen.Where(r => r.ReservierungsNummer == model.ReservierungsNummer).FirstOrDefault();
                res.AusgeliehenStatus = ReservierungViewModel.EntliehenStatus.isAusgeliehen;
                _context.Reservierungen.Update(res);
                _context.SaveChanges();
                return RedirectToAction("Ubersicht");
            }
            return RedirectToAction("Ubersicht");
        }


        private GeraetViewModel GetGeraetByKategorie(GeraeteKategorie kat)
        {
            var cont = _context.Geraete.Where(g => (g.GeKategorie.Name == kat.Name && g.GeraeteStatus == GeraetViewModel.Status.isVerfugbar));
            return cont.FirstOrDefault();
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
        //public IActionResult GerateTabelleReload(GeraeteReservierungModel model)
        //{
        //    var gerateList = _context.Geraete.Where(g => g.Kategorie == model.Kategorie.Name);
        //    model.GerateTabelle.CleanList();

        //    foreach (var item in gerateList)
        //    {
        //        model.GerateTabelle.AddGeraet(item);
        //    }
        //    return RedirectToAction("Ubersicht");
        //}
        public IActionResult GerateTabelleReload(int id)
        {
            return RedirectToAction("Ubersicht");
        }
        public JsonResult GerateTabelleReloadJson(int id)
        {
            var result = Json(_context.Geraete.Where(g => g.GeKategorie.ID == id && g.GeraeteStatus == GeraetViewModel.Status.isVerfugbar).OrderBy(o=>o.ID).ToList());
            return result;
        }
        public List<GeraetViewModel> GetGeraete()
        {
            return _context.Geraete.ToList();
        }
    }
}
