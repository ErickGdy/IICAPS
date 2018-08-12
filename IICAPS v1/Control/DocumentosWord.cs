using IICAPS_v1.DataObject;
using System;
using word = Microsoft.Office.Interop.Word;

namespace IICAPS_v1.Control
{
    class DocumentosWord
    {
        ControlIicaps control;
        public DocumentosWord (DocumentosInscripcion doc)
        {
            //Objeto del Tipo Word Application 
            word.Application objWordApplication;
            //Objeto del Tipo Word Document 
            word.Document objWordDocument;
            // Objeto para interactuar con el Interop 
            Object oMissing = System.Reflection.Missing.Value;
            //Creamos una instancia de una Aplicación Word. 
            objWordApplication = new word.Application();
            //A la aplicación Word, le añadimos un documento. 
            objWordDocument = objWordApplication.Documents.Add(ref oMissing, ref oMissing,
                                                             ref oMissing, ref oMissing);
            //Activamos el documento recien creado, de forma que podamos escribir en el 
            objWordDocument.Activate();
            //Empezamos a escribir
            objWordApplication.Selection.Font.Size = 16; //Tamaño de la Fuente 
            objWordApplication.Selection.Font.Bold = 1; // Negrita 
            objWordApplication.Selection.TypeText("Recibo de documentos para " +control.obtenerProgramaAlumno(doc.alumno));
            objWordApplication.Selection.Font.Size = 12; //Tamaño de la Fuente 
            objWordApplication.Selection.Font.Bold = 1; // Negrita 
            objWordApplication.Selection.TypeText("Se han recibido los siguientes documentos del alumno: ");
            objWordApplication.Selection.Font.Size = 12; //Tamaño de la Fuente 
            objWordApplication.Selection.Font.Bold = 0; // Negrita 
            objWordApplication.Selection.TypeText(doc.alumno);
            objWordApplication.Selection.TypeText("Fecha: ");
            objWordApplication.Selection.TypeText(DateTime.Now.ToShortDateString());
            objWordApplication.Selection.TypeText("Documentos recibidos: ");
            if (doc.actaNacimientoCop)
            {
                objWordApplication.Selection.TypeText("Copia del acta de Nacimiento");
            }
            if (doc.actaNacimientoOrg)
            {
                objWordApplication.Selection.TypeText("Acta de Nacimiento");
            }
            if (doc.tituloCedulaOrg)
            {
                objWordApplication.Selection.TypeText("Titulo Licenciatura y Cedula Profesional");
            }
            if (doc.tituloLicCop)
            {
                objWordApplication.Selection.TypeText("Copia del Titulo de Licenciatura");
            }
            if (doc.cedProfCop)
            {
                objWordApplication.Selection.TypeText("Copia de la Cedula Profesional");
            }
            if (doc.solicitudOpcTitulacion)
            {
                objWordApplication.Selection.TypeText("Solicitud como opcion de titulacion");
            }
            if (doc.certificadoLicCop)
            {
                objWordApplication.Selection.TypeText("Copia del Certificado de Licenciatura");
            }
            if (doc.constanciaLibSSOrg)
            {
                objWordApplication.Selection.TypeText("Constancia de Liberacion del Servicio Social");
            }
            if (doc.curp)
            {
                objWordApplication.Selection.TypeText("CURP");
            }
            if (doc.fotografias)
            {
                objWordApplication.Selection.TypeText("Fotografias");
            }
            objWordApplication.Selection.TypeText("Recibio: ");
            objWordApplication.Selection.TypeText(doc.recibioEmpleado);
            //Hace visible la Aplicacion para que veas lo que se ha escrito 
            objWordApplication.Visible = true;
        }
    }
}
