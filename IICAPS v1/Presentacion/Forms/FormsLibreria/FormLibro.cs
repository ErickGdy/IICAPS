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
    public partial class FormLibro : Form
    {
        ControlIicaps control;
        bool modificacion = false;
        Libro libro;
        public FormLibro(Libro libro, bool consultar)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            this.libro = libro ?? new Libro();
            modificacion = true;
            txtTitulo.Text = libro.Titulo;
            txtAutor.Text = libro.Autor;
            txtEditorial.Text = libro.Editorial;
            txtVitrina1.Value = libro.Stock_vitrina_1;
            txtVitrina2.Value = libro.Stock_vitrina_2;
            txtCosto.Value = libro.Precio_base;
            txtAlmacen.Value = libro.Stock_almacen;
            if (libro.Stock_vitrina_1 <= 0 || libro.Stock_vitrina_2 <= 0)
            {
                checkStock.Checked=true;
            }
                    
            if (consultar)
            {
                txtTitulo.Enabled = false;
                txtAutor.Enabled = false;
                txtEditorial.Enabled = false;
                txtVitrina1.Enabled = false;
                txtVitrina2.Enabled = false;
                txtAlmacen.Enabled = false;
                txtCosto.Enabled = false;
                checkStock.Visible = false;
                checkStock.Checked = true;
                btnAceptar.Enabled = false;
            }
        }

        public FormLibro()
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            this.libro = libro ?? new Libro();
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                this.libro.Titulo = txtTitulo.Text;
                libro.Stock_vitrina_1 = Convert.ToInt32(txtVitrina1.Value);
                libro.Stock_vitrina_2 = Convert.ToInt32(txtVitrina2.Value);
                libro.Stock_almacen = Convert.ToInt32(txtAlmacen.Value);
                libro.Editorial = txtEditorial.Text;
                libro.Autor = txtAutor.Text;
                libro.Precio_base = txtCosto.Value;
                try
                {
                    if (modificacion)
                    {
                        if (control.ActualizarLibro(libro))
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
                        if (control.AgregarLibro(libro))
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
                MessageBox.Show("Titulo no puede estar vacio");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool validarCampos()
        {
            if (txtTitulo.Text != "")
                return true;
            return false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkStock.Checked)
            {
                groupStock.Visible = true;
            }
            else
            {
                groupStock.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "https://www.gandhi.com.mx/catalogsearch/result/?q=";
            foreach (string word in txtTitulo.Text.Split())
            {
                url += word + "%20";
            }
            url = url.Substring(0, url.Length - 3);
            System.Diagnostics.Process.Start(url);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string url = "http://www.etrillas.mx/catalogos.php?verBusqueda=";
            foreach (string word in txtTitulo.Text.Split())
            {
                url += word + "%20";
            }
            url = url.Substring(0, url.Length - 3);
            System.Diagnostics.Process.Start(url);
        }
    }
}
