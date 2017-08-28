using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FES.SchulHardwareAusleihSystem.Interfaces.Model
{
    public interface ILoginModel
    {
        [Required]
        [EmailAddress]
        string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        string Passwort { get; set; }

    }
}
