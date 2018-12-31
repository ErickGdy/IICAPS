using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Usuario
    {
        public bool Estado { get; set; }
        public string Matricula { get; set; }
        public string Nombre_De_Usuario { get; set; }
        public string Contrasena { get; set; }
        public int Nivel_Acceso { get; set; }
    }
}
