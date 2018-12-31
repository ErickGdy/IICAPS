using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Psicoterapeuta
    {
        public int ID { get; set; }
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Carrera { get; set; }
        public string Especialidad { get; set; }
        public string Horario { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }
    }
}
