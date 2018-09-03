using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IICAPS_v1.Control;
using IICAPS_v1.DataObject;
using IICAPS_v1.Presentacion;
using MySql.Data.MySqlClient;

namespace IICAPS_v1.Presentacion.Mains.Escuela
{
    public partial class DetallePagosAlumno : Form
    {

        private static DetallePagosAlumno instance;
        ControlIicaps control;
        string alumno="", concepto="";
        double auxTotal = 0, auxPendiente = 0;
        DataGridViewPrinter MyDataGridViewPrinter;
        public DetallePagosAlumno(string rfc, string con)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            alumno = rfc;
            concepto = con;
            lblNombreAlumno.Text = control.obtenerNombreAlumno(alumno);
            this.Text = lblNombreAlumno.Text;
            try
            {
                actualizarTabla(control.obtenerPagosAlumnoTable(alumno, concepto));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void actualizarTabla(MySqlDataAdapter data)
        {
            try
            {
                double auxTotal = 0, auxPendiente = 0;
                DataTable dtDatos = new DataTable();
                //Con la informacion del adaptador se llena el datatable
                data.Fill(dtDatos);
                //Se asigna el datatable como origen de datos del datagridview
                dataGridViewPagos.DataSource = dtDatos;
                //Actualiza el valor del ancho de la columnas
                int x = (dataGridViewPagos.Width - 20) / dataGridViewPagos.Columns.Count;
                foreach (DataGridViewColumn aux in dataGridViewPagos.Columns)
                {
                    aux.Width = x;
                }
                for (int i = 0; i < dataGridViewPagos.Rows.Count; i++)
                {
                    auxTotal += Convert.ToDouble(dataGridViewPagos.Rows[i].Cells[3].Value.ToString());
                }
                List<Materia> materias = new List<Materia>(); 
                materias = control.consultarMapaCurricularPrograma(control.obtenerProgramaAlumno(alumno));
                double costoCredito = 0;
                Programa programa = control.consultarPrograma(control.obtenerProgramaAlumno(alumno));
                if (programa.Nivel.Contains("Maestria") || programa.Nivel.Contains("MAESTRIA") || programa.Nivel.Contains("Maestría") || programa.Nivel.Contains("MAESTRÍA"))
                    costoCredito = 5000;
                else
                    costoCredito = 4000;
                if (materias != null)
                {
                    foreach (Materia m in materias)
                    {
                        auxPendiente += Convert.ToDouble(m.costo);
                    }
                }
                auxPendiente = auxPendiente - auxTotal + costoCredito;
                if (auxPendiente >= 0)
                {
                    lblTotalPagado.Text = "$" + auxTotal.ToString();
                }
                else
                {
                    auxTotal = 0;
                    lblTotalPagado.Text = "$" + auxTotal.ToString();
                }
                
                if (auxPendiente >= 0)
                {
                    lblPendiente.Text = "$" + auxPendiente.ToString();
                }
                else
                {
                    auxPendiente = 0;
                    lblPendiente.Text = "$" + auxPendiente.ToString();
                }
                this.auxPendiente = auxPendiente;
                this.auxTotal = auxTotal;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Pago pago = new Pago();
            pago.alumnoID = alumno;
            FormRegistrarPago fa = new FormRegistrarPago(pago, false);
            fa.FormClosed += new FormClosedEventHandler(form_Closed);
            fa.ShowDialog();
        }
        private void form_Closed(object sender, FormClosedEventArgs e)
        {
            actualizarTabla(control.obtenerPagosAlumnoTable(alumno, concepto));
        }
        private void quitarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String id = dataGridViewPagos.CurrentRow.Cells[0].Value.ToString();
                Pago pago = control.consultarPago(id);
                FormRegistrarPago fa = new FormRegistrarPago(pago, true);
                fa.FormClosed += new FormClosedEventHandler(form_Closed);
                fa.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void limpiarBusqueda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            limpiarBusqueda.Visible = false;
            txtBuscar.Text = "";
            try
            {
                actualizarTabla(control.obtenerPagosAlumnoTable(alumno, concepto));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarTabla(control.obtenerPagosAlumnoTable(alumno, concepto));
        }

        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String id = dataGridViewPagos.CurrentRow.Cells[0].Value.ToString();
                DialogResult dialogresult = MessageBox.Show("¿Desea cancelar el pago?", "Cancelar Pago", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogresult == DialogResult.OK)
                {
                    if (control.cancelarPago(id))
                    {
                        MessageBox.Show("Pago cancelado");
                        actualizarTabla(control.obtenerPagosAlumnoTable(alumno, concepto));
                    }
                    else
                        MessageBox.Show("Error al cancelar el pago");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            int ancho = this.Width;
            //Actualiza el tamaño de la tabla con respecto al tamaño de la ventana
            dataGridViewPagos.Width = ancho - 50;
            dataGridViewPagos.Height = this.Height - 155;
            //actualiza la posicion de los controles con respecto al tamaño de la ventana
            btnAgregarPago.Location = new Point (ancho - 195, btnAgregarPago.Location.Y);
            txtBuscar.Location = new Point (ancho - 230, txtBuscar.Location.Y);
            pictureBoxBuscar.Location = new Point (ancho - 260, pictureBoxBuscar.Location.Y);
            limpiarBusqueda.Location = new Point (ancho - 55, limpiarBusqueda.Location.Y);
            //Actualiza el valor del ancho de la columnas
            if (dataGridViewPagos.Columns.Count != 0)
            {
                int x = (dataGridViewPagos.Width - 20) / dataGridViewPagos.Columns.Count;
                foreach (DataGridViewColumn aux in dataGridViewPagos.Columns)
                {
                    aux.Width = x;
                }
            }
        }
        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            string texto = txtBuscar.Text;
            if (texto != "")
            {
                limpiarBusqueda.Visible = true;
                if (texto.Length > 2)
                {
                    try
                    {
                        actualizarTabla(control.obtenerPagosAlumnoTable(alumno, concepto));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                limpiarBusqueda.Visible = false;
                try
                {
                    actualizarTabla(control.obtenerPagosAlumnoTable(alumno, concepto));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (SetupThePrinting())
                {
                    PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
                    MyPrintPreviewDialog.Document = MyPrintDocument;
                    MyPrintPreviewDialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar documento de impresion");
            }
        }

        private void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }

        private bool SetupThePrinting()
        {
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;

            //if (MyPrintDialog.ShowDialog() != DialogResult.OK)
            //    return false;

            MyPrintDocument.DocumentName = "HistorialPagos"+alumno;
            MyPrintDocument.PrinterSettings = MyPrintDialog.PrinterSettings;
            MyPrintDocument.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            MyPrintDocument.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);
            MyPrintDocument.DefaultPageSettings.Landscape = true;

            MyDataGridViewPrinter = new DataGridViewPrinter(dataGridViewPagos, MyPrintDocument, false, true, "Alumno: " + lblNombreAlumno .Text + "\n Concepto: " + concepto, new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true, false);

            return true;
        }
    }
}
