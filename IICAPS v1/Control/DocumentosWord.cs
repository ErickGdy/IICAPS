using IICAPS_v1.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using System.Windows.Forms;

namespace IICAPS_v1.Control
{
    class DocumentosWord
    {
        ControlIicaps control;
        string imgHeader1 = "C:\\logoaltacalidad.jpg";
        string imgFooter1 = "C:\\piealtacalidad.jpg";
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
                HeaderFooter header = section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary];
                header.Range.ParagraphFormat.SpaceAfter = 0;
                header.Shapes.AddPicture(imgHeader1);
            }
            //Agregar pie de pagina
            foreach (Microsoft.Office.Interop.Word.Section wordSection in documento.Sections)
            {
                //Pie de pagina y configuracion
                Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                HeaderFooter footer = wordSection.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary];
                footer.Range.ParagraphFormat.SpaceAfter = 0;
                footer.Shapes.AddPicture(imgFooter1);
            }
            //Agregar parrafo de texto con estilo de titulo 1
            Microsoft.Office.Interop.Word.Paragraph parra1 = documento.Content.Paragraphs.Add(ref missing);
            object styleHeading1 = "Título 1";
            parra1.Range.set_Style(ref styleHeading1);
            parra1.Range.Text = "Recibo de documentos para: " + control.obtenerProgramaAlumno(doc.alumno);
            parra1.Range.InsertParagraphAfter();
            //Parrafos restantes del documento
            documento.Content.SetRange(0, 0);
            Microsoft.Office.Interop.Word.Paragraph parra2 = documento.Content.Paragraphs.Add(ref missing);
            object style1 = "Normal";
            parra2.Range.set_Style(ref style1);
            //parra1.Range.Text = "Recibo de documentos para: " + control.obtenerProgramaAlumno(doc.alumno);
            parra2.Range.Text = "Se han recibido los siguientes documentos del alumno: " + doc.alumno + Environment.NewLine;
            parra2.Range.InsertParagraphAfter();
            if (doc.actaNacimientoCop)
            {
                word.Selection.TypeText("Copia del acta de Nacimiento - Recibido" + Environment.NewLine);
            }
            if (doc.actaNacimientoOrg)
            {
                word.Selection.TypeText("Acta de Nacimiento - Recibido" + Environment.NewLine);
            }
            if (doc.tituloCedulaOrg)
            {
                word.Selection.TypeText("Título Licenciatura y Cedula Profesional - Recibido" + Environment.NewLine);
            }
            if (doc.tituloLicCop)
            {
                word.Selection.TypeText("Copia del Título de Licenciatura - Recibido" + Environment.NewLine);
            }
            if (doc.cedProfCop)
            {
                word.Selection.TypeText("Copia de la Cedula Profesional - Recibido" + Environment.NewLine);
            }
            if (doc.solicitudOpcTitulacion)
            {
                word.Selection.TypeText("Solicitud como opción de titulación - Recibido" + Environment.NewLine);
            }
            if (doc.certificadoLicCop)
            {
                word.Selection.TypeText("Copia del Certificado de Licenciatura - Recibido" + Environment.NewLine);
            }
            if (doc.constanciaLibSSOrg)
            {
                word.Selection.TypeText("Constancia de Liberación del Servicio Social - Recibido" + Environment.NewLine);
            }
            if (doc.curp)
            {
                word.Selection.TypeText("CURP - Recibido" + Environment.NewLine);
            }
            if (doc.fotografias)
            {
                word.Selection.TypeText("Fotografías - Recibido" + Environment.NewLine);
            }
            word.Selection.TypeText("Recibió: " + Environment.NewLine);
            word.Selection.TypeText(doc.recibioEmpleado);
            //Hacemos visible el documento
            word.Visible = true;
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
