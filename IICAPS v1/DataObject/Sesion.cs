using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Sesion
    {
        public int id { get; set; }
        public decimal Costo { get; set; }
        public string tipo { get; set; }
        public string Estado { get; set; }
    }
}
