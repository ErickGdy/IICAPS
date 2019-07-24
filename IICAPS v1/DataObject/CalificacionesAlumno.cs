using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    class CalificacionesAlumno
    {
        public string RFC { get; set; }
        public string Alumno { get; set; }
        public List<Calificacion> Calificaciones { get; set; }
    }
}
