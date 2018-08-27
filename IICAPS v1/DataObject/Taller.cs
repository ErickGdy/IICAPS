using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Taller
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public DateTime fecha { get; set; }
        public decimal costoClientes { get; set; }
        public decimal costoPublico { get; set; }
        public int capacidad { get; set; }
        public string requisitos { get; set; }
    }
}
