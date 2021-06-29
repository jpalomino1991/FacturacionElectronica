using System.ComponentModel.DataAnnotations;

namespace FacturacionElectronica.Models
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Ingrese correo electrónico")]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
    }
}
