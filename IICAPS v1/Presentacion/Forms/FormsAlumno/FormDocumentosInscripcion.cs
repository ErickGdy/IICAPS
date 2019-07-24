using IICAPS_v1.Control;
using IICAPS_v1.DataObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IICAPS_v1.Presentacion
{
    public partial class FormDocumentosInscripcion : Form
    {
        ControlIicaps control;
        DocumentosInscripcion documentacion;
        DocumentosInscripcion doc;
        bool modificacion = false;
        public FormDocumentosInscripcion(DocumentosInscripcion doc, bool consulta)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            List<String> auxPrograma = new List<string>();
            List<String> auxIDPrograma = new List<string>();
            List<String> auxAlumno = new List<string>();
            List<String> auxIDAlumno = new List<string>();
            List<String> auxRecibio = new List<string>();
            List<String> auxIDRecibio = new List<string>();
            foreach (Programa p in control.ObtenerProgramas())
            {
                auxPrograma.Add(p.Nombre);
                auxIDPrograma.Add(p.Codigo.ToString());
            }
            cmbIDPrograma.Items.AddRange(auxIDPrograma.ToArray());
            cmbPrograma.Items.AddRange(auxPrograma.ToArray());
            foreach (Empleado e in control.ObtenerEmpleados())
            {
                auxRecibio.Add(e.Nombre);
                auxIDRecibio.Add(e.Matricula);
            }
            cmbIDRecibio.Items.AddRange(auxIDRecibio.ToArray());
            cmbRecibio.Items.AddRange(auxRecibio.ToArray());
            if (doc != null)
            {
                modificacion = true;
                documentacion = doc;
                cmbIDPrograma.SelectedItem = control.ObtenerProgramaAlumno(doc.Alumno);
                cmbIDRecibio.SelectedItem = doc.RecibioEmpleado;
                cmbPrograma.SelectedIndex = cmbIDPrograma.SelectedIndex;
                cmbIDAlumno.SelectedItem = doc.Alumno;
                cmbAlumno.SelectedIndex = cmbIDAlumno.SelectedIndex;
                cmbRecibio.SelectedIndex = cmbIDRecibio.SelectedIndex;
                if (documentacion.ActaNacimientoOrg)
                    checkedListBox1.SetItemChecked(0, true);
                if (documentacion.ActaNacimientoCop)
                    checkedListBox1.SetItemChecked(1, true);
                if (documentacion.TituloCedulaOrg)
                    checkedListBox1.SetItemChecked(2, true);
                if (documentacion.TituloLicCop)
                    checkedListBox1.SetItemChecked(3, true);
                if (documentacion.CedProfCop)
                    checkedListBox1.SetItemChecked(4, true);
                if (documentacion.Curp)
                    checkedListBox1.SetItemChecked(5, true);
                if (documentacion.Fotografias)
                    checkedListBox1.SetItemChecked(6, true);
                if (consulta)
                {
                    cmbPrograma.Enabled = false;
                    cmbAlumno.Enabled = false;
                    cmbRecibio.Enabled = false;
                    checkedListBox1.Enabled = false;
                    btnAceptar.Enabled = false;
                }
            }
        }

        public FormDocumentosInscripcion(DocumentosInscripcion doc, bool consulta, string programa, string alumno)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            List<String> auxPrograma = new List<string>();
            List<String> auxIDPrograma = new List<string>();
            List<String> auxAlumno = new List<string>();
            List<String> auxIDAlumno = new List<string>();
            List<String> auxRecibio = new List<string>();
            List<String> auxIDRecibio = new List<string>();
            foreach (Programa p in control.ObtenerProgramas())
            {
                auxPrograma.Add(p.Nombre);
                auxIDPrograma.Add(p.Codigo.ToString());
            }
            cmbIDPrograma.Items.AddRange(auxIDPrograma.ToArray());
            cmbPrograma.Items.AddRange(auxPrograma.ToArray());
            foreach (Empleado e in control.ObtenerEmpleados())
            {
                auxRecibio.Add(e.Nombre);
                auxIDRecibio.Add(e.Matricula);
            }
            cmbIDRecibio.Items.AddRange(auxIDRecibio.ToArray());
            cmbRecibio.Items.AddRange(auxRecibio.ToArray());
            if (programa != null)
                cmbPrograma.SelectedItem = programa;
            try
            {
                if (alumno != null)
                    cmbAlumno.SelectedItem = alumno;
            }
            catch (Exception ex) { }
            if (doc != null)
            {
                modificacion = true;
                documentacion = doc;
                cmbIDPrograma.SelectedItem = control.ObtenerProgramaAlumno(doc.Alumno);
                cmbIDRecibio.SelectedItem = doc.RecibioEmpleado;
                cmbPrograma.SelectedIndex = cmbIDPrograma.SelectedIndex;
                cmbIDAlumno.SelectedItem = doc.Alumno;
                cmbAlumno.SelectedIndex = cmbIDAlumno.SelectedIndex;
                cmbRecibio.SelectedIndex = cmbIDRecibio.SelectedIndex;
                if (documentacion.ActaNacimientoOrg)
                    checkedListBox1.SetItemChecked(0, true);
                if (documentacion.ActaNacimientoCop)
                    checkedListBox1.SetItemChecked(1, true);
                if (documentacion.TituloCedulaOrg)
                    checkedListBox1.SetItemChecked(2, true);
                if (documentacion.TituloLicCop)
                    checkedListBox1.SetItemChecked(3, true);
                if (documentacion.CedProfCop)
                    checkedListBox1.SetItemChecked(4, true);
                if (documentacion.Curp)
                    checkedListBox1.SetItemChecked(5, true);
                if (documentacion.Fotografias)
                    checkedListBox1.SetItemChecked(6, true);
                if (consulta)
                {
                    cmbPrograma.Enabled = false;
                    cmbAlumno.Enabled = false;
                    cmbRecibio.Enabled = false;
                    checkedListBox1.Enabled = false;
                    btnAceptar.Enabled = false;
                }
            }
        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!agregarEntregaDocumentos())
                    MessageBox.Show("Error al guardar los datos de la entrega de documentos");
                else
                {
                    MessageBox.Show("Datos de entrega de documentos guardados exitosamente");
                    Close();
                    Dispose();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //private bool validarCampos()
        //{
        //    if (cmbAlumno.SelectedItem != null && cmbPrograma != null && cmbRecibio != null)
        //        return false;
        //    return true;
        //}

        private bool agregarEntregaDocumentos()
        {
            //if (validarCampos())
            //{
                cmbIDPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
                cmbIDAlumno.SelectedIndex = cmbAlumno.SelectedIndex;
                cmbIDRecibio.SelectedIndex = cmbRecibio.SelectedIndex;
                doc = new DocumentosInscripcion();
                doc.Alumno = cmbIDAlumno.SelectedItem.ToString();
                doc.ActaNacimientoOrg = checkedListBox1.GetItemChecked(0);
                doc.ActaNacimientoCop = checkedListBox1.GetItemChecked(1);
                doc.TituloCedulaOrg = checkedListBox1.GetItemChecked(2);
                doc.TituloLicCop = checkedListBox1.GetItemChecked(3);
                doc.CedProfCop = checkedListBox1.GetItemChecked(4);
                doc.Curp = checkedListBox1.GetItemChecked(5);
                doc.Fotografias = checkedListBox1.GetItemChecked(6);
                doc.RecibioEmpleado = cmbIDRecibio.SelectedItem.ToString();
                doc.TipoInscripcion = 1;
                if (modificacion)
                {
                    doc.Alumno = cmbIDAlumno.SelectedItem.ToString();
                    if (control.ActualizarEntregaDocumentos(doc))
                    {
                        Thread t = new Thread(new ThreadStart(ThreadMethodDocumentos));
                        t.Start();
                        return true;
                    }
                    else
                        throw new Exception("Error al actualizar los datos de la entrega de documentos");
                }
                else
                {
                    if (control.AgregarEntregaDocumentos(doc))
                    {
                        Thread t = new Thread(new ThreadStart(ThreadMethodDocumentos));
                        t.Start();
                        return true;
                    }
                    else
                        throw new Exception("Error al agregar los datos de la entrega de documentos");
                }
            //}
            //else
            //    MessageBox.Show("No deje ningun campo vacio");
            //return false;
        }

        private void cmbPrograma_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIDPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
            List<String> auxAlumno = new List<string>();
            List<String> auxIDAlumno = new List<string>();
            foreach (Alumno a in control.ObtenerAlumnosByPrograma(cmbIDPrograma.SelectedItem.ToString()))
            {
                auxAlumno.Add(a.Nombre);
                auxIDAlumno.Add(a.Rfc.ToString());
            }
            cmbIDAlumno.Items.AddRange(auxIDAlumno.ToArray());
            cmbAlumno.Items.AddRange(auxAlumno.ToArray());
            cmbAlumno.SelectedIndex = 0;
        }
        private void ThreadMethodDocumentos()
        {
            DocumentosWord word = new DocumentosWord(doc);
        }
    }
}
