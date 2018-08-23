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
        Materia materia;
        public FormMaterias(Materia materia, bool consultar)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            List<String> auxNombres = new List<string>();
            List<String> auxId = new List<string>();
            this.materia = new Materia();
            foreach (Programa p in control.obtenerProgramas())
            {
                auxNombres.Add(p.Nombre);
                auxId.Add(p.Codigo.ToString());
            }
            cmbProgramas.DataSource = auxNombres;
            cmbIDProgramas.DataSource = auxId;
            if (materia!= null)
            {
                this.materia = materia;
                modificacion = true;
                txtNombre.Text = materia.nombre;
                txtDuracion.Text = materia.duracion;
                txtSemestre.Text = materia.semestre;
                txtCosto.Text = materia.costo.ToString();
                if (materia.programa != null)
                {
                    cmbIDProgramas.Enabled = false;
                    cmbProgramas.Enabled = false;
                    checkPrograma.Checked = true;
                    cmbIDProgramas.SelectedValue = materia.programa;
                    cmbProgramas.SelectedIndex = cmbIDProgramas.SelectedIndex;
                    checkPrograma.Enabled = false;
                }
                if (consultar)
                {
                    txtNombre.Enabled = false;
                    txtDuracion.Enabled = false;
                    txtSemestre.Enabled = false;
                    txtCosto.Enabled = false;
                    checkPrograma.Checked = true;
                    checkPrograma.Enabled = false;
                    cmbProgramas.Enabled = false;
                    btnAceptar.Enabled = false;
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                cmbIDProgramas.SelectedIndex = cmbProgramas.SelectedIndex;
                this.materia.nombre = txtNombre.Text;
                materia.semestre = txtSemestre.Value.ToString();
                materia.duracion = txtDuracion.Text;
                materia.costo = txtCosto.Value;
                try
                {
                    if (modificacion)
                    {
                        if (control.actualizarMateria(materia))
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
                        if (checkPrograma.Checked)
                            materia.programa = cmbIDProgramas.SelectedValue.ToString();
                        if (control.agregarMateria(materia))
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
            if (txtNombre.Text != "" && txtDuracion.Text != "" && txtCosto.Value>0)
                return true;
            return false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPrograma.Checked)
            {
                cmbProgramas.Visible = true;
                label4.Visible = true;
            }else
            {
                cmbProgramas.Visible = false;
                label4.Visible = false;
            }
        }
    }
}
