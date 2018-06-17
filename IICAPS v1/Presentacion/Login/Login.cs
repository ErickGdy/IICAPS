using IICAPS.Presentacion.Mains;
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
        public Login()
        {
            InitializeComponent();
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
            bntPagos p = new bntPagos();
            p.Show();
            this.Hide();
        }

        private void checkRecordar_CheckedChanged(object sender, EventArgs e)
        {
            //Escribir usuario en un txt
        }

        private void olvidarPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
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
    }
}
