using IICAPS_v1.Control;
using IICAPS_v1.DataObject;
using IICAPS_v1.Presentacion.Mains.Escuela;
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
    public partial class FormHistorialPagosAlumno : Form
    {
        ControlIicaps control;
        public FormHistorialPagosAlumno(string programa, string alumno)
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
            List<String> auxConcepto = new List<string>();
            foreach (string c in control.obtenerConceptosDePagoAlumno("Escuela"))
            {
                auxConcepto.Add(c);
            }
            cmbConcepto.Items.AddRange(auxConcepto.ToArray());
            if(programa!=null && alumno != null)
            {
                cmbIDPrograma.SelectedItem = programa;
                cmbPrograma.SelectedIndex = cmbIDPrograma.SelectedIndex;
                cmbPrograma.Visible = false;
                cmbIDAlumno.SelectedItem = alumno;
                cmbAlumno.SelectedIndex = cmbIDAlumno.SelectedIndex;
                cmbAlumno.Visible = false;
                cmbConcepto.SelectedIndex = 0;
                txtAlumno.Text = cmbAlumno.SelectedItem.ToString();
                txtPrograma.Text = cmbPrograma.SelectedItem.ToString();
                txtAlumno.Visible = true;
                txtPrograma.Visible = true;
            }
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
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                String rfc = cmbIDAlumno.SelectedItem.ToString();
                String concepto = cmbConcepto.SelectedItem.ToString();
                DetallePagosAlumno fa = new DetallePagosAlumno(rfc, concepto);
                fa.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
