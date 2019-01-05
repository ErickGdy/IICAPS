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
    public partial class FormSesiones : Form
    {
        ControlIicaps control;
        Paciente paciente;
        List<string> empleadosNombres = new List<string>();
        List<string> empleadosID = new List<string>();
        List<string> pacientesID = new List<string>();
        List<string> pacientesNombre = new List<string>();
        Sesion sesion;
        public FormSesiones(string id, Sesion sesion_aux)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            try
            {
                foreach (Psicoterapeuta item in control.obtenerPsicoterapeutas())
                {
                    empleadosNombres.Add(item.Nombre);
                    empleadosID.Add(item.Matricula);
                }
                cmbPsicoterapeutas.DataSource = empleadosNombres;
                foreach (Paciente item in control.obtenerPacientes())
                {
                    pacientesNombre.Add(item.nombre + ' ' + item.apellidos);
                    pacientesID.Add(item.id.ToString());
                }
                cmbPaciente.DataSource = pacientesNombre;
                if (id != null)
                {
                    this.paciente = control.consultarPaciente(id);
                    txtCosto.Value = paciente.costoEspecial;
                    if (paciente.psicoterapeuta != null) 
                        cmbPsicoterapeutas.SelectedIndex = empleadosID.IndexOf(paciente.psicoterapeuta);
                    cmbPaciente.SelectedIndex = pacientesID.IndexOf(id);
                    cmbPaciente.Enabled = false;
                }
                if (sesion_aux != null)
                {
                    sesion = sesion_aux;
                    txtCosto.Value = sesion_aux.Costo;
                    cmbPsicoterapeutas.SelectedIndex = empleadosID.IndexOf(sesion_aux.psicoterapeuta.ToString());
                    cmbPaciente.SelectedIndex = pacientesID.IndexOf(sesion_aux.paciente.ToString());
                    txtFecha.Value = sesion_aux.fecha;
                    txtHora.Value = new DateTime(2000, 01, 01, sesion_aux.hora.Hours, sesion_aux.hora.Minutes, 0);
                    txtObservaciones.Text = sesion_aux.observaciones;
                    txtTipo.Text = sesion_aux.tipo;
                }
                else
                    sesion = new Sesion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos del paciente, introduzcalos manualmente");
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarCamposPaciente())
            {
               
                try
                {

                    sesion.observaciones= txtObservaciones.Text;
                    sesion.paciente = Convert.ToInt32(pacientesID.ElementAt(cmbPaciente.SelectedIndex));
                    sesion.psicoterapeuta = empleadosID.ElementAt(cmbPsicoterapeutas.SelectedIndex);
                    sesion.Costo= Convert.ToDecimal(txtCosto.Value);
                    sesion.tipo = txtTipo.Text;
                    sesion.fecha = txtFecha.Value;
                    sesion.hora = new TimeSpan(txtHora.Value.Hour, txtHora.Value.Minute, 0);
                    try
                    {
                        sesion.reservacion = control.consultarReservacion(sesion.hora, sesion.fecha,sesion.psicoterapeuta.ToString());
                        if(sesion.reservacion==null)
                            MessageBox.Show("No existe reservación previa para la sesión");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No existe reservación previa para la sesión");
                    }
                    if (sesion.id != 0)
                    {
                        if (control.actualizarSesion(sesion))
                        {
                            MessageBox.Show("Datos actualizados exitosamente!");
                            Close();
                            Dispose();
                        }
                        else
                            MessageBox.Show("Error al guardar datos, verifique los campos y vuelva a intentarlo");
                    }else
                    {
                        if (control.agregarSesion(sesion))
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
            }
            else
                MessageBox.Show("No dejar campos vacios");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool validarCamposPaciente()
        {
            if (cmbPaciente.SelectedIndex != -1 && txtFecha.Text != "" && cmbPsicoterapeutas.SelectedIndex!=-1 && txtCosto.Value != 0)
                return true;
            return false;
        }
    }
}
