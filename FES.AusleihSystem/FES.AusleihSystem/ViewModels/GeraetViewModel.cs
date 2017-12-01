using FES.AusleihSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FES.AusleihSystem.ViewModels
{
    public class GeraetViewModel
    {
        /// <summary>
        /// Hier werden die Eigenschaften für ein Gerät festgelegt und somit
        /// auch die DBTabelle festgelegt.
        /// </summary>
        public enum Status
        {
            isVerfugbar,
            isReserviert,
            isAusgeliehen,
            isDefekt,
            isEntfernt
        };

        public int ID { get; set; }
        public string Name { get; set; }
        public int EAN { get; set; }
        public Status GeraeteStatus { get; set; } = Status.isVerfugbar;
        
        public virtual ReservierungViewModel Reservierung { get; set; }
        public string Kategorie { get; set; }
        public GeraeteKategorie GeKategorie { get; set; }
    }
}
