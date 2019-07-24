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
    public partial class FormEvaluacion : Form
    {
        ControlIicaps control;
        Paciente paciente;
        List<string> empleadosID = new List<string>();
        List<string> pacientesID = new List<string>();
        Evaluacion evaluacion;
        public FormEvaluacion(string id, Evaluacion eval, bool consultar)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            try
            {
                List<string> empleadosNombres = new List<string>();
                foreach (Psicoterapeuta item in control.ObtenerPsicoterapeutas())
                {
                    empleadosNombres.Add(item.Nombre);
                    empleadosID.Add(item.Matricula);
                }
                cmbPsicoterapeutas.DataSource = empleadosNombres;
                List<string> pacientesNombre = new List<string>();
                foreach (Paciente item in control.ObtenerPacientes())
                {
                    pacientesNombre.Add(item.Nombre + ' ' + item.Apellidos);
                    pacientesID.Add(item.Id.ToString());
                }
                cmbPaciente.DataSource = pacientesNombre;
                if (id != null)
                {
                    this.paciente = control.ConsultarPaciente(id);
                    txtCosto.Value = paciente.CostoEspecial;
                    if (paciente.Psicoterapeuta != null) 
                        cmbPsicoterapeutas.SelectedIndex = empleadosID.IndexOf(paciente.Psicoterapeuta);
                    cmbPaciente.SelectedIndex = pacientesID.IndexOf(id);
                    cmbPaciente.Enabled = false;
                }
                if (eval!=null)
                {
                    evaluacion = eval;
                    txtCosto.Value = eval.Costo;
                    cmbPsicoterapeutas.SelectedIndex = empleadosID.IndexOf(eval.Psicoterapeuta.ToString());
                    cmbPaciente.SelectedIndex = pacientesID.IndexOf(eval.Paciente.ToString());
                    txtFecha.Value = eval.Fecha;
                    txtHora.Value = new DateTime(2000,01,01,eval.Hora.Hours,eval.Hora.Minutes,0);
                    txtObservaciones.Text = eval.Observaciones;
                    txtPruebas.Text = eval.Pruebas;
                }else
                    evaluacion = new Evaluacion();
                if (consultar)
                {
                    this.btnAceptar.Click -= new System.EventHandler(this.btnAceptar_Click);
                    this.btnAceptar.Click += new System.EventHandler(this.btnCancelar_Click);
                }
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
                    evaluacion.Observaciones= txtObservaciones.Text;
                    evaluacion.Paciente = Convert.ToInt32(pacientesID.ElementAt(cmbPaciente.SelectedIndex));
                    evaluacion.Psicoterapeuta = empleadosID.ElementAt(cmbPsicoterapeutas.SelectedIndex);
                    evaluacion.Costo= Convert.ToDecimal(txtCosto.Value);
                    evaluacion.Pruebas = txtPruebas.Text;
                    evaluacion.Fecha = txtFecha.Value;
                    evaluacion.Hora = new TimeSpan(txtHora.Value.Hour, txtHora.Value.Minute,0);
                    try
                    {
                        evaluacion.Reservacion = control.ConsultarReservacion(evaluacion.Hora, evaluacion.Fecha, evaluacion.Psicoterapeuta.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No existe reservación previa para la evaluación");
                    }
                    if (evaluacion.Id != 0)
                    {
                        if (control.AgregarEvaluacion(evaluacion))
                        {
                            MessageBox.Show("Datos guardados exitosamente!");
                            Close();
                            Dispose();
                        }
                        else
                            MessageBox.Show("Error al guardar datos, verifique los campos y vuelva a intentarlo");
                    }
                    else
                    {
                        if (control.ActualizarEvaluacion(evaluacion))
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
