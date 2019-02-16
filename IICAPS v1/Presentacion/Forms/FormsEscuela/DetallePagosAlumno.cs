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

        ControlIicaps control;
        Alumno alumno;
        decimal auxTotal = 0, auxPendiente = 0;
        DataGridViewPrinter MyDataGridViewPrinter;
        public DetallePagosAlumno(Alumno al)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            alumno = al;
            lblNombreAlumno.Text = alumno.nombre;
            this.Text = lblNombreAlumno.Text;
            try
            {
                actualizarTablaPagos(control.obtenerPagosDeAlumnoTable(alumno.rfc));
                actualizarTablaCobros(control.obtenerCobrosDeAlumnoTable(alumno.rfc));
                calcularMontos();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
         private void calcularMontos(){
            decimal auxTotal = 0, auxPendiente = 0;
            for (int i = 0; i < dataGridViewCobros.Rows.Count; i++)
            {
                auxPendiente += Convert.ToDecimal(dataGridViewCobros.Rows[i].Cells[4].Value.ToString());
            }
            for (int i = 0; i < dataGridViewPagos.Rows.Count; i++)
            {
                auxTotal += Convert.ToDecimal(dataGridViewPagos.Rows[i].Cells[1].Value.ToString());
            }
            lblTotalPagado.Text = auxTotal.ToString();
            lblPendiente.Text = auxPendiente.ToString();

            this.auxPendiente = auxPendiente;
            this.auxTotal = auxTotal;
        }
        private void actualizarTablaCobros(MySqlDataAdapter data)
        {
            try
            {
                DataTable dtDatos = new DataTable();
                //Con la informacion del adaptador se llena el datatable
                data.Fill(dtDatos);
                //Se asigna el datatable como origen de datos del datagridview
                dataGridViewCobros.DataSource = dtDatos;
                //Actualiza el valor del ancho de la columnas
                int x = (dataGridViewCobros.Width - 20) / (dataGridViewCobros.Columns.Count-1);
                foreach (DataGridViewColumn aux in dataGridViewCobros.Columns)
                {
                    aux.Width = x;
                }
                dataGridViewCobros.Columns[0].Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void actualizarTablaPagos(MySqlDataAdapter data)
        {
            try
            {
                
                DataTable dtDatos = new DataTable();
                //Con la informacion del adaptador se llena el datatable
                data.Fill(dtDatos);
                //Se asigna el datatable como origen de datos del datagridview
                dataGridViewPagos.DataSource = dtDatos;
                //Actualiza el valor del ancho de la columnas
                int x = (dataGridViewPagos.Width - 20) / (dataGridViewPagos.Columns.Count - 1);
                foreach (DataGridViewColumn aux in dataGridViewPagos.Columns)
                {
                    aux.Width = x;
                }
                dataGridViewPagos.Columns[0].Visible = false;                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            PagoAlumno pago = new PagoAlumno();
            pago.alumnoID = alumno.rfc;
            pago.cantidad = Convert.ToDouble(lblPendiente.Text);
            pago.concepto = "Pago de Adeudo General";
            FormRegistrarPago fa = new FormRegistrarPago(pago, false);
            fa.FormClosed += new FormClosedEventHandler(form_Closed);
            fa.ShowDialog();
        }
        private void form_Closed(object sender, FormClosedEventArgs e)
        {
            actualizarTablaPagos(control.obtenerPagosDeAlumnoTable(alumno.rfc));
            actualizarTablaCobros(control.obtenerCobrosDeAlumnoTable(alumno.rfc));
            calcularMontos();
        }
        private void quitarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String id = dataGridViewPagos.CurrentRow.Cells[0].Value.ToString();
                PagoAlumno pago = control.consultarPagoAlumno(Convert.ToInt32(id));
                FormRegistrarPago fa = new FormRegistrarPago(pago, true);
                fa.FormClosed += new FormClosedEventHandler(form_Closed);
                fa.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarTablaPagos(control.obtenerPagosDeAlumnoTable(alumno.rfc));
        }

        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String id = dataGridViewPagos.CurrentRow.Cells[0].Value.ToString();
                DialogResult dialogresult = MessageBox.Show("¿Desea cancelar el pago?", "Cancelar Pago", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogresult == DialogResult.OK)
                {
                    if (control.cancelarPagoAlumno(id))
                    {
                        MessageBox.Show("Pago cancelado");
                        actualizarTablaPagos(control.obtenerPagosDeAlumnoTable(alumno.rfc));
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
            dataGridViewCobros.Width = ancho - 50;
            dataGridViewPagos.Height = (this.Height - 250)/2;
            dataGridViewCobros.Height = (this.Height - 250)/2;
            //actualiza la posicion de los controles con respecto al tamaño de la ventana
            btnAgregarPago.Location = new Point (ancho - 195, btnAgregarPago.Location.Y);
            //Actualiza el valor del ancho de la columnas
            if (dataGridViewPagos.Columns.Count != 0)
            {
                int x = (dataGridViewPagos.Width - 20) / (dataGridViewPagos.Columns.Count-1);
                foreach (DataGridViewColumn aux in dataGridViewPagos.Columns)
                {
                    aux.Width = x;
                }
            }
            if (dataGridViewCobros.Columns.Count != 0)
            {
                int x = (dataGridViewCobros.Width - 20) / (dataGridViewCobros.Columns.Count-1);
                foreach (DataGridViewColumn aux in dataGridViewCobros.Columns)
                {
                    aux.Width = x;
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

        private void btnActualizarCobros_Click(object sender, EventArgs e)
        {
            actualizarTablaCobros(control.obtenerCobrosDeAlumnoTable(alumno.rfc));
        }

        private void lblTotalPendiente_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(lblPendiente.Text) <= 0)
                btnAgregarPago.Enabled = false;
            else
                btnAgregarPago.Enabled = true;
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

            MyDataGridViewPrinter = new DataGridViewPrinter(dataGridViewPagos, MyPrintDocument, false, "Alumno: " + lblNombreAlumno.Text + "\n "  , new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Point), Color.Black, "pie de página", new Font("Arial", 8, FontStyle.Bold, GraphicsUnit.Point), Color.Black, false);

            return true;
        }
    }
}
