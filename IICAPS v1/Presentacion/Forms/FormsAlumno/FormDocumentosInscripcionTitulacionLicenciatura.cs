using IICAPS_v1.Control;
using IICAPS_v1.DataObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IICAPS_v1.Presentacion
{
    public partial class FormDocumentosInscripcionTitulacionLicenciatura : Form
    {
        ControlIicaps control;
        DocumentosInscripcion documentacion;
        bool modificacion = false;
        public FormDocumentosInscripcionTitulacionLicenciatura(DocumentosInscripcion doc)
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
            foreach (Programa p in control.obtenerProgramas())
            {
                auxPrograma.Add(p.Nombre);
                auxIDPrograma.Add(p.Codigo.ToString());
            }
            cmbIDPrograma.Items.AddRange(auxIDPrograma.ToArray());
            cmbPrograma.Items.AddRange(auxPrograma.ToArray());
            if (doc != null)
            {
                modificacion = true;
                documentacion = doc;
                cmbPrograma.SelectedItem = control.obtenerProgramaAlumno(doc.alumno);
                cmbAlumno.SelectedItem = doc.alumno;
                cmbRecibio.SelectedItem = doc.recibioEmpleado;
                cmbIDPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
                cmbIDAlumno.SelectedIndex = cmbAlumno.SelectedIndex;
                cmbIDRecibio.SelectedIndex = cmbRecibio.SelectedIndex;
                if (documentacion.actaNacimientoOrg)
                    checkedListBox1.SetItemChecked(0, true);
                if (documentacion.actaNacimientoCop)
                    checkedListBox1.SetItemChecked(1, true);
                if (documentacion.solicitudOpcTitulacion)
                    checkedListBox1.SetItemChecked(2, true);
                if (documentacion.certificadoLicCop)
                    checkedListBox1.SetItemChecked(3, true);
                if (documentacion.constanciaLibSSOrg)
                    checkedListBox1.SetItemChecked(4, true);
                if (documentacion.curp)
                    checkedListBox1.SetItemChecked(5, true);
                if (documentacion.fotografias)
                    checkedListBox1.SetItemChecked(6, true);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!agregarEntregaDocumentosTitulacion())
                    MessageBox.Show("Error al guardar los datos de la entrega de documentos de titulacion");
                else
                {
                    MessageBox.Show("Datos de entrega de documentos de titulacion guardados exitosamente");
                    Close();
                    Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool validarCampos()
        {
            if (cmbAlumno.SelectedItem != null && cmbPrograma != null && cmbRecibio != null)
                return false;
            return true;
        }

        private bool agregarEntregaDocumentosTitulacion()
        {
            if (validarCampos())
            {
                cmbIDPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
                cmbIDAlumno.SelectedIndex = cmbAlumno.SelectedIndex;
                cmbIDRecibio.SelectedIndex = cmbRecibio.SelectedIndex;
                DocumentosInscripcion doc = new DocumentosInscripcion();
                doc.alumno = cmbIDAlumno.SelectedItem.ToString();
                doc.actaNacimientoOrg = checkedListBox1.GetItemChecked(0);
                doc.actaNacimientoCop = checkedListBox1.GetItemChecked(1);
                doc.solicitudOpcTitulacion = checkedListBox1.GetItemChecked(2);
                doc.certificadoLicCop = checkedListBox1.GetItemChecked(3);
                doc.constanciaLibSSOrg = checkedListBox1.GetItemChecked(4);
                doc.curp = checkedListBox1.GetItemChecked(5);
                doc.fotografias = checkedListBox1.GetItemChecked(6);
                doc.recibioEmpleado = cmbIDRecibio.SelectedItem.ToString();
                if (modificacion)
                {
                    doc.alumno = cmbAlumno.SelectedItem.ToString();
                    if (control.actualizarEntregaDocumentos(doc))
                        return true;
                    else
                        throw new Exception("Error al actualizar los datos de la entrega de documentos de titulacion");
                }
                else
                {
                    if (control.agregarEntregaDocumentos(doc))
                        return true;
                else
                    throw new Exception("Error al agregar los datos de la entrega de documentos de titulacion");
                }
            }
            else
                MessageBox.Show("No deje ningun campo vacio");
            return false;
        }

        private void cmbPrograma_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIDPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
            List<String> auxAlumno = new List<string>();
            List<String> auxIDAlumno = new List<string>();
            foreach (Alumno a in control.obtenerAlumnosByPrograma(cmbIDPrograma.SelectedItem.ToString()))
            {
                auxAlumno.Add(a.nombre);
                auxIDAlumno.Add(a.rfc.ToString());
            }
            cmbIDAlumno.Items.AddRange(auxIDAlumno.ToArray());
            cmbAlumno.Items.AddRange(auxAlumno.ToArray());
            cmbAlumno.SelectedIndex = 0;
        }
    }
}
