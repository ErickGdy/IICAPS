using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Institucion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public decimal CostoEspecial { get; set; }
        public string Nombre_tutor { get; set; }
        public string Telefono_tutor { get; set; }
        public string Estado { get; set; }
        public string[] Datos_facturacion { get; set; }
        public string Psicoterapeuta { get; set; }

        public string NombreCompleto()
        {
            try
            {
                return this.Nombre + " " + Apellidos;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
