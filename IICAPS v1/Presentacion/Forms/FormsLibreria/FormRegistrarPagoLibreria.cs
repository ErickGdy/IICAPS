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
        PagoLibreria p;
        public FormRegistrarPagoLibreria(PagoLibreria pago, bool consultar)
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
            try
            {
                auxConcepto.Add("Pago de Adeudo General");
                foreach (string c in control.ObtenerConceptosDePagoLibreria("libreria"))
                {
                    auxConcepto.Add(c);
                }
                cmbConcepto.Items.AddRange(auxConcepto.ToArray());
            }
            catch (Exception ex) { }
            try
            {
                foreach (Programa p in control.ObtenerProgramas())
                {
                    auxPrograma.Add(p.Nombre);
                    auxIDPrograma.Add(p.Codigo.ToString());
                }
                cmbIDPrograma.Items.AddRange(auxIDPrograma.ToArray());
                cmbPrograma.Items.AddRange(auxPrograma.ToArray());
            }
            catch (Exception ex) { }
            try
            {
                foreach (Empleado e in control.ObtenerEmpleados())
                {
                    auxRecibio.Add(e.Nombre);
                    auxIDRecibio.Add(e.Matricula);
                }
                cmbIDRecibio.Items.AddRange(auxIDRecibio.ToArray());
                cmbRecibio.Items.AddRange(auxRecibio.ToArray());
            }
            catch (Exception ex) { }
            try
            {
                if (pago != null)
                {
                    pagos = pago;
                    cmbIDPrograma.SelectedItem = control.ObtenerProgramaAlumno(pago.CompradorID);
                    cmbIDRecibio.SelectedItem = pago.Recibio;
                    cmbPrograma.SelectedIndex = cmbIDPrograma.SelectedIndex;
                    cmbIDAlumno.SelectedItem = pago.CompradorID;
                    cmbAlumno.SelectedIndex = cmbIDAlumno.SelectedIndex;
                    cmbRecibio.SelectedIndex = cmbIDRecibio.SelectedIndex;
                    cmbConcepto.SelectedItem = pago.Concepto;
                    cmbAlumno.Enabled = false; 
                    cmbPrograma.Enabled = false;
                    numericUpDown1.Maximum = Convert.ToDecimal(pago.Cantidad);
                    numericUpDown1.Value = Convert.ToDecimal(pago.Cantidad);
                    txtObservaciones.Text = pago.Observaciones;
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
            cmbIDPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
            cmbIDAlumno.SelectedIndex = cmbAlumno.SelectedIndex;
            cmbIDRecibio.SelectedIndex = cmbRecibio.SelectedIndex;
            p = new PagoLibreria
            {
                CompradorID = cmbIDAlumno.SelectedItem.ToString(),
                Cantidad = Convert.ToDecimal(numericUpDown1.Value),
                Concepto = cmbConcepto.SelectedItem.ToString(),
                Observaciones = txtObservaciones.Text,
                Recibio = cmbIDRecibio.SelectedItem.ToString(),
                FechaPago = DateTime.Now
            };
            List<Cobro> pagoPendientes = control.ConsultarCobrosDeAlumnoLibreria(p.CompradorID);
            if (pagoPendientes != null) {
                decimal cantidad = Convert.ToDecimal(p.Cantidad);
                int posicion = 0;
                List<Cobro> cobrosActualizados = new List<Cobro>();
                while (cantidad > 0)
                {
                    Cobro aux = pagoPendientes.ElementAt(posicion);
                    if (aux.Restante > 0)
                    {
                        if (aux.Restante > cantidad)
                        {
                            aux.Restante -= cantidad;
                            aux.Pago += cantidad;
                            cantidad = 0;
                        }
                        else
                        {
                            cantidad -= aux.Restante;
                            aux.Restante = 0;
                            aux.Pago = aux.Cantidad;
                        }
                        cobrosActualizados.Add(aux);
                    }
                    posicion++;
                }
                if (control.AgregarPagoLibreria(p, cobrosActualizados))
                {
                    Thread t = new Thread(new ThreadStart(ThreadMethodDocumentos));
                    t.Start();
                    return true;
                }
                else
                    throw new Exception("Error al agregar pago del alumno");
            }
            return false;
            
        }
        private void ThreadMethodDocumentos()
        {
            //DocumentosWord word = new DocumentosWord(p);
        }

        private void CmbPrograma_selectedIndexChanged(object sender, EventArgs e)
        {
            cmbIDPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
            List<String> auxAlumno = new List<string>();
            List<String> auxIDAlumno = new List<string>();
            foreach (Alumno a in control.ObtenerAlumnosByPrograma(cmbIDPrograma.SelectedItem.ToString()))
            {
                auxAlumno.Add(a.Nombre);
                auxIDAlumno.Add(a.Rfc.ToString());
            }
            cmbIDAlumno.Items.AddRange(auxIDAlumno.ToArray());
            cmbAlumno.Items.AddRange(auxAlumno.ToArray());
            cmbAlumno.SelectedIndex = 0;
        }
    }
}
