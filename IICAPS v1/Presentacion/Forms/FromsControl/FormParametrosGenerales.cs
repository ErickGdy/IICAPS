using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IICAPS_v1.Control;
using IICAPS_v1.DataObject;
using IICAPS_v1.Presentacion;

namespace IICAPS_v1.Presentacion
{
    public partial class FormParametrosGenerales : Form
    {

        private static FormParametrosGenerales instance;
        ControlIicaps control;
        public FormParametrosGenerales()
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            try
            {
                txtCosto_Credito_Especialidad_Diplomado.Text = control.parametros_Generales.Costo_Credito_Especialidad_Diplomado.ToString();
                txtCosto_Credito_Maestria.Text = control.parametros_Generales.Costo_Credito_Maestria.ToString();
                txtPagoClase.Text = control.parametros_Generales.Porcentaje_Pago_Clase.ToString();
                txtPagoTaller.Text = control.parametros_Generales.Porcentaje_Pago_Taller.ToString();
                txtPagoSesion.Text = control.parametros_Generales.Porcentaje_Pago_Sesion.ToString();
                txtPorcentajeEvaluacion.Text = control.parametros_Generales.Porcentaje_Pago_Evaluacion.ToString();
                txtDirector.Text = control.parametros_Generales.Director;
                txtSede.Text = control.parametros_Generales.Sede;
                listUbicaciones.Items.AddRange(control.parametros_Generales.Ubicaciones.ToArray());
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener todos los parametros");
            }
        }

        public static FormParametrosGenerales getInstance()
        {
            if (instance == null)
                instance = new FormParametrosGenerales();
            return instance;
        }

       

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                int ancho = this.Width;
                //Actualiza el tamaño de la tabla con respecto al tamaño de la ventana
                layoutVentana.Size = this.Size;
                layoutForm.Size = new Size(this.Width-65,this.Height-95);
                layoutButtons.Size = layoutVentana.GetControlFromPosition(1, 2).Size;
                pictureBoxHeader.Location = new Point(((pictureBoxHeader.Width-this.Width-145) /2)*(-1),pictureBoxHeader.Location.Y);
                pictureBoxHeader.BringToFront();
            }
            catch (Exception ex) { }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                ParametrosGenerales parametros = new ParametrosGenerales();
                parametros.Costo_Credito_Especialidad_Diplomado = Convert.ToDecimal(txtCosto_Credito_Especialidad_Diplomado.Text);
                parametros.Costo_Credito_Maestria = Convert.ToDecimal(txtCosto_Credito_Maestria.Text);
                parametros.Porcentaje_Pago_Sesion = Convert.ToDecimal(txtPagoSesion.Text);
                parametros.Porcentaje_Pago_Taller = Convert.ToDecimal(txtPagoTaller.Text);
                parametros.Porcentaje_Pago_Clase = Convert.ToDecimal(txtPagoClase.Text);
                parametros.Porcentaje_Pago_Evaluacion = Convert.ToDecimal(txtPorcentajeEvaluacion.Text);
                parametros.Director = txtDirector.Text;
                parametros.Sede = txtSede.Text;
                parametros.Ubicaciones = new List<string>();
                foreach (string item in listUbicaciones.Items)
                {
                    parametros.Ubicaciones.Add(item);
                }
                if (control.ActualizarParametrosGenerales(parametros))
                {
                    MessageBox.Show("Datos actualizados exitosamente");
                    this.Dispose();
                }
                else
                    MessageBox.Show("Error al actualizar datos");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAgregarUbicacion_Click(object sender, EventArgs e)
        {

            if (txtUbicacion.Text != "" && txtUbicacion.Text.Replace(" ", "") != "")
            {
                listUbicaciones.Items.Add(txtUbicacion.Text);
                txtUbicacion.Text = "";
            }
        }

        private void btnRemoverUbicacion_Click(object sender, EventArgs e)
        {
            try
            {
                listUbicaciones.Items.RemoveAt(listUbicaciones.SelectedIndex);
            }
            catch (Exception ex) { }
        }

        
    }
}
