using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class TallerAsistente
    {
        public int ID { get; set; }
        public int taller { get; set; }
        public string nombre { get; set; }
        public string rfc { get; set; }
        public string curp { get; set; }
        public string observaciones { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public decimal pago { get; set; }
        public decimal costo { get; set; }
        public decimal anticipo { get; set; }
        public decimal restante { get; set; }
    }
}
