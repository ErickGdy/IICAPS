using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class CreditoAlumno
    {
        public int id { get; set; }
        public string alumno { get; set; }
        public decimal cantidadMensualidad { get; set; }
        public int cantidadMeses { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public string observaciones { get; set; }
        public string estado { get; set; }
        public decimal cantidadAbonoCredito { get; set; }
        public decimal cantidadAbonoMensual { get; set; }
    }
}
