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
    public partial class FormEmpleados : Form
    {
        ControlIicaps control;
        Usuario usuario;
        Empleado empleado;  
        public FormEmpleados(Empleado empleado, Usuario user)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            if (empleado == null)
                this.empleado = new Empleado();
            else
            {
                this.empleado = empleado;
                txtMatricula.Text = empleado.Matricula;
                txtNombre.Text = empleado.Nombre;
                //txtFecha.Value = empleado.fecha;
                txtCorreo.Text = empleado.Correo;
                txtTelefono.Text = empleado.Telefono;
                txtPuesto.Text = empleado.Puesto;
            }
            if (user != null)
            {
                this.usuario = user;
                checkBox1.Checked = true;
                cmbNivelAcceso.SelectedIndex = user.Nivel_Acceso;
                txtUsuario.Text = user.Nombre_De_Usuario;
                txtContraseña.Text = user.Contrasena;
                txtContraseña2.Text = user.Contrasena;
            }
            else
                this.usuario = new Usuario();

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                if (validarContraseña())
                {
                    if(txtMatricula.Text=="")
                        GenerarMatricula();
                    empleado.Matricula = txtMatricula.Text;
                    empleado.Nombre = txtNombre.Text;
                    empleado.Telefono = txtTelefono.Text;
                    empleado.Correo = txtCorreo.Text;
                    empleado.Puesto = txtPuesto.Text;
                    Usuario user_aux = usuario;
                    if (checkBox1.Checked)
                    {
                        usuario.Matricula = txtMatricula.Text;
                        usuario.Nombre_De_Usuario = txtUsuario.Text;
                        usuario.Contrasena = txtContraseña.Text;
                        usuario.Nivel_Acceso = cmbNivelAcceso.SelectedIndex;
                    }
                    else
                    {
                        if (usuario.Matricula != null)
                        {
                            try
                            {
                                if (control.EliminarUsuario(usuario.Matricula))
                                    MessageBox.Show("Usuario desactivado!");
                                else
                                    MessageBox.Show("Error al desactivar usuario!");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        user_aux = null;
                    }
                    try
                    {
                        if (this.empleado.ID != 0)
                        {
                            if (control.ActualizarEmpleado(this.empleado, user_aux))
                            {
                                MessageBox.Show("Datos de empleado actualizados exitosamente!");
                                Close();
                                Dispose();
                            }
                            else
                                MessageBox.Show("Error al guardar datos, verifique los campos y vuelva a intentarlo");
                        }
                        else
                        {
                            if (control.AgregarEmpleado(this.empleado, user_aux))
                            {
                                MessageBox.Show("Datos guardados exitosamente!");
                                Close();
                                Dispose();
                            }
                            else
                                MessageBox.Show("Error al guardar datos, verifique los campos y vuelva a intentarlo");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }else
                    MessageBox.Show("La contraseña no coincide, verifique los campos y vuelva a intentarlo");
            }
            else
                MessageBox.Show("No dejar campos vacios");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool validarCampos()
        {
            if (txtNombre.Text != "" && txtTelefono.Text != "" && txtCorreo.Text != "" )
                return true;
            return false;
        }
        private bool validarContraseña()
        {
            if (checkBox1.Checked)
            {
                if (txtContraseña.Text == txtContraseña2.Text)
                    return true;
                else
                {
                    txtContraseña.Text = "";
                    txtContraseña2.Text = "";
                    return false;
                }
            }
            else
                return true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtUsuario.Enabled = true;
                txtContraseña.Enabled = true;
                txtContraseña2.Enabled = true;
                cmbNivelAcceso.Enabled = true;
            }else
            {
                txtUsuario.Enabled = false;
                txtContraseña.Enabled = false;
                txtContraseña2.Enabled = false;
                cmbNivelAcceso.Enabled = false;
            }
        }

        private string GenerarMatricula()
        {
            string matricula = "";
            string validacion = "";
            do {
                string[] nombre = txtNombre.Text.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string iniciales = "";
                int numeroLetra = 0;
                do
                {
                    for (int i = 0; i < nombre.Count(); i++)
                    {
                        try
                        {
                            iniciales += nombre[i].Substring(numeroLetra, 1);
                        }
                        catch (Exception re)
                        {
                            iniciales += nombre[i].Substring(nombre[i].Length-1, 1);
                        }
                    }
                    numeroLetra++;
                } while (iniciales.Length < 4);
                Random rdm = new Random();
                matricula =rdm.Next(1, 99).ToString("00") + "-" + iniciales.Substring(0,4).ToUpper() + "-" + rdm.Next(1, 9999).ToString("0000");
                validacion = control.ValidarMatricula(matricula);
            } while (validacion!=null);
            txtMatricula.Text= matricula;
            return matricula;
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
