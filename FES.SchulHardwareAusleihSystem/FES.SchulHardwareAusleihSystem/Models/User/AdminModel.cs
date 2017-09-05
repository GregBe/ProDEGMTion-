using FES.SchulHardwareAusleihSystem.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FES.SchulHardwareAusleihSystem.Models.User
{
    public class AdminModel : IAdminModel
    {
        /// <summary>
        /// Zum Anlegen neuer Nutzer werden die Email als Nutzername verlangt als auch ein Passwort
        /// </summary>
        [Required]
        [EmailAddress]
        public string NutzerName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NutzerPasswort { get; set; }
    }
}
