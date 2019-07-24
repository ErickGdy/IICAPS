﻿using IICAPS_v1.Control;
using IICAPS_v1.DataObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IICAPS_v1.Presentacion
{
    public partial class ReporteDePaciente : Form
    {
        ControlIicaps control;
        string id_Paciente;
        Paciente paciente;
        private PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
        private PrintDocument printDocument1 = new PrintDocument();
        Boolean datosFacturacion_Showed=false;

        public ReporteDePaciente(String id_Paciente)
        {
            try
            {
                this.id_Paciente = id_Paciente;
                InitializeComponent();
                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                lblFecha.Text = DateTime.Now.ToShortDateString();
                control = ControlIicaps.getInstance();
                this.paciente = control.ConsultarPaciente(id_Paciente);
                actualizarDatos();
                actualizarTabla();
            }
            catch (Exception ex) {
                MessageBox.Show("Error al obtener datos del paciente");
                Dispose();
            }
        }
        public void actualizarDatos()
        {
            txtnombre.Text = paciente.Nombre;
            txtApellidos.Text = paciente.Apellidos;
            txtTelefono.Text = paciente.Telefono;
            txtEscuelaEmpresa.Text = paciente.Institucion;
            txtFecha.Text = paciente.FechaNacimiento.ToShortDateString();
            txtNombreTutor.Text = paciente.Nombre_tutor;
            txtTelefonoTutor.Text = paciente.Telefono_tutor;
            if (paciente.Psicoterapeuta != null)
                txtPsicoterapeuta.Text = control.ConsultarEmpleado(paciente.Psicoterapeuta).Nombre;
            if (paciente.Datos_facturacion != null)
            {
                txtFacturacionRFC.Text = paciente.Datos_facturacion[0];
                txtFacturacionNombre.Text = paciente.Datos_facturacion[1];
                txtFacturacionRazonSocial.Text = paciente.Datos_facturacion[2];
                txtFacturacionDireccion.Text = paciente.Datos_facturacion[3];
            }
        }
        private void actualizarTabla()
        {
            try
            {
                SqlDataAdapter data = control.ObtenerSesionesPacienteTable(id_Paciente);
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
                //dataGridView1.Columns[dataGridView1.Columns.Count - 1].DefaultCellStyle.NullValue = "Sin asignar";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
           e.Graphics.DrawImage(memoryImage, 140, 0);
        }
        private void printPreviewButton_Click(object sender, EventArgs e)
        {
            menuStrip1.Visible = false;
            CaptureScreen();
            menuStrip1.Visible = true;
            printDocument1.DocumentName = "Reporte de paciente No. "+id_Paciente;
            printDocument1.DefaultPageSettings.Landscape = true;
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();

        }

        Bitmap memoryImage;

        private void CaptureScreen()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X+8, this.Location.Y+35, 18, 45, s);
        }

        private void linkDatosFacturacion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (datosFacturacion_Showed)
            {
                linkDatosFacturacion.Text = "Mostrar datos de facturación";
                groupFacturacion.Visible = false;
                panelTabla.Location = new Point( panelTabla.Location.X, panelTabla.Location.Y-groupFacturacion.Height);
                this.Size = new Size( this.Width, this.Height-groupFacturacion.Height);
                datosFacturacion_Showed = false;
            }
            else
            {
                linkDatosFacturacion.Text = "Ocultar datos de facturación";
                groupFacturacion.Visible = true;
                panelTabla.Location = new Point(panelTabla.Location.X, panelTabla.Location.Y + groupFacturacion.Height);
                this.Size = new Size(this.Width, this.Height + groupFacturacion.Height);
                datosFacturacion_Showed = true;
            }
            ReporteDeSesiones_SizeChanged(null,null);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void editarDatosDePacienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FormPaciente fp = new FormPaciente(paciente);
                fp.ShowDialog();
                this.paciente = control.ConsultarPaciente(id_Paciente);
                actualizarDatos();
            }
            catch (Exception ex)
            {

            }
        }

        private void ReporteDeSesiones_SizeChanged(object sender, EventArgs e)
        {
            panelTabla.Location = new Point((this.Width - panelTabla.Width) / 2, panelTabla.Location.Y);
            panelDatos.Location = new Point((this.Width - panelDatos.Width) / 2, panelDatos.Location.Y);
            groupFacturacion.Location = new Point((this.Width - groupFacturacion.Width) / 2, groupFacturacion.Location.Y);
            if (datosFacturacion_Showed)
                panelTabla.Size = new Size(panelTabla.Width,this.Height - panelDatos.Height - groupFacturacion.Height - 60 - 71);
            else
                panelTabla.Size = new Size(panelTabla.Width,this.Height - panelDatos.Height - 60 - 71);
            btnCerrar.Location = new Point(btnCerrar.Location.X, panelTabla.Height - 45);
            dataGridView1.Height = panelTabla.Height - 80;
            pictureFooter.Location = new Point(pictureFooter.Location.X,this.Height-105);
        }

        private void agendarNuevaSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSesiones fs = new FormSesiones(paciente.Id.ToString(),null);
            fs.ShowDialog();
            actualizarTabla();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                DialogResult dialogresult = MessageBox.Show("¿Desea eliminar sesión?", "Eliminar sesión", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogresult == DialogResult.OK)
                {
                    if (control.CancelarSesion(id))
                    {
                        MessageBox.Show("Sesión cancelada");
                        actualizarTabla();
                    }
                    else
                        MessageBox.Show("Error al cancelar sesión");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void evaluacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EvaluacionesPaciente fev = new EvaluacionesPaciente(paciente.Id.ToString());
            fev.Show();
        }

        private void pagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PagosPaciente pp = new PagosPaciente(paciente.Id.ToString());
            pp.Show();
        }
    }
}
