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
using System.Drawing.Printing;

namespace IICAPS_v1.Presentacion.Mains.Escuela
{
    public partial class DetalleGrupoListas : Form
    {

        private static DetalleGrupoListas instance;
        ControlIicaps control;
        Grupo grupo;
        List<Empleados> empleados;
        List<Materia> materias;
        List<Alumno> alumnos;
        DataGridViewPrinter MyDataGridViewPrinter;
        string[] paseDeListaValues = new string[] { "Asistencia","Retardo", "Falta" };
        public DetalleGrupoListas(Grupo gru)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            this.grupo = gru;
            alumnos = control.obtenerAlumnosGrupos(this.grupo.codigo);
            lblNombreGrupo.Text = "Grupo: "+ grupo.codigo + " - " + grupo.generacion;
            this.Text = lblNombreGrupo.Text;
            try
            {
                empleados = control.obtenerEmpleados();
                if(empleados != null)
                foreach (Empleados emp in empleados)
                {
                    cmbMaestros.Items.Add(emp.nombre);
                }
                materias = control.consultarMapaCurricularPrograma(this.grupo.programa);
                if(materias!= null)
                foreach (Materia mat in materias)
                {
                    cmbMaterias.Items.Add(mat.nombre);
                }
                Main_SizeChanged(null, null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static DetalleGrupoListas getInstance(Grupo g)
        {
            if (instance == null)
                instance = new DetalleGrupoListas(g);
            return instance;
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            int ancho = this.Width;
            //Actualiza el tamaño de la tabla con respecto al tamaño de la ventana
            dataGridView1.Width = ancho - 50;
            dataGridView1.Height = this.Height - 255;
            //actualiza la posicion de los controles con respecto al tamaño de la ventana
            //btnBuscarLista.Location = new Point (ancho - 195, btnBuscarLista.Location.Y);
            lblNombreGrupo.Location = new Point( (ancho/2)- (lblNombreGrupo.Width/2),lblNombreGrupo.Location.Y);
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

        private void pasarListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(cmbMaestros.SelectedIndex >= 0)
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                agregarColumnas(null);
                foreach (Alumno alu in alumnos)
                    dataGridView1.Rows.Add(alu.nombre);
                agregarColumnaOpciones();
                dataGridView1.ReadOnly = false;
                btnGuardar.Visible = true;
                lblFecha.Visible = true;
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker1.Visible = true;
                imprimirListaToolStripMenuItem1.Enabled = false;
                Main_SizeChanged(null, null);
            }
            else
            {
                MessageBox.Show("Seleccione maestro");
                cmbMaestros.Visible = true;
                lblMaestro.Visible = true;
                dataGridView1.ReadOnly = true;
            }
        }


        private void btnBuscarLista_Click(object sender, EventArgs e)
        {
          if(cmbMaterias.SelectedIndex>=0)
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                List<PaseDeListaAlumno> asistencias = control.obtenerAsistenciaAlumnosMateriaTable(this.grupo.codigo, materias.ElementAt(cmbMaterias.SelectedIndex).id, this.alumnos);
                if (asistencias != null)
                {
                    agregarColumnas(asistencias.First().asistencias);
                    foreach (PaseDeListaAlumno p in asistencias)
                    {
                        List<string> valores = new List<string>();
                        valores.Add(p.alumno);
                        if(p.asistencias!=null)
                        foreach (Asistencias asis in p.asistencias)
                        {
                            valores.Add(asis.Estado);
                        }
                        dataGridView1.Rows.Add(valores.ToArray());
                    }
                }
                else
                {
                    agregarColumnas(null);
                }
                imprimirListaToolStripMenuItem1.Enabled = true;
                pasarListaToolStripMenuItem1.Enabled = true;
                Main_SizeChanged(null,null);
            }
            catch (Exception ex)
            {
                imprimirListaToolStripMenuItem1.Enabled = false;
                pasarListaToolStripMenuItem1.Enabled = false;
                MessageBox.Show(ex.Message);
            }
        }
        private void agregarColumnas(List<Asistencias> lista)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "columnaNombre";
            column.HeaderText = "Nombre de Alumno";
            column.Width = 300;
            dataGridView1.Columns.Add(column);
            if(lista!=null)
                foreach(Asistencias aux in lista)
                {
                    DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
                    columna.DataPropertyName = "Columna-"+aux.Fecha.ToShortDateString();
                    columna.HeaderText = aux.Fecha.ToShortDateString();
                    columna.DefaultCellStyle.NullValue = paseDeListaValues[2];
                    columna.Width = 85;
                    dataGridView1.Columns.Add(columna);
                }
        } 
        private void agregarColumnaOpciones()
        {
            //DataGridViewCheckBoxColumn columna = new DataGridViewCheckBoxColumn();
            //columna.DataPropertyName = "Columna-" + DateTime.Now.ToShortDateString();
            //columna.Name = "Columna-" + DateTime.Now.ToShortDateString();
            //columna.HeaderText = DateTime.Now.ToShortDateString();
            //columna.Width = 100;
            //columna.DefaultCellStyle.NullValue = true;
            //dataGridView1.Columns.Add(columna);

            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            column.DataPropertyName = "Columna-" + DateTime.Now.ToShortDateString();
            column.Name = "Columna-" + DateTime.Now.ToShortDateString();
            column.HeaderText = DateTime.Now.ToShortDateString();
            column.DropDownWidth = 160;
            column.Width = 150;
            column.MaxDropDownItems = 3;
            column.DefaultCellStyle.NullValue = "Asistencia";
            column.FlatStyle = FlatStyle.Flat;
            column.Items.AddRange(paseDeListaValues);
            dataGridView1.Columns.Add(column);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                List<PaseDeListaAlumno> aux = new List<PaseDeListaAlumno>();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    PaseDeListaAlumno pls = new PaseDeListaAlumno();
                    pls.RFC = alumnos.ElementAt(i).rfc;
                    pls.alumno = alumnos.ElementAt(i).nombre;
                    Asistencias asis = new Asistencias();
                    asis.Estado = dataGridView1.Rows[i].Cells[1].FormattedValue.ToString();
                    asis.Fecha = DateTime.Now;
                    List<Asistencias> ls = new List<Asistencias>();
                    ls.Add(asis);
                    pls.asistencias = ls;
                    aux.Add(pls);
                }
                if (control.registrarAsistencias(aux, empleados.ElementAt(cmbMaestros.SelectedIndex).correo, this.grupo.codigo, materias.ElementAt(cmbMaterias.SelectedIndex).id.ToString(), control.formatearFecha(dateTimePicker1.Value)))
                {
                    MessageBox.Show("Datos guardados exitosamente");
                    this.Close();
                }
                else
                    MessageBox.Show("Error al guardar datos");

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }
        private void imprimirListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.DataPropertyName = "Columna - " + DateTime.Now.ToShortDateString();
            columna.HeaderText = DateTime.Now.ToShortDateString();
            columna.Width = 85;
            dataGridView1.Columns.Add(columna);

            if (SetupThePrinting())
            {
                PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
                MyPrintPreviewDialog.Document = MyPrintDocument;
                MyPrintPreviewDialog.ShowDialog();
            }
            dataGridView1.Columns.Remove(columna);
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

            MyPrintDocument.DocumentName = lblNombreGrupo.Text;
            MyPrintDocument.PrinterSettings = MyPrintDialog.PrinterSettings;
            MyPrintDocument.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            MyPrintDocument.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);
            MyPrintDocument.DefaultPageSettings.Landscape = true;

            MyDataGridViewPrinter = new DataGridViewPrinter(dataGridView1, MyPrintDocument, true, true, "Pase de lista" + lblNombreGrupo.Text + "\n Materia: " + materias.ElementAt(cmbMaterias.SelectedIndex).nombre + "\n \n \n Maestro:________________________________________", new Font("Tahoma", 14, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);

            return true;
        }
    }
}
