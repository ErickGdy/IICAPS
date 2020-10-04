using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Prestamo
    {
        public int Id { get; set; }
        public string Comprador_ID { get; set; }
        public string Recibio { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaLimite { get; set; }
        public int Dias { get; set; }
        public List<DetallePrestamoLibro> DetallesPrestamo { get; set; }
    }               
}
