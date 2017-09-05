using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FES.SchulHardwareAusleihSystem.Configuration
{
    public class NutzerRollenSeeding
    {
        public string[] Roles= new string[]
        {
            "Admin",
            "Lehrer",
            "Schueler",
        };
        private IApplicationBuilder _app;

        public NutzerRollenSeeding(IApplicationBuilder app)
        {
            _app = app;
        }
        /// <summary>
        /// Erstellt Nutzerrollen, insofern diese noch nicht existieren
        /// mit if wird geprüft ob die Role vorhanden ist.
        /// Kann in eigene Methode ausgelagert werden zu einem späteren Zeitpunkt.
        /// </summary>
        public async void Seed()
        {
            IServiceScopeFactory scopeFactory = _app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
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

    }
}
