using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Pago
    {
        public int id { get; set; }
        public int parent_id { get; set; }
        public string emisor { get; set; }
        public DateTime fechaPago { get; set; }
        public double cantidad { get; set; }
        public string concepto { get; set; }
        public string area { get; set; }
        public string observaciones { get; set; }
        public string recibio { get; set; }
        public string estado { get; set; }

        public string formatoFolio()
        {
            return id.ToString("0000000");
        }
    }
}
