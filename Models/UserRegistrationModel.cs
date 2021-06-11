using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionElectronica.Models
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "Ingrese Razón Social")]
        [Display(Name = "Razón Social")]
        public string RazonSocial { get; set; }
        [Required(ErrorMessage = "Ingrese Número de Documento")]
        [Display(Name = "Número de Documento")]
        public string NumeroDocumento { get; set; }
        [Required(ErrorMessage = "Debe poner un correo")]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Contraseña requerida")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmPassword { get; set; }
    }
}
