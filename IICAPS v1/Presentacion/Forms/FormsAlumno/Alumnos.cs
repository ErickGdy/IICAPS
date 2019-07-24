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
            foreach (Programa aux in control.ObtenerProgramas())
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
                txtNombre.Text = a.Nombre;
                txtDireccion.Text = a.Direccion;
                txtTelefono1.Text = a.Telefono1;
                txtTelefono2.Text = a.Telefono2;
                txtCorreo.Text = a.Correo;
                txtFacebook.Text = a.Facebook;
                txtCURP.Text = a.Curp;
                txtRFC.Text = a.Rfc;
                cmbSexo.SelectedItem = a.Sexo;
                cmbEstadoCivil.SelectedItem = a.EstadoCivil;
                txtEscuelaProcedencia.Text = a.EscuelaProcedencia;
                txtCarrera.Text = a.Carrera;
                cmbNivel.SelectedItem = a.Nivel;
                cmbPrograma.SelectedItem = a.Programa;
                cmbNombresPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
                label12.Text = "Programa actual";
                txtObservaciones.Text = a.Observaciones;
                txtMatricula.Text = a.Matricula;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (agregarAlumno()) { 
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
            if (txtCURP.Text != "" && txtRFC.Text != "" && txtNombre.Text != "" && txtDireccion.Text != "" && txtCorreo.Text != "" && txtTelefono1.Text != "" && txtEscuelaProcedencia.Text != "" && txtCarrera.Text != "")
                return true;
            return false;
        }

        private bool agregarAlumno()
        {
            if (validarCampos())
            {
                Alumno a = new Alumno();
                a.Nombre = txtNombre.Text;
                a.Direccion = txtDireccion.Text;
                a.Telefono1 = txtTelefono1.Text;
                if (txtTelefono2.Text == null || txtTelefono2.Text == "")
                    a.Telefono2 = " ";
                else
                    a.Telefono2 = txtTelefono2.Text;
                a.Correo = txtCorreo.Text;
                a.Facebook = txtFacebook.Text;
                a.Curp = txtCURP.Text;
                a.Rfc = txtRFC.Text;
                a.Sexo = cmbSexo.SelectedItem.ToString();
                a.EstadoCivil = cmbEstadoCivil.SelectedItem.ToString();
                a.EscuelaProcedencia = txtEscuelaProcedencia.Text;
                a.Carrera = txtCarrera.Text;
                a.Nivel = cmbNivel.SelectedItem.ToString();
                a.Programa = cmbPrograma.SelectedItem.ToString();
                a.Tipo = "Regular";
                a.Estado = "Registrado";
                a.Fecha = DateTime.Now;
                a.Observaciones = txtObservaciones.Text;
                a.Matricula = txtMatricula.Text;
                if (modificacion)
                {
                    a.Rfc = txtRFC.Text;
                    if (control.ActualizarAlumno(a))
                        return true;
                    else
                        throw new Exception("Error al actualizar los datos del alumno");
                }
                else
                {
                    if (control.AgregarAlumno(a))
                        return true;
                    else
                        throw new Exception("Error al agregar el alumno");
                }
            }
            else
                throw new Exception("No deje los campos marcados con * vacios");
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

        private void button1_Click(object sender, EventArgs e)
        {
            tabDatosAlumno.SelectTab(tabDatosAlumno.SelectedIndex - 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabDatosAlumno.SelectTab(tabDatosAlumno.SelectedIndex + 1);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://consultas.curp.gob.mx/CurpSP/inicio2_2.jsp");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.mi-rfc.com.mx/consulta-rfc-homoclave");
        }

        private void onlyNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            };
        }


        private void noSpaces_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
    }
}
