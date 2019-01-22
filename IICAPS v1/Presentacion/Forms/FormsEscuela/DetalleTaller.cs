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
    public partial class DetalleTaller : Form
    {

        private static DetalleTaller instance;
        ControlIicaps control;
        Taller taller;
        Pago pago;
        public DetalleTaller(Taller tal)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            this.taller = tal;
            lblNombreGrupo.Text = taller.nombre;
            this.Text = lblNombreGrupo.Text;
            try
            {
                actualizarTabla(control.obtenerAsistentesTalleresTable(taller.id.ToString()));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static DetalleTaller getInstance(Taller taller)
        {
            if (instance != null)
            {
                return instance = new DetalleTaller(taller);
            }else
            {
                return instance;
            }
        }
        private void actualizarTabla(MySqlDataAdapter data)
        {
            try
            {
                DataTable dtDatos = new DataTable();
                //Con la informacion del adaptador se llena el datatable
                data.Fill(dtDatos);
                //Se asigna el datatable como origen de datos del datagridview
                dataGridView1.DataSource = dtDatos;
                //Actualiza el valor del ancho de la columnas
                int x = (dataGridView1.Width - 20) / (dataGridView1.Columns.Count-1);
                foreach (DataGridViewColumn aux in dataGridView1.Columns)
                {
                    aux.Width = x;
                }
                dataGridView1.Columns[0].Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FormInscricionTaller fa = new FormInscricionTaller(this.taller.id.ToString(),null);
            fa.FormClosed += new FormClosedEventHandler(form_Closed);
            fa.ShowDialog();
        }
        private void form_Closed(object sender, FormClosedEventArgs e)
        {
            actualizarTabla(control.obtenerAsistentesTalleresTable(taller.id.ToString(), txtBuscar.Text));
        }
        private void quitarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                DialogResult dialogresult = MessageBox.Show("¿Desea remover al asistente del taller?", "Remover asistente", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogresult == DialogResult.OK)
                {
                    if (control.borrarAsistenteTaller(id))
                    {
                        MessageBox.Show("Asistente removido");
                        actualizarTabla(control.obtenerAsistentesTalleresTable(taller.id.ToString(), txtBuscar.Text));
                    }
                    else
                        MessageBox.Show("Error al remover asistente");
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
            txtBuscar.Text = "";
            try
            {
                actualizarTabla(control.obtenerAsistentesTalleresTable(taller.id.ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarTabla(control.obtenerAsistentesTalleresTable(taller.id.ToString(), txtBuscar.Text));
        }
        private void Main_SizeChanged(object sender, EventArgs e)
        {
            int ancho = this.Width;
            //Actualiza el tamaño de la tabla con respecto al tamaño de la ventana
            dataGridView1.Width = ancho - 50;
            dataGridView1.Height = this.Height - 155;
            //actualiza la posicion de los controles con respecto al tamaño de la ventana
            btnAgregar.Location = new Point (ancho - 195, btnAgregar.Location.Y);
            txtBuscar.Location = new Point (ancho - 230, txtBuscar.Location.Y);
            pictureBoxBuscar.Location = new Point (ancho - 260, pictureBoxBuscar.Location.Y);
            limpiarBusqueda.Location = new Point (ancho - 55, limpiarBusqueda.Location.Y);
            //Actualiza el valor del ancho de la columnas
            if (dataGridView1.Columns.Count != 0)
            {
                int x = (dataGridView1.Width - 20) / dataGridView1.Columns.Count;
                foreach (DataGridViewColumn aux in dataGridView1.Columns)
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
                        actualizarTabla(control.obtenerAsistentesTalleresTable(taller.id.ToString(), texto));
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
                    actualizarTabla(control.obtenerAsistentesTalleresTable(taller.id.ToString())) ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
        }

        private void modificiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String ID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                FormInscricionTaller fa = new FormInscricionTaller(this.taller.id.ToString(), control.obtenerAsistenteTaller(ID));
                fa.FormClosed += new FormClosedEventHandler(form_Closed);
                fa.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void registrarPagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String ID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                TallerAsistente asisT =  control.obtenerAsistenteTaller(ID);
                FormPago fp = new FormPago(asisT.restante, "Pago de Taller", "Escuela");
                fp.ShowDialog();
                pago = fp.getPagos();
                fp.Dispose();
                if (control.registrarPagoAsistenciaTaller(pago, asisT.ID.ToString()))
                {
                    MessageBox.Show("Pago registrado exitosamente");
                    Thread t = new Thread(new ThreadStart(ThreadMethodDocumentos));
                    t.Start();
                    btnActualizar_Click(null,null);
                }
                else
                    throw new Exception("Error al registrar pago");
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
