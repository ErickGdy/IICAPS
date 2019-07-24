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
    public partial class FormTalleres : Form
    {
        ControlIicaps control;
        Taller taller;    
        public FormTalleres(Taller taller)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            if(taller==null)
                this.taller = new Taller();
            else
            {
                this.taller = taller;
                txtNombre.Text = taller.Nombre;
                txtFecha.Value = taller.Fecha;
                txtCostoClientes.Value = taller.CostoClientes;
                txtCostoPublico.Value = taller.CostoPublico;
                txtCapacidad.Value = taller.Capacidad;
                txtRequisitos.Text = taller.Requisitos;
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                this.taller.Nombre = txtNombre.Text;
                this.taller.Fecha = txtFecha.Value;
                this.taller.CostoClientes = txtCostoClientes.Value;
                this.taller.CostoPublico = txtCostoPublico.Value;
                this.taller.Capacidad = Convert.ToInt32(txtCapacidad.Value);
                this.taller.Requisitos = txtRequisitos.Text;
                try
                {
                    if (this.taller.Id != 0)
                    {
                        if (control.ActualizarTaller(this.taller))
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
                        if (control.AgregarTaller(this.taller))
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

        private bool validarCampos()
        {
            if (txtNombre.Text != "" && txtFecha.Text != "")
                return true;
            return false;
        }
    }
}
