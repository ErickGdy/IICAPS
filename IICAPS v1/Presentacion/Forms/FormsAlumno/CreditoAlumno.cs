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
            foreach (Programa p in control.obtenerProgramas())
            {
                auxPrograma.Add(p.Nombre);
                auxIDPrograma.Add(p.Codigo.ToString());
            }
            cmbIDPrograma.Items.AddRange(auxIDPrograma.ToArray());
            cmbPrograma.Items.AddRange(auxPrograma.ToArray());
            numCantidad.Enabled = false;
            if (c != null)
            {
                modificacion = true;
                cmbIDPrograma.SelectedItem = control.obtenerProgramaAlumno(c.alumno);
                cmbPrograma.SelectedIndex = cmbIDPrograma.SelectedIndex;
                cmbIDAlumno.SelectedItem = c.alumno;                
                cmbAlumno.SelectedIndex = cmbIDAlumno.SelectedIndex;
                numMensualidad.Value = Convert.ToDecimal(c.cantidadMensualidad);
                numCantidad.Value = c.cantidadMeses;
                txtObservaciones.Text = c.observaciones;
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
            Programa programa = control.consultarPrograma(cmbIDPrograma.SelectedItem.ToString());
            List<Materia> lista = control.consultarMapaCurricularPrograma(cmbIDPrograma.SelectedItem.ToString());
            decimal totalmaterias = 0, grantotal = 0;
            foreach (Materia materia in lista)
            {
                totalmaterias += Convert.ToDecimal(materia.costo);
            }
            decimal costoCredito;
            if (programa.Nivel.Contains("Maestria") || programa.Nivel.Contains("MAESTRIA") || programa.Nivel.Contains("Maestría") || programa.Nivel.Contains("MAESTRÍA"))
                costoCredito = control.parametros_Generales.Costo_Credito_Maestria;
            else
                costoCredito = control.parametros_Generales.Costo_Credito_Maestria;
            grantotal = totalmaterias + costoCredito;
            decimal aux1 = grantotal / Convert.ToDecimal(numMensualidad.Value);
            numCantidad.Value = Convert.ToDecimal(aux1);
            decimal var1 = costoCredito / Convert.ToDecimal(numCantidad.Value);
            decimal var2 = totalmaterias / Convert.ToDecimal(numCantidad.Value);
            varaux1 = var1;
            varaux2 = var2;
            lblCredito.Text = lblCredito.Text + var1.ToString();
            lblMensualidad.Text = lblMensualidad.Text + var2.ToString();
            
        }

        private bool validarCampos()
        {
            if (numMensualidad.Value != 0 && numCantidad.Value != 0 && txtObservaciones.Text != "")
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
                c.alumno = cmbIDAlumno.SelectedItem.ToString();
                c.cantidadMensualidad = Convert.ToDecimal(numMensualidad.Value);
                c.cantidadMeses = Convert.ToInt32(numCantidad.Value);
                c.observaciones = txtObservaciones.Text;
                c.cantidadAbonoCredito = varaux1;
                c.cantidadAbonoMensual = varaux2;
                c.estado = "Aprobado";
                if (modificacion)
                {
                    c.alumno = cmbIDAlumno.SelectedItem.ToString();
                    if (control.actualizarCredito(c))
                    {
                        DocumentosWord word = new DocumentosWord(c);
                        return true;
                    }
                    else
                        throw new Exception("Error al actualizar los datos del credito");
                }
                else
                {
                    if (control.agregarCreditoAlumno(c))
                    {
                        DocumentosWord word = new DocumentosWord(c);
                        return true;
                    }
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
            cmbIDAlumno.Items.AddRange(auxIDAlumno.ToArray());
            cmbAlumno.Items.AddRange(auxAlumno.ToArray());
            cmbAlumno.SelectedIndex = 0;
        }
    }
}
