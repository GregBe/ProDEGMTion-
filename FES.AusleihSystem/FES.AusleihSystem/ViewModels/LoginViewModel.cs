using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FES.AusleihSystem.ViewModels
{
    public class LoginViewModel
    {
        /// <summary>
        /// Eigenschafte welche beim Login vom Nutzer eingegeben werden
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Passwort { get; set; }

        //public bool ErneutEinLoggen { get; set; }
    }
}
