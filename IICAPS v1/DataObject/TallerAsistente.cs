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
        public int Taller { get; set; }
        public string Nombre { get; set; }
        public string Rfc { get; set; }
        public string Curp { get; set; }
        public string Observaciones { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public decimal Pago { get; set; }
        public decimal Costo { get; set; }
        public decimal Restante { get; set; }
    }
}
