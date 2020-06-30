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
                lblNombreHeader.Text = al.Nombre;
                lblProgramaHeader.Text = control.ConsultarPrograma(al.Programa).Nombre;
                programas = control.ObtenerProgramas();
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
                        control.ConsultarCalificacionesAlumno(alumno.Rfc, control.ConsultarGrupoAlumno(alumno.Rfc)).Fill(dtDatos);
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
                        al = control.ConsultarEntregaDocumentos(alumno.Rfc);
                        checkInscActaCopia.Checked = al.ActaNacimientoOrg;
                        checkInscActaOrignial.Checked = al.ActaNacimientoOrg;
                        checkInscCopiaCedula.Checked = al.CedProfCop;
                        checkInscCopiaTitulo.Checked = al.TituloLicCop;
                        checkInscCURP.Checked = al.Curp;
                        checkInscFotos.Checked = al.Fotografias;
                        checkInscTituloCedula.Checked = al.TituloCedulaOrg;
                        checkTituActa.Checked = al.ActaNacimientoOrg;
                        checkTituCopiaActa.Checked = al.ActaNacimientoCop;
                        checkTituCopiaCertificado.Checked = al.CertificadoLicCop;
                        checkTituCopiaConstancia.Checked = al.ConstanciaLibSSCop;
                        checkTituCURP.Checked = al.Curp;
                        checkTituFotos.Checked = al.Fotografias;
                        checkTituSolicitud.Checked = al.SolicitudOpcTitulacion;
                    }
                    catch (Exception ex) { }
                    break;
                case 3:
                    try { 
                        alumno = control.ConsultarAlumno(alumno.Rfc);
                        lblCarrera.Text = alumno.Carrera;
                        lblCorreo.Text = alumno.Correo;
                        lblCurp.Text = alumno.Curp;
                        lblDireccion.Text = alumno.Direccion;
                        lblEscuelaProcedencia.Text = alumno.EscuelaProcedencia;
                        lblEstadoCivil.Text = alumno.EstadoCivil;
                        lblFacebook.Text = alumno.Facebook;
                        lblNivel.Text = alumno.Nivel;
                        lblNombre.Text = alumno.Nombre;
                        lblRFC.Text = alumno.Rfc;
                        lblSexo.Text = alumno.Sexo;
                        lblTelefono1.Text = alumno.Telefono1;
                        lblTelefono2.Text = alumno.Telefono2;
                        lblObservaciones.Text = alumno.Observaciones;
                        lblMatricula.Text = alumno.Matricula;
                    }
                    catch (Exception ex) { }
                    break;
                case 4:
                    //Generar documento Kardex
                    cmbProgramaSitacionAcademica.Items.Clear();
                    try
                    {
                        programasAlumno = control.ObtenerProgramasAlumno(alumno.Rfc);
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
                        string group = control.ConsultarGrupoAlumno(alumno.Rfc);
                        if (group != null)
                        {
                            lblMensajeInscripciones.Text = "Alumno inscrito en el grupo " + group;
                        }
                        else
                            lblMensajeInscripciones.Text = "El alumno no se encuentra inscrito en ningún grupo";
                        foreach (Grupo grupo in control.ObtenerGrupos(cmbGrupos.Text, alumno.Programa))
                        {
                            cmbGrupos.Items.Add(grupo.Codigo + " - " + grupo.Generacion);
                            if (group == grupo.Codigo)
                                group = grupo.Codigo + " - " + grupo.Generacion;
                        }
                        cmbGrupos.SelectedItem = group;
                        if (al == null)
                            al = control.ConsultarEntregaDocumentos(alumno.Rfc);
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
            try
            {
                Alumnos fa = new Alumnos(alumno);
                fa.FormClosed += new FormClosedEventHandler(form_ClosedInformacion);
                fa.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al intentar abrir el formulario");
            }
        }

        private void form_ClosedInformacion(object sender, FormClosedEventArgs e)
        {
            try
            {
                alumno = control.ConsultarAlumno(alumno.Rfc);
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
                string grupo = control.ConsultarGrupoAlumno(alumno.Rfc);
                if (grupo != null)
                {
                    foreach (Programa aux in programas)
                    {
                        if (aux.Codigo == programa)
                        {
                            Thread t = new Thread(new ThreadStart(() => new DocumentosWord(alumno, control.ObtenerCalificacionesAlumno(alumno.Rfc, grupo), grupo, aux.Nombre)));
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
                DocumentosInscripcion al = control.ConsultarEntregaDocumentos(alumno.Rfc);
                if (tabControlDocumentacion.SelectedIndex == 0)
                {
                    FormDocumentosInscripcion fa = new FormDocumentosInscripcion(al, false,lblProgramaHeader.Text, alumno.Nombre);
                    fa.FormClosed += new FormClosedEventHandler(form_ClosedDocumentos);
                    fa.Show();
                }
                else
                {
                    FormDocumentosInscripcionTitulacionLicenciatura fa = new FormDocumentosInscripcionTitulacionLicenciatura(al, false, lblProgramaHeader.Text, alumno.Nombre);
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
                alumno = control.ConsultarAlumno(alumno.Rfc);
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
                foreach (Grupo grupo in control.ObtenerGrupos(cmbGrupos.Text, alumno.Programa))
                {
                    cmbGrupos.Items.Add(grupo.Codigo + " - " + grupo.Generacion);
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
                    if (control.InscribirAlumnoGrupo(alumno.Rfc, grupo, alumno.Programa))
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
            if (al.Validar_documentacion())
            {
                btnInscribir_Inscripciones.Enabled = false;
                lblMensajeInscripcion.Text = "Documentacion pendiente de entrega";
            }
            else
            {
                if (control.ConsultarGrupoAlumno(alumno.Rfc) == null)
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
                    if (control.DarDeBajaAlumno(alumno.Rfc))
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
                    if (control.DarDeAltaAlumno(alumno.Rfc))
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
            CreditoAlumnos ca = new CreditoAlumnos(control.ConsultarCreditoActivoAlumno(alumno.Rfc), alumno);
            ca.Show();
        }
    }
}
