using FES.AusleihSystem.Data;
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

        public static string[] Roles = new string[]
        {
            "Admin",
            "Lehrer",
            "Schueler"

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

                foreach(var role in Roles)
                {
                    if(await roleManager.FindByNameAsync(role) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole { Name = role });
                    }
                }
            }
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

            if (_context.UserRoles.Count() < 1)
            {
                SeedRollen();
                Console.WriteLine("Rollen wurden geseedet");
            }
                
        }
    }
}
