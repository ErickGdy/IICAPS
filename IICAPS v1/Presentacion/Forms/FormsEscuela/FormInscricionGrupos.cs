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
    public partial class FormInscricionGrupos : Form
    {
        ControlIicaps control;
        public FormInscricionGrupos(string grupo, string alumno)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            List<String> auxNombres = new List<string>();
            List<String> auxId = new List<string>();
            foreach (Grupo p in control.obtenerGrupos())
            {
                auxNombres.Add(p.generacion);
                auxId.Add(p.codigo);
            }
            cmbGrupoNombres.DataSource = auxNombres;
            cmbGrupoID.DataSource = auxId;
            auxNombres.Clear();
            auxId.Clear();
            foreach (Alumno al in control.obtenerAlumnos())
            {
                auxNombres.Add(al.nombre);
                auxId.Add(al.rfc);
            }
            cmbAlumnoNombres.DataSource = auxNombres;
            cmbAlumnoID.DataSource = auxId;
            if (grupo!= null)
            {
                cmbGrupoNombres.Enabled = false;
                cmbGrupoID.SelectedItem = grupo;
                cmbGrupoNombres.SelectedIndex = cmbGrupoID.SelectedIndex;
            }
            if (alumno != null)
            {
                cmbAlumnoNombres.Enabled = false;
                cmbAlumnoID.SelectedItem = alumno;
                cmbAlumnoNombres.SelectedIndex = cmbAlumnoID.SelectedIndex;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            cmbGrupoID.SelectedIndex = cmbGrupoNombres.SelectedIndex;
            string grupo = cmbGrupoID.SelectedItem.ToString();
            cmbAlumnoID.SelectedIndex = cmbAlumnoNombres.SelectedIndex;
            string alumno = cmbAlumnoID.SelectedItem.ToString();
            try
            {
                if (control.inscribirAlumnoGrupo(alumno,grupo,control.consultarGrupo(grupo).programa))
                {
                    MessageBox.Show("Alumno inscrito exitosamente!");
                    Close();
                    Dispose();
                }
                else
                    MessageBox.Show("Error al guardar datos, verifique los campos y vuelva a intentarlo");
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
    }
}
