using IICAPS_v1.Control;
using IICAPS_v1.DataObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IICAPS_v1.Presentacion
{
    public partial class FormInscricionClubDeTareas : Form
    {
        ControlIicaps control;
        List<String> auxId;
        ClubDeTareas clubDeTareas;
        ClubDeTareasAsistente asistenT;
        Pago pago;
        public FormInscricionClubDeTareas(string idClubDeTareas, ClubDeTareasAsistente asistente)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            List<String> auxNombres = new List<string>();
            auxId = new List<string>();
            try
            {
                foreach (ClubDeTareas p in control.ObtenerClubDeTareas())
                {
                    auxNombres.Add(p.Fecha.ToShortDateString());
                    auxId.Add(p.ID.ToString());
                }
                cmbClubDeTareas.DataSource = auxNombres;
                if (idClubDeTareas != null)
                {
                    cmbClubDeTareas.SelectedIndex = auxId.IndexOf(idClubDeTareas);
                    cmbClubDeTareas.Enabled = false;
                }
            }
            catch (Exception ex)
            {
            }
            if (asistente != null)
            {
                asistenT = asistente;
                cmbClubDeTareas.SelectedIndex = auxId.IndexOf(asistente.Club_Tareas_ID.ToString());
                txtNombre.Text = asistente.Nombres;
                txtApellidos.Text = asistente.Apellidos;
                txtNombre_Tutor.Text = asistente.NombreTutor;
                txtTelefono_Tutor.Text = asistente.TelefonoTutor;
                txtAnticipo.Value = asistente.Pago;
                lblAnticipo.Text="Pagó: $";
                txtAnticipo.ReadOnly = true;
                txtCosto.Value = asistente.Costo;
                txtObservaciones.Text = asistente.Observaciones;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
                asistenT.Club_Tareas_ID = Convert.ToInt32(auxId.ElementAt(cmbClubDeTareas.SelectedIndex));
                asistenT.Nombres = txtNombre.Text;
                asistenT.Apellidos = txtApellidos.Text;
                asistenT.NombreTutor = txtNombre_Tutor.Text;
                asistenT.TelefonoTutor = txtTelefono_Tutor.Text;
                asistenT.Pago = txtAnticipo.Value;
                asistenT.Costo = txtCosto.Value;
                asistenT.Observaciones = txtObservaciones.Text;
                try
                {
                if (control.RegistrarAsistenteClubDeTareas(asistenT))
                {
                    MessageBox.Show("Asistencia registrada exitosamente!");
                    this.Hide();
                    if (!txtAnticipo.ReadOnly)
                    {
                        FormPago fp = new FormPago(asistenT.Pago, "Pago de Club De Tareas", "Psicoterapia");
                        fp.ShowDialog();
                        pago = fp.getPagos();
                        if (control.RegistrarPagoAsistenciaClubDeTareas(pago, control.ObtenerAsistentesClubDeTareas(asistenT.Club_Tareas_ID.ToString()).Last().ID.ToString()))
                        {
                            MessageBox.Show("Pago registrado exitosamente");
                            Thread t = new Thread(new ThreadStart(ThreadMethodDocumentos));
                            t.Start();
                        }
                        else
                            throw new Exception("Error al registrar pago");
                    }
                    Close();
                    Dispose();
                }
                else
                    MessageBox.Show("Error al guardar datos, verifique los campos y compruebe que el asistente no se encuentre registrado y vuelva a intentarlo");
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


        private void cmbClubDeTareas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clubDeTareas = control.ConsultarClubDeTareas(auxId.ElementAt(cmbClubDeTareas.SelectedIndex));
                txtCosto.Value = clubDeTareas.Costo;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al obtener datos del club de tareas");
            }
        }
        private void ThreadMethodDocumentos()
        {
            DocumentosWord word = new DocumentosWord(pago);
        }
    }
}
