using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class DocumentosInscripcion
    {
        public string alumno { get; set; }
        public bool actaNacimientoOrg { get; set; }
        public bool actaNacimientoCop { get; set; }
        public bool tituloCedulaOrg { get; set; }
        public bool tituloCedulaCop { get; set; }
        public bool tituloLicCop { get; set; }
        public bool cedProfCop { get; set; }
        public bool solicitudOpcTitulacion { get; set; }
        public bool certificadoLicCop { get; set; }
        public bool constanciaLibSSOrg { get; set; }
        public bool constanciaLibSSCop { get; set; }
        public bool curp { get; set; }
        public bool fotografias { get; set; }
        public string recibioEmpleado { get; set; }
        public int tipoInscripcion { get; set; }


        public bool validarDocumentacion()
        {
            if (actaNacimientoCop && actaNacimientoOrg && curp && fotografias)
            {
                if (tituloCedulaCop && tituloCedulaOrg && tituloLicCop && cedProfCop)
                {
                    return true;
                }
                else if (solicitudOpcTitulacion && constanciaLibSSOrg && certificadoLicCop)
                {
                    return true;
                }
            }
            return false;
                
        }
    }
}
