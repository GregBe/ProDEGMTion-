﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FES.AusleihSystem.ViewModels
{
    public class GeraetViewModel
    {
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
    }
}