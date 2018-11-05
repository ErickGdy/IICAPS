using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Agenda
    {
        public int id { get; set; }
        public int sesion { get; set; }
        public int paciente { get; set; }
        public int reservacion { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string ubicacion { get; set; }
        public string observaciones { get; set; }
        public string estado { get; set; }

    }
}
