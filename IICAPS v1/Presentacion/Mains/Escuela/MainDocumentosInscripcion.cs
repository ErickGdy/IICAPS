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

namespace IICAPS_v1.Presentacion.Mains.Escuela
{
    public partial class MainDocumentosInscripcion : Form
    {

        private static MainDocumentosInscripcion instance;
        ControlIicaps control;
        public MainDocumentosInscripcion()
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            try
            {
                actualizarTablaDocumentosInscripcion(control.obtenerEntregaDocumentos());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static MainDocumentosInscripcion getInstance()
        {
            if (instance == null)
                instance = new MainDocumentosInscripcion();
            return instance;
        }

        private void actualizarTablaDocumentosInscripcion(MySqlDataAdapter data)
        {
            try
            {
                DataTable dtDatos = new DataTable();
                //Con la informacion del adaptador se llena el datatable
                data.Fill(dtDatos);
                //Se asigna el datatable como origen de datos del datagridview
                dataGridViewDocumentos.DataSource = dtDatos;
                //Actualiza el valor de la etiqueta donde se muestra el total de productos
                dataGridViewDocumentos.Columns[0].Width = 100;
                dataGridViewDocumentos.Columns[1].Width = 20;
                dataGridViewDocumentos.Columns[2].Width = 20;
                dataGridViewDocumentos.Columns[3].Width = 20;
                dataGridViewDocumentos.Columns[4].Width = 20;
                dataGridViewDocumentos.Columns[5].Width = 20;
                dataGridViewDocumentos.Columns[6].Width = 20;
                dataGridViewDocumentos.Columns[7].Width = 20;
                dataGridViewDocumentos.Columns[8].Width = 20;
                dataGridViewDocumentos.Columns[9].Width = 20;
                dataGridViewDocumentos.Columns[10].Width = 20;
                dataGridViewDocumentos.Columns[11].Width = 20;
                dataGridViewDocumentos.Columns[12].Width = 100;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btnAgregarAlumno_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Seleccione SI para agregar Documentos de Inscripción o NO para agregar Documentos de Inscripcion para Titulación de Licenciatura ",
                "Agregar Documentos", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                FormDocumentosInscripcion fa = new FormDocumentosInscripcion(null);
                fa.FormClosed += new FormClosedEventHandler(form_Closed);
                fa.Show();
            }
            else
            {
                FormDocumentosInscripcionTitulacionLicenciatura fa = new FormDocumentosInscripcionTitulacionLicenciatura(null);
                fa.FormClosed += new FormClosedEventHandler(form_Closed);
                fa.Show();
            }
        }

        private void form_Closed(object sender, FormClosedEventArgs e)
        {
            actualizarTablaDocumentosInscripcion(control.obtenerEntregaDocumentosTable(txtBuscarDocumentos.Text));
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            consultarDocumentosInscripcion();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String rfc = dataGridViewDocumentos.CurrentRow.Cells[0].Value.ToString();
                DocumentosInscripcion documentos = control.consultarEntregaDocumentos(rfc);
                if (documentos.tipoInscripcion == 2)
                {
                    FormDocumentosInscripcionTitulacionLicenciatura fa = new FormDocumentosInscripcionTitulacionLicenciatura(null);
                    fa.FormClosed += new FormClosedEventHandler(form_Closed);
                    fa.Show();
                }
                else if (documentos.tipoInscripcion == 1)
                {
                    FormDocumentosInscripcion fa = new FormDocumentosInscripcion(null);
                    fa.FormClosed += new FormClosedEventHandler(form_Closed);
                    fa.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void limpiarBusqueda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            limpiarBusqueda.Visible = false;
            txtBuscarDocumentos.Text = "";
            try
            {
                actualizarTablaDocumentosInscripcion(control.obtenerEntregaDocumentos());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void consultarDocumentosInscripcion()
        {
            try
            {
                String rfc = dataGridViewDocumentos.CurrentRow.Cells[0].Value.ToString();
                DocumentosInscripcion documentos = control.consultarEntregaDocumentos(rfc);
                if (documentos.tipoInscripcion == 2)
                {
                    FormDocumentosInscripcionTitulacionLicenciatura fa = new FormDocumentosInscripcionTitulacionLicenciatura(null);
                    fa.FormClosed += new FormClosedEventHandler(form_Closed);
                    fa.Show();
                }
                else if (documentos.tipoInscripcion == 1)
                {
                    FormDocumentosInscripcion fa = new FormDocumentosInscripcion(null);
                    fa.FormClosed += new FormClosedEventHandler(form_Closed);
                    fa.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            actualizarTablaDocumentosInscripcion(control.obtenerEntregaDocumentosTable(txtBuscarDocumentos.Text));
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            int ancho = this.Width;
            //Actualiza el tamaño de la tabla con respecto al tamaño de la ventana
            dataGridViewDocumentos.Width = ancho - 195;
            dataGridViewDocumentos.Height = this.Height - 130;
            //actualiza la posicion de los controles con respecto al tamaño de la ventana
            btnAgregar.Location = new Point(ancho - 169, btnAgregar.Location.Y);
            txtBuscarDocumentos.Location = new Point(ancho - 216, txtBuscarDocumentos.Location.Y);
            pictureBox2.Location = new Point(ancho - 245, pictureBox2.Location.Y);
            limpiarBusqueda.Location = new Point(ancho - 39, limpiarBusqueda.Location.Y);
            //Actualiza el valor del ancho de la columnas
            if (dataGridViewDocumentos.Columns.Count != 0)
            {
                int x = (dataGridViewDocumentos.Width - 20) / dataGridViewDocumentos.Columns.Count;
                foreach (DataGridViewColumn aux in dataGridViewDocumentos.Columns)
                {
                    aux.Width = x;
                }
            }
        }
    }
}
