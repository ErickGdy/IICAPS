using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class DocumentosInscripcion
    {
        public string Alumno { get; set; }
        public bool ActaNacimientoOrg { get; set; }
        public bool ActaNacimientoCop { get; set; }
        public bool TituloCedulaOrg { get; set; }
        public bool TituloCedulaCop { get; set; }
        public bool TituloLicCop { get; set; }
        public bool CedProfCop { get; set; }
        public bool SolicitudOpcTitulacion { get; set; }
        public bool CertificadoLicCop { get; set; }
        public bool ConstanciaLibSSOrg { get; set; }
        public bool ConstanciaLibSSCop { get; set; }
        public bool Curp { get; set; }
        public bool Fotografias { get; set; }
        public string RecibioEmpleado { get; set; }
        public int TipoInscripcion { get; set; }


        public bool Validar_documentacion()
        {
            if (ActaNacimientoCop && ActaNacimientoOrg && Curp && Fotografias)
            {
                if (TituloCedulaCop && TituloCedulaOrg && TituloLicCop && CedProfCop)
                {
                    return true;
                }
                else if (SolicitudOpcTitulacion && ConstanciaLibSSOrg && CertificadoLicCop)
                {
                    return true;
                }
            }
            return false;
                
        }
    }
}
