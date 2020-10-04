using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class PagoLibreria
    {
        public int Id { get; set; }
        //0 para Publico general, != 0 para alumno
        public string CompradorID { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Total { get; set; }
        public decimal Pago { get; set; }
        public decimal Cambio { get; set; }
        public string Concepto { get; set; }
        public string Observaciones { get; set; }
        public string Recibio { get; set; }
        public string Parent_ID { get; set; }

        public string Formato_folio()
        {
            return Id.ToString("0000000");
        }
    }
}
