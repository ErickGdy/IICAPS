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
using IICAPS_v1.Presentacion.Mains.Escuela;
using System.Threading;

namespace IICAPS_v1.Presentacion
{
    public partial class FormDetalleAlumno : Form
    {

        private static FormDetalleAlumno instance;
        ControlIicaps control;
        Alumno alumno;
        List<Programa> programas;
        List<string> programasAlumno;
        DocumentosInscripcion al;
        public FormDetalleAlumno(Alumno al)
        {
            InitializeComponent();
            this.alumno = al;
            control = ControlIicaps.getInstance();
            try
            {
                actualizarPanel(0);
                lblNombreHeader.Text = al.nombre;
                lblProgramaHeader.Text = control.consultarPrograma(al.programa).Nombre;
                programas = control.obtenerProgramas();
                linkLabel3_LinkClicked(null,null);
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
                
                switch (option)
                {
                    case 1:
                        llenarPanel(option);
                        panelDocumentacion.Visible = false;
                        panelInformacionPersonal.Visible = false;
                        panelSituacionAcademica.Visible = false;
                        panelInscripcion.Visible = false;
                        panelCalificaciones.Visible = true;
                        break;
                    case 2:
                        llenarPanel(option);
                        panelCalificaciones.Visible = false;
                        panelInformacionPersonal.Visible = false;
                        panelSituacionAcademica.Visible = false;
                        panelInscripcion.Visible = false;
                        panelDocumentacion.Visible = true;
                        break;
                    case 3:
                        llenarPanel(option);
                        panelCalificaciones.Visible = false;
                        panelDocumentacion.Visible = false;
                        panelSituacionAcademica.Visible = false;
                        panelInscripcion.Visible = false;
                        panelInformacionPersonal.Visible = true;
                        break;
                    case 4:
                        llenarPanel(option);
                        panelCalificaciones.Visible = false;
                        panelDocumentacion.Visible = false;
                        panelInformacionPersonal.Visible = false;
                        panelInscripcion.Visible = false;
                        panelSituacionAcademica.Visible = true;
                        break;
                    case 5:
                        llenarPanel(option);
                        panelCalificaciones.Visible = false;
                        panelDocumentacion.Visible = false;
                        panelInformacionPersonal.Visible = false;
                        panelSituacionAcademica.Visible = false;
                        panelInscripcion.Visible = true;
                        break;
                    case 6:
                        llenarPanel(option);
                        //panelInscripcion.Visible = true;
                        break;
                    default:
                        panelCalificaciones.Visible = false;
                        panelDocumentacion.Visible = false;
                        panelInformacionPersonal.Visible = false;
                        panelSituacionAcademica.Visible = false;
                        panelInscripcion.Visible = false;
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
                    try
                    {
                        DataTable dtDatos = new DataTable();
                        //Con la informacion del adaptador se llena el datatable
                        control.consultarCalificacionesAlumno(alumno.rfc, control.consultarGrupoAlumno(alumno.rfc)).Fill(dtDatos);
                        //Se asigna el datatable como origen de datos del datagridview
                        dataGridViewCalificaciones.DataSource = dtDatos;
                        //Actualiza el valor del ancho de la columnas
                        int x = (dataGridViewCalificaciones.Width - 20) / dataGridViewCalificaciones.Columns.Count;
                        foreach (DataGridViewColumn aux in dataGridViewCalificaciones.Columns)
                        {
                            aux.Width = x;
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        al = control.consultarEntregaDocumentos(alumno.rfc);
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
                        lblObservaciones.Text = alumno.observaciones;
                        lblMatricula.Text = alumno.matricula;
                    }
                    catch (Exception ex) { }
                    break;
                case 4:
                    //Generar documento Kardex
                    cmbProgramaSitacionAcademica.Items.Clear();
                    try
                    {
                        programasAlumno = control.obtenerProgramasAlumno(alumno.rfc);
                        foreach (Programa aux in programas)
                        {
                            foreach (string codigo in programasAlumno)
                            {
                                if (codigo == aux.Codigo)
                                    cmbProgramaSitacionAcademica.Items.Add(aux.Nombre);
                            }
                        }
                        try
                        {
                            cmbProgramaSitacionAcademica.SelectedItem = lblProgramaHeader.Text;
                        }
                        catch (Exception ex) { }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al obtener datos de programa del alumno");
                    }
                    break;
                case 5:
                    try
                    {
                        cmbGrupos.Items.Clear();
                        string group = control.consultarGrupoAlumno(alumno.rfc);
                        if (group != null)
                        {
                            lblMensajeInscripciones.Text = "Alumno inscrito en el grupo " + group;
                        }
                        else
                            lblMensajeInscripciones.Text = "El alumno no se encuentra inscrito en ningún grupo";
                        foreach (Grupo grupo in control.obtenerGrupos(cmbGrupos.Text, alumno.programa))
                        {
                            cmbGrupos.Items.Add(grupo.codigo + " - " + grupo.generacion);
                            if (group == grupo.codigo)
                                group = grupo.codigo + " - " + grupo.generacion;
                        }
                        cmbGrupos.SelectedItem = group;
                        if (al == null)
                            al = control.consultarEntregaDocumentos(alumno.rfc);
                    }
                    catch (Exception ex) {
                    }
                    break;
                case 6:
                    try
                    {
                        DetallePagosAlumno dtl = new DetallePagosAlumno(alumno);
                        dtl.Show();
                    }
                    catch (Exception ex)
                    {
                    }
                    break;
                default:
                    break;
            }
        }

       

        public void Main_SizeChanged(object sender, EventArgs e)
        {
            int ancho = this.Width;
            int alto = this.Height;
            //Actualiza el tamaño de la tabla con respecto al tamaño de la ventana
            groupMenus.Location = new Point(ancho - 223,groupMenus.Location.Y);
            panelCalificaciones.Size = new Size(ancho-420,alto - 130);
            panelDocumentacion.Size = new Size(ancho - 420, alto - 130);
            panelInformacionPersonal.Size = new Size(ancho - 420, alto - 130);
            panelInscripcion.Size = new Size(ancho - 420, alto - 130);
            panelSituacionAcademica.Size = new Size(ancho - 420, alto - 130);
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
            try
            {
                string programa = programasAlumno.ElementAt(cmbProgramaSitacionAcademica.SelectedIndex);
                string grupo = control.consultarGrupoAlumno(alumno.rfc);
                if (grupo != null)
                {
                    foreach (Programa aux in programas)
                    {
                        if (aux.Codigo == programa)
                        {
                            Thread t = new Thread(new ThreadStart(() => new DocumentosWord(alumno, control.obtenerCalificacionesAlumno(alumno.rfc, grupo), grupo, aux.Nombre)));
                            t.Start();
                            break;
                        }
                    }
                }else
                {
                    MessageBox.Show("El alumno no esta inscrito en ningun grupo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos del alumno");
            }
        }

        private void btnActualizarDocumentos_Click(object sender, EventArgs e)
        {
            try
            {
                DocumentosInscripcion al = control.consultarEntregaDocumentos(alumno.rfc);
                if (tabControlDocumentacion.SelectedIndex == 0)
                {
                    FormDocumentosInscripcion fa = new FormDocumentosInscripcion(al, false,lblProgramaHeader.Text, alumno.nombre);
                    fa.FormClosed += new FormClosedEventHandler(form_ClosedDocumentos);
                    fa.Show();
                }
                else
                {
                    FormDocumentosInscripcionTitulacionLicenciatura fa = new FormDocumentosInscripcionTitulacionLicenciatura(al, false, lblProgramaHeader.Text, alumno.nombre);
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

        private void linkInscripciones_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                actualizarPanel(5);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbGrupos_KeyUp(object sender, KeyEventArgs e)
        {
            if(cmbGrupos.Text.Length>2)
            try
            {
                cmbGrupos.Items.Clear();
                foreach (Grupo grupo in control.obtenerGrupos(cmbGrupos.Text, alumno.programa))
                {
                    cmbGrupos.Items.Add(grupo.codigo + " - " + grupo.generacion);
                }
                cmbGrupos.Select(cmbGrupos.Text.Length,0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos de grupos");
            }
            
        }

        private void btnInscribir_Inscripciones_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbGrupos.SelectedItem != null)
                {
                    string grupo = cmbGrupos.SelectedItem.ToString();
                    grupo = grupo.Substring(0, grupo.IndexOf(" - "));
                    if (control.inscribirAlumnoGrupo(alumno.rfc, grupo, alumno.programa))
                    {
                        MessageBox.Show("Alumno inscrito en el grupo " + grupo);
                        actualizarPanel(5);
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un grupo válido");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void moverForms(int distancia)
        {
            lblCentroAlumnado.Location= new Point(lblCentroAlumnado.Location.X - distancia, lblCentroAlumnado.Location.Y);
            lblNombreHeader.Location= new Point(lblNombreHeader.Location.X - distancia, lblNombreHeader.Location.Y);
            lblProgramaHeader.Location= new Point(lblProgramaHeader.Location.X - distancia, lblProgramaHeader.Location.Y);
            lblLineaHeader.Location= new Point(lblLineaHeader.Location.X - distancia, lblLineaHeader.Location.Y);
            groupMenus.Location = new Point(groupMenus.Location.X - distancia, groupMenus.Location.Y);
            panelCalificaciones.Location = new Point(panelCalificaciones.Location.X - distancia, panelCalificaciones.Location.Y);
            panelDocumentacion.Location = new Point(panelDocumentacion.Location.X - distancia, panelDocumentacion.Location.Y);
            panelInformacionPersonal.Location = new Point(panelInformacionPersonal.Location.X - distancia, panelInformacionPersonal.Location.Y);
            panelInscripcion.Location = new Point(panelInscripcion.Location.X - distancia, panelInscripcion.Location.Y);
            panelSituacionAcademica.Location = new Point(panelSituacionAcademica.Location.X - distancia, panelSituacionAcademica.Location.Y);
        }

        private void panelCalificaciones_SizeChanged(object sender, EventArgs e)
        {
            dataGridViewCalificaciones.Width = panelCalificaciones.Width - 30;
            dataGridViewCalificaciones.Height = panelCalificaciones.Height - 60;
            if (dataGridViewCalificaciones.Columns.Count != 0)
            {
                int x = (dataGridViewCalificaciones.Width - 20) / dataGridViewCalificaciones.Columns.Count;
                foreach (DataGridViewColumn aux in dataGridViewCalificaciones.Columns)
                {
                    aux.Width = x;
                }
            }
        }

        private void cmbProgramaSitacionAcademica_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProgramaSitacionAcademica.SelectedIndex != -1)
                btnBuscarSituacionAcademica.Enabled = true;
            else
                btnBuscarSituacionAcademica.Enabled = false;
        }

        private void cmbGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (al.validarDocumentacion())
            {
                btnInscribir_Inscripciones.Enabled = false;
                lblMensajeInscripcion.Text = "Documentacion pendiente de entrega";
            }
            else
            {
                if (control.consultarGrupoAlumno(alumno.rfc) == null)
                    if (cmbGrupos.SelectedIndex != -1)
                        btnInscribir_Inscripciones.Enabled = true;
                    else
                        btnInscribir_Inscripciones.Enabled = false;
                else
                    btnInscribir_Inscripciones.Enabled = false;
                lblMensajeInscripcion.Text = "";
            }
                
            
        }

        private void darDeBajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogresult = MessageBox.Show("¿Desea dar de baja el alumno?", "Baja de Alumno", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogresult == DialogResult.OK)
                {
                    if (control.darDeBajaAlumno(alumno.rfc))
                    {
                        MessageBox.Show("Alumno dado de baja");
                        actualizarPanel(5);
                    }
                    else
                        MessageBox.Show("Error al dar la baja del alumno");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void darDeAltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogresult = MessageBox.Show("¿Desea dar de Alta el alumno?", "Alta de Alumno", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogresult == DialogResult.OK)
                {
                    if (control.darDeAltaAlumno(alumno.rfc))
                    {
                        MessageBox.Show("Alumno dado de Alta");
                        actualizarPanel(5);
                    }
                    else
                        MessageBox.Show("Error al dar el Alta del alumno");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkHistoriaPagos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            actualizarPanel(0);
            Thread t = new Thread(new ThreadStart(ThreadMethodPagos));
            t.Start();
        }
        private void ThreadMethodPagos()
        {
            try
            {
                DetallePagosAlumno dtl = new DetallePagosAlumno(alumno);
                dtl.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabelCredito_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreditoAlumnos ca = new CreditoAlumnos(control.consultarCreditoActivoAlumno(alumno.rfc), alumno);
            ca.Show();
        }
    }
}
