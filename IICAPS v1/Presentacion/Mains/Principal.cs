using IICAPS.Presentacion.Mains.Administracion;
using IICAPS.Presentacion.Mains.Escuela;
using IICAPS.Presentacion.Mains.Libreria;
using IICAPS.Presentacion.Mains.Psicoterapia;
using IICAPS_v1.Control;
using IICAPS_v1.DataObject;
using IICAPS_v1.Presentacion;
using IICAPS_v1.Presentacion.Mains;
using IICAPS_v1.Presentacion.Mains.Escuela;
using IICAPS_v1.Presentacion.Mains.Psicoterapia;
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
        Usuario usuario;
        public Principal(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            btnIndex_Click(null, null);
            try
            {
                btnUsuario.Text = ControlIicaps.getInstance().consultarEmpleado(usuario.Matricula).Nombre;
            }
            catch (Exception ex)
            {
                btnUsuario.Text = usuario.Nombre_De_Usuario;
            }
            switch (usuario.Nivel_Acceso)
            {
                case 0:
                    btnMenuAdministracion.Visible = true;
                    btnMenuEscuela.Visible = true;
                    btnMenuPsicoterapia.Visible = true;
                    btnMenuLibreria.Visible = true;
                    break;
                case 3:
                    btnMenuAdministracion.Visible = false;
                    btnMenuEscuela.Visible = true;
                    btnMenuEscuela.Location = new Point(167, 0);
                    btnMenuPsicoterapia.Visible = false;
                    btnMenuLibreria.Visible = false;
                    break;
                case 2:
                    btnMenuAdministracion.Visible = false;
                    btnMenuEscuela.Visible = false;
                    btnMenuPsicoterapia.Visible = true;
                    btnMenuPsicoterapia.Location = new Point(167, 0);
                    btnMenuLibreria.Visible = false;
                    break;
                case 1:
                    btnMenuAdministracion.Visible = false;
                    btnMenuEscuela.Visible = false;
                    btnMenuPsicoterapia.Visible = false;
                    btnMenuLibreria.Visible = true;
                    btnMenuLibreria.Location = new Point(167, 0);
                    break;
                default:
                    break;

            }
        }

        
        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Contacte a soporte tecnico para restablecer contraseña");

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
        public void minimizeForms()
        {
            foreach (Form aux in MdiChildren)
            {
                aux.Hide();
            }
            inhabilitarBoton("","");

        }
        public void configurarForm(Form form)
        {
            form.MdiParent = this;
            form.Size = new Size(this.Size.Width - 20, this.Size.Height - 45);
        } 
        private void inhabilitarBoton(string boton, string modulo)
        {
            ocultarPaneles(modulo);
            btnMenuEscuela.Enabled = true;
            btnMenuPsicoterapia.Enabled = true;
            btnMenuAdministracion.Enabled = true;
            btnMenuLibreria.Enabled = true;
            try
            {
                this.btnMenuEscuela.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.btnMenuPsicoterapia.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.btnMenuAdministracion.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.btnMenuLibreria.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            catch (Exception ex) { }
            switch (modulo)
            {
                case "Escuela":
                    btnMenuEscuela.Enabled = false;
                    btnMenuEscuela.Font = new System.Drawing.Font("Montserrat", 9.749999F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    break;
                case "Psicoterapia":
                    btnMenuPsicoterapia.Enabled = false;
                    btnMenuPsicoterapia.Font = new System.Drawing.Font("Montserrat", 9.749999F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    break;
                case "Administracion":
                    btnMenuAdministracion.Enabled = false;
                    btnMenuAdministracion.Font = new System.Drawing.Font("Montserrat", 9.749999F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    break;
                case "Libreria":
                    btnMenuLibreria.Enabled = false;
                    btnMenuLibreria.Font = new System.Drawing.Font("Montserrat", 9.749999F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    break;
                default:
                    break;
            }
        }
        private void actualizarPaneles()
        {
            panelMenuEscuela.Height = this.Height - 85;
            panelMenuPsicoterapia.Height = this.Height - 85;
            panelMenuAdministracion.Height = this.Height - 85;
            panelMenuLibreria.Height = this.Height - 85;
            btnParametrosAdministracion.Location = new Point(0, this.Height - 70- 125);
            logoDisgrawMenuEscuela.Location = new Point(4, this.Height - 125);
            logoDisgrawMenuAdministracion.Location = new Point(4, this.Height - 125);
            logoDisgrawMenuPsicoterapia.Location = new Point(4, this.Height - 125);
            logoDisgrawMenuLibreria.Location = new Point(4, this.Height - 125);
        }
        private void ocultarPaneles(string modulo)
        {
            panelMenuPsicoterapia.Visible = false;
            panelMenuEscuela.Visible = false;
            panelMenuAdministracion.Visible = false;
            panelMenuLibreria.Visible = false;
            switch (modulo)
            {
                case "Escuela":
                    panelMenuEscuela.Visible = true;
                    break;
                case "Alumno":
                    panelMenuEscuela.Visible = true;
                    break;
                case "Paciente":
                    panelMenuPsicoterapia.Visible = true;
                    break;
                case "Psicoterapia":
                    panelMenuPsicoterapia.Visible = true;
                    break;
                case "Administracion":
                    panelMenuAdministracion.Visible = true;
                    break;
                case "Libreria":
                    panelMenuLibreria.Visible = true;
                    break;
                default:
                    panelMenuEscuela.Visible = false;
                    panelMenuAdministracion.Visible = false;
                    panelMenuLibreria.Visible = false;
                    panelMenuPsicoterapia.Visible = false;
                    break;
            }

        }
        private void btnIndex_Click(object sender, EventArgs e)
        {
            closeForms();
            ocultarPaneles("");
            MainIICAPS form = new MainIICAPS();
            configurarForm(form);
            form.Show();
        }
        private void Principal_SizeChanged(object sender, EventArgs e)
        {
            foreach (Form aux in MdiChildren)
            {
                configurarForm(aux);
            }
            menuStrip.Width = this.Width - 15;
            panelMenuSuperior.Width = this.Width - 15;
            actualizarPaneles();
        }


        /** ACCIONES DE BOTONES DE ESCUELA**/
        private void btnMenuEscuela_Click(object sender, EventArgs e)
        {
            minimizeForms();
            EscuelaMain form = EscuelaMain.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Escuela", "Escuela");
        }
        //EN CASO DE REQUERIR ALGO ESPECIAL PARA MAESTROS USAR ESTE BOTON
        private void btnMenuMaestros_Click(object sender, EventArgs e)
        {
            minimizeForms();
            EscuelaMain form = EscuelaMain.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Maestro", "Escuela");
        }

        private void btnAlumno_Click(object sender, EventArgs e)
        {
            minimizeForms();
            MainAlumnos form = MainAlumnos.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Alumno", "Alumno");
        }

        private void btnRegistroAlumno_Click(object sender, EventArgs e)
        {
            Alumnos fa = new Alumnos(null);
            fa.Show();
        }

        private void btnCreditoAlumno_Click(object sender, EventArgs e)
        {
            btnMenuCreditos_Click(null, null);
        }

        private void btnMenuCreditos_Click(object sender, EventArgs e)
        {
            minimizeForms();
            MainCreditos form = MainCreditos.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Credito","Alumno");
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

        private void btnGruposEscuela_Click(object sender, EventArgs e)
        {
            minimizeForms();
            MainGrupos form = MainGrupos.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Grupos", "Escuela");
        }

        private void btnDocumentacionAlumno_Click(object sender, EventArgs e)
        {
            minimizeForms();
            MainDocumentosInscripcion form = MainDocumentosInscripcion.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Documentación", "Alumno");
        }

        private void btnPagosAlumno_Click(object sender, EventArgs e)
        {
            minimizeForms();
            MainPagos form = MainPagos.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Pagos", "Alumno");
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImpresionDocumentos fa = new ImpresionDocumentos();
            fa.Show();
        }

        private void btnTalleres_Click(object sender, EventArgs e)
        {
            minimizeForms();
            MainTalleres form = MainTalleres.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Taller", "Escuela");
        }


        /** ACCIONES DE BOTONES DE PSICOTERAPIA**/
        private void btnMenuPsicoterapia_Click(object sender, EventArgs e)
        {
            minimizeForms();
            PsicoterapiaMain form = PsicoterapiaMain.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Psicoterapia", "Psicoterapia");
        }

        private void btnMenuPacientes_Click(object sender, EventArgs e)
        {
            minimizeForms();
            MainPacientes form = MainPacientes.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Paciente", "Paciente");
        }

        private void btnRegistroPacientePsicoterapia_Click(object sender, EventArgs e)
        {
            FormPaciente formAgregaPaciente = new FormPaciente(null);
            formAgregaPaciente.Show();
        }

        private void btnPsicoterapeutasAdministracion_Click(object sender, EventArgs e)
        {
            minimizeForms();
            MainPsicoterapeutas form = MainPsicoterapeutas.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Psicoterapeutas", "Administracion");
        }

        private void btnClubDeTareasPsicoterapia_Click(object sender, EventArgs e)
        {
            minimizeForms();
            MainClubDeTareas form = MainClubDeTareas.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Club De Tareas", "Psicoterapia");
        }

        private void btnPsicoterapia_Click(object sender, EventArgs e)
        {
            btnMenuPsicoterapia_Click(null, null);
        }

        private void btnAgendaPsicoterapia_Click(object sender, EventArgs e)
        {
            minimizeForms();
            MainAgenda form = MainAgenda.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Psicoterapia", "Psicoterapia");
        }
        //ACCIONES BOTONES ADMINISTRACIÓN
        private void btnParametrosAdministracion_Click(object sender, EventArgs e)
        {
            minimizeForms();
            try
            {
                FormParametrosGenerales form = FormParametrosGenerales.getInstance();
                configurarForm(form);
                form.Show();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            inhabilitarBoton("Administracion", "Administracion");
        }
        private void btnMenuAdministracion_Click(object sender, EventArgs e)
        {
            minimizeForms();
            AdministracionMain form = AdministracionMain.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Administracion", "Administracion");
        }

        private void btnEmpleadosAdministracion_Click(object sender, EventArgs e)
        {
            minimizeForms();
            MainEmpleados form = MainEmpleados.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Empleados", "Administracion");
        }

        //ACCIONES BOTONES LIBRERÍA
        private void btnMenuLibreria_Click(object sender, EventArgs e)
        {
            minimizeForms();
            LibreriaMain form = LibreriaMain.getInstance();
            configurarForm(form);
            form.Show();
            inhabilitarBoton("Libreria", "Libreria");
        }

        private void btnEscuela_Click(object sender, EventArgs e)
        {
            btnMenuEscuela_Click(null,null);
        }

        private void btnLibreria_Click(object sender, EventArgs e)
        {
            btnMenuLibreria_Click(null,null);
        }
    }
}
