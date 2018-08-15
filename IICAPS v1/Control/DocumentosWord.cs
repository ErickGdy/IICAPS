using IICAPS_v1.DataObject;
using Microsoft.Office.Interop.Word;
using System;
using System.Windows.Forms;
using word = Microsoft.Office.Interop.Word;

namespace IICAPS_v1.Control
{
    class DocumentosWord
    {
        ControlIicaps control;
        string imgHeader1 = "C:\\SistemaIICAPS\\imagenes\\logoaltacalidad.jpg";
        string imgFooter1 = "C:\\SistemaIICAPS\\imagenes\\piealtacalidad.jpg";
        public DocumentosWord(DocumentosInscripcion doc)
        {
            control = ControlIicaps.getInstance();
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
            //Guardamos el documento
            object filename = @"C:\\SistemaIICAPS\\Documentos\\ReciboDocumentos" + doc.alumno;
            documento.SaveAs2(ref filename);
            //documento.Close(ref missing, ref missing, ref missing);
            //documento = null;
            //word.Quit(ref missing, ref missing, ref missing);
            //word = null;
            MessageBox.Show("¡Recibo de documentos creado exitosamente!");
        }


        public DocumentosWord(Pago pago)
        {
            control = ControlIicaps.getInstance();
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
            //Hacemos visible el documento
            word.Visible = true;
            //Guardamos el documento
            object filename = @"C:\\SistemaIICAPS\\Documentos\\ReciboDePago" + pago.alumnoID;
            documento.SaveAs2(ref filename);
            //documento.Close(ref missing, ref missing, ref missing);
            //documento = null;
            //word.Quit(ref missing, ref missing, ref missing);
            //word = null;
            MessageBox.Show("¡Recibo de documentos creado exitosamente!");
        }
    }
}
