using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class PagoAlumno
    {
        public int id { get; set; }
        public string alumnoID { get; set; }
        public DateTime fechaPago { get; set; }
        public double cantidad { get; set; }
        public string concepto { get; set; }
        public string observaciones { get; set; }
        public string recibio { get; set; }
    }
}
