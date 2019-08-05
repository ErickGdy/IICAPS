using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IICAPS_v1.DataObject
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; } 
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public decimal Precio_base { get; set; }
        public string Programa { get; set; }
        public int Stock_vitrina_1 { get; set; }
        public int Stock_vitrina_2 { get; set; }
        public int Stock_almacen { get; set; }
        public int Prestados { get; set; }
        public int Stock_total() {
            return Stock_almacen + Stock_vitrina_1 + Stock_vitrina_2 + Prestados;
        }
    }
}
