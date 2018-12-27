using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Sesion
    {
        public int id { get; set; }
        public Reservacion reservacion { get; set; }
        public int psicoterapeuta { get; set; }
        public int paciente { get; set; }
        public decimal Costo { get; set; }
        public string tipo { get; set; }
        public string observaciones { get; set; }
        public string estado { get; set; }
    }
}
