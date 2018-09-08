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
    public partial class DetalleGrupoCalificaciones : Form
    {

        private static DetalleGrupoCalificaciones instance;
        ControlIicaps control;
        Grupo grupo;
        List<Empleados> empleados;
        List<Materia> materias;
        List<Alumno> alumnos;
        DataGridViewPrinter MyDataGridViewPrinter;
        string[] paseDeListaValues = new string[] { "Asistencia","Retardo", "Falta" };
        public DetalleGrupoCalificaciones(Grupo gru)
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
                if (cmbMaestros.SelectedIndex >= 0)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.Columns.Clear();
                    agregarColumnas(true);
                    foreach (CalificacionesAlumno cal in control.obtenerCalificacionesAlumnosMateriaTable(this.grupo.codigo, materias.ElementAt(cmbMaterias.SelectedIndex).id, alumnos))
                    {
                        if (cal.calificaciones != null)
                            dataGridView1.Rows.Add(cal.alumno, cal.calificaciones.ElementAt(0).calificacionTareas, cal.calificaciones.ElementAt(0).calificacionTareas);
                        else
                            dataGridView1.Rows.Add(cal.alumno, null, null);
                    }
                    dataGridView1.ReadOnly = false;
                    btnGuardar.Visible = true;
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
                List<CalificacionesAlumno> calificaciones = control.obtenerCalificacionesAlumnosMateriaTable(this.grupo.codigo, materias.ElementAt(cmbMaterias.SelectedIndex).id, this.alumnos);
                if (calificaciones != null)
                {
                    agregarColumnas(true);
                    foreach (CalificacionesAlumno p in calificaciones)
                    {
                        List<string> valores = new List<string>();
                        valores.Add(p.alumno);
                        if(p.calificaciones!=null)
                        {
                            valores.Add(p.calificaciones.ElementAt(0).calificacionTareas.ToString());
                            valores.Add(p.calificaciones.ElementAt(0).calificacionFinal.ToString());
                        }
                        dataGridView1.Rows.Add(valores.ToArray());
                    }
                }
                else
                {
                    agregarColumnas(false);
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
        private void agregarColumnas(bool columnasCalificaciones)
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
                columnaCalificacionTareas.DefaultCellStyle.NullValue = "0";
                dataGridView1.Columns.Add(columnaCalificacionTareas);
                //AGREGAR COLUMNA CALIFICACION FINAL
                DataGridViewTextBoxColumn columnaCalificacionFinal = new DataGridViewTextBoxColumn();
                columnaCalificacionFinal.DataPropertyName = "CalificacionFinal";
                columnaCalificacionFinal.Name = "CalificacionFinal";
                columnaCalificacionFinal.HeaderText = "Calificacion Final";
                columnaCalificacionFinal.Width = 150;
                columnaCalificacionFinal.DefaultCellStyle.NullValue = "0";
                dataGridView1.Columns.Add(columnaCalificacionFinal);
            }
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
                    asis.Fecha = dateTimePicker1.Value;
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
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            agregarColumnas(true);
            foreach  (Alumno al in alumnos)
            {
                dataGridView1.Rows.Add(al.nombre,null,null);
            }
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

            MyDataGridViewPrinter = new DataGridViewPrinter(dataGridView1, MyPrintDocument, false, "Pase de lista" + lblNombreGrupo.Text + "\n Materia: " + materias.ElementAt(cmbMaterias.SelectedIndex).nombre + "\n \n Maestro:__________________________________________ \n\n", new Font("Tahoma", 14, FontStyle.Bold, GraphicsUnit.Point), Color.Black, "FOOTER GIGANTE PARA VER QUE PASA CUANDO QUIERO IMPRIMIR UN SUPPER FOOTER GIGANTESCO" ,new Font("Tahoma", 14, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);

            return true;
        }

    }
}
