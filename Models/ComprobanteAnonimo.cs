using System;

namespace FacturacionElectronica.Models
{
    public class ComprobanteAnonimo
    {
        public Int64 CodigoComprobante { get; set; }
        public byte CodigoTipoComprobante { get; set; }
        public string NombreTipoComprobante { get; set; }
        public string SerieNumero { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaComprobante { get; set; }
        public byte CodigoEstadoComprobante { get; set; }
        public string NombreEstadoComprobante { get; set; }
        public int CodigoEstadoComprobanteSUNAT { get; set; }
        public string NombreEstadoComprobanteSUNAT { get; set; }
    }
}
