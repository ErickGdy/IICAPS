using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IICAPS_v1.Control;
using IICAPS_v1.DataObject;
using IICAPS_v1.Presentacion;
using MySql.Data.MySqlClient;
using System.Threading;

namespace IICAPS_v1.Presentacion.Mains.Escuela
{
    public partial class MainCreditos : Form
    {

        private static MainCreditos instance;
        ControlIicaps control;
        CreditoAlumno credito;
        public MainCreditos()
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            try
            {
                actualizarTablaCreditos(control.obtenerCreditoAlumnosTable());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static MainCreditos getInstance()
        {
            if (instance == null)
                instance = new MainCreditos();
            return instance;
        }

        private void actualizarTablaCreditos(MySqlDataAdapter data)
        {
            try
            {
                DataTable dtDatos = new DataTable();
                //Con la informacion del adaptador se llena el datatable
                data.Fill(dtDatos);
                //Se asigna el datatable como origen de datos del datagridview
                dataGridViewCreditos.DataSource = dtDatos;
                //Actualiza el valor de la etiqueta donde se muestra el total de productos
                if (dataGridViewCreditos.Columns.Count != 0)
                {
                    int x = (dataGridViewCreditos.Width - 20) / dataGridViewCreditos.Columns.Count;
                    foreach (DataGridViewColumn aux in dataGridViewCreditos.Columns)
                    {
                        aux.Width = x;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btnAgregarAlumno_Click(object sender, EventArgs e)
        {
            CreditoAlumnos fa = new CreditoAlumnos(null, false);
            fa.FormClosed += new FormClosedEventHandler(form_Closed);
            fa.Show();
        }

        private void form_Closed(object sender, FormClosedEventArgs e)
        {
            actualizarTablaCreditos(control.obtenerCreditoAlumnosTable(txtBuscarCredito.Text));
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            consultarCredito();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String rfc = dataGridViewCreditos.CurrentRow.Cells[1].Value.ToString();
                CreditoAlumno credito = control.consultarCreditoAlumno(rfc);
                CreditoAlumnos fa = new CreditoAlumnos(credito, false);
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
            txtBuscarCredito.Text = "";
            try
            {
                actualizarTablaCreditos(control.obtenerCreditoAlumnosTable());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void consultarCredito()
        {
            try
            {
                String rfc = dataGridViewCreditos.CurrentRow.Cells[1].Value.ToString();
                CreditoAlumno credito = control.consultarCreditoAlumno(rfc);
                CreditoAlumnos fa = new CreditoAlumnos(credito, true);
                fa.FormClosed += new FormClosedEventHandler(form_Closed);
                fa.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            actualizarTablaCreditos(control.obtenerCreditoAlumnosTable(txtBuscarCredito.Text));
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            int ancho = this.Width;
            //Actualiza el tamaño de la tabla con respecto al tamaño de la ventana
            dataGridViewCreditos.Width = ancho - 195;
            dataGridViewCreditos.Height = this.Height - 130;
            //actualiza la posicion de los controles con respecto al tamaño de la ventana
            btnAgregar.Location = new Point(ancho - 169, btnAgregar.Location.Y);
            txtBuscarCredito.Location = new Point(ancho - 216, txtBuscarCredito.Location.Y);
            pictureBox2.Location = new Point(ancho - 245, pictureBox2.Location.Y);
            limpiarBusqueda.Location = new Point(ancho - 39, limpiarBusqueda.Location.Y);
            //Actualiza el valor del ancho de la columnas
            if (dataGridViewCreditos.Columns.Count != 0)
            {
                int x = (dataGridViewCreditos.Width - 20) / dataGridViewCreditos.Columns.Count;
                foreach (DataGridViewColumn aux in dataGridViewCreditos.Columns)
                {
                    aux.Width = x;
                }
            }
        }

        private void darDeBajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String rfc = dataGridViewCreditos.CurrentRow.Cells[1].Value.ToString();
            credito = control.consultarCreditoAlumno(rfc);
            Thread t = new Thread(new ThreadStart(ThreadMethodDocumentos));
            t.Start();
        }

        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String id = dataGridViewCreditos.CurrentRow.Cells[0].Value.ToString();
                DialogResult dialogresult = MessageBox.Show("¿Desea cancelar el crédito?", "Cancelar crédito", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogresult == DialogResult.OK)
                {
                    if (control.cancelarCredito(id))
                    {
                        MessageBox.Show("Crédito cancelado");
                        actualizarTablaCreditos(control.obtenerCreditoAlumnosTable());
                    }
                    else
                        MessageBox.Show("Error al cancelar el crédito");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ThreadMethodDocumentos()
        {
            DocumentosWord word = new DocumentosWord(credito);
        }
    }
}
