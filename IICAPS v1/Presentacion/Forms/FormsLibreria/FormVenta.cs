using IICAPS_v1.Control;
using IICAPS_v1.DataObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IICAPS_v1.Presentacion
{
    public partial class FormVenta : Form
    {
        ControlIicaps control;
        VentaLibro Venta;
        PagoLibreria Pago;
        List<ComboBoxItem> auxAlumno = new List<ComboBoxItem>();
        List<ComboBoxItem> auxLibros = new List<ComboBoxItem>();
        public FormVenta(VentaLibro ventaAux, PagoLibreria pago, bool consultar)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            
            List<int> auxLibrosID = new List<int>();
            try
            {
                foreach (Libro c in control.ObtenerLibros())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Text = c.Titulo;
                    item.ValueItem = c.Id;
                    auxLibros.Add(item);
                }
                cmbLibros.Items.AddRange(auxLibros.ToArray());
            }
            catch (Exception ex) { }
            try
            {
                foreach (Alumno e in control.ObtenerAlumnos())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Text = e.Nombre;
                    item.ValueItem = e.Rfc;
                    auxAlumno.Add(item);
                }
                cmbAlumno.Items.AddRange(auxAlumno.ToArray());
            }
            catch (Exception ex) { }
            try
            {
                if (ventaAux != null)
                {
                    Venta = ventaAux;
                    if (Venta.Comprador_ID != "Publico General")
                    {
                        Alumno al = control.ConsultarAlumno(Venta.Comprador_ID);
                        ComboBoxItem item = new ComboBoxItem();
                        item.Text = al.Nombre;
                        item.ValueItem = al.Rfc;
                        cmbAlumno.SelectedItem = item;
                        groupAlumno.Enabled = true;
                    }
                    else
                    {
                        checkPublico.Checked = true;
                    }
                    foreach (DetalleVentaLibro item in Venta.DetallesVenta)
                    {
                        dataGridView1.Rows.Add(dataGridView1.Rows.Count, dataGridView1.Rows.Count, item.Libro, item.Cantidad.ToString(), item.Total.ToString());
                    }
                    cmbEmpleados.SelectedItem = Venta.Recibio;
                    //cmbEmpleados.SelectedValue = Venta.Recibio;
                    rb_Contado.Checked = Venta.TipoVenta == "Contado" ? true : false;
                    rb_Credito.Checked = Venta.TipoVenta == "Crédito" ? true : false;
                    txtPago.Value = pago != null ? pago.Cantidad : 0;  
                    if (consultar)
                    {
                        rb_Credito.Enabled = false;
                        rb_Contado.Enabled = false;
                        cmbAlumno.Enabled = false;
                        groupAlumno.Enabled = false;
                        cmbEmpleados.Enabled = false;
                        txtCantidad.Enabled = false;
                        txtPago.Enabled = false;
                        btnAceptar.Enabled = false;
                        btnCancelar.Text = "Aceptar";
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void BtnCancelar_click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void BtnAceptar_click(object sender, EventArgs e)
        {
            try
            {
                if (!Agregar_pago())
                    MessageBox.Show("Error al guardar los datos de la entrega de documentos");
                else
                {
                    MessageBox.Show("Datos del pago guardados exitosamente");
                    Close();
                    Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool Agregar_pago()
        {
            Venta = Venta ?? new VentaLibro();
            Venta.Comprador_ID = (cmbAlumno.SelectedItem as ComboBoxItem).ValueItem.ToString();
            Venta.Recibio = cmbEmpleados.SelectedItem.ToString();
            Pago = Pago ?? new PagoLibreria();
            Pago.CompradorID = checkPublico.Checked ? "Público General" : (cmbAlumno.SelectedItem as ComboBoxItem).ValueItem.ToString();
            Pago.Cantidad = Convert.ToDecimal(txtPago.Value);
            Pago.Concepto = "Pago Libreria";
            Pago.Observaciones = txtObservaciones.Text;
            Pago.Recibio = cmbEmpleados.SelectedItem.ToString();
            Pago.FechaPago = DateTime.Now;
            Cobro cobro = null;
            if (rb_Credito.Checked) {
                cobro = new Cobro()
                {
                    Remitente = Pago.CompradorID,
                    Pago = txtPago.Value,
                    Cantidad = Pago.Cantidad,
                    Restante = txtCantidad.Value - txtPago.Value,
                    Concepto = "Credito Libreria",
                    Fecha = DateTime.Now
                };
            }
            List<DetalleVentaLibro> libros = new List<DetalleVentaLibro>();
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                DetalleVentaLibro libro = new DetalleVentaLibro()
                {
                    Libro = item.Cells[1].ToString(),
                    Precio_Unitario = Convert.ToDecimal(item.Cells[2].ToString()),
                    Cantidad = Convert.ToInt32(item.Cells[3].ToString()),
                    Total = Convert.ToDecimal(item.Cells[4].ToString())
                };
                libros.Add(libro);
            }
            //Crear logica de venta y modificacion
            if (control.AgregarPagoLibreria(Pago,cobro))
            {
                Thread t = new Thread(new ThreadStart(ThreadMethodDocumentos));
                t.Start();
                return true;
            }
            else
                throw new Exception("Error al agregar pago del alumno");
            
        }
        private void ThreadMethodDocumentos()
        {
            DocumentosWord word = new DocumentosWord(Pago);
        }
        private void txtLibro_KeyUp(object sender, KeyEventArgs e)
        {
            cmbLibros.DataSource=auxLibros.Where(z => z.Text.Contains(txtLibro.Text) || z.ValueItem.ToString().Contains(txtLibro.Text));
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            txtPago.Maximum = Convert.ToInt32(txtTotal.Text);
        }

        private void txtBuscarRFC_KeyUp(object sender, KeyEventArgs e)
        {
            cmbAlumno.DataSource = auxAlumno.Where(z => z.Text.Contains(txtBuscarRFC.Text) || z.ValueItem.ToString().Contains(txtBuscarRFC.Text));
        }
    }
}


