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
                txtNombre.Text = taller.nombre;
                txtFecha.Value = taller.fecha;
                txtCostoClientes.Value = taller.costoClientes;
                txtCostoPublico.Value = taller.costoPublico;
                txtCapacidad.Value = taller.capacidad;
                txtRequisitos.Text = taller.requisitos;
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                this.taller.nombre = txtNombre.Text;
                this.taller.fecha = txtFecha.Value;
                this.taller.costoClientes = txtCostoClientes.Value;
                this.taller.costoPublico = txtCostoPublico.Value;
                this.taller.capacidad = Convert.ToInt32(txtCapacidad.Value);
                this.taller.requisitos = txtRequisitos.Text;
                try
                {
                    if (this.taller.id != 0)
                    {
                        if (control.actualizarTaller(this.taller))
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
                        if (control.agregarTaller(this.taller))
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
