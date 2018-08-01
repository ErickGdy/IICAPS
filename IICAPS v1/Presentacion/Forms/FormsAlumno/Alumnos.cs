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
    public partial class Alumnos : Form
    {
        ControlIicaps control;
        bool modificacion = false;
        public Alumnos(Alumno a)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            cmbEstadoCivil.SelectedIndex = 0;
            cmbNivel.SelectedIndex = 0;
            List<string> lista = new List<string>();
            List<string> listaID = new List<string>();
            foreach (Programa aux in control.obtenerProgramas())
            {
                lista.Add(aux.Nombre);
                listaID.Add(aux.Codigo);
            }
            cmbPrograma.Items.AddRange(listaID.ToArray());
            cmbNombresPrograma.Items.AddRange(lista.ToArray());
            cmbNombresPrograma.SelectedIndex = 0;
            cmbSexo.SelectedIndex = 0;
            if (a != null)
            {
                modificacion = true;
                txtNombre.Text = a.nombre;
                txtDireccion.Text = a.direccion;
                txtTelefono1.Text = a.telefono1;
                txtTelefono2.Text = a.telefono2;
                txtCorreo.Text = a.correo;
                txtFacebook.Text = a.facebook;
                txtCURP.Text = a.curp;
                txtRFC.Text = a.rfc;
                cmbSexo.SelectedItem = a.sexo;
                cmbEstadoCivil.SelectedItem = a.estadoCivil;
                txtEscuelaProcedencia.Text = a.escuelaProcedencia;
                txtCarrera.Text = a.carrera;
                cmbNivel.SelectedItem = a.nivel;
                cmbPrograma.SelectedItem = a.programa;
                cmbNombresPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
                label12.Text = "Programa actual";
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!agregarAlumno())
                    MessageBox.Show("Error al guardar los datos del alumno");
                else
                {
                    MessageBox.Show("Datos del Alumno guardados exitosamente");
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
            if (txtNombre.Text != "" && txtDireccion.Text != "" && txtCorreo.Text != "" && txtTelefono1.Text != "" && txtEscuelaProcedencia.Text != "" && txtCarrera.Text != "")
                return true;
            return false;
        }

        private bool agregarAlumno()
        {
            if (validarCampos())
            {
                Alumno a = new Alumno();
                a.nombre = txtNombre.Text;
                a.direccion = txtDireccion.Text;
                a.telefono1 = txtTelefono1.Text;
                a.telefono2 = txtTelefono2.Text;
                a.correo = txtCorreo.Text;
                a.facebook = txtFacebook.Text;
                a.curp = txtCURP.Text;
                a.rfc = txtRFC.Text;
                a.sexo = cmbSexo.SelectedItem.ToString();
                a.estadoCivil = cmbEstadoCivil.SelectedItem.ToString();
                a.escuelaProcedencia = txtEscuelaProcedencia.Text;
                a.carrera = txtCarrera.Text;
                a.nivel = cmbNivel.SelectedItem.ToString();
                a.programa = cmbPrograma.SelectedItem.ToString();
                if (modificacion)
                {
                    a.rfc = txtRFC.Text;
                    if (control.actualizarAlumno(a))
                        return true;
                    else
                        throw new Exception("Error al actualizar los datos del alumno");
                }
                else
                {
                    if (control.agregarAlumno(a))
                        return true;
                    else
                        throw new Exception("Error al agregar el alumno");
                }
            }
            else
                MessageBox.Show("No deje los campos marcados con * vacios");
            return false;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtTelefono1.Text = "";
            txtTelefono2.Text = "";
            txtCorreo.Text = "";
            txtFacebook.Text = "";
            txtCURP.Text = "";
            txtRFC.Text = "";
            cmbSexo.Text = "";
            cmbEstadoCivil.SelectedItem = "";
            txtEscuelaProcedencia.Text = "";
            txtCarrera.Text = "";
            cmbNivel.SelectedItem = "";
            cmbPrograma.SelectedItem = "";
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            tabDatosAlumno.SelectTab(tabDatosAlumno.SelectedIndex + 1);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            tabDatosAlumno.SelectTab(tabDatosAlumno.SelectedIndex - 1);
        }

        private void cmbNombresPrograma_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbPrograma.SelectedIndex = cmbNombresPrograma.SelectedIndex;
        }
    }
}
