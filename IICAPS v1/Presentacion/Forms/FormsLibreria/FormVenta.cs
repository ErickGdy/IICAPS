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
        List<PagoLibreria> Pagos;
        PagoLibreria Pago;
        List<ComboBoxItem> auxAlumno = new List<ComboBoxItem>();
        List<ComboBoxItem> auxLibros = new List<ComboBoxItem>();
        List<ComboBoxItem> auxEmpleados = new List<ComboBoxItem>();
        public FormVenta(VentaLibro ventaAux, List<PagoLibreria> pagos_old, bool consultar)
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
                foreach (Empleado e in control.ObtenerEmpleadosAll())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Text = e.Nombre;
                    item.ValueItem = e.ID;
                    auxEmpleados.Add(item);
                }
                cmbEmpleados.Items.AddRange(auxEmpleados.ToArray());
            }
            catch (Exception ex) { }
            try
            {
                if (ventaAux != null)
                {
                    Venta = ventaAux;
                    foreach (DetalleVentaLibro item in Venta.DetallesVenta)
                    {
                        dataGridView1.Rows.Add(dataGridView1.Rows.Count+1, item.Id, item.Libro_Id, item.Cantidad.ToString(), item.Total.ToString());
                    }
                    try
                    {
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
                    }
                    catch { }
                    try
                    {
                        group_Pagos.Visible = true;
                        Pagos = pagos_old;
                        foreach (PagoLibreria item in Pagos)
                        {
                            dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, item.Id, item.FechaPago.ToShortDateString(), item.Pago-item.Cambio, item.Observaciones);
                        }
                    }
                    catch { }
                    try
                    {
                        Empleado em = control.ConsultarEmpleado(Venta.Recibio);
                        ComboBoxItem item_em = new ComboBoxItem();
                        item_em.Text = em.Nombre;
                        item_em.ValueItem = em.ID;
                        cmbEmpleados.SelectedItem = item_em;
                        //cmbEmpleados.SelectedValue = Venta.Recibio;
                    }
                    catch { }

                    rb_Contado.Checked = Venta.TipoVenta == "Contado" ? true : false;
                    rb_Credito.Checked = Venta.TipoVenta == "Crédito" ? true : false;
                    txtPago.Value = Venta.cobro != null ? Venta.cobro.Pago : Venta.Total;
                    txtPago.Minimum = Venta.cobro != null ? Venta.cobro.Pago : Venta.Total;
                    txtTotal.Text = Venta.Total.ToString();
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

        public FormVenta()
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            Venta = Venta ?? new VentaLibro();
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
                foreach (Empleado e in control.ObtenerEmpleados())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Text = e.Nombre;
                    item.ValueItem = e.ID;
                    auxEmpleados.Add(item);
                }
                cmbEmpleados.Items.AddRange(auxEmpleados.ToArray());
            }
            catch (Exception ex) { }
            checkPublico.Checked = true;
        }

        private void BtnCancelar_click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void BtnAceptar_click(object sender, EventArgs e)
        {
            try
            {
                if (validar_Campos())
                {
                    if (!Agregar_Venta())
                        MessageBox.Show("Error al guardar los datos de la entrega de documentos");
                    else
                    {
                        MessageBox.Show("Datos del pago guardados exitosamente");
                        Close();
                        Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool validar_Campos()
        {
            if (dataGridView1.RowCount <= 0)
            {
                MessageBox.Show("Seleccione al menos un articulo");
                return false;
            }
            if (rb_Contado.Checked && Convert.ToDecimal(txtTotal.Text) > txtPago.Value)
            {
                MessageBox.Show("El pago recibido no puede ser menor a la cantidad total a pagar");
                return false;
            }
            if (!checkPublico.Checked)
                if (cmbAlumno.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un alumno o seleccione venta para Publico en General ");
                    return false;
                }
            if (cmbEmpleados.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el Empleado quien realiza la venta");
                return false;
            }

                return true;
        }

        private bool Agregar_Venta()
        {

            Pago = new PagoLibreria();
            Pago.CompradorID = checkPublico.Checked ? "Público General" : (cmbAlumno.SelectedItem as ComboBoxItem).ValueItem.ToString();
            Pago.Total = Convert.ToDecimal(txtTotal.Text);
            Pago.Pago = Convert.ToDecimal(txtPago.Value);
            Pago.Cambio = Convert.ToDecimal(txtCambio.Text);
            Pago.Concepto = "Venta Libros";
            Pago.Observaciones = txtObservaciones.Text;
            Pago.Recibio = (cmbEmpleados.SelectedItem as ComboBoxItem).ValueItem.ToString();
            Pago.FechaPago = DateTime.Now;
            Venta.Comprador_ID = checkPublico.Checked ? "Público General" : (cmbAlumno.SelectedItem as ComboBoxItem).ValueItem.ToString();
            Venta.Recibio = cmbEmpleados.SelectedItem.ToString();
            Venta.TipoVenta = rb_Credito.Checked ? "Credito" : "Contado";
            Venta.Total = Pago.Total;
            Venta.Observaciones = txtObservaciones.Text;

            Cobro cobro = null;
            if (Venta.Comprador_ID != "Público General") {
                cobro = new Cobro()
                {
                    Remitente = Pago.CompradorID,
                    Pago = txtPago.Value,
                    Cantidad = Pago.Total,
                    Restante = Pago.Total - Pago.Pago,
                    Concepto = "Credito Libreria",
                    Fecha = DateTime.Now
                };
            }
            Venta.DetallesVenta = new List<DetalleVentaLibro>();
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                DetalleVentaLibro libro = new DetalleVentaLibro()
                {
                    Libro_Id = item.Cells[1].Value.ToString(),
                    Libro = control.ConsultarLibro(item.Cells[1].Value.ToString()),
                    Precio_Unitario = Convert.ToDecimal(item.Cells[3].Value.ToString()),
                    Cantidad = Convert.ToInt32(item.Cells[4].Value.ToString()),
                    Total = Convert.ToDecimal(item.Cells[5].Value.ToString())
                };
                Venta.DetallesVenta.Add(libro);
            }
            //Crear logica de venta y modificacion
            if (Venta.Id != 0 ? control.ActualizarVentaLibreria(Venta, control.ConsultarVentaLibreria_DetallesDeVenta(Venta.Id.ToString()), Pagos, Pago, cobro) : control.AgregarVentaLibreria(Venta, Pago,cobro))
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
            try
            {
                cmbLibros.Items.Clear();
                if (String.IsNullOrEmpty(txtLibro.Text))
                    cmbLibros.Items.AddRange(auxLibros.ToArray());
                else
                    cmbLibros.Items.AddRange(auxLibros.Where(z => z.Text.ToLower().Contains(txtLibro.Text.ToLower()) || z.ValueItem.ToString().ToLower().Contains(txtLibro.Text.ToLower())).ToArray());
                cmbLibros.SelectedIndex = 0;
            }
            catch(Exception ex) { }
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
                actualizar_cambio();
        }

        private void txtBuscarRFC_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                cmbAlumno.Items.Clear();
                if (String.IsNullOrEmpty(txtBuscarRFC.Text))
                    cmbAlumno.Items.AddRange(auxAlumno.ToArray());
                else
                    cmbAlumno.Items.AddRange(auxAlumno.Where(z => z.Text.ToLower().Contains(txtBuscarRFC.Text.ToLower()) || z.ValueItem.ToString().ToLower().Contains(txtBuscarRFC.Text.ToLower())).ToArray());
                cmbAlumno.SelectedIndex = 0;
            }
            catch (Exception ex) { }
        }

        private void checkPublico_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscarRFC.Enabled = checkPublico.Checked ? false : true;
            cmbAlumno.Enabled = checkPublico.Checked ? false : true;
            rb_Credito.Enabled = checkPublico.Checked ? false : true;
            rb_Contado.Checked = checkPublico.Checked ? true : rb_Contado.Checked; 
        }

        private void btnAgregarLibro_Click(object sender, EventArgs e)
        {
            try
            {
                Libro aux = control.ConsultarLibro((cmbLibros.SelectedItem as ComboBoxItem).ValueItem.ToString());

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() == (cmbLibros.SelectedItem as ComboBoxItem).ValueItem.ToString()) {
                        dataGridView1.Rows[i].Cells[4].Value = Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value) + txtCantidad.Value;
                        if ((aux.Stock_total() - aux.Prestados) < Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value))
                        {
                            dataGridView1.Rows[i].Cells[4].Value = (aux.Stock_total() - aux.Prestados);
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells[5].Value = (Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value) * Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value));
                            actulalizarTotal();
                        }
                        return;
                    }
                }
                dataGridView1.Rows.Add(dataGridView1.Rows.Count+1, aux.Id, aux.Titulo, aux.Precio_base, txtCantidad.Value, (txtCantidad.Value * aux.Precio_base));
                //Clear fields
                txtCantidad.Value = 1;
                txtLibro.Text = "";
                actulalizarTotal();
            }
            catch { }
        }
        private void actulalizarTotal()
        {
            decimal total = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                total += Convert.ToDecimal(dataGridView1.Rows[i].Cells[5].Value);
            }
            txtTotal.Text = total.ToString("0.00");
        }
            private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                ordenarRows();
            }
            catch { }
        }
        private void ordenarRows()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i+1;
            }
        }

        private void cmbLibros_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Libro aux = control.ConsultarLibro((cmbLibros.SelectedItem as ComboBoxItem).ValueItem.ToString());
                txtCantidad.Maximum = (aux.Stock_total() - aux.Prestados);
            }
            catch { }
        }

        private void txtPago_ValueChanged(object sender, EventArgs e)
        {
            actualizar_cambio();
        }
        private void actualizar_cambio() {
            if (!string.IsNullOrEmpty(txtTotal.Text) && txtPago.Value > 0)
                txtCambio.Text =  txtPago.Value - Convert.ToDecimal(txtTotal.Text) < 0 ? "0.00" : (txtPago.Value - Convert.ToDecimal(txtTotal.Text)).ToString("0.00");
            else
                txtCambio.Text = "0.00";
        }

        private void txtPago_KeyUp(object sender, KeyEventArgs e)
        {
            actualizar_cambio();
        }
    }
}


