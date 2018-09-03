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
    public partial class FormPago : Form
    {
        ControlIicaps control;
        Pago pago;
        List<String> empleados;
        bool consultar;
        public FormPago(Pago pago, bool cons)
        {
            InitializeComponent();
            pago = new Pago();
            consultar = cons;
            control = ControlIicaps.getInstance();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            empleados = new List<string>();
            List<String> aux = new List<string>();
            foreach (string c in control.obtenerConceptosDePagos("Escuela"))
            {
                aux.Add(c);
            }
            cmbConcepto.Items.AddRange(aux.ToArray());
            cmbConcepto.SelectedItem = pago.concepto;
            aux.Clear();
            foreach (Empleados e in control.obtenerEmpleados())
            {
                aux.Add(e.nombre);
                empleados.Add(e.correo);
            }
            cmbRecibio.Items.AddRange(aux.ToArray());
            if (pago != null)
            {
                this.pago = pago;
                cmbRecibio.SelectedIndex = empleados.IndexOf(pago.recibio);
                cmbConcepto.SelectedItem = pago.concepto;
                txtCantidad.Value = Convert.ToDecimal(pago.cantidad);
                txtObservaciones.Text = pago.observaciones;
                txtEmisor.Text = pago.emisor;
                lblFolio.Text = pago.formatoFolio();
                lblFolio.Visible = true;
                lblFolio1.Visible = true;
                if (consultar)
                {
                    cmbRecibio.Enabled = false;
                    txtCantidad.Enabled = false;
                    cmbConcepto.Enabled = false;
                    txtObservaciones.Enabled = false;
                    btnAceptar.Enabled = false;
                    txtEmisor.Enabled = false;
                }
            }
        }
        public FormPago(decimal cantidad, string concepto)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            empleados = new List<string>();
            List<String> aux = new List<string>();
            pago = new Pago();
            foreach (string c in control.obtenerConceptosDePagos("Escuela"))
            {
                aux.Add(c);
            }
            cmbConcepto.Items.AddRange(aux.ToArray());
            cmbConcepto.SelectedItem = concepto;
            aux.Clear();
            foreach (Empleados e in control.obtenerEmpleados())
            {
                aux.Add(e.nombre);
                empleados.Add(e.correo);
            }
            cmbRecibio.Items.AddRange(aux.ToArray());
            txtCantidad.Value = cantidad;
            txtCantidad.Maximum = cantidad;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if(consultar)
                    Dispose();
                if (validarCampos()) {
                    pago.cantidad = Convert.ToDouble(txtCantidad.Value);
                    pago.concepto = cmbConcepto.SelectedItem.ToString();
                    pago.observaciones = txtObservaciones.Text;
                    pago.recibio = empleados.ElementAt(cmbRecibio.SelectedIndex);
                    pago.fechaPago = DateTime.Now;
                    pago.emisor = txtEmisor.Text;
                    pago.id = control.obtenerUltimoIDPagos();
                    Close();
                }else
                {
                    MessageBox.Show("No dejar campos vacios");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public Pago getPagos()
        {
            return pago;
        }
         
        private bool validarCampos()
        {
            if (txtCantidad.Value > 0 && cmbConcepto.SelectedIndex >= 0 && cmbRecibio.SelectedIndex >= 0 && txtEmisor.Text != "")
                return true;
            else
                return false;
        }

    }
}
