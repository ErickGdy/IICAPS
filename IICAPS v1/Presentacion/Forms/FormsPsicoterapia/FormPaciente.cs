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
    public partial class FormPaciente : Form
    {
        ControlIicaps control;
        Paciente paciente;
        List<string> empleadosNombres = new List<string>();
        public FormPaciente(Paciente paciente)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            List<string> empleadosID = new List<string>();
            foreach (Psicoterapeuta item in control.ObtenerPsicoterapeutas())
            {
                empleadosNombres.Add(item.Nombre);
                empleadosID.Add(item.Matricula);
            }
            cmbPsicoterapeutas.DataSource = empleadosNombres;
            if (paciente==null)
                this.paciente = new Paciente();
            else
            {
                this.paciente = paciente;
                txtNombre.Text = paciente.Nombre;
                txtApellidos.Text = paciente.Apellidos;
                //txtFecha.Value = paciente.fecha;
                txtCosto.Value = paciente.CostoEspecial;
                txtTelefono.Text = paciente.Telefono;
                txtNombreTutor.Text = paciente.Nombre_tutor;
                txtTelefonoTutor.Text = paciente.Telefono_tutor;
                txtInsitutcion.Text = paciente.Institucion;
                txtFecha.Text = paciente.FechaNacimiento.ToShortDateString();
                if (paciente.Datos_facturacion != null)
                {
                    txtRFC.Text = paciente.Datos_facturacion[0];
                    txtNombreFacturacion.Text = paciente.Datos_facturacion[1];
                    txtRazonSocial.Text = paciente.Datos_facturacion[2];
                    txtDireccionFacturacion.Text = paciente.Datos_facturacion[3];
                }

            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarCamposPaciente())
            {
                paciente.Nombre = txtNombre.Text;
                paciente.Apellidos = txtApellidos.Text;
                paciente.FechaNacimiento = txtFecha.Value;
                paciente.CostoEspecial= Convert.ToDecimal(txtCosto.Value);
                paciente.Telefono = txtTelefono.Text;
                paciente.Nombre_tutor=txtNombreTutor.Text;
                paciente.Telefono_tutor=txtTelefonoTutor.Text;
                paciente.Institucion = txtInsitutcion.Text;
                if (validarCamposFacturacion())
                {
                    paciente.Datos_facturacion = new string[4];
                    paciente.Datos_facturacion[0] = txtRFC.Text;
                    paciente.Datos_facturacion[1] = txtNombreFacturacion.Text;
                    paciente.Datos_facturacion[2] = txtRazonSocial.Text;
                    paciente.Datos_facturacion[3] = txtDireccionFacturacion.Text;
                }
                try
                {
                    if (this.paciente.Id != 0)
                    {
                        if (control.ActualizarPaciente(this.paciente))
                        {
                            MessageBox.Show("Datos actualizados exitosamente!");
                            Close();
                            Dispose();
                        }
                        else
                            MessageBox.Show("Error al guardar datos, verifique los campos y vuelva a intentarlo");
                    }
                    else
                    {
                        if (control.AgregarPaciente(this.paciente))
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
            if (txtNombre.Text != "" && txtFecha.Text != "" && txtApellidos.Text != "" && txtTelefono.Text != "" && txtCosto.Text != "")
                return true;
            return false;
        }
        private bool validarCamposFacturacion()
        {
            if (txtRFC.Text != "" && txtNombreFacturacion.Text != "" && txtRazonSocial.Text != "" && txtDireccionFacturacion.Text != "")
                return true;
            return false;
        }
    }
}
