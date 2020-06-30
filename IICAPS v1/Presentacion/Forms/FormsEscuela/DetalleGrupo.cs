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
using System.Data.SqlClient;

namespace IICAPS_v1.Presentacion.Mains.Escuela
{
    public partial class DetalleGrupo : Form
    {

        private static DetalleGrupo instance;
        ControlIicaps control;
        Grupo grupo;
        public DetalleGrupo(Grupo gru)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            this.grupo = gru;
            lblNombreGrupo.Text = grupo.Codigo + " - " + grupo.Generacion;
            this.Text = lblNombreGrupo.Text;
            try
            {
                actualizarTabla(control.ObtenerAlumnosGruposTable(grupo.Codigo));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static DetalleGrupo getInstance(Grupo g)
        {
            if (instance == null)
                instance = new DetalleGrupo(g);
            return instance;
        }

        private void actualizarTabla(SqlDataAdapter data)
        {
            try
            {
                DataTable dtDatos = new DataTable();
                //Con la informacion del adaptador se llena el datatable
                data.Fill(dtDatos);
                //Se asigna el datatable como origen de datos del datagridview
                dataGridView1.DataSource = dtDatos;
                //Actualiza el valor del ancho de la columnas
                int x = (dataGridView1.Width - 20) / dataGridView1.Columns.Count;
                foreach (DataGridViewColumn aux in dataGridView1.Columns)
                {
                    aux.Width = x;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                FormInscricionGrupos fa = new FormInscricionGrupos(this.grupo.Codigo, null);
                fa.FormClosed += new FormClosedEventHandler(form_Closed);
                fa.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al intentar abrir el formulario");
            }
        }
        private void form_Closed(object sender, FormClosedEventArgs e)
        {
            actualizarTabla(control.ObtenerAlumnosGruposTable(grupo.Codigo,txtBuscar.Text));
        }
        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String id = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                Alumno alu = control.ConsultarAlumno(id);
                //MessageBox.Show("Reporte referente al grupo "+ grupo.codigo);
                FormDetalleAlumno fa = new FormDetalleAlumno(alu);
                fa.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                fa.SizeChanged -= new System.EventHandler(fa.Main_SizeChanged);
                fa.moverForms(170);
                fa.Width = fa.Width - 150;
                fa.FormClosed += new FormClosedEventHandler(form_Closed);
                fa.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void quitarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String id = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                DialogResult dialogresult = MessageBox.Show("¿Desea remover al alumno del grupo?", "Remover Alumno", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogresult == DialogResult.OK)
                {
                    if (control.QuitarAlumnoDeGrupo(this.grupo.Codigo, id))
                    {
                        MessageBox.Show("Alumno removido");
                        actualizarTabla(control.ObtenerAlumnosGruposTable(grupo.Codigo, txtBuscar.Text));
                    }
                    else
                        MessageBox.Show("Error al remover alumno");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cambiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String id = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                FormInscricionGrupos fa = new FormInscricionGrupos(null, id);
                fa.FormClosed += new FormClosedEventHandler(form_Closed);
                fa.ShowDialog();
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
                actualizarTabla(control.ObtenerAlumnosGruposTable(grupo.Codigo));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarTabla(control.ObtenerAlumnosGruposTable(grupo.Codigo,txtBuscar.Text));
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
                        actualizarTabla(control.ObtenerAlumnosGruposTable(grupo.Codigo,texto));
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
                    actualizarTabla(control.ObtenerGruposTable());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
        }


        private void listasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetalleGrupoListas dtg = new DetalleGrupoListas(this.grupo);
            dtg.Show();
            this.Close();
        }
    }
}
