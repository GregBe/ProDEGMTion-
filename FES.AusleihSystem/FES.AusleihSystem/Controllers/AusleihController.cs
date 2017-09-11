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
    public class AusleihController: Controller
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
            using (var ctx = _context)
            {
                res = ctx.Reservierungen.ToList();
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
    

            return View();
        }

        private NutzerViewModel GetNutzer()
        {

                if(_context.Nutzer.Count() > 0)
                {
                    return _context.Nutzer.First();
                }
                else
                {
                    return new NutzerViewModel()
                    {
                        Email ="t@t.de",
                        Passwort ="..Aa12",
                        NutzerRolle= new Rolle(),
                    };
                }
                
            
        }
        private List<GeraetViewModel> GetGeraet(int ean)
        {
            var result = new List<GeraetViewModel>();
            IQueryable<GeraetViewModel> temp;
      
            
            temp = _context.Geraete.Where(g => g.EAN == ean);
            
            foreach (var geraet in temp)
            {
                result.Add(geraet);
            }
            return result;
        }
    }
}
