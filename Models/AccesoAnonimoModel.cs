using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionElectronica.Models
{
    public class AccesoAnonimoModel
    {
        [Required]
        [Display(Name = "Tipo de Comprobante")]
        public int tipoComprobante { get; set; }
        [Required]
        [Display(Name = "Serie")]
        public int serie { get; set; }
        [Required]
        [Display(Name = "Número")]
        public int numero { get; set; }
        [Display(Name = "Monto Total")]
        public decimal monto { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Emisión")]
        public DateTime fecha { get; set; }
    }
}
