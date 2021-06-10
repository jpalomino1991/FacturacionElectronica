using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionElectronica.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Ingrese correo electrónico")]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ingrese contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}
