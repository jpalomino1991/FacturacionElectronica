using Microsoft.AspNetCore.Identity;

namespace FacturacionElectronica.Models
{
    public class User : IdentityUser
    {
        public string RazonSocial { get; set; }
        public string NumeroDocumento { get; set; }
    }
}
