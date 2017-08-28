using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FES.SchulHardwareAusleihSystem.Interfaces
{
    public interface INutzer
    {
        /// <summary>
        /// Dieses Interface beinhaltet alles rund um den Nutzer
        /// </summary>
        string Name { get; set; }
        INutzerRolle NutzerRolle { get; set; }
        string Email { get; set; }
    }
}
