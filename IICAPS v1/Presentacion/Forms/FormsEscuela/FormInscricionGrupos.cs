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
            try
            {
                List<String> auxNombres = new List<string>();
                List<String> auxId = new List<string>();
                foreach (Grupo p in control.ObtenerGrupos())
                {
                    auxNombres.Add(p.Generacion);
                    auxId.Add(p.Codigo);
                }
                cmbGrupoNombres.DataSource = auxNombres;
                cmbGrupoID.DataSource = auxId;
                cmbGrupoNombres.SelectedIndex = 0;
                auxNombres.Clear();
                auxId.Clear();
                foreach (Alumno al in control.ObtenerAlumnos())
                {
                    auxNombres.Add(al.Nombre);
                    auxId.Add(al.Rfc);
                }
                cmbAlumnoNombres.DataSource = auxNombres;
                cmbAlumnoID.DataSource = auxId;
                cmbAlumnoNombres.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("No se ha registrado ningún programa y/o grupo", "Error al obtener datos de programas y/o grupos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnAceptar.Enabled = false;
            }
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
                if (control.InscribirAlumnoGrupo(alumno,grupo,control.ConsultarGrupo(grupo).Programa))
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
