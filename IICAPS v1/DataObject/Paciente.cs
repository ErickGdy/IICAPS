using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Paciente
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string institucion { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string telefono { get; set; }
        public decimal costoEspecial { get; set; }
        public string nombre_tutor { get; set; }
        public string telefono_tutor { get; set; }
        public string estado { get; set; }
        public string[] datos_facturacion { get; set; }
        public string psicoterapeuta { get; set; }

        public string NombreCompleto()
        {
            try
            {
                return this.nombre + " " + apellidos;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
