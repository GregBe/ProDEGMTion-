using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FES.SchulHardwareAusleihSystem.Interfaces.Model
{
    public interface IAdminModel
    {
        [Required]
        [EmailAddress]
        string NutzerName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        string NutzerPasswort { get; set; }
    }
}
