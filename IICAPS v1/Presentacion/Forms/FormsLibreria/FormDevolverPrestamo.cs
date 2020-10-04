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
    public partial class FormDevolverPrestamo : Form
    {
        ControlIicaps control;
        PagoLibreria pagos;
        Cobro Cobro;
        Prestamo Prestamo;
        List<ComboBoxItem> Empleados = new List<ComboBoxItem>();
        public FormDevolverPrestamo(Prestamo PrestamoAux, Cobro cobroAux)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            Prestamo = PrestamoAux;
            try
            {
                txtRestante.Value = cobroAux.Restante;
                txtPago.Maximum = cobroAux.Restante;
            }
            catch { }
            try
            {
                foreach (Empleado c in control.ObtenerEmpleados())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Text = c.Nombre;
                    item.ValueItem = c.Matricula;
                    Empleados.Add(item);
                }
                cmbRecibio.Items.AddRange(Empleados.ToArray());
            }
            catch { }

            try
            {
                foreach (DetallePrestamoLibro item in Prestamo.DetallesPrestamo)
                {
                    if (item.Entregado)
                    {
                        for (int i = 0; i < tablaDevueltos.Rows.Count; i++)
                        {
                            if (tablaDevueltos.Rows[i].Cells[1].Value.ToString() == item.Libro_Id)
                            {
                                tablaDevueltos.Rows[i].Cells[3].Value = Convert.ToInt32(tablaDevueltos.Rows[i].Cells[3].Value.ToString()) + 1;
                                break;
                            }
                        }
                        tablaDevueltos.Rows.Add(tablaDevueltos.Rows.Count + 1, item.Libro_Id, item.Libro, item.Cantidad.ToString());
                    }
                   
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[1].Value.ToString() == item.Libro_Id)
                        {
                            dataGridView1.Rows[i].Cells[3].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value.ToString()) + 1;
                            break;
                        }
                    }
                    dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, item.Libro_Id, item.Libro, item.Cantidad.ToString());
                }
                try
                {
                    Alumno al = control.ConsultarAlumno(Prestamo.Comprador_ID);
                    txtAlumno.Text = al.Nombre;
                }
                catch { }
                try
                {
                    Empleado em = control.ConsultarEmpleado(Prestamo.Recibio);
                    txtEmpleado.Text = em.Nombre;
                }
                catch { }
                txtObservaciones.Text = Prestamo.Observaciones;
                try
                {
                    foreach (PagoLibreria item in control.ConsultarPrestamoLibreria_Pagos(Prestamo.Id.ToString()))
                    {
                        dataGridView2.Rows.Add(dataGridView1.Rows.Count + 1, item.FechaPago.ToShortDateString(), item.Pago.ToString(), item.Observaciones);
                    }
                }
                catch { }
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

            cmbIDRecibio.SelectedIndex = cmbRecibio.SelectedIndex;
            PagoLibreria p = null;
            if (txtRestante.Value > 0)
            {
                p = new PagoLibreria
                {
                    CompradorID = Prestamo.Comprador_ID,
                    Pago = Convert.ToDecimal(txtPago.Value),
                    Concepto = "Prestamo Libreria",
                    Observaciones = txtObservaciones.Text,
                    Recibio = cmbIDRecibio.SelectedItem.ToString(),
                    FechaPago = DateTime.Now,
                    Parent_ID = Prestamo.Id.ToString()
                };
            }
            decimal cantidad = txtPago.Value;
            Cobro.Restante -= cantidad;
            Cobro.Pago += cantidad;
            List<Cobro> cobros = new List<Cobro>();
            cobros.Add(Cobro);
            if (control.AgregarPagoLibreria(p, cobros))
            {
                Thread t = new Thread(new ThreadStart(ThreadMethodDocumentos));
                t.Start();
                return true;
            }
            else
                throw new Exception("Error al agregar pago a librería");
        }
        private void ThreadMethodDocumentos()
        {
            //DocumentosWord word = new DocumentosWord(p);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            String id = tablaPendientes.CurrentRow.Cells[1].Value.ToString();

            for (int i = 0; i < tablaDevueltos.Rows.Count; i++)
            {
                if (tablaDevueltos.Rows[i].Cells[1].Value.ToString() == id)
                {
                    tablaDevueltos.Rows[i].Cells[3].Value = Convert.ToInt32(tablaDevueltos.Rows[i].Cells[3].Value.ToString()) + 1;
                    break;
                }
            }
            tablaDevueltos.Rows.Add(tablaDevueltos.Rows.Count + 1, id, tablaPendientes.CurrentRow.Cells[2].Value, 1);
            if ((Convert.ToInt32(tablaPendientes.CurrentRow.Cells[2].Value) - 1) == 0)
            {
                tablaPendientes.Rows.RemoveAt(tablaPendientes.CurrentRow.Index);
            }
            else
            {
                tablaPendientes.CurrentRow.Cells[2].Value = Convert.ToInt32(tablaPendientes.CurrentRow.Cells[2].Value) - 1;
                for (int i = 0; i < tablaPendientes.Rows.Count; i++)
                {
                    tablaPendientes.Rows[i].Cells[0].Value = i + 1;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            String id = tablaDevueltos.CurrentRow.Cells[1].Value.ToString();

            for (int i = 0; i < tablaPendientes.Rows.Count; i++)
            {
                if (tablaPendientes.Rows[i].Cells[1].Value.ToString() == id)
                {
                    tablaPendientes.Rows[i].Cells[3].Value = Convert.ToInt32(tablaPendientes.Rows[i].Cells[3].Value.ToString()) + 1;
                    break;
                }
            }
            tablaPendientes.Rows.Add(tablaPendientes.Rows.Count + 1, id, tablaDevueltos.CurrentRow.Cells[2].Value, 1);
            if ((Convert.ToInt32(tablaDevueltos.CurrentRow.Cells[2].Value) - 1) == 0)
            {
                tablaDevueltos.Rows.RemoveAt(tablaDevueltos.CurrentRow.Index);
            }
            else
            {
                tablaDevueltos.CurrentRow.Cells[2].Value = Convert.ToInt32(tablaDevueltos.CurrentRow.Cells[2].Value) - 1;
                for (int i = 0; i < tablaDevueltos.Rows.Count; i++)
                {
                    tablaDevueltos.Rows[i].Cells[0].Value = i + 1;
                }
            }
        }

    }
}
