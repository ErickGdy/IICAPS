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
    public partial class FormGrupos : Form
    {
        ControlIicaps control;
        bool modificacion;
        Grupo grupo;    
        public FormGrupos(Grupo grupo)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            List<String> auxNombres = new List<string>();
            List<String> auxId = new List<string>();
            this.grupo = new Grupo();
            foreach (Programa p in control.ObtenerProgramas())
            {
                auxNombres.Add(p.Nombre);
                auxId.Add(p.Codigo.ToString());
            }
            cmbProgramas.DataSource = auxNombres;
            cmbIDProgramas.DataSource = auxId;
            cmbProgramas.SelectedIndex = 0;
            if (grupo!= null)
            {
                this.grupo = grupo;
                modificacion = true;
                txtGeneracion.Text = grupo.Generacion;
                txtCodigo.Text = grupo.Codigo;
                //txtCodigo.ReadOnly = true;
                if (grupo.Programa != null)
                {
                    cmbIDProgramas.SelectedItem = grupo.Programa;
                    cmbProgramas.SelectedIndex = cmbIDProgramas.SelectedIndex;
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                cmbIDProgramas.SelectedIndex = cmbProgramas.SelectedIndex;
                this.grupo.Generacion = txtGeneracion.Text;
                this.grupo.Codigo = txtCodigo.Text;
                this.grupo.Programa = cmbIDProgramas.SelectedItem.ToString();
                try
                {
                    if (modificacion)
                    {
                        if (control.ActualizarGrupo(this.grupo))
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
                        if (control.AgregarGrupo(this.grupo))
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
            if (txtGeneracion.Text != "" && txtCodigo.Text != "")
                return true;
            return false;
        }
    }
}
