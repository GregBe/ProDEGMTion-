using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FES.SchulHardwareAusleihSystem.Models.Hardware
{
    public class Geraet
    {
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public int EAN { get; set; }
        public string Notiz { get; set; }
    }
}
