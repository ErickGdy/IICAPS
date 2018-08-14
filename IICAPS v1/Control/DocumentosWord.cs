using IICAPS_v1.DataObject;
using System;
using System.Windows.Forms;
using word = Microsoft.Office.Interop.Word;

namespace IICAPS_v1.Control
{
    class DocumentosWord
    {
        ControlIicaps control;
        public DocumentosWord (DocumentosInscripcion doc)
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            //Quitar animacion y visibilidad mientras se crea y edita
            word.ShowAnimation = false;
            word.Visible = false;
            //Missing para rellenar parametros de creacion
            object missing = System.Reflection.Missing.Value;
            //Creacion del documento
            Microsoft.Office.Interop.Word.Document documento = word.Documents.Add(ref missing, ref missing, ref missing);
            //Agregar encabezado
            foreach (Microsoft.Office.Interop.Word.Section section in documento.Sections)
            {
                //Encabezado y configuracion
                Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                headerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdRed;
                headerRange.Font.Size = 12;
                headerRange.Font.Name = "Arial";
                headerRange.Text = "Instituto de Investigación, Capacitación y Psicoterapia";
            }
            //Agregar pie de pagina
            foreach (Microsoft.Office.Interop.Word.Section wordSection in documento.Sections)
            {
                //Pie de pagina y configuracion
                Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                footerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkRed;
                footerRange.Font.Size = 11;
                footerRange.Font.Name = "Arial";
                footerRange.Text = DateTime.Now.ToShortDateString();
            }
            //Agregar parrafo de texto con estilo de titulo 1
            Microsoft.Office.Interop.Word.Paragraph parra1 = documento.Content.Paragraphs.Add(ref missing);
            object styleHeading1 = "Título 1";
            parra1.Range.set_Style(ref styleHeading1);
            //parra1.Range.Text = "Recibo de documentos para: " + control.obtenerProgramaAlumno(doc.alumno);
            parra1.Range.InsertParagraphAfter();
            //Parrafos restantes del documento
            //documento.Content.SetRange(0, 0);
            object style1 = "Normal";
            parra1.Range.set_Style(ref style1);
            //parra1.Range.Text = "Recibo de documentos para: " + control.obtenerProgramaAlumno(doc.alumno);
            parra1.Range.InsertParagraphAfter();
            word.Selection.TypeText("Se han recibido los siguientes documentos del alumno: " + doc.alumno + Environment.NewLine);
            if (doc.actaNacimientoCop)
            {
                word.Selection.TypeText("Copia del acta de Nacimiento - Entregado" + Environment.NewLine);
            }
            if (doc.actaNacimientoOrg)
            {
                word.Selection.TypeText("Acta de Nacimiento - Entregado" + Environment.NewLine);
            }
            if (doc.tituloCedulaOrg)
            {
                word.Selection.TypeText("Título Licenciatura y Cedula Profesional - Entregado" + Environment.NewLine);
            }
            if (doc.tituloLicCop)
            {
                word.Selection.TypeText("Copia del Título de Licenciatura - Entregado" + Environment.NewLine);
            }
            if (doc.cedProfCop)
            {
                word.Selection.TypeText("Copia de la Cedula Profesional - Entregado" + Environment.NewLine);
            }
            if (doc.solicitudOpcTitulacion)
            {
                word.Selection.TypeText("Solicitud como opción de titulación - Entregado" + Environment.NewLine);
            }
            if (doc.certificadoLicCop)
            {
                word.Selection.TypeText("Copia del Certificado de Licenciatura - Entregado" + Environment.NewLine);
            }
            if (doc.constanciaLibSSOrg)
            {
                word.Selection.TypeText("Constancia de Liberación del Servicio Social - Entregado" + Environment.NewLine);
            }
            if (doc.curp)
            {
                word.Selection.TypeText("CURP - Entregado" + Environment.NewLine);
            }
            if (doc.fotografias)
            {
                word.Selection.TypeText("Fotografías - Entregado" + Environment.NewLine);
            }
            word.Selection.TypeText("Recibió: " + Environment.NewLine);
            word.Selection.TypeText(doc.recibioEmpleado);
            //Hacemos visible el documento
            word.Visible = true;
            //Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            ////Objeto del Tipo Word Application 
            //word.Application objWordApplication;
            ////Objeto del Tipo Word Document 
            //word.Document objWordDocument;
            //// Objeto para interactuar con el Interop 
            //Object oMissing = System.Reflection.Missing.Value;
            ////Creamos una instancia de una Aplicación Word. 
            //objWordApplication = new word.Application();
            ////A la aplicación Word, le añadimos un documento. 
            //objWordDocument = objWordApplication.Documents.Add(ref oMissing, ref oMissing,
            //                                                 ref oMissing, ref oMissing);
            ////Activamos el documento recien creado, de forma que podamos escribir en el 
            //objWordDocument.Activate();
            ////Empezamos a escribir
            //objWordApplication.Selection.Font.Size = 16; //Tamaño de la Fuente 
            //objWordApplication.Selection.Font.Bold = 1; // Negrita 
            ////objWordApplication.Selection.TypeText("Recibo de documentos para " +control.obtenerProgramaAlumno(doc.alumno));
            //objWordApplication.Selection.Font.Size = 12; //Tamaño de la Fuente 
            //objWordApplication.Selection.Font.Bold = 1; // Negrita 
            //objWordApplication.Selection.TypeText("Se han recibido los siguientes documentos del alumno: ");
            //objWordApplication.Selection.Font.Size = 12; //Tamaño de la Fuente 
            //objWordApplication.Selection.Font.Bold = 0; // Negrita 
            //objWordApplication.Selection.TypeText(doc.alumno);
            //objWordApplication.Selection.TypeText("Fecha: ");
            //objWordApplication.Selection.TypeText(DateTime.Now.ToShortDateString());
            //objWordApplication.Selection.TypeText("Documentos recibidos: ");

