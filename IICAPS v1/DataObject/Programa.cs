using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class Programa
    {
        public string Nivel { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Duracion { get; set; }
        public string Horario { get; set; }
        public string Modalidad { get; set; }
        public string RequisitosEspecialidad { get; set; }
        public string RequisitosTitulacion { get; set; }
        public string RequisitosDiplomado { get; set; }
        public string Objetivo { get; set; }
        public string PerfilIngreso { get; set; }
        public string MapaCurricular { get; set; }
        public string PerfilEgreso { get; set; }
        public string ProcesoSeleccion { get; set; }
        public decimal CostoInscripcionSemestral { get; set; }
        public decimal CostoMensualidad { get; set; }
        public decimal CostoCursoPropedeutico { get; set; }
        public bool Activo { get; set; }
    }
}
