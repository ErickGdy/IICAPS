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
    public partial class FormPrestamo : Form
    {
        ControlIicaps control;
        Prestamo Prestamo;
        PagoLibreria Pago;
        List<ComboBoxItem> auxAlumno = new List<ComboBoxItem>();
        List<ComboBoxItem> auxLibros = new List<ComboBoxItem>();
        List<ComboBoxItem> auxEmpleados = new List<ComboBoxItem>();
        public FormPrestamo(Prestamo prestamoAux, bool consultar)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            txtFechaPrestamo.Text = DateTime.Now.ToShortDateString();
            
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
            catch{ }
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
                if (prestamoAux != null)
                {
                    Prestamo = prestamoAux;
                    foreach (DetallePrestamoLibro item in Prestamo.DetallesPrestamo)
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[1].Value.ToString() == item.Libro_Id)
                            {
                                dataGridView1.Rows[i].Cells[3].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value.ToString()) + 1;
                                break;
                            }
                        }
                        dataGridView1.Rows.Add(dataGridView1.Rows.Count+1, item.Libro_Id, item.Libro, item.Cantidad.ToString());

                    }
                    try
                    {
                        Alumno al = control.ConsultarAlumno(Prestamo.Comprador_ID);
                        ComboBoxItem item = new ComboBoxItem();
                        item.Text = al.Nombre;
                        item.ValueItem = al.Rfc;
                        cmbAlumno.SelectedItem = item;
                        groupAlumno.Enabled = true;
                    }
                    catch { }

                    try
                    {
                        Empleado em = control.ConsultarEmpleado(Prestamo.Recibio);
                        ComboBoxItem item_em = new ComboBoxItem();
                        item_em.Text = em.Nombre;
                        item_em.ValueItem = em.ID;
                        cmbEmpleados.SelectedItem = item_em;
                        //cmbEmpleados.SelectedValue = Prestamo.Recibio;
                    }
                    catch { }

                    txtDias.Value = Prestamo.Dias;
                    txtFechaPrestamo.Text = Prestamo.FechaPrestamo.ToShortDateString();
                    txtFechaLimite.Text = Prestamo.FechaLimite.ToShortDateString();
                    if (consultar)
                    {
                        cmbAlumno.Enabled = false;
                        groupAlumno.Enabled = false;
                        cmbEmpleados.Enabled = false;
                        txtCantidad.Enabled = false;
                        txtDias.Enabled = false;
                        btnAceptar.Enabled = false;
                        btnCancelar.Text = "Aceptar";
                    }
                }
            }
            catch (Exception ex) { }
        }

        public FormPrestamo()
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            Prestamo = Prestamo ?? new Prestamo();
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
                    if (!Agregar_Prestamo())
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
            if (cmbEmpleados.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el Empleado quien realiza la Prestamo");
                return false;
            }

                return true;
        }

        private bool Agregar_Prestamo()
        {
            
            Pago = Pago ?? new PagoLibreria();
            Pago.Total = Convert.ToDecimal(txtFechaPrestamo.Text);
            Pago.Pago = Convert.ToDecimal(txtDias.Value);
            Pago.Cambio = Convert.ToDecimal(txtFechaLimite.Text);
            Pago.Concepto = "Pago Libreria";
            Pago.Observaciones = txtObservaciones.Text;
            Pago.Recibio = (cmbEmpleados.SelectedItem as ComboBoxItem).ValueItem.ToString();
            Pago.FechaPago = DateTime.Now;
            Prestamo.Recibio = cmbEmpleados.SelectedItem.ToString();
            Prestamo.Observaciones = txtObservaciones.Text;

            Prestamo.DetallesPrestamo = new List<DetallePrestamoLibro>();
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                for (int i = 0; i < Convert.ToInt32(item.Cells[2].Value.ToString()); i++)
                {
                    DetallePrestamoLibro libro = new DetallePrestamoLibro()
                    {
                        Libro_Id = item.Cells[1].Value.ToString(),
                        Cantidad = 1,
                        Libro = control.ConsultarLibro(item.Cells[2].Value.ToString()),
                        Entregado = false
                    };
                    Prestamo.DetallesPrestamo.Add(libro);
                }
            }
            //Crear logica de Prestamo y modificacion
            if (Prestamo.Id != 0 ? control.ActualizarPrestamoLibreria(Prestamo, control.ConsultarPrestamoLibreria_DetallesDePrestamo(Prestamo.Id.ToString())) : control.AgregarPrestamoLibreria(Prestamo))
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

        private void btnAgregarLibro_Click(object sender, EventArgs e)
        {
            try
            {
                Libro aux = control.ConsultarLibro((cmbLibros.SelectedItem as ComboBoxItem).ValueItem.ToString());

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() == (cmbLibros.SelectedItem as ComboBoxItem).ValueItem.ToString()) {
                        if ((aux.Stock_total() - aux.Prestados) < Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value))
                        {
                            dataGridView1.Rows[i].Cells[3].Value = (aux.Stock_total() - aux.Prestados);
                        }
                        return;
                    }
                }
                dataGridView1.Rows.Add(dataGridView1.Rows.Count+1, aux.Id, aux.Titulo, txtCantidad.Value);
                //Clear fields
                txtCantidad.Value = 1;
                txtLibro.Text = "";
            }
            catch { }
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

    }
}


