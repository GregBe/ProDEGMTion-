using FES.AusleihSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FES.AusleihSystem.ViewModels
{
    public class NutzerViewModel
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Passwort { get; set; }
        public Rolle NutzerRolle { get; set; }
    }
}
