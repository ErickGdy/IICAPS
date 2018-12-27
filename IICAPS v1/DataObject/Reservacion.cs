using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Reservacion
    {
        public int id { get; set; }
        public string codigo_Reservacion { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan hora_Inicio { get; set; }
        public TimeSpan duracion { get; set; }
        public TimeSpan hora_Fin { get; set; }
        public string ubicacion { get; set; }
        public string observaciones { get; set; }
        public string reservante { get; set; }
        public string concepto { get; set; }
        public string id_parent { get; set; }
        public string agendaText()
        {
            return this.concepto.ToUpper() + " \n" + this.reservante + "\n";
        }

        public string formatoFolio()
        {
            return id.ToString("0000000");
        }
    }
}
