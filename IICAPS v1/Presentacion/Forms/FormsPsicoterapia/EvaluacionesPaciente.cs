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

namespace IICAPS_v1.Presentacion
{
    public partial class EvaluacionesPaciente : Form
    {

        private static EvaluacionesPaciente instance;
        ControlIicaps control;
        string ID_paciente;
        public EvaluacionesPaciente(string paciente)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            try
            {
                this.ID_paciente = paciente;
                lblNombrePaciente.Text = control.ConsultarPaciente(ID_paciente).NombreCompleto();
                actualizarTabla(control.ObtenerEvaluacionPacienteTable(ID_paciente));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static EvaluacionesPaciente getInstance(string paciente)
        {
            if (instance != null)
            {
                return instance = new EvaluacionesPaciente(paciente);
            }else
            {
                return instance;
            }
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
            FormEvaluacion fa = new FormEvaluacion(this.ID_paciente,null,false);
            fa.FormClosed += new FormClosedEventHandler(form_Closed);
            fa.ShowDialog();
        }
        private void form_Closed(object sender, FormClosedEventArgs e)
        {
            actualizarTabla(control.ObtenerEvaluacionPacienteTable(ID_paciente, txtBuscar.Text));
        }
        private void quitarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                DialogResult dialogresult = MessageBox.Show("¿Desea eliminar evaluación?", "Eliminar Evaluación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogresult == DialogResult.OK)
                {
                    if (control.CancelarEvaluacion(control.ConsultarEvaluacion(id)))
                    {
                        MessageBox.Show("Evaluación eliminadao");
                        actualizarTabla(control.ObtenerEvaluacionPacienteTable(ID_paciente, txtBuscar.Text));
                    }
                    else
                        MessageBox.Show("Error al eliminar evaluación");
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
                actualizarTabla(control.ObtenerEvaluacionPacienteTable(ID_paciente));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarTabla(control.ObtenerEvaluacionPacienteTable(ID_paciente, txtBuscar.Text));
        }
        private void Main_SizeChanged(object sender, EventArgs e)
        {
            int ancho = this.Width;
            //Actualiza el tamaño de la tabla con respecto al tamaño de la ventana
            dataGridView1.Width = ancho - 50;
            dataGridView1.Height = this.Height - 155;
            //actualiza la posicion de los controles con respecto al tamaño de la ventana
            btnAgregar.Location = new Point (ancho - 206, btnAgregar.Location.Y);
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
                        actualizarTabla(control.ObtenerEvaluacionPacienteTable(ID_paciente, texto));
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
                    actualizarTabla(control.ObtenerEvaluacionPacienteTable(ID_paciente)) ;
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
                FormEvaluacion fa = new FormEvaluacion(this.ID_paciente, control.ConsultarEvaluacion(ID),false);
                fa.FormClosed += new FormClosedEventHandler(form_Closed);
                fa.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
             try
                {
                    String ID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    FormEvaluacion fa = new FormEvaluacion(this.ID_paciente, control.ConsultarEvaluacion(ID),true);
                    fa.FormClosed += new FormClosedEventHandler(form_Closed);
                    fa.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
    }
}
