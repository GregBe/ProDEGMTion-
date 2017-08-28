using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FES.SchulHardwareAusleihSystem.Interfaces.Model
{
    public interface IHardwareModel
    {
        string Bezeichnung { get; set; }
        string[] Bemerkung { get; set; }
        bool Ausleihbar { get; set; }
        int EanCode { get; set; }

    }
}
