using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Cobro
    {
        public int Id { get; set; }
        public string Parent_id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Restante { get; set; }
        public decimal Pago { get; set; }
        public string Concepto { get; set; }
        public string Remitente { get; set; }

    }
}