            ////Hace visible la Aplicacion para que veas lo que se ha escrito 
            //objWordApplication.Visible = true;
        }

        public void wordDocumentosInscripcion(DocumentosInscripcion doc)
        {
            try
            {
                //Instancia de word
                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                //Quitar animacion y visibilidad mientras se crea y edita
                word.ShowAnimation = false;
                word.Visible = false;
                //Missing para rellenar parametros de creacion
                object missing = System.Reflection.Missing.Value;
                //Creacion del documento
                Microsoft.Office.Interop.Word.Document documento = word.Documents.Add(ref missing, ref missing, ref missing);
                //Agregar encabezado
                foreach (Microsoft.Office.Interop.Word.Section section in documento.Sections)
                {
                    //Encabezado y configuracion
                    Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                    headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    headerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdRed;
                    headerRange.Font.Size = 12;
                    headerRange.Font.Name = "Arial";
                    headerRange.Text = "Instituo de Investigación, Capacitación y Psicoterapia";
                }
                //Agregar pie de pagina
                foreach (Microsoft.Office.Interop.Word.Section wordSection in documento.Sections)
                {
                    //Pie de pagina y configuracion
                    Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    footerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkRed;
                    footerRange.Font.Size = 11;
                    footerRange.Font.Name = "Arial";
                    footerRange.Text = DateTime.Now.ToShortDateString();
                }
                //Agregar parrafo de texto con estilo de titulo 1
                Microsoft.Office.Interop.Word.Paragraph parra1 = documento.Content.Paragraphs.Add(ref missing);
                object styleHeading1 = "Heading 1";
                parra1.Range.set_Style(ref styleHeading1);
                parra1.Range.Text = "Recibo de documentos para: " + control.obtenerProgramaAlumno(doc.alumno);
                parra1.Range.InsertParagraphAfter();
                //Parrafos restantes del documento
                documento.Content.SetRange(0, 0);
                documento.Content.Text = "Se han recibido los siguientes documentos del alumno: " + Environment.NewLine;
                if (doc.actaNacimientoCop)
                {
                    documento.Content.Text = "Copia del acta de Nacimiento - Entregado" + Environment.NewLine;
                }
                if (doc.actaNacimientoOrg)
                {
                    documento.Content.Text = "Acta de Nacimiento - Entregado" + Environment.NewLine;
                }
                if (doc.tituloCedulaOrg)
                {
                    documento.Content.Text = "Titulo Licenciatura y Cedula Profesional - Entregado" + Environment.NewLine;
                }
                if (doc.tituloLicCop)
                {
                    documento.Content.Text = "Copia del Titulo de Licenciatura - Entregado" + Environment.NewLine;
                }
                if (doc.cedProfCop)
                {
                    documento.Content.Text = "Copia de la Cedula Profesional - Entregado" + Environment.NewLine;
                }
                if (doc.solicitudOpcTitulacion)
                {
                    documento.Content.Text = "Solicitud como opcion de titulacion - Entregado" + Environment.NewLine;
                }
                if (doc.certificadoLicCop)
                {
                    documento.Content.Text = "Copia del Certificado de Licenciatura - Entregado" + Environment.NewLine;
                }
                if (doc.constanciaLibSSOrg)
                {
                    documento.Content.Text = "Constancia de Liberacion del Servicio Social - Entregado" + Environment.NewLine;
                }
                if (doc.curp)
                {
                    documento.Content.Text = "CURP - Entregado" + Environment.NewLine;
                }
                if (doc.fotografias)
                {
                    documento.Content.Text = "Fotografias - Entregado" + Environment.NewLine;
                }
                documento.Content.Text = "Recibio: " + Environment.NewLine;
                documento.Content.Text = doc.recibioEmpleado;
                //Hacemos visible el documento
                word.Visible = true;
                //Guardamos el documento
                //object filename = @"c:\users\danie\documents\temp1.docx";
                //documento.SaveAs2(ref filename);
                //documento.Close(ref missing, ref missing, ref missing);
                //documento = null;
                //word.Quit(ref missing, ref missing, ref missing);
                //word = null;
                //MessageBox.Show("¡Recibo de documentos creado exitosamente!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
