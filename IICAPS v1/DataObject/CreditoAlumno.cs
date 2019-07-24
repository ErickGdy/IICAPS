using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class CreditoAlumno
    {
        public decimal Pago { get; set; }
        public int Id { get; set; }
        public string Alumno { get; set; }
        public decimal CantidadMensualidad { get; set; }
        public int CantidadMeses { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }
        public decimal CantidadAbonoCredito { get; set; }
        public decimal CantidadAbonoMensual { get; set; }
    }
}
