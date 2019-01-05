using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class ClubDeTareasAsistente
    {
        public int ID { get; set; }
        public int Club_Tareas_ID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NombreTutor { get; set; }
        public string TelefonoTutor { get; set; }
        public string Observaciones { get; set; }
        public decimal Pago { get; set; }
        public decimal Costo { get; set; }
        public decimal Restante { get; set; }
    }
}
