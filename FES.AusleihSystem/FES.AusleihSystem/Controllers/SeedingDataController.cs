using FES.AusleihSystem.Data;
using FES.AusleihSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FES.AusleihSystem.Controllers
{
    public class SeedingDataController: Controller
    {
        private string[] GeraeteNamen = new string[]
        {
            "Beamer",
            "Laptop",
            "PC",
            "Monitor",
            "Lautsprecher",
            "Maus",
            "Tastatur",
        };
        private ApplicationDbContext _context;
        private int SeedSize = 100;
        public SeedingDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Seeding()
        {
            SeedGeraeteData();
            return RedirectToAction("Index", "Home");
        }

        public void SeedGeraeteData()
        {
            Random ran;
            int name;
            int ean;
            ran = new Random();
            //Geräte Seeding
            for (int i=0; i< SeedSize; i++)
            {
                
                name = ran.Next(0, 6);
                ean = ran.Next(10000000, 99999999);
                var gerat = new GeraetViewModel()
                {
                    Name = GeraeteNamen[name],
                    GeraeteStatus = GeraetViewModel.Status.isVerfugbar,
                    EAN = ean,
                };
                _context.Geraete.Add(gerat);
            }
            _context.SaveChanges();
        }
    }
}
