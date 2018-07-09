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
    public partial class MateriasDialog : Form
    {
        ControlIicaps control;
        public Materia m;
        public MateriasDialog()
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validarCampos())
                {
                    m = new Materia();
                    m.nombre = txtNombre.Text;
                    m.semestre = txtSemestre.Value.ToString();
                    m.duracion = txtDuracion.Text;
                    m.costo = txtCosto.Value;
                    if (control.agregarMateria(m))
                    {
                        Hide();
                        Close();
                    }
                    else
                        MessageBox.Show("Error al guardar Materia en la base de datos");
                }
                else
                    MessageBox.Show("No dejar campos vacios");
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

        private bool validarCampos()
        {
            if (txtNombre.Text != "" && txtDuracion.Text != "" && txtSemestre.Text != "" && txtCosto.Value > 0)
                return true;
            return false;
        }
        public Materia getMateria()
        {
            return m;
        }
    }
}
