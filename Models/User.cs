using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionElectronica.Models
{
    public class User : IdentityUser
    {
        public string RazonSocial { get; set; }
        public string NumeroDocumento { get; set; }
    }
}
