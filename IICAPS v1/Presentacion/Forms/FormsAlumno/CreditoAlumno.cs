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
    public partial class CreditoAlumnos : Form
    {
        ControlIicaps control;
        bool modificacion = false;
        public CreditoAlumnos(CreditoAlumno c)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            List<String> auxPrograma = new List<string>();
            List<String> auxIDPrograma = new List<string>();
            List<String> auxAlumno = new List<string>();
            List<String> auxIDAlumno = new List<string>();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            foreach (Programa p in control.obtenerProgramas())
            {
                auxPrograma.Add(p.Nombre);
                auxIDPrograma.Add(p.Codigo.ToString());
            }
            cmbPrograma.DataSource = auxPrograma;
            cmbAlumno.DataSource = auxAlumno;
            cmbIDPrograma.DataSource = auxIDPrograma;
            cmbIDAlumno.DataSource = auxIDAlumno;
            if (c != null)
            {
                modificacion = true;
                cmbPrograma.SelectedItem = control.obtenerProgramaAlumno(c.alumno);
                cmbAlumno.SelectedItem = c.alumno;
                cmbIDPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
                cmbIDAlumno.SelectedIndex = cmbAlumno.SelectedIndex;
                numMensualidad.Value = Convert.ToDecimal(c.cantidadMensualidad);
                numCantidad.Value = c.cantidadMeses;
                txtObservaciones.Text = c.observaciones;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
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
            //String programa = cmbPrograma.SelectedItem.ToString();
            double inscripcion = 500, meses = 0;
            //if (programa.Contains("Maestria") || programa.Contains("MAESTRIA") || programa.Contains("Maestría") || programa.Contains("MAESTRÍA"))
            //{
                //cuanto este la informacion de los programas cambiar los parametros por querys
                double pagomensualidad = 0, pagocredito = 0, maestria = 62500, costocredito = 5000;
                pagomensualidad = Convert.ToDouble(numMensualidad.Value);
                meses = (maestria + costocredito) / pagomensualidad;
                meses = Math.Ceiling(meses);
                pagomensualidad = maestria / meses;
                pagocredito = costocredito / meses;
                numCantidad.Value = Convert.ToDecimal(meses);
                lblMensualidad.Text = lblMensualidad.Text + pagomensualidad.ToString();
                lblCredito.Text = lblCredito.Text + pagocredito.ToString();
            //}
        }

        private bool validarCampos()
        {
            if (cmbPrograma.SelectedItem != null && cmbAlumno.SelectedItem != null && numMensualidad.Value != 0 && numCantidad.Value != 0 && txtObservaciones.Text != "")
                return true;
            return false;
        }

        private bool agregarCredito()
        {
            if (validarCampos())
            {
                cmbIDPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
                cmbIDAlumno.SelectedIndex = cmbAlumno.SelectedIndex;
                CreditoAlumno c = new CreditoAlumno();
                c.alumno = cmbAlumno.SelectedItem.ToString();
                c.cantidadMensualidad = Convert.ToDouble(numMensualidad.Value);
                c.cantidadMeses = Convert.ToInt32(numCantidad.Value);
                c.observaciones = txtObservaciones.Text;
                if (modificacion)
                {
                    c.alumno = cmbAlumno.SelectedItem.ToString();
                    if (control.actualizarCredito(c))
                        return true;
                    else
                        throw new Exception("Error al actualizar los datos del credito");
                }
                else
                {
                    if (control.agregarCreditoAlumno(c))
                        return true;
                    else
                        throw new Exception("Error al agregar el credito");
                }
            }
            else
                MessageBox.Show("No deje ningun campo vacio");
            return false;
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
            cmbAlumno.DataSource = auxAlumno;
            cmbIDAlumno.DataSource = auxIDAlumno;
        }
    }
}
