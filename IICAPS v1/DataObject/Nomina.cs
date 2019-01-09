using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Nomina
    {
        public int ID { get; set; }
        public string Psicoterapeutas { get; set; }
        public string Entrego { get; set; }
        public DateTime DiaEntrega { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }

    }
}
