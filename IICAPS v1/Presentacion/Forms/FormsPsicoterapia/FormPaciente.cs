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
            foreach (Psicoterapeuta item in control.obtenerPsicoterapeutas())
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
                txtNombre.Text = paciente.nombre;
                txtApellidos.Text = paciente.apellidos;
                //txtFecha.Value = paciente.fecha;
                txtCosto.Value = paciente.costoEspecial;
                txtTelefono.Text = paciente.telefono;
                txtNombreTutor.Text = paciente.nombre_tutor;
                txtTelefonoTutor.Text = paciente.telefono_tutor;
                txtInsitutcion.Text = paciente.institucion;
                txtFecha.Text = paciente.fechaNacimiento.ToShortDateString();
                if (paciente.datos_facturacion != null)
                {
                    txtRFC.Text = paciente.datos_facturacion[0];
                    txtNombreFacturacion.Text = paciente.datos_facturacion[1];
                    txtRazonSocial.Text = paciente.datos_facturacion[2];
                    txtDireccionFacturacion.Text = paciente.datos_facturacion[3];
                }

            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarCamposPaciente())
            {
                paciente.nombre = txtNombre.Text;
                paciente.apellidos = txtApellidos.Text;
                paciente.fechaNacimiento = txtFecha.Value;
                paciente.costoEspecial= Convert.ToDecimal(txtCosto.Value);
                paciente.telefono = txtTelefono.Text;
                paciente.nombre_tutor=txtNombreTutor.Text;
                paciente.telefono_tutor=txtTelefonoTutor.Text;
                paciente.institucion = txtInsitutcion.Text;
                if (validarCamposFacturacion())
                {
                    paciente.datos_facturacion = new string[4];
                    paciente.datos_facturacion[0] = txtRFC.Text;
                    paciente.datos_facturacion[1] = txtNombreFacturacion.Text;
                    paciente.datos_facturacion[2] = txtRazonSocial.Text;
                    paciente.datos_facturacion[3] = txtDireccionFacturacion.Text;
                }
                try
                {
                    if (this.paciente.id != 0)
                    {
                        if (control.actualizarPaciente(this.paciente))
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
                        if (control.agregarPaciente(this.paciente))
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
