using FES.AusleihSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FES.AusleihSystem.Models
{
    public class GeraeteReservierungModel
    {

        public int NutzerID { get; set; }
        public int GeraeteEan { get; set; }
        public GeraeteKategorie Kategorie { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime ReservierungsEnde { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReservierungsBeginn { get; set; }
        
    }
}
