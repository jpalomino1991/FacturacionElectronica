﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionElectronica.Models
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "Ingrese Razón Social")]
        public string RazonSocial { get; set; }
        [Required(ErrorMessage = "Ingrese Número de Documento")]
        public string NumeroDocumento { get; set; }
        [Required(ErrorMessage = "Debe poner un correo")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Contraseña requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}