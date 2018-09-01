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
                footer.Range.ParagraphFormat.SpaceAfter = 50;
                footer.Shapes.AddPicture(imgFooter1);
            }
            //Agregar parrafo de texto con estilo de titulo 1
            Microsoft.Office.Interop.Word.Paragraph parra1 = documento.Content.Paragraphs.Add(ref missing);
            object styleHeading1 = "Título 1";
            parra1.Range.set_Style(ref styleHeading1);
            parra1.Range.Font.Bold = 1;
            parra1.Range.Font.Color = WdColor.wdColorDarkGreen;
            parra1.Range.Text = Environment.NewLine + Environment.NewLine  +"Recibo de documentos para: " + control.obtenerNombrePrograma(control.obtenerProgramaAlumno(doc.alumno));
            parra1.Range.InsertParagraphAfter();
            //Parrafos restantes del documento
            Microsoft.Office.Interop.Word.Paragraph parra2 = documento.Content.Paragraphs.Add(ref missing);
            object style1 = "Título 2";
            parra2.Range.set_Style(ref style1);
            parra2.Range.Font.Bold = 1;
            parra2.Range.Font.Color = WdColor.wdColorDarkRed;
            //parra1.Range.Text = "Recibo de documentos para: " + control.obtenerProgramaAlumno(doc.alumno);
            parra2.Range.Text = "Se han recibido los siguientes documentos del alumno: " + control.obtenerNombreAlumno(doc.alumno) + Environment.NewLine;
            //Damos formato a los parrafos siguientes dentro de los if
            object style2 = "Normal";
            if (doc.actaNacimientoCop)
            {
                Microsoft.Office.Interop.Word.Paragraph parra3 = documento.Content.Paragraphs.Add(ref missing);
                parra3.Range.set_Style(ref style2);
                parra3.Range.Font.Size = 12;
                parra3.Range.Text = "Copia del acta de Nacimiento - Recibido" + Environment.NewLine;
            }
            if (doc.actaNacimientoOrg)
            {
                Microsoft.Office.Interop.Word.Paragraph parra4 = documento.Content.Paragraphs.Add(ref missing);
                parra4.Range.set_Style(ref style2);
                parra4.Range.Font.Size = 12;
                parra4.Range.Text = "Acta de Nacimiento - Recibido" + Environment.NewLine;
            }
            if (doc.tituloCedulaOrg)
            {
                Microsoft.Office.Interop.Word.Paragraph parra5 = documento.Content.Paragraphs.Add(ref missing);
                parra5.Range.set_Style(ref style2);
                parra5.Range.Font.Size = 12;
                parra5.Range.Text = "Título Licenciatura y Cedula Profesional - Recibido" + Environment.NewLine;
            }
            if (doc.tituloLicCop)
            {
                Microsoft.Office.Interop.Word.Paragraph parra6 = documento.Content.Paragraphs.Add(ref missing);
                parra6.Range.set_Style(ref style2);
                parra6.Range.Font.Size = 12;
                parra6.Range.Text = "Copia del Título de Licenciatura - Recibido" + Environment.NewLine;
            }
            if (doc.cedProfCop)
            {
                Microsoft.Office.Interop.Word.Paragraph parra7 = documento.Content.Paragraphs.Add(ref missing);
                parra7.Range.set_Style(ref style2);
                parra7.Range.Font.Size = 12;
                parra7.Range.Text = "Copia de la Cedula Profesional - Recibido" + Environment.NewLine;
            }
            if (doc.solicitudOpcTitulacion)
            {
                Microsoft.Office.Interop.Word.Paragraph parra8 = documento.Content.Paragraphs.Add(ref missing);
                parra8.Range.set_Style(ref style2);
                parra8.Range.Font.Size = 12;
                parra8.Range.Text = "Solicitud como opción de titulación - Recibido" + Environment.NewLine;
            }
            if (doc.certificadoLicCop)
            {
                Microsoft.Office.Interop.Word.Paragraph parra9 = documento.Content.Paragraphs.Add(ref missing);
                parra9.Range.set_Style(ref style2);
                parra9.Range.Text = "Copia del Certificado de Licenciatura - Recibido" + Environment.NewLine;
            }
            if (doc.constanciaLibSSOrg)
            {
                Microsoft.Office.Interop.Word.Paragraph parra10 = documento.Content.Paragraphs.Add(ref missing);
                parra10.Range.set_Style(ref style2);
                parra10.Range.Font.Size = 12;
                parra10.Range.Text = "Constancia de Liberación del Servicio Social - Recibido" + Environment.NewLine;
            }
            if (doc.curp)
            {
                Microsoft.Office.Interop.Word.Paragraph parra11 = documento.Content.Paragraphs.Add(ref missing);
                parra11.Range.set_Style(ref style2);
                parra11.Range.Font.Size = 12;
                parra11.Range.Text = "CURP - Recibido" + Environment.NewLine;
            }
            if (doc.fotografias)
            {
                Microsoft.Office.Interop.Word.Paragraph parra12 = documento.Content.Paragraphs.Add(ref missing);
                parra12.Range.set_Style(ref style2);
                parra12.Range.Font.Size = 12;
                parra12.Range.Text = "Fotografías - Recibido" + Environment.NewLine;
            }
            Microsoft.Office.Interop.Word.Paragraph parra13 = documento.Content.Paragraphs.Add(ref missing);
            parra13.Range.set_Style(ref style2);
            parra13.Range.Font.Size = 12;
            parra13.Range.Text= Environment.NewLine + "Recibió: " + control.obtenerNombreEmpleado(doc.recibioEmpleado) + Environment.NewLine + "Firma: __________________________________";
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


        public DocumentosWord(PagoAlumno pago)
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
                footer.Range.ParagraphFormat.SpaceAfter = 50;
                footer.Shapes.AddPicture(imgFooter1);
            }
            //Agregar parrafo de texto con estilo de titulo 1
            Microsoft.Office.Interop.Word.Paragraph parra1 = documento.Content.Paragraphs.Add(ref missing);
            object styleHeading1 = "Título 1";
            parra1.Range.set_Style(ref styleHeading1);
            parra1.Range.Font.Color = WdColor.wdColorDarkGreen;
            parra1.Range.Text = Environment.NewLine + Environment.NewLine  +"Se recibió el pago de: " + control.obtenerNombreAlumno(pago.alumnoID);
            parra1.Range.InsertParagraphAfter();
            //Parrafos restantes del documento
            documento.Content.SetRange(0, 0);
            Microsoft.Office.Interop.Word.Paragraph parra2 = documento.Content.Paragraphs.Add(ref missing);
            object style1 = "Normal";
            parra2.Range.set_Style(ref style1);
            parra2.Range.Font.Size = 12;
            //parra1.Range.Text = "Recibo de documentos para: " + control.obtenerProgramaAlumno(doc.alumno);
            parra2.Range.Text = "La cantidad de: $" + pago.cantidad + Environment.NewLine + 
                "Por concepto de: " +pago.concepto + Environment.NewLine + 
                "Fecha: "+pago.fechaPago+ Environment.NewLine + Environment.NewLine + 
                "Recibió: "+ control.obtenerNombreEmpleado(pago.recibio) + Environment.NewLine + "Firma: ___________________________________________";
            parra2.Range.InsertParagraphAfter();
            //Hacemos visible el documento
            word.Visible = true;
            //Guardamos el documento
            object filename = @"C:\\SistemaIICAPS\\Documentos\\ReciboDePago" + pago.alumnoID;
            documento.SaveAs2(ref filename);
            //documento.Close(ref missing, ref missing, ref missing);
            //documento = null;
            //word.Quit(ref missing, ref missing, ref missing);
            //word = null;
            MessageBox.Show("¡Recibo de pago creado exitosamente!");
        }

        public DocumentosWord(CreditoAlumno credito)
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
                footer.Range.ParagraphFormat.SpaceAfter = 50;
                footer.Shapes.AddPicture(imgFooter1);
            }
            //Agregar parrafo de texto con estilo de titulo 1
            Microsoft.Office.Interop.Word.Paragraph parra1 = documento.Content.Paragraphs.Add(ref missing);
            object styleHeading1 = "Título 1";
            parra1.Range.set_Style(ref styleHeading1);
            parra1.Range.Font.Color = WdColor.wdColorDarkGreen;
            parra1.Range.Font.Bold = 1;
            parra1.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter; 
            parra1.Range.Text = Environment.NewLine + Environment.NewLine  +"CONTRATO DE CRÉDITO EDUCATIVO";
            parra1.Range.InsertParagraphAfter();
            //Parrafos restantes del documento
            Microsoft.Office.Interop.Word.Paragraph parra2 = documento.Content.Paragraphs.Add(ref missing);
            object style1 = "Normal";
            parra2.Range.set_Style(ref style1);
            parra2.Range.Font.Size = 13;
            parra2.Format.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
            //parra1.Range.Text = "Recibo de documentos para: " + control.obtenerProgramaAlumno(doc.alumno);
            parra2.Range.Text = "Los Mochis Sinaloa, "+DateTime.Now.ToLongDateString();
            parra2.Range.InsertParagraphAfter();
            Microsoft.Office.Interop.Word.Paragraph parra3 = documento.Content.Paragraphs.Add(ref missing);
            parra3.Range.set_Style(ref style1);
            parra3.Range.Font.Size = 12;
            parra3.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            parra3.Range.Text = "Por medio de la presente autorizo la solicitud de crédito educativo a él(la) alumno(a) "+control.obtenerNombreAlumno(credito.alumno)+
                ", inscrita actualmente en el programa "+control.obtenerNombrePrograma(control.obtenerProgramaAlumno(credito.alumno))+".";
            parra3.Range.InsertParagraphAfter();
            Microsoft.Office.Interop.Word.Paragraph parra4 = documento.Content.Paragraphs.Add(ref missing);
            parra4.Range.set_Style(ref style1);
            parra4.Range.Font.Size = 12;
            parra4.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            parra4.Range.Text = "Dicho crédito consistirá en pagar una colegiatura de $" + credito.cantidadMensualidad +
                " ($" + credito.cantidadAbonoMensual + " colegiatura + $" + credito.cantidadAbonoCredito + " costo del crédito) durante "+credito.cantidadMeses+" meses.";
            parra4.Range.InsertParagraphAfter();
            Microsoft.Office.Interop.Word.Paragraph parra5 = documento.Content.Paragraphs.Add(ref missing);
            parra5.Range.set_Style(ref style1);
            parra5.Range.Font.Size = 12;
            parra5.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            parra5.Range.Text = "TERMINOS Y CONDICIONES." + Environment.NewLine +
                " Con base en el Art. 6  del reglamento de pagos del Instituto de Investigación, Capacitación y Psicoterapia " +
                "el alumno que cuenta con el crédito educativo pagará la cantidad que indique el documento: ´´CONTRATO DE CREDITO EDUCATIVO´´ " +
                "firmado por el Director de la institución y sellado." + Environment.NewLine+
                " El crédito necesita ser autorizado por el director de la institución. Este será vigente cuando el " +
                "cumplimiento del pago sea puntual, es decir, del día primero al último del mes que corresponde la mensualidad." + Environment.NewLine +
                "Si el tiempo de puntualidad del pago se ve vencido, el CREDITO EDUCATIVO quedará anulado, siendo necesario liquidar " +
                "la diferencia del total de las mensualidades que el alumno pagó con el crédito educativo para ingresar de nuevo a los " +
                "módulos programados del programa en el que está inscrito." + Environment.NewLine +
                "Cualquier prórroga deberá ser solicitada durante el mes vigente del pago en el departamento de " +
                "Administración Escolar del Instituto, y esta necesita ser solicitada y autorizada por escrito." + Environment.NewLine;
            parra5.Range.InsertParagraphAfter();
            //var ptable = documento.Paragraphs.Add();
            //ptable.Format.SpaceAfter = 10f;
            //Microsoft.Office.Interop.Word.Table table1 = documento.Tables.Add(parra5.Range, 3, 3, ref missing, ref missing);
            //int r, c;
            //for (r = 0; r < 3; r++)
            //{
            //    for (c = 0; c < 3; c++)
            //    {
            //        if (r == 0 && c == 0 || r == 0 && c == 2)
            //            table1.Cell(r, c).Range.Text = "__________________________________";
            //        if (r == 1 && c == 0)
            //            table1.Cell(r, c).Range.Text = "Jesús Mario Mancillas Peñuelas";
            //        if (r == 1 && c == 2)
            //            table1.Cell(r, c).Range.Text = control.obtenerNombreAlumno(credito.alumno);
            //        if (r == 2 && c == 0)
            //            table1.Cell(r, c).Range.Text = "Director";
            //        if (r == 1 && c == 2)
            //            table1.Cell(r, c).Range.Text = "Alumno(a)";
            //    }
            //}
            //table1.Columns[0].Width = word.InchesToPoints(2);
            //table1.Columns[1].Width = word.InchesToPoints(1);
            //table1.Columns[2].Width = word.InchesToPoints(2);
            //table1.AllowAutoFit = true;
            Microsoft.Office.Interop.Word.Paragraph parra6 = documento.Content.Paragraphs.Add(ref missing);
            parra6.Range.set_Style(ref style1);
            parra6.Range.Font.Size = 12;
            parra6.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            parra6.Range.Text = "______________________________           ______________________________" + Environment.NewLine +
                "Jesús Mario Mancillas Peñuelas           " + control.obtenerNombreAlumno(credito.alumno) + Environment.NewLine +
                "           Director                                 Alumno" + Environment.NewLine;
            parra6.Range.InsertParagraphAfter();
            //Hacemos visible el documento
            word.Visible = true;
            //Guardamos el documento
            object filename = @"C:\\SistemaIICAPS\\Documentos\\ContratoCreditoEducativo" + credito.alumno;
            documento.SaveAs2(ref filename);
            //documento.Close(ref missing, ref missing, ref missing);
            //documento = null;
            //word.Quit(ref missing, ref missing, ref missing);
            //word = null;
            MessageBox.Show("¡Contrato de crédito educativo creado exitosamente!");
        }
    }
}
