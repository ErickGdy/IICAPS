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
    public partial class ImpresionDocumentos : Form
    {
        ControlIicaps control;
        public ImpresionDocumentos()
        {
            InitializeComponent();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            control = ControlIicaps.getInstance();
            List<String> auxPrograma = new List<string>();
            List<String> auxIDPrograma = new List<string>();
            List<String> auxAlumno = new List<string>();
            List<String> auxIDAlumno = new List<string>();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            foreach (Programa p in control.obtenerProgramas())
            {
                auxPrograma.Add(p.Nombre);
                auxIDPrograma.Add(p.Codigo.ToString());
            }
            cmbIDPrograma.Items.AddRange(auxIDPrograma.ToArray());
            cmbPrograma.Items.AddRange(auxPrograma.ToArray());
        }

        private void cmbPrograma_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIDPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
            List<String> auxAlumno = new List<string>();
            List<String> auxIDAlumno = new List<string>();
            try
            {
                foreach (Alumno a in control.obtenerAlumnosByPrograma(cmbIDPrograma.SelectedItem.ToString()))
                {
                    auxAlumno.Add(a.nombre);
                    auxIDAlumno.Add(a.rfc.ToString());
                }
                cmbIDAlumno.Items.AddRange(auxIDAlumno.ToArray());
                cmbAlumno.Items.AddRange(auxAlumno.ToArray());
                cmbAlumno.SelectedIndex = 0;
            }
            catch (Exception ex) { }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                cmbIDAlumno.SelectedIndex = cmbAlumno.SelectedIndex;
                Alumno alumno = control.consultarAlumno(cmbIDAlumno.SelectedItem.ToString());
                string grupo = control.consultarGrupoAlumno(alumno.rfc);
                string programa = control.obtenerProgramaAlumno(alumno.rfc);
                if (cmbTipoDocumento.SelectedItem.Equals("Constancia"))
                {

                }
                if (cmbTipoDocumento.SelectedItem.Equals("Kardex"))
                {
                    DocumentosWord word = new DocumentosWord(alumno, control.obtenerCalificacionesAlumno(alumno.rfc, grupo), grupo,programa);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos de l alumno");
            }
        }
    }
}
