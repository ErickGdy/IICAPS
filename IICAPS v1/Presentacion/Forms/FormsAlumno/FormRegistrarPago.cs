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
    public partial class FormRegistrarPago : Form
    {
        ControlIicaps control;
        PagoAlumno pagos;
        public FormRegistrarPago(PagoAlumno pago, bool consultar)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            List<String> auxPrograma = new List<string>();
            List<String> auxIDPrograma = new List<string>();
            List<String> auxAlumno = new List<string>();
            List<String> auxIDAlumno = new List<string>();
            List<String> auxRecibio = new List<string>();
            List<String> auxIDRecibio = new List<string>();
            List<String> auxConcepto = new List<string>();
            foreach (string c in control.obtenerConceptosDePagoAlumno("Escuela"))
            {
                auxConcepto.Add(c);
            }
            cmbConcepto.Items.AddRange(auxConcepto.ToArray());
            foreach (Programa p in control.obtenerProgramas())
            {
                auxPrograma.Add(p.Nombre);
                auxIDPrograma.Add(p.Codigo.ToString());
            }
            cmbIDPrograma.Items.AddRange(auxIDPrograma.ToArray());
            cmbPrograma.Items.AddRange(auxPrograma.ToArray());
            foreach (Empleados e in control.obtenerEmpleados())
            {
                auxRecibio.Add(e.nombre);
                auxIDRecibio.Add(e.correo);
            }
            cmbIDRecibio.Items.AddRange(auxIDRecibio.ToArray());
            cmbRecibio.Items.AddRange(auxRecibio.ToArray());
            if (pago != null)
            {
                pagos = pago;
                cmbIDPrograma.SelectedItem = control.obtenerProgramaAlumno(pago.alumnoID);
                cmbIDRecibio.SelectedItem = pago.recibio;
                cmbPrograma.SelectedIndex = cmbIDPrograma.SelectedIndex;
                cmbIDAlumno.SelectedItem = pago.alumnoID;
                cmbAlumno.SelectedIndex = cmbIDAlumno.SelectedIndex;
                cmbRecibio.SelectedIndex = cmbIDRecibio.SelectedIndex;
                cmbConcepto.SelectedItem = pago.concepto;
                numericUpDown1.Value = Convert.ToDecimal(pago.cantidad);
                txtObservaciones.Text = pago.observaciones;
                if (consultar)
                {
                    cmbPrograma.Enabled = false;
                    cmbAlumno.Enabled = false;
                    cmbRecibio.Enabled = false;
                    numericUpDown1.Enabled = false;
                    cmbConcepto.Enabled = false;
                    txtObservaciones.Enabled = false;
                    btnAceptar.Enabled = false;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!agregarPago())
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

        private bool agregarPago()
        {
            cmbIDPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
            cmbIDAlumno.SelectedIndex = cmbAlumno.SelectedIndex;
            cmbIDRecibio.SelectedIndex = cmbRecibio.SelectedIndex;
            PagoAlumno p = new PagoAlumno();
            p.alumnoID = cmbIDAlumno.SelectedItem.ToString();
            p.cantidad = Convert.ToDouble(numericUpDown1.Value);
            p.concepto = cmbConcepto.SelectedItem.ToString();
            p.observaciones = txtObservaciones.Text;
            p.recibio = cmbIDRecibio.SelectedItem.ToString();
            p.fechaPago = DateTime.Now;
                if (control.agregarPagoAlumno(p))
                {
                    DocumentosWord word = new DocumentosWord(p);
                    return true;
                }
                else
                    throw new Exception("Error al agregar los datos de la entrega de documentos");
        }

        private void cmbPrograma_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIDPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
            List<String> auxAlumno = new List<string>();
            List<String> auxIDAlumno = new List<string>();
            foreach (Alumno a in control.obtenerAlumnosByPrograma(cmbIDPrograma.SelectedItem.ToString()))
            {
                auxAlumno.Add(a.nombre);
                auxIDAlumno.Add(a.rfc.ToString());
            }
            cmbIDAlumno.Items.AddRange(auxIDAlumno.ToArray());
            cmbAlumno.Items.AddRange(auxAlumno.ToArray());
            cmbAlumno.SelectedIndex = 0;
        }
    }
}
