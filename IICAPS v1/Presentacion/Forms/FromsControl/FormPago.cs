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
        public FormPago(Pago pago, bool cons, string modulo)
        {
            InitializeComponent();
            pago = new Pago();
            consultar = cons;
            control = ControlIicaps.getInstance();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            empleados = new List<string>();
            List<String> aux = new List<string>();
            foreach (string c in control.ObtenerConceptos("Pago",modulo))
            {
                aux.Add(c);
            }
            cmbConcepto.Items.AddRange(aux.ToArray());
            cmbConcepto.SelectedItem = pago.Concepto;
            aux.Clear();
            foreach (Empleado e in control.ObtenerEmpleados())
            {
                aux.Add(e.Nombre);
                empleados.Add(e.Matricula);
            }
            cmbRecibio.Items.AddRange(aux.ToArray());
            if (pago != null)
            {
                this.pago = pago;
                cmbRecibio.SelectedIndex = empleados.IndexOf(pago.Recibio);
                cmbConcepto.SelectedItem = pago.Concepto;
                txtCantidad.Value = Convert.ToDecimal(pago.Cantidad);
                txtObservaciones.Text = pago.Observaciones;
                txtEmisor.Text = pago.Emisor;
                lblFolio.Text = pago.Formato_folio();
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
        public FormPago(decimal cantidad, string concepto, string modulo)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            empleados = new List<string>();
            List<String> aux = new List<string>();
            pago = new Pago();
            foreach (string c in control.ObtenerConceptos("Pago", modulo))
            {
                aux.Add(c);
            }
            cmbConcepto.Items.AddRange(aux.ToArray());
            cmbConcepto.SelectedItem = concepto;
            aux.Clear();
            foreach (Empleado e in control.ObtenerEmpleados())
            {
                aux.Add(e.Nombre);
                empleados.Add(e.Matricula);
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
                    pago.Cantidad = Convert.ToDouble(txtCantidad.Value);
                    pago.Concepto = cmbConcepto.SelectedItem.ToString();
                    pago.Observaciones = txtObservaciones.Text;
                    pago.Recibio = empleados.ElementAt(cmbRecibio.SelectedIndex);
                    pago.FechaPago = DateTime.Now;
                    pago.Emisor = txtEmisor.Text;
                    pago.Id = control.ObtenerUltimoIDPagos();
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
