using IICAPS_v1.Control;
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
    public partial class ReporteDePsicoterapeuta : Form
    {
        ControlIicaps control;
        string ID_Psicoterapeuta;
        Psicoterapeuta psicoterapeuta;
        private PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
        private PrintDocument printDocument1 = new PrintDocument();

        public ReporteDePsicoterapeuta(String ID_Psicoterapeuta)
        {
            try
            {
                this.ID_Psicoterapeuta = ID_Psicoterapeuta;
                InitializeComponent();
                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                lblFecha.Text = DateTime.Now.ToShortDateString();
                control = ControlIicaps.getInstance();
                this.psicoterapeuta = control.ConsultarPsicoterapeuta(ID_Psicoterapeuta);
                actualizarDatos();
                actualizarTabla();
            }
            catch (Exception ex) {
                MessageBox.Show("Error al obtener datos del psicoterapeuta");
                Dispose();
            }
        }
        public void actualizarDatos()
        {
            txtNombre.Text = psicoterapeuta.Nombre;
            txtMatricula.Text = psicoterapeuta.Matricula;
            txtTelefono.Text = psicoterapeuta.Telefono;
            txtCarrera.Text = psicoterapeuta.Carrera;
            txtHorario.Text = psicoterapeuta.Horario;
            txtObservaciones.Text = psicoterapeuta.Observaciones;
            txtEspecialidad.Text = psicoterapeuta.Especialidad;
        }
        private void actualizarTabla()
        {
            try
            {
                SqlDataAdapter data = control.ObtenerConsultasPsicoterapeutaTable(ID_Psicoterapeuta);
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
            printDocument1.DocumentName = "Reporte de psicotereuta - "+ID_Psicoterapeuta;
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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void ReporteDeSesiones_SizeChanged(object sender, EventArgs e)
        {
            panelTabla.Location = new Point((this.Width - panelTabla.Width) / 2, panelTabla.Location.Y);
            panelDatos.Location = new Point((this.Width - panelDatos.Width) / 2, panelDatos.Location.Y);
            panelTabla.Size = new Size(panelTabla.Width,this.Height - panelDatos.Height - 60 - 71);
            btnCerrar.Location = new Point(btnCerrar.Location.X, panelTabla.Height - 45);
            dataGridView1.Height = panelTabla.Height - 80;
            pictureFooter.Location = new Point(pictureFooter.Location.X,this.Height-105);
        }

        private void nominaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NominaPsicoterapeuta nps = new NominaPsicoterapeuta(ID_Psicoterapeuta);
            nps.Show();
        }

        private void pacientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PsicoterapeutaPacientes pp = new PsicoterapeutaPacientes(this.ID_Psicoterapeuta);
            pp.Show();
        }
    }
}
