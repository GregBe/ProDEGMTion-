using FES.SchulHardwareAusleihSystem.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FES.SchulHardwareAusleihSystem.Models.User
{
    public class LoginModel: ILoginModel
    {
        /// <summary>
        /// DataAnnotations werden benötigt um Felder zu markieren
        /// oder deren "Datantyp" zu bestimmen 
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Passwort { get; set; }

    }
}
