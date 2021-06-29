using System.ComponentModel.DataAnnotations;

namespace FacturacionElectronica.Models
{
    public class AccesoAnonimoModel
    {
        [Required(ErrorMessage = "Ingrese tipo de comprobante")]
        [Display(Name = "Tipo de Comprobante")]
        public int tipoComprobante { get; set; }
        [Required(ErrorMessage = "Ingrese serie de comprobante")]
        [Display(Name = "Serie")]
        public string serie { get; set; }
        [Required(ErrorMessage = "Ingrese número de comprobante")]
        [Display(Name = "Número")]
        public string numero { get; set; }
        [Required(ErrorMessage = "Ingrese monto total")]
        [Display(Name = "Monto Total")]
        public string monto { get; set; }
        [Required(ErrorMessage = "Ingrese fecha de emisión")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Emisión")]
        public string fecha { get; set; }
        public string error { get; set; }
    }
}
