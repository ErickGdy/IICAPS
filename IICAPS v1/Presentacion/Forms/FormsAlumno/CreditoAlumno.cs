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
            if (c != null)
            {
                modificacion = true;
                cmbPrograma.SelectedItem = control.obtenerProgramaAlumno(c.alumno);
                cmbAlumno.SelectedItem = c.alumno;
                numMensualidad.Value = Convert.ToDecimal(c.cantidadMensualidad);
                numCantidad.Value = c.cantidadMeses;
                dateTimePicker1.Value = c.fechaSolicitud;
                txtObservaciones.Text = c.observaciones;
            }
        }

        public CreditoAlumnos(CreditoAlumno credito, bool c)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            if(credito != null)
            {
                modificacion = true;
                cmbPrograma.SelectedItem = control.obtenerProgramaAlumno(credito.alumno);
                cmbAlumno.SelectedItem = credito.alumno;
                numMensualidad.Value = Convert.ToDecimal(credito.cantidadMensualidad);
                numCantidad.Value = credito.cantidadMeses;
                dateTimePicker1.Value = credito.fechaSolicitud;
                txtObservaciones.Text = credito.observaciones;
            }
            if (c == true)
            {
                cmbPrograma.Enabled = false;
                cmbAlumno.Enabled = false;
                numMensualidad.ReadOnly = true;
                numCantidad.ReadOnly = true;
                dateTimePicker1.Enabled = false;
                txtObservaciones.ReadOnly = true;
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
            if (cmbPrograma.SelectedItem != null && cmbAlumno.SelectedItem != null && numMensualidad.Value != 0 && numCantidad.Value != 0 && dateTimePicker1.Value != null && txtObservaciones.Text != "")
                return true;
            return false;
        }

        private bool agregarCredito()
        {
            if (validarCampos())
            {
                CreditoAlumno c = new CreditoAlumno();
                c.alumno = cmbAlumno.SelectedItem.ToString();
                c.cantidadMensualidad = Convert.ToDouble(numMensualidad.Value);
                c.cantidadMeses = Convert.ToInt32(numCantidad.Value);
                c.fechaSolicitud = dateTimePicker1.Value;
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
    }
}
