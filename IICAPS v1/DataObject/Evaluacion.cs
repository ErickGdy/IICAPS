using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Evaluacion
    {
        public int Id { get; set; }
        public string Psicoterapeuta { get; set; }
        public decimal Costo { get; set; }
        public int Paciente { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public Reservacion Reservacion { get; set; }
        public string Observaciones { get; set; }
        public string Pruebas { get; set; }
        public string Estado { get; set; }
    }
}
