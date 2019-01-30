using IICAPS_v1.Control;
using IICAPS_v1.DataObject;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IICAPS_v1.Presentacion.Mains.Escuela
{
    public partial class MainPagos : Form
    {
        private static MainPagos instance;
        ControlIicaps control;
        PagoAlumno pago;
        public MainPagos()
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            try
            {
                actualizarTablaPagos(control.obtenerPagosAlumnosTable());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static MainPagos getInstance()
        {
            if (instance == null)
                instance = new MainPagos();
            return instance;
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
                //Actualiza el valor de la etiqueta donde se muestra el total de productos
                if (dataGridViewPagos.Columns.Count != 0)
                {
                    int x = (dataGridViewPagos.Width - 20) / (dataGridViewPagos.Columns.Count-1);
                    foreach (DataGridViewColumn aux in dataGridViewPagos.Columns)
                    {
                        aux.Width = x;
                    }
                dataGridViewPagos.Columns[0].Visible = false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FormRegistrarPago fa = new FormRegistrarPago(null, false);
            fa.FormClosed += new FormClosedEventHandler(form_Closed);
            fa.Show();
        }

        private void form_Closed(object sender, FormClosedEventArgs e)
        {
            actualizarTablaPagos(control.obtenerPagosAlumnosTable(txtBuscarCredito.Text));
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            consultarPago();
        }

        private void consultarPago()
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

        private void MainPagos_SizeChanged(object sender, EventArgs e)
        {
            int ancho = this.Width;
            //Actualiza el tamaño de la tabla con respecto al tamaño de la ventana
            dataGridViewPagos.Width = ancho - 195;
            dataGridViewPagos.Height = this.Height - 130;
            //actualiza la posicion de los controles con respecto al tamaño de la ventana
            btnAgregar.Location = new Point(ancho - 169, btnAgregar.Location.Y);
            txtBuscarCredito.Location = new Point(ancho - 216, txtBuscarCredito.Location.Y);
            pictureBox2.Location = new Point(ancho - 245, pictureBox2.Location.Y);
            limpiarBusqueda.Location = new Point(ancho - 39, limpiarBusqueda.Location.Y);
            //Actualiza el valor del ancho de la columnas
            if (dataGridViewPagos.Columns.Count != 0)
            {
                int x = (dataGridViewPagos.Width - 20) / (dataGridViewPagos.Columns.Count-1);
                foreach (DataGridViewColumn aux in dataGridViewPagos.Columns)
                {
                    aux.Width = x;
                }
                dataGridViewPagos.Columns[0].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            actualizarTablaPagos(control.obtenerPagosAlumnosTable(txtBuscarCredito.Text));
        }

        private void imprimirReciboToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String id = dataGridViewPagos.CurrentRow.Cells[0].Value.ToString();
            pago = control.consultarPagoAlumno(Convert.ToInt32(id));
            Thread t = new Thread(new ThreadStart(ThreadMethodDocumentos));
            t.Start();
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
                        actualizarTablaPagos(control.obtenerPagosAlumnosTable());
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
        private void ThreadMethodDocumentos()
        {
            DocumentosWord word = new DocumentosWord(pago);
        }
    }
}
