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
        public string psicoterapeuta { get; set; }
        public decimal costo { get; set; }
        public int paciente { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan hora { get; set; }
        public Reservacion reservacion { get; set; }
        public string observaciones { get; set; }
        public string pruebas { get; set; }
        public string estado { get; set; }
    }
}
