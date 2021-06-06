using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionElectronica.Models
{
    public class taComprobanteArchivo
    {
        public Int64 CodigoComprobante { get; set; }
        public string NombreArchivo { get; set; }
        public byte[] ItemImage { get; set; }
    }
}
