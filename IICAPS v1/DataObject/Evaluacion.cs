using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Evaluacion
    {
        public int id { get; set; }
        public decimal costo { get; set; }
        public int paciente { get; set; }
        public int reservacion { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string ubicacion { get; set; }
        public string observaciones { get; set; }
        public decimal pago { get; set; }
        public string pruebas { get; set; }
        public string estado { get; set; }
    }
}
