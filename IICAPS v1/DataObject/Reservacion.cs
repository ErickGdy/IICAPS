using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Reservacion
    {
        public int Id { get; set; }
        public string Codigo_Reservacion { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora_Inicio { get; set; }
        public TimeSpan Duracion { get; set; }
        public TimeSpan Hora_Fin { get; set; }
        public string Ubicacion { get; set; }
        public string Observaciones { get; set; }
        public string Reservante { get; set; }
        public string Concepto { get; set; }
        public string Id_parent { get; set; }
        public string Agenda_text()
        {
            return this.Concepto.ToUpper() + " \n" + this.Reservante + "\n";
        }

        public string Formato_folio()
        {
            return Id.ToString("0000000");
        }
    }
}
