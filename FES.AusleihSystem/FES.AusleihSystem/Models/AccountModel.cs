using FES.AusleihSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FES.AusleihSystem.Models
{
    public class AccountModel
    {
        public string Email { get; set; }
        public string NutzerID { get; set; }

        public List<ReservierungViewModel> Reservierungen { get; set; }
    }
}
