using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FES.AusleihSystem.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public string Passwort { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort erneut eingeben")]
        [Compare("Passwort", ErrorMessage = "Die Passwörter stimmen nicht überein")]
        public string ConfPasswort { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; } = "Lehrer";
    }
}
