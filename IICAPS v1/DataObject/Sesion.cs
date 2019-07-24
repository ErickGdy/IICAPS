using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Sesion
    {
        public int Id { get; set; }
        public Reservacion Reservacion { get; set; }
        public string Psicoterapeuta { get; set; }
        public int Paciente { get; set; }
        public decimal Costo { get; set; }
        public decimal Pago { get; set; }
        public decimal Pendiente { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Tipo { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }
    }
}
