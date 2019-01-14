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
using MySql.Data.MySqlClient;

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
                listUbicaciones.Items.AddRange(control.parametros_Generales.ubicaciones.ToArray());
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al obtener todos los parametros");
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
                layoutForm.Size = layoutVentana.GetControlFromPosition(1, 1).Size;
                layoutButtons.Size = layoutVentana.GetControlFromPosition(1, 2).Size;
                //layoutForm.Controls.Remove(layoutForm.GetControlFromPosition(1, 1));
                if (ancho < 1000)
                {
                    pictureBox1000.Visible = true;
                    pictureBox1000.Width = layoutForm.Width;
                    pictureBox1000.Height = pictureBox1000.PreferredSize.Height;
                    pictureBox1000.BringToFront();
                    layoutForm.Controls.Add(pictureBox1000,0,0);
                    layoutForm.SetColumnSpan(pictureBox1000,2);
                }else
                    if (ancho < 1200)
                    {
                        pictureBox1200.Visible = true;
                        pictureBox1200.Width = layoutForm.Width;
                        pictureBox1200.BringToFront();
                        layoutForm.Controls.Add(pictureBox1200, 0, 0);
                        layoutForm.SetColumnSpan(pictureBox1200, 2);
                    }
                    else
                        if (ancho < 1500)
                        {
                            pictureBox1500.Visible = true;
                            pictureBox1500.Width = layoutForm.Width;
                            pictureBox1500.BringToFront();
                            layoutForm.Controls.Add(pictureBox1500, 0, 0);
                            layoutForm.SetColumnSpan(pictureBox1500, 2);
                        }
                        else
                        {
                            pictureBox2000.Visible = true;
                            pictureBox2000.Width = layoutForm.Width;
                            pictureBox2000.BringToFront();
                            layoutForm.Controls.Add(pictureBox2000, 0, 0);
                            layoutForm.SetColumnSpan(pictureBox2000, 2);
                        }
                
            }
            catch (Exception ex) { }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ParametrosGenerales parametros = new ParametrosGenerales();
            parametros.Costo_Credito_Especialidad_Diplomado = Convert.ToDecimal(txtCosto_Credito_Especialidad_Diplomado.Text);
            parametros.Costo_Credito_Maestria = Convert.ToDecimal(txtCosto_Credito_Maestria.Text);
            parametros.Porcentaje_Pago_Sesion = Convert.ToDecimal(txtPagoSesion.Text);
            parametros.Porcentaje_Pago_Taller = Convert.ToDecimal(txtPagoTaller.Text);
            parametros.Porcentaje_Pago_Clase = Convert.ToDecimal(txtPagoClase.Text);
            parametros.ubicaciones = new List<string>();
            foreach (string item in listUbicaciones.Items)
            {
                parametros.ubicaciones.Add(item);
            }
            if (control.actualizarParametrosGenerales(parametros))
            {
                MessageBox.Show("Datos actualizados exitosamente");
                this.Dispose();
            }
            else
                MessageBox.Show("Error al actualizar datos");
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
