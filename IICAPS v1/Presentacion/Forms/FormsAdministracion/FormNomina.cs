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
    public partial class FormNomina : Form
    {
        ControlIicaps control;
        Nomina nomina;
        List<string> empleados = new List<string>();
        List<string> empleadosNombre = new List<string>();
        List<string> psicoterapeutas = new List<string>();
        List<string> psicoterapeutasNombre = new List<string>();
        public FormNomina(Nomina nomina)
        {
            InitializeComponent();
            try
            {
                control = ControlIicaps.getInstance();
                foreach (Empleado aux in control.obtenerEmpleados())
                {
                    empleadosNombre.Add(aux.Nombre);
                    empleados.Add(aux.Matricula);
                }
                cmbEntrega.DataSource = empleadosNombre;
                foreach (Psicoterapeuta aux in control.obtenerPsicoterapeutas())
                {
                    psicoterapeutasNombre.Add(aux.Nombre);
                    psicoterapeutas.Add(aux.Matricula);
                }
                cmbPsicoterapeuta.DataSource = psicoterapeutasNombre;
                if (nomina == null)
                    this.nomina = new Nomina();
                else
                {
                    this.nomina = nomina;
                    txtCantidad.Text = nomina.Total.ToString();
                    txtFechafin.Value = nomina.FechaFin;
                    txtFechaInicio.Value = nomina.FechaInicio;
                    cmbPsicoterapeuta.SelectedIndex = psicoterapeutas.IndexOf(nomina.Psicoterapeutas);
                    if(nomina.Entrego!=null)
                        cmbEntrega.SelectedItem = empleados.IndexOf(nomina.Entrego);
                }
            }
            catch (Exception ex) { }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            try
            {
                nomina.FechaInicio = txtFechaInicio.Value;
                nomina.FechaFin = txtFechafin.Value;
                nomina.Psicoterapeutas = psicoterapeutas.ElementAt(cmbPsicoterapeuta.SelectedIndex);
                nomina.Entrego = empleados.ElementAt(cmbEntrega.SelectedIndex);
                nomina.Total = Convert.ToDecimal(txtCantidad.Text);
                if (this.nomina.ID != 0)
                {
                    if (control.actualizarNomina(this.nomina))
                    {
                        MessageBox.Show("Datos de nomina actualizados exitosamente!");
                        Close();
                        Dispose();
                    }
                    else
                        MessageBox.Show("Error al guardar datos, verifique los campos y vuelva a intentarlo");
                }
                else
                {
                    nomina.DiaEntrega = new DateTime();
                    if (control.agregarNomina(this.nomina))
                    {
                        MessageBox.Show("Datos guardados exitosamente!");
                        Close();
                        Dispose();
                    }
                    else
                        MessageBox.Show("Error al guardar datos, verifique los campos y vuelva a intentarlo");
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

    }
}
