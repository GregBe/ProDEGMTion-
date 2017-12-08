using FES.AusleihSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
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
            [StringValue("Verfügbar")]
            isVerfugbar,
            [StringValue("Reserviert")]
            isReserviert,
            [StringValue("Ausgeliehen")]
            isAusgeliehen,
            [StringValue("Defekt")]
            isDefekt,
            [StringValue("Entfernt")]
            isEntfernt
        };

        public int ID { get; set; }
        public string Name { get; set; }
        public int EAN { get; set; }
        public Status GeraeteStatus { get; set; } = Status.isVerfugbar;
        //public string[] Kommentar { get; set; }
        public virtual ReservierungViewModel Reservierung { get; set; }
        public string Kategorie { get; set; }
        public GeraeteKategorie GeKategorie { get; set; }
    }

    public class StringValue : System.Attribute
    {
        private readonly string _value;

        public StringValue(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }

    }

    public static class StringEnum
    {
        public static string GetStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();

            //Check first in our cached results...

            //Look for our 'StringValueAttribute' 

            //in the field's custom attributes

            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs =
               fi.GetCustomAttributes(typeof(StringValue),
                                       false) as StringValue[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }
}
