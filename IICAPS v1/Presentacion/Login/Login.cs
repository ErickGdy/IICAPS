using IICAPS.Presentacion.Mains;
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

namespace IICAPS.Presentacion
{
    public partial class Login : Form
    {
        ControlIicaps control;
        public Login()
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            try
            {
                string user = control.leerUserDoc();
                if (user != "")
                {
                    txtUsuario.Text = user;
                    txtUsuario.ForeColor = Color.Black;
                    txtPass.Focus();
                }else
                    txtUsuario.Focus();
            }
            catch (Exception ex) { }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "Usuario")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.Black;
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "Usuario";
                txtUsuario.ForeColor = Color.Gray;
            }
        }
        private void textPass_Enter(object sender, EventArgs e)
        {
            if (txtPass.Text == "Contraseña")
            {
                txtPass.Text = "";
                txtPass.ForeColor = Color.Black;
                this.txtPass.PasswordChar = '*';
            }
        }
        private void textPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.Text = "Contraseña";
                txtPass.ForeColor = Color.Gray;
                txtPass.PasswordChar = '\0';
            }
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text != "" && txtPass.Text != "")
            {
                //Validaciones
                try
                {
                    Usuario user = control.consultarUsuario(txtUsuario.Text);
                    if (user == null)
                        MessageBox.Show("Usuario invalido");
                    else
                        if (user.Contrasena != txtPass.Text)
                        MessageBox.Show("Contraseña incorrecta");
                    else
                        abrirVentanaPrincipal(user);
                }
                catch (Exception ex)
                {
                    //Si fallo arroja una excepcion y la mostramos en un label
                    MessageBox.Show("Ha ocurrido un error al intentar acceder a la base de datos");
                }
                //Si se agregó mostramos el mensaje en un label

            }
            else
            {
                MessageBox.Show("Ingrese usuario y contraseña");
            }

        }

        private void olvidarPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Contacte a soporte tecnico para restablecer contraseña");
            //LoginOlvidarContrasena lc = new LoginOlvidarContrasena();
            //lc.Show();
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
        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

            if (e.KeyChar == '\r')
            {
                btnIniciarSesion_Click(null,null);
            }
        }

        private void abrirVentanaPrincipal(Usuario user)
        {
            if (checkRecordar.Checked)
                control.recordarUsuario(txtUsuario.Text);
            else
                control.recordarUsuario(null);

            Principal p = new Principal(user);
            p.Show();
            this.Hide();
        }
    }
}