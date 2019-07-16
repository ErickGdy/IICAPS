using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IICAPS_v1.DataObject
{
    public class Libro
    {
        public int id { get; set; }
        public string titulo { get; set; } 
        public string autor { get; set; }
        public string editorial { get; set; }
        public decimal precio_base { get; set; }
        public string programa { get; set; }
        public int stock_vitrina_1 { get; set; }
        public int stock_vitrina_2 { get; set; }
        public int stock_almacen { get; set; }
        public int stock_total() {
            return stock_almacen + stock_vitrina_1 + stock_vitrina_2;
        }
    }
}
