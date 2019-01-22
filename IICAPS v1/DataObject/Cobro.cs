using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Cobro
    {
        public int id { get; set; }
        public string parent_id { get; set; }
        public DateTime fecha { get; set; }
        public decimal cantidad { get; set; }
        public decimal restante { get; set; }
        public decimal pago { get; set; }
        public string concepto { get; set; }
        public string alumno { get; set; }

    }
}
