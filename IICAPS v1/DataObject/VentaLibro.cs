using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class VentaLibro
    {
        public int Id { get; set; }
        public string Comprador_ID { get; set; }
        public string TipoVenta { get; set; }
        public string Recibio { get; set; }
        public decimal Total { get; set; }
        public string Observaciones { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetalleVentaLibro> DetallesVenta { get; set; }

        public Cobro cobro { get; set; }
    }
}
