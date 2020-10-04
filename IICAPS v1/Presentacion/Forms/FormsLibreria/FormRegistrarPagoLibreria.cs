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
    public partial class FormRegistrarPagoLibreria : Form
    {
        ControlIicaps control;
        PagoLibreria pagos;
        Cobro Cobro;
        VentaLibro Venta;
        List<ComboBoxItem> Empleados = new List<ComboBoxItem>();
        public FormRegistrarPagoLibreria(VentaLibro ventaAux, Cobro cobroAux)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            Venta = ventaAux;
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
                foreach (DetalleVentaLibro item in Venta.DetallesVenta)
                {
                    dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, item.Id, item.Libro_Id, item.Cantidad.ToString(), item.Total.ToString());
                }
                try
                {
                    Alumno al = control.ConsultarAlumno(Venta.Comprador_ID);
                    txtAlumno.Text = al.Nombre;
                }
                catch { }
                try
                {
                    Empleado em = control.ConsultarEmpleado(Venta.Recibio);
                    txtEmpleado.Text = em.Nombre;
                }
                catch { }
                txtObservaciones.Text = ventaAux.Observaciones;
                try
                {
                    foreach (PagoLibreria item in control.ConsultarVentaLibreria_Pagos(ventaAux.Id.ToString()))
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
            PagoLibreria p = new PagoLibreria
            {
                CompradorID = Venta.Comprador_ID,
                Pago = Convert.ToDecimal(txtPago.Value),
                Concepto = cmbConcepto.SelectedItem.ToString(),
                Observaciones = txtObservaciones.Text,
                Recibio = cmbIDRecibio.SelectedItem.ToString(),
                FechaPago = DateTime.Now,
                Parent_ID = Venta.Id.ToString()
            };
            decimal cantidad = Convert.ToDecimal(p.Pago);
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


    }
}
