﻿using IICAPS.Presentacion.Mains.Escuela;
using IICAPS_v1.Control;
using IICAPS_v1.Presentacion;
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

namespace IICAPS.Presentacion.Mains
{
    public partial class Principal : Form
    {
        
        public Principal()
        {
            InitializeComponent();
            
        }

        
        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //LoginCambiarContrasena lc = new LoginCambiarContrasena(user);
            //lc.Show();
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /** Metodos de control**/
        private void closeForms()
        {
            foreach (Form aux in MdiChildren)
            {
                aux.Dispose();
            }
            inhabilitarBoton("","");
        }
        private void minimizeForms()
        {
            foreach (Form aux in MdiChildren)
            {
                aux.Hide();
            }
            inhabilitarBoton("","");

        }
        private void configurarForm(Form form)
        {
            form.MdiParent = this;
            form.Size = new Size(this.Size.Width - 20, this.Size.Height - 45);
        } 

        private void inhabilitarBoton(string boton, string modulo)
        {
            ocultarPaneles(modulo);
            btnMenuEscuela.Enabled = true;
            this.btnMenuEscuela.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnMenuAlumnos.Enabled = true;
            this.btnMenuAlumnos.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            switch (modulo)
            {
                case "Escuela":
                    btnMenuEscuela.Enabled = false;
                    btnMenuEscuela.Font = new System.Drawing.Font("Montserrat", 9.749999F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    break;
                case "Alumno":
                    btnMenuAlumnos.Enabled = false;
                    btnMenuAlumnos.Font = new System.Drawing.Font("Montserrat", 9.749999F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    break;
                default:
                    break;
            }
        }

        private void Principal_SizeChanged(object sender, EventArgs e)
        {
            foreach (Form aux in MdiChildren)
            {
                configurarForm(aux);
            }
            menuStrip.Width = this.Width-15;
            panelMenuSuperior.Width = this.Width-15;
            actualizarPaneles();
        }

        private void actualizarPaneles()
        {
            panelMenuEscuela.Height = this.Height - 85;
            logoDisgrawMenuEscuela.Location = new Point(4, this.Height - 125);
        }
        private void ocultarPaneles(string modulo)
        {
            switch (modulo)
            {
                case "Escuela":
                    panelMenuEscuela.Visible = true;

                    break;
                case "Alumno":
                    panelMenuEscuela.Visible = true;

                    break;
                default:
                    panelMenuEscuela.Visible = false;
                    break;
            }

        }
        private void btnIndex_Click(object sender, EventArgs e)
        {
            closeForms();
            ocultarPaneles("");
        }


        private void btnMenuEscuela_Click(object sender, EventArgs e)
        {
            minimizeForms();
            EscuelaMain form = EscuelaMain.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Escuela", "Escuela");
        }

        
        private void btnMenuAlumnos_Click(object sender, EventArgs e)
        {
            minimizeForms();
            MainAlumnos form = MainAlumnos.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Alumno", "Alumno");
        }

        private void btnAlumno_Click(object sender, EventArgs e)
        {
            btnMenuAlumnos_Click(null,null);
        }

        private void btnRegistroAlumno_Click(object sender, EventArgs e)
        {
            Alumnos fa = new Alumnos(null);
            fa.Show();
        }

        private void btnProgramasEscuela_Click(object sender, EventArgs e)
        {
            minimizeForms();
            MainProgramas form = MainProgramas.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Programas", "Escuela");
        }

        private void btnMateriasEscuela_Click(object sender, EventArgs e)
        {
            minimizeForms();
            MainMaterias form = MainMaterias.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Materias", "Escuela");
        }
    }
}
