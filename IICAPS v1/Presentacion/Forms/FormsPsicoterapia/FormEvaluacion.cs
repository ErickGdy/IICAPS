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
        List<string> empleadosNombres = new List<string>();
        List<string> empleadosID = new List<string>();
        List<string> pacientesID = new List<string>();
        List<string> pacientesNombre = new List<string>();
        Evaluacion evaluacion;
        public FormEvaluacion(string id, Evaluacion eval, bool consultar)
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
                if (eval!=null)
                {
                    evaluacion = eval;
                    txtCosto.Value = eval.costo;
                    cmbPsicoterapeutas.SelectedIndex = empleadosID.IndexOf(eval.psicoterapeuta.ToString());
                    cmbPaciente.SelectedIndex = pacientesID.IndexOf(eval.paciente.ToString());
                    txtFecha.Value = eval.fecha;
                    txtHora.Value = new DateTime(2000,01,01,eval.hora.Hours,eval.hora.Minutes,0);
                    txtObservaciones.Text = eval.observaciones;
                    txtPruebas.Text = eval.pruebas;
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
                    evaluacion.observaciones= txtObservaciones.Text;
                    evaluacion.paciente = Convert.ToInt32(pacientesID.ElementAt(cmbPaciente.SelectedIndex));
                    evaluacion.psicoterapeuta = empleadosID.ElementAt(cmbPsicoterapeutas.SelectedIndex);
                    evaluacion.costo= Convert.ToDecimal(txtCosto.Value);
                    evaluacion.pruebas = txtPruebas.Text;
                    evaluacion.fecha = txtFecha.Value;
                    evaluacion.hora = new TimeSpan(txtHora.Value.Hour, txtHora.Value.Minute,0);
                    try
                    {
                        evaluacion.reservacion = control.consultarReservacion(evaluacion.hora, evaluacion.fecha, evaluacion.psicoterapeuta.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No existe reservación previa para la evaluación");
                    }
                    if (evaluacion.id != 0)
                    {
                        if (control.agregarEvaluacion(evaluacion))
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
                        if (control.actualizarEvaluacion(evaluacion))
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
