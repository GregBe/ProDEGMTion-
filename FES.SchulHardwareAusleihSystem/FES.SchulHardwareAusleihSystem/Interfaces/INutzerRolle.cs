using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FES.SchulHardwareAusleihSystem.Interfaces
{
    public interface INutzerRolle
    {
        string Name { get; set; }
        int BerechtigungsStufe { get; set; }
    }
}
