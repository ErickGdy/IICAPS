using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Taller
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public decimal CostoClientes { get; set; }
        public decimal CostoPublico { get; set; }
        public int Capacidad { get; set; }
        public string Requisitos { get; set; }
    }
}
