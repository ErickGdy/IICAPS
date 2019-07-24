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
using System.Drawing.Printing;

namespace IICAPS_v1.Presentacion.Mains.Escuela
{
    public partial class DetalleGrupoCalificaciones : Form
    {

        private static DetalleGrupoCalificaciones instance;
        ControlIicaps control;
        Grupo grupo;
        List<Materia> materias;
        List<Alumno> alumnos;
        DataGridViewPrinter MyDataGridViewPrinter;
        string[] paseDeListaValues = new string[] { "Asistencia","Retardo", "Falta" };
        public DetalleGrupoCalificaciones(Grupo gru)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            this.grupo = gru;
            alumnos = control.ObtenerAlumnosGrupos(this.grupo.Codigo);
            lblNombreGrupo.Text = "Grupo: "+ grupo.Codigo + " - " + grupo.Generacion;
            this.Text = lblNombreGrupo.Text;
            try
            {
                materias = control.ConsultarMapaCurricularPrograma(this.grupo.Programa);
                if(materias!= null)
                foreach (Materia mat in materias)
                {
                    cmbMaterias.Items.Add(mat.Nombre);
                }
                Main_SizeChanged(null, null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static DetalleGrupoCalificaciones getInstance(Grupo g)
        {
            if (instance == null)
                instance = new DetalleGrupoCalificaciones(g);
            return instance;
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            int ancho = this.Width;
            //Actualiza el tamaño de la tabla con respecto al tamaño de la ventana
            dataGridView1.Width = ancho - 50;
            dataGridView1.Height = this.Height - 265;
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

        private void capturarCalificacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                agregarColumnas(true,true);
                foreach (CalificacionesAlumno cal in control.ObtenerCalificacionesAlumnosMateriaTable(this.grupo.Codigo, materias.ElementAt(cmbMaterias.SelectedIndex).Id, alumnos))
                {
                    if (cal.Calificaciones != null)
                        dataGridView1.Rows.Add(cal.Alumno, cal.Calificaciones.ElementAt(0).CalificacionTareas, cal.Calificaciones.ElementAt(0).CalificacionTareas);
                    else
                        dataGridView1.Rows.Add(cal.Alumno, null, null);
                }
                dataGridView1.ReadOnly = false;
                btnGuardar.Visible = true;
                imprimirListaToolStripMenuItem1.Enabled = false;
                Main_SizeChanged(null, null);
            } catch (Exception ex) 
            {
                MessageBox.Show("Error al obtener datos para capturacion de calificaciones");
            }
        }

        private void btnBuscarLista_Click(object sender, EventArgs e)
        {
          if(cmbMaterias.SelectedIndex>=0)
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                List<CalificacionesAlumno> calificaciones = control.ObtenerCalificacionesAlumnosMateriaTable(this.grupo.Codigo, materias.ElementAt(cmbMaterias.SelectedIndex).Id, this.alumnos);
                if (calificaciones != null)
                {
                    agregarColumnas(true,false);
                    foreach (CalificacionesAlumno p in calificaciones)
                    {
                        List<string> valores = new List<string>();
                        valores.Add(p.Alumno);
                        if(p.Calificaciones!=null)
                        {
                            valores.Add(p.Calificaciones.ElementAt(0).CalificacionTareas.ToString());
                            valores.Add(p.Calificaciones.ElementAt(0).CalificacionFinal.ToString());
                        }
                        dataGridView1.Rows.Add(valores.ToArray());
                    }
                }
                else
                {
                    agregarColumnas(false,false);
                }
                imprimirListaToolStripMenuItem1.Enabled = true;
                capturarCalificacionesToolStripMenuItem1.Enabled = true;
                Main_SizeChanged(null,null);
            }
            catch (Exception ex)
            {
                imprimirListaToolStripMenuItem1.Enabled = false;
                capturarCalificacionesToolStripMenuItem1.Enabled = false;
                MessageBox.Show(ex.Message);
            }
        }
        private void agregarColumnas(bool columnasCalificaciones, bool capturaCalificaciones )
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "columnaNombre";
            column.HeaderText = "Nombre de Alumno";
            column.Width = 300;
            column.ReadOnly = true;
            dataGridView1.Columns.Add(column);
            if (columnasCalificaciones)
            {
                //agregar cositas de calificaciones

                //AGREGAR COLUMNA CALIFICACION DE TAREAS
                DataGridViewTextBoxColumn columnaCalificacionTareas = new DataGridViewTextBoxColumn();
                columnaCalificacionTareas.DataPropertyName = "columnaCalificacionTareas";
                columnaCalificacionTareas.Name = "columnaCalificacionTareas";
                columnaCalificacionTareas.HeaderText = "Calificacion Tareas";
                columnaCalificacionTareas.Width = 150;
                dataGridView1.Columns.Add(columnaCalificacionTareas);
                //AGREGAR COLUMNA CALIFICACION FINAL
                DataGridViewTextBoxColumn columnaCalificacionFinal = new DataGridViewTextBoxColumn();
                columnaCalificacionFinal.DataPropertyName = "CalificacionFinal";
                columnaCalificacionFinal.Name = "CalificacionFinal";
                columnaCalificacionFinal.HeaderText = "Calificacion Final";
                columnaCalificacionFinal.Width = 150;
                dataGridView1.Columns.Add(columnaCalificacionFinal);
                if (capturaCalificaciones) { 
                    columnaCalificacionTareas.DefaultCellStyle.NullValue = "0";
                    columnaCalificacionFinal.DefaultCellStyle.NullValue = "0";
                }
            }
        } 

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                List<CalificacionesAlumno> aux = new List<CalificacionesAlumno>();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    CalificacionesAlumno pls = new CalificacionesAlumno();
                    pls.RFC = alumnos.ElementAt(i).Rfc;
                    pls.Alumno = alumnos.ElementAt(i).Nombre;
                    Calificacion cal = new Calificacion();
                    cal.CalificacionTareas = Convert.ToSingle(dataGridView1.Rows[i].Cells[1].FormattedValue.ToString());
                    cal.CalificacionFinal = Convert.ToSingle(dataGridView1.Rows[i].Cells[2].FormattedValue.ToString());
                    List<Calificacion> ls = new List<Calificacion>();
                    ls.Add(cal);
                    pls.Calificaciones = ls;
                    aux.Add(pls);
                }
                if (control.RegistrarCalificaciones(aux, this.grupo.Codigo, materias.ElementAt(cmbMaterias.SelectedIndex).Id.ToString()))
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
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            agregarColumnas(true, false);
            foreach  (Alumno al in alumnos)
            {
                dataGridView1.Rows.Add(al.Nombre,null,null);
            }
            Main_SizeChanged(null,null);
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
                MessageBox.Show("Error al generar documento de impresión");
            }

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

            MyDataGridViewPrinter = new DataGridViewPrinter(dataGridView1, MyPrintDocument, false, "Captura de Calificaciones" + lblNombreGrupo.Text + "\n Materia: " + materias.ElementAt(cmbMaterias.SelectedIndex).Nombre + "\n \n Maestro:__________________________________________ \n\n", new Font("Tahoma", 14, FontStyle.Bold, GraphicsUnit.Point), Color.Black, "FOOTER GIGANTE PARA VER QUE PASA CUANDO QUIERO IMPRIMIR UN SUPPER FOOTER GIGANTESCO" ,new Font("Tahoma", 14, FontStyle.Bold, GraphicsUnit.Point), Color.Black, false);

            return true;
        }

    }
}
