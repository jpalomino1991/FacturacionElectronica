using FacturacionElectronica.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionElectronica.CustomValidation
{
    public class CustomDocumentType : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (UserRegistrationModel)validationContext.ObjectInstance;

            if(user.TipoDocumento == 1)
                return (user.NumeroDocumento.Length == 8 ? ValidationResult.Success : new ValidationResult("El número debe ser de 8 dígitos"));
            else
                return (user.NumeroDocumento.Length == 11 ? ValidationResult.Success : new ValidationResult("El número debe ser de 11 dígitos"));
        }
    }
}
