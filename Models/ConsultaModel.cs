using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionElectronica.Models
{
    public class ConsultaModel
    {
        public int tipoComprobante { get; set; }
        public int serie { get; set; }
        public int numero { get; set; }
        public decimal monto { get; set; }
        public DateTime fecha { get; set; }

    }
}
