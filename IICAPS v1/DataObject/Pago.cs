using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Pago
    {
        public int Id { get; set; }
        public int Parent_id { get; set; }
        public string Emisor { get; set; }
        public DateTime FechaPago { get; set; }
        public double Cantidad { get; set; }
        public string Concepto { get; set; }
        public string Area { get; set; }
        public string Observaciones { get; set; }
        public string Recibio { get; set; }
        public string Estado { get; set; }

        public string Formato_folio()
        {
            return Id.ToString("0000000");
        }
    }
}
