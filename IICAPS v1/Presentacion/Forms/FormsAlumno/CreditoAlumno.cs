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
    public partial class CreditoAlumnos : Form
    {
        ControlIicaps control;
        bool modificacion = false;
        CreditoAlumno c;
        decimal varaux1 = 0, varaux2 = 0;
        public CreditoAlumnos(CreditoAlumno c, bool consultar)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            List<String> auxPrograma = new List<string>();
            List<String> auxIDPrograma = new List<string>();
            List<String> auxAlumno = new List<string>();
            List<String> auxIDAlumno = new List<string>();
            lblFecha.Text = DateTime.Now.ToShortDateString();
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
            numCantidad.Enabled = false;
            if (c != null)
            {
                modificacion = true;
                cmbIDPrograma.SelectedItem = control.ObtenerProgramaAlumno(c.Alumno);
                cmbPrograma.SelectedIndex = cmbIDPrograma.SelectedIndex;
                cmbPrograma.Enabled = false;
                cmbIDAlumno.SelectedItem = c.Alumno;                
                cmbAlumno.SelectedIndex = cmbIDAlumno.SelectedIndex;
                cmbAlumno.Enabled = false;
                numMensualidad.Value = Convert.ToDecimal(c.CantidadMensualidad);
                numCantidad.Value = c.CantidadMeses;
                txtObservaciones.Text = c.Observaciones;
                lblCredito.Text = lblCredito.Text + c.CantidadAbonoCredito.ToString();
                lblMensualidad.Text = lblMensualidad.Text + c.CantidadAbonoMensual.ToString();
                lblPago.Text = "Pagado: $"+c.Pago;
                if (consultar)
                {
                    cmbPrograma.Enabled = false;
                    cmbAlumno.Enabled = false;
                    numMensualidad.Enabled = false;
                    txtObservaciones.Enabled = false;
                    btnCalcular.Enabled = false;
                }
            }
        }
        public CreditoAlumnos(CreditoAlumno c, Alumno alumno)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            List<String> auxPrograma = new List<string>();
            List<String> auxIDPrograma = new List<string>();
            List<String> auxAlumno = new List<string>();
            List<String> auxIDAlumno = new List<string>();
            lblFecha.Text = DateTime.Now.ToShortDateString();
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
            numCantidad.Enabled = false;
            if (c != null)
            {
                modificacion = true;
                try
                {
                    cmbIDPrograma.SelectedItem = control.ObtenerProgramaAlumno(c.Alumno);
                }
                catch (Exception ex) { }
                cmbPrograma.SelectedIndex = cmbIDPrograma.SelectedIndex;
                cmbPrograma.Enabled = false;
                cmbIDAlumno.SelectedItem = c.Alumno;
                cmbAlumno.SelectedIndex = cmbIDAlumno.SelectedIndex;
                cmbAlumno.Enabled = false;
                numMensualidad.Value = Convert.ToDecimal(c.CantidadMensualidad);
                numCantidad.Value = c.CantidadMeses;
                txtObservaciones.Text = c.Observaciones;
                lblCredito.Text = lblCredito.Text + c.CantidadAbonoCredito.ToString();
                lblMensualidad.Text = lblMensualidad.Text + c.CantidadAbonoMensual.ToString();
                lblPago.Text = "Pagado: $" + c.Pago;
            }else
            {
                if (alumno != null)
                {
                    try
                    {
                        lblPago.Text = "Pagado: $" + control.ConsultarCobroColegiatura(alumno.Rfc).Pago.ToString();
                        cmbIDPrograma.SelectedItem = control.ObtenerProgramaAlumno(alumno.Rfc);
                    }
                    catch (Exception ex) { }
                    cmbPrograma.SelectedIndex = cmbIDPrograma.SelectedIndex;
                    cmbPrograma.Enabled = false;
                    cmbAlumno.SelectedItem = alumno.Nombre;
                    cmbAlumno.Enabled = false;
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validarCampos())
                {
                    if (!agregarCredito())
                        MessageBox.Show("Error al guardar los datos del credito");
                    else
                    {
                        MessageBox.Show("Datos del credito guardados exitosamente");
                        Close();
                        Dispose();
                    }
                }
                else
                    MessageBox.Show("No deje ningun campo vacio");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                Programa programa = control.ConsultarPrograma(cmbIDPrograma.SelectedItem.ToString());
                List<Materia> lista = control.ConsultarMapaCurricularPrograma(cmbIDPrograma.SelectedItem.ToString());
                decimal totalmaterias = 0, grantotal = 0;
                foreach (Materia materia in lista)
                {
                    totalmaterias += Convert.ToDecimal(materia.Costo);
                }
                decimal costoCredito;
                if (programa.Nivel.Contains("Maestria") || programa.Nivel.Contains("MAESTRIA") || programa.Nivel.Contains("Maestría") || programa.Nivel.Contains("MAESTRÍA"))
                    costoCredito = control.parametros_Generales.Costo_Credito_Maestria;
                else
                    costoCredito = control.parametros_Generales.Costo_Credito_Especialidad_Diplomado;
                grantotal = totalmaterias + costoCredito - Convert.ToDecimal(lblPago.Text.Substring(9));
                decimal aux1 = grantotal / Convert.ToDecimal(numMensualidad.Value);
                numCantidad.Value = Decimal.Round(aux1);
                decimal var1 = Decimal.Round(costoCredito / Convert.ToDecimal(numCantidad.Value));
                decimal var2 = Decimal.Round((totalmaterias - Convert.ToDecimal(lblPago.Text.Substring(9))) / Convert.ToDecimal(numCantidad.Value));
                varaux1 = var1;
                varaux2 = var2;
                lblCredito.Text = "Crédito: $" + var1.ToString("F");
                lblMensualidad.Text = "Mensualidad: $" + var2.ToString("F");
                decimal calculo = var1 + var2;
                if (calculo!= numMensualidad.Value)
                {
                    numMensualidad.Value = calculo;
                    MessageBox.Show("Mensualidad calculada en $"+calculo+" por un periodo de "+numCantidad.Value + " meses");
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Error al calcular montos, verifique los datos proporcionados e intente de nuevo");
            }
            
        }

        private bool validarCampos()
        {
            if (numMensualidad.Value != 0 && numCantidad.Value != 0 && txtObservaciones.Text != "")
                return true;
            return false;
        }

        private bool agregarCredito()
        {           
            cmbIDPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
            cmbIDAlumno.SelectedIndex = cmbAlumno.SelectedIndex;
            c = new CreditoAlumno();
            c.Alumno = cmbIDAlumno.SelectedItem.ToString();
            c.CantidadMensualidad = Convert.ToDecimal(numMensualidad.Value);
            c.CantidadMeses = Convert.ToInt32(numCantidad.Value);
            c.Observaciones = txtObservaciones.Text;
            c.CantidadAbonoCredito = varaux1;
            c.CantidadAbonoMensual = varaux2;
            c.Estado = "Activo";
            if (modificacion)
            {
                c.Alumno = cmbIDAlumno.SelectedItem.ToString();
                if (control.ActualizarCredito(c))
                {
                    //Run method in a thread
                    Thread t = new Thread(new ThreadStart(ThreadMethodDocumentos));
                    t.Start();
                    return true;
                }
                else
                    throw new Exception("Error al actualizar los datos del credito");
            }
            else
            {
                if (control.AgregarCreditoAlumno(c))
                {
                    Thread t = new Thread(new ThreadStart(ThreadMethodDocumentos));
                    t.Start();
                    return true;
                }
                else
                    throw new Exception("Error al agregar el credito");
            }
        }

        private void CreditoAlumnos_Load(object sender, EventArgs e)
        {

        }

        private void cmbPrograma_SelectedIndexChanged(object sender, EventArgs e)
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
        private void ThreadMethodDocumentos()
        {
            DocumentosWord word = new DocumentosWord(c);
        }
    }
}
