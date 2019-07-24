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
    public partial class DetalleGrupoListas : Form
    {

        private static DetalleGrupoListas instance;
        ControlIicaps control;
        Grupo grupo;
        List<Empleado> empleados;
        List<Materia> materias;
        List<Alumno> alumnos;
        DataGridViewPrinter MyDataGridViewPrinter;
        string[] paseDeListaValues = new string[] { "Asistencia","Retardo", "Falta" };
        public DetalleGrupoListas(Grupo gru)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            cmbSabado.SelectedItem = 0;
            this.grupo = gru;
            alumnos = control.ObtenerAlumnosGrupos(this.grupo.Codigo);
            lblNombreGrupo.Text = "Grupo: "+ grupo.Codigo + " - " + grupo.Generacion;
            this.Text = lblNombreGrupo.Text;
            try
            {
                empleados = control.ObtenerEmpleados();
                if(empleados != null)
                foreach (Empleado emp in empleados)
                {
                    cmbMaestros.Items.Add(emp.Nombre);
                }
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

        private void pasarListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(cmbMaestros.SelectedIndex >= 0)
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                agregarColumnas(null);
                foreach (Alumno alu in alumnos)
                    dataGridView1.Rows.Add(alu.Nombre);
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
                List<PaseDeListaAlumno> asistencias = control.ObtenerAsistenciaAlumnosMateriaTable(this.grupo.Codigo, materias.ElementAt(cmbMaterias.SelectedIndex).Id, this.alumnos);
                if (asistencias != null)
                {
                    agregarColumnas(asistencias.First().Asistencias);
                    foreach (PaseDeListaAlumno p in asistencias)
                    {
                        List<string> valores = new List<string>();
                        valores.Add(p.Alumno);
                        if(p.Asistencias!=null)
                        foreach (Asistencias asis in p.Asistencias)
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
                    if (aux.IsTarde)
                    {
                        columna.DataPropertyName = "Columna-" + aux.Fecha.ToShortDateString() + " - TARDE";
                        columna.HeaderText = aux.Fecha.ToShortDateString() + " - TARDE";
                    }
                    else
                    {
                        columna.DataPropertyName = "Columna-" + aux.Fecha.ToShortDateString();
                        columna.HeaderText = aux.Fecha.ToShortDateString();
                    }
                    columna.DefaultCellStyle.NullValue = paseDeListaValues[2];
                    columna.Width = 85;
                    dataGridView1.Columns.Add(columna);
                }
        } 
        private void agregarColumnaOpciones()
        {
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
                    pls.RFC = alumnos.ElementAt(i).Rfc;
                    pls.Alumno = alumnos.ElementAt(i).Nombre;
                    Asistencias asis = new Asistencias();
                    asis.Estado = dataGridView1.Rows[i].Cells[1].FormattedValue.ToString();
                    asis.Fecha = dateTimePicker1.Value;
                    if (cmbSabado.Visible&& cmbSabado.SelectedItem.ToString()=="Tarde")
                            asis.IsTarde = true;
                    else
                        asis.IsTarde = false;
                    List<Asistencias> ls = new List<Asistencias>();
                    ls.Add(asis);
                    pls.Asistencias = ls;
                    aux.Add(pls);
                }
                if (control.RegistrarAsistencias(aux, empleados.ElementAt(cmbMaestros.SelectedIndex).Matricula, this.grupo.Codigo, materias.ElementAt(cmbMaterias.SelectedIndex).Id.ToString(), control.FormatearFecha(dateTimePicker1.Value)))
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
            //AGREGAR COLUMNA PARA PASE DE LISTA
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.DataPropertyName = "Columna - " + DateTime.Now.ToShortDateString() + "- inicio";
            columna.HeaderText = DateTime.Now.ToShortDateString();
            columna.Width = 85;
            dataGridView1.Columns.Add(columna);
            //AGREGAR SEGUNDA COLUMNA  SI ES SABADO
            DataGridViewTextBoxColumn columna2 = new DataGridViewTextBoxColumn();
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                columna2.DataPropertyName = "Columna - " + DateTime.Now.ToShortDateString() + "- fin";
                columna2.HeaderText = DateTime.Now.ToShortDateString();
                columna2.Width = 85;
                dataGridView1.Columns.Add(columna2);
            }
            //AGREGAR COLUMNA DE OBSERVACIONES
            DataGridViewTextBoxColumn columnaO = new DataGridViewTextBoxColumn();
            columnaO.DataPropertyName = "Observaciones";
            columnaO.HeaderText = "Observaciones";
            columnaO.Width = 85;
            dataGridView1.Columns.Add(columnaO);

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
                MessageBox.Show("Error al generar documento de impresion");
            }
            dataGridView1.Columns.Remove(columna);
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                dataGridView1.Columns.Remove(columna2);
            }
            dataGridView1.Columns.Remove(columnaO);

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

            MyDataGridViewPrinter = new DataGridViewPrinter(dataGridView1, MyPrintDocument, false, "Pase de lista" + lblNombreGrupo.Text + "\n Materia: " + materias.ElementAt(cmbMaterias.SelectedIndex).Nombre + "\n \n Maestro:__________________________________________ \n\n", new Font("Tahoma", 14, FontStyle.Bold, GraphicsUnit.Point), Color.Black, "FOOTER GIGANTE PARA VER QUE PASA CUANDO QUIERO IMPRIMIR UN SUPPER FOOTER GIGANTESCO", new Font("Tahoma", 14, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
            return true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.DayOfWeek == DayOfWeek.Saturday)
            {
                lblSabado.Visible = true;
                cmbSabado.Visible = true;
            }else
            {
                lblSabado.Visible = false;
                cmbSabado.Visible = false;
            }
        }
    }
}
