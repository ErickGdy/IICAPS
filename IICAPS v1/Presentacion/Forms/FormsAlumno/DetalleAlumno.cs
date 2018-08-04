using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IICAPS_v1.Control;
using IICAPS_v1.DataObject;
using IICAPS_v1.Presentacion;
using MySql.Data.MySqlClient;

namespace IICAPS_v1.Presentacion
{
    public partial class FormDetalleAlumno : Form
    {

        private static FormDetalleAlumno instance;
        ControlIicaps control;
        Alumno alumno;
        public FormDetalleAlumno(Alumno al)
        {
            InitializeComponent();
            this.alumno = al;
            control = ControlIicaps.getInstance();
            try
            {
                actualizarPanel(0);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static FormDetalleAlumno getInstance(Alumno al)
        {
            if (instance == null)
                instance = new FormDetalleAlumno(al);
            return instance;
        }

        private void actualizarPanel(int option)
        {
            try
            {
                panelCalificaciones.Visible = false;
                panelDocumentacion.Visible = false;
                panelInformacionPersonal.Visible = false;
                panelSituacionAcademica.Visible = false;
                switch (option)
                {
                    case 1:
                        panelCalificaciones.Visible = true;
                        llenarPanel(option);
                        break;
                    case 2:
                        panelDocumentacion.Visible = true;
                        llenarPanel(option);
                        break;
                    case 3:
                        panelInformacionPersonal.Visible = true;
                        llenarPanel(option);
                        break;
                    case 4:
                        panelSituacionAcademica.Visible = true;
                        llenarPanel(option);
                        break;
                    default:
                        panelCalificaciones.Visible = false;
                        panelDocumentacion.Visible = false;
                        panelInformacionPersonal.Visible = false;
                        panelSituacionAcademica.Visible = false;
                        break;

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void llenarPanel(int option)
        {
            switch (option)
            {
                case 1:
                    panelCalificaciones.Visible = true;
                    
                    break;
                case 2:
                    panelDocumentacion.Visible = true;
                    try
                    {
                        DocumentosInscripcion al = control.consultarEntregaDocumentos(alumno.rfc);
                        checkInscActaCopia.Checked = al.actaNacimientoOrg;
                        checkInscActaOrignial.Checked = al.actaNacimientoOrg;
                        checkInscCopiaCedula.Checked = al.cedProfCop;
                        checkInscCopiaTitulo.Checked = al.tituloLicCop;
                        checkInscCURP.Checked = al.curp;
                        checkInscFotos.Checked = al.fotografias;
                        checkInscTituloCedula.Checked = al.tituloCedulaOrg;
                        checkTituActa.Checked = al.actaNacimientoOrg;
                        checkTituCopiaActa.Checked = al.actaNacimientoCop;
                        checkTituCopiaCertificado.Checked = al.certificadoLicCop;
                        checkTituCopiaConstancia.Checked = al.constanciaLibSSCop;
                        checkTituCURP.Checked = al.curp;
                        checkTituFotos.Checked = al.fotografias;
                        checkTituSolicitud.Checked = al.solicitudOpcTitulacion;
                    }
                    catch (Exception ex) { }
                    break;
                case 3:
                    panelInformacionPersonal.Visible = true;
                    try { 
                        alumno = control.consultarAlumno(alumno.rfc);
                        lblCarrera.Text = alumno.carrera;
                        lblCorreo.Text = alumno.correo;
                        lblCurp.Text = alumno.curp;
                        lblDireccion.Text = alumno.direccion;
                        lblEscuelaProcedencia.Text = alumno.escuelaProcedencia;
                        lblEstadoCivil.Text = alumno.estadoCivil;
                        lblFacebook.Text = alumno.facebook;
                        lblNivel.Text = alumno.nivel;
                        lblNombre.Text = alumno.nombre;
                        lblRFC.Text = alumno.rfc;
                        lblSexo.Text = alumno.sexo;
                        lblTelefono1.Text = alumno.telefono1;
                        lblTelefono2.Text = alumno.telefono2;
                    }
                    catch (Exception ex) { }
                    break;
                case 4:
                    panelSituacionAcademica.Visible = true;
                    
                    break;
                default:
                    break;
            }
        }

       

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            int ancho = this.Width;
            //Actualiza el tamaño de la tabla con respecto al tamaño de la ventana
            //dataGridViewAlumnos.Width = ancho - 195;
            //dataGridViewAlumnos.Height = this.Height - 130;
            ////actualiza la posicion de los controles con respecto al tamaño de la ventana
            //btnAgregar.Location = new Point(ancho - 169, btnAgregar.Location.Y);
            //txtBuscarAlumno.Location = new Point(ancho - 216, txtBuscarAlumno.Location.Y);
            //pictureBox2.Location = new Point(ancho - 245, pictureBox2.Location.Y);
            //limpiarBusqueda.Location = new Point(ancho - 39, limpiarBusqueda.Location.Y);
            ////Actualiza el valor del ancho de la columnas
            //if (dataGridViewAlumnos.Columns.Count != 0)
            //{
            //    int x = (dataGridViewAlumnos.Width - 20) / dataGridViewAlumnos.Columns.Count;
            //    foreach (DataGridViewColumn aux in dataGridViewAlumnos.Columns)
            //    {
            //        aux.Width = x;
            //    }
            //}
        }

        private void btnActualizarInformacionPersonal_Click(object sender, EventArgs e)
        {
            Alumnos fa = new Alumnos(alumno);
            fa.FormClosed += new FormClosedEventHandler(form_ClosedInformacion);
            fa.Show();
        }

        private void form_ClosedInformacion(object sender, FormClosedEventArgs e)
        {
            try
            {
                alumno = control.consultarAlumno(alumno.rfc);
                actualizarPanel(3);
            }
            catch (Exception ex)
            {

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                actualizarPanel(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                actualizarPanel(2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                actualizarPanel(3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                actualizarPanel(4);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuscarSituacionAcademica_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Generando pdf con situacion académica");
        }

        private void btnActualizarDocumentos_Click(object sender, EventArgs e)
        {
            try
            {
                DocumentosInscripcion al = control.consultarEntregaDocumentos(alumno.rfc);
                if (tabControlDocumentacion.SelectedIndex == 0)
                {
                    FormDocumentosInscripcion fa = new FormDocumentosInscripcion(al);
                    fa.FormClosed += new FormClosedEventHandler(form_ClosedDocumentos);
                    fa.Show();
                }
                else
                {
                    FormDocumentosInscripcionTitulacionLicenciatura fa = new FormDocumentosInscripcionTitulacionLicenciatura(al);
                    fa.FormClosed += new FormClosedEventHandler(form_ClosedDocumentos);
                    fa.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos de documentos");
            }
        }

        private void form_ClosedDocumentos(object sender, FormClosedEventArgs e)
        {
            try
            {
                alumno = control.consultarAlumno(alumno.rfc);
                actualizarPanel(2);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
