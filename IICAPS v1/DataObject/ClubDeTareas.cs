using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class ClubDeTareas
    {
        public int id { get; set; }
        public string Psicoterapeuta { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public Reservacion reservacion { get; set; }
        public decimal Costo { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }
    }
}
