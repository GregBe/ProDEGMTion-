using FES.AusleihSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FES.AusleihSystem.Models
{
    public class GeraeteReservierungModel
    {

        public int NutzerID { get; set; }
        public int GeraeteEan { get; set; }
        public GeraeteKategorie Kategorie { get; set; }

        [NotMapped]
        public List<GeraeteKategorie> KategorieList { get; set; } = new List<GeraeteKategorie>();

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime ReservierungsEnde { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReservierungsBeginn { get; set; }
        [DataType(DataType.Time)]
        public DateTime ReservierungsBeginnZeit { get; set; }
        [DataType(DataType.Time)]
        public DateTime ReservierungsEndeZeit { get; set; }
    }
}
