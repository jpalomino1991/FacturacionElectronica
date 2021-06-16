using FacturacionElectronica.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionElectronica.Models
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "Ingrese razón social")]
        [Display(Name = "Razón Social")]
        public string RazonSocial { get; set; }
        [NotMapped]
        [Display(Name = "Tipo de Documento")]
        public int TipoDocumento { get; set; }
        [Required(ErrorMessage = "Ingrese número de documento")]
        [Display(Name = "Número de Documento")]
        [CustomDocumentType]
        public string NumeroDocumento { get; set; }
        [Required(ErrorMessage = "Ingrese correo electrónico")]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ingrese contraseña")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmPassword { get; set; }
    }
}
