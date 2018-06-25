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
    public partial class FormPrograma : Form
    {
        ControlIicaps control;
        bool modificacion;
        public FormPrograma(Materia materia)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                Programa p = new Programa();
                try
                {
                    if (modificacion)
                    {
                        if (control.actualizarPrograma(p))
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
                        if (control.agregarPrograma(p))
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
            if (txtNombre.Text != "" && txtCosto.Value>0)
                return true;
            return false;
        }
    }
}
