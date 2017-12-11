using FES.AusleihSystem.Data;
using FES.AusleihSystem.Models;
using FES.AusleihSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FES.AusleihSystem.Controllers
{
    public class SeedingDataController : Controller
    {
        /// <summary>
        /// Fügt Geräte und Rollen hinzu
        /// Testdaten
        /// </summary>
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

        public static string[] Roles = new string[]
        {
            "Admin",
            "Lehrer",
            "Schueler"

        };
        public static string[] Kategorien = new string[]
        {
            "Beamer",
            "Laptop",
            "Drucker"

        };

        private ApplicationDbContext _context;
        private IServiceProvider _service;
        private int SeedSize = 100;
        public SeedingDataController(ApplicationDbContext context, IServiceProvider service)
        {
            _context = context;
            _service = service;
        }

        public IActionResult Seeding()
        {
            SeedGeraeteData();
            return RedirectToAction("Index", "Home");
        }
        public async void SeedRollen()
        {
            IServiceScopeFactory scopeFactory = _service.GetRequiredService<IServiceScopeFactory>();
            using (IServiceScope scope = scopeFactory.CreateScope())
            {
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                foreach (var role in Roles)
                {
                    if (await roleManager.FindByNameAsync(role) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole { Name = role });
                    }
                }
            }
        }
        public void KategorieSeeding()
        {
            for (int i = 0; i < Kategorien.Length; i++)
            {
                var Kategorie = new GeraeteKategorie
                {
                    Name = Kategorien[i]
                };
                Console.Write(Kategorien[i] + "wurde hinzugefügt, ");
                _context.Kategorien.Add(Kategorie);
            }
            _context.SaveChanges();
            Console.WriteLine("Kategorien wurden geseedet");

        }
        public void SeedGeraeteData()
        {
            Random ran;
            int name;
            int ean;
            KategorieSeeding();
            ran = new Random();
            //Geräte Seeding
            for (int i = 0; i < SeedSize; i++)
            {
                name = ran.Next(0, 6);
                var catint = ran.Next(1, 4);
                ean = ran.Next(10000000, 99999999);
                var gerat = new GeraetViewModel()
                {
                    Name = GeraeteNamen[name],
                    GeraeteStatus = GeraetViewModel.Status.isVerfugbar,
                    Kategorie = _context.Kategorien.Where((o) => o.ID == catint).FirstOrDefault().Name,
                    GeKategorie = _context.Kategorien.Where((o) => o.ID == catint).FirstOrDefault(),                
                   // Kategorie = _context.Kategorien.Where((o) => o.ID == name % 3).FirstOrDefault(),
                    EAN = ean,
                };
                _context.Geraete.Add(gerat);
            }
            _context.SaveChanges();

            if (_context.UserRoles.Count() < 1)
            {
                SeedRollen();
                Console.WriteLine("Rollen wurden geseedet");
            }
            if (_context.Kategorien.Count() < 1)
            {
               
            }

        }
        public void AddKatToGeraet()
        {
            var ger = _context.Geraete.Where((o) => o.GeKategorie == null || o.Kategorie == null);
            var rand = new Random();
            
            foreach (var item in ger.ToList())
            {
                var i = rand.Next(1, 3);
                var gerat = _context.Geraete.Where((o) => o.ID == item.ID).FirstOrDefault();
                gerat.Kategorie = _context.Kategorien.Where((o) => o.ID == i).FirstOrDefault().Name;
                gerat.GeKategorie = _context.Kategorien.Where((o) => o.ID == i).FirstOrDefault();
                _context.Geraete.Remove(item);
                _context.Geraete.Add(gerat);
            }
            _context.SaveChangesAsync();
        }
    }
}
