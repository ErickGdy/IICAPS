using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Materia
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string duracion { get; set; }
        public string semestre { get; set; }
        public decimal costo { get; set; }
        public string programa { get; set; }

    }
}
