using FES.AusleihSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FES.AusleihSystem.ViewModels
{
    public class ReservierungViewModel
    {
        /// <summary>
        ///Beschreibt welche Eigenschaften bei einer Reservierung erfasst werden und an den Controller
        ///weitergeleitet werden.
        /// </summary>
        /// 

        [Key]
        public int ReservierungsNummer { get; set; }
        
        public string NutzerID { get; set; }
        public List<GeraetViewModel> GeraeteListe { get; set; } = new List<GeraetViewModel>();
        [DataType(DataType.DateTime)]
        public DateTime ReservierungsZeitpunkt { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:HH\:mm}")]
        public DateTime ReservierungsEnde { get; set; }

        public enum EntliehenStatus
        {
            [StringValue("Ausgeliehen")]
            isAusgeliehen,
            [StringValue("Nicht ausgeliehen")]
            isNichtAusgeliehen,
        };

        public EntliehenStatus AusgeliehenStatus { get; set; } = EntliehenStatus.isNichtAusgeliehen;

        public TimeSpan ReservierungsDauerTage { get; set; }
        public TimeSpan ReservierungsDauerStunden { get; set; }
        [DataType(DataType.Time)]
        public DateTime ReservierungsBeginn { get; set; }

    }
}
