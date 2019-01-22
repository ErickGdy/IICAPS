using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class ParametrosGenerales
    {
        public string Director{ get; set; }
        public string Sede{ get; set; }
        public decimal Costo_Credito_Especialidad_Diplomado { get; set; }
        public decimal Costo_Credito_Maestria { get; set; }
        public decimal Porcentaje_Pago_Sesion { get; set; }
        public decimal Porcentaje_Pago_Taller { get; set; }
        public decimal Porcentaje_Pago_Clase { get; set; }
        public decimal Porcentaje_Pago_Evaluacion { get; set; }
        public List<string> ubicaciones { get; set; }

    }
}
