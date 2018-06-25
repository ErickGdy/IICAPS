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
    public partial class FormMaterias : Form
    {
        ControlIicaps control;
        bool modificacion;
        public FormMaterias(Materia materia)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            List<String> auxNombres = new List<string>();
            List<String> auxId = new List<string>();
            foreach (Programa p in control.obtenerProgramas())
            {
                auxNombres.Add(p.Nombre);
                auxId.Add(p.Codigo.ToString());
            }
            cmbProgramas.DataSource = auxNombres;
            cmbIDProgramas.DataSource = auxId;
            if (materia!= null)
            {
                modificacion = true;
                txtNombre.Text = materia.nombre;
                txtDuracion.Text = materia.duracion;
                txtSemestre.Text = materia.semestre;
                txtCosto.Text = materia.costo.ToString();
                cmbIDProgramas.SelectedValue = materia.programa;
                cmbProgramas.SelectedIndex = cmbIDProgramas.SelectedIndex;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                cmbIDProgramas.SelectedIndex = cmbProgramas.SelectedIndex;
                Materia m = new Materia();
                m.nombre = txtNombre.Text;
                m.programa = cmbIDProgramas.SelectedValue.ToString();
                m.semestre = txtSemestre.Value.ToString();
                m.duracion = txtDuracion.Text;
                m.costo = txtCosto.Value;
                try
                {
                    if (modificacion)
                    {
                        if (control.actualizarMateria(m))
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
                        if (control.agregarMateria(m))
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
            if (txtNombre.Text != "" && txtDuracion.Text != "" && txtSemestre.Text != "" && txtCosto.Value>0)
                return true;
            return false;
        }
    }
}
