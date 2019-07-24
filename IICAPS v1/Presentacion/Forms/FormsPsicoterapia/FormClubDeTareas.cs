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
    public partial class FormClubDeTareas : Form
    {
        ControlIicaps control;
        ClubDeTareas ClubDeTareas;
        List<string> empleados = new List<string>();
        int empleadosCount=0;
        public FormClubDeTareas(ClubDeTareas club)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            //LLENAR COMBOBOX PSICOTERAPUTAS
            Llenar_ComboBox_Personal();
            if(club==null)
                this.ClubDeTareas = new ClubDeTareas();
            else
            {
                this.ClubDeTareas = club;
                txtFecha.Value = ClubDeTareas.Fecha;
                txtCosto.Value = ClubDeTareas.Costo;
                txtObservaciones.Text = ClubDeTareas.Observaciones;
                cmbPsicoteraputa.SelectedIndex = empleados.IndexOf(ClubDeTareas.Encargado);
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                this.ClubDeTareas.Fecha = txtFecha.Value;
                this.ClubDeTareas.Hora = new TimeSpan(txtFecha.Value.Hour,txtHora.Value.Minute,0);
                this.ClubDeTareas.Costo = txtCosto.Value;
                this.ClubDeTareas.Encargado = empleados.ElementAt(cmbPsicoteraputa.SelectedIndex);
                this.ClubDeTareas.Observaciones = txtObservaciones.Text;
                try
                {
                    if (this.ClubDeTareas.ID != 0)
                    {
                        if (control.ActualizarClubDeTareas(this.ClubDeTareas))
                        {
                            MessageBox.Show("Datos actualizados exitosamente!");
                            Close();
                            Dispose();
                        }
                        else
                            MessageBox.Show("Error al guardar datos, verifique los campos y vuelva a intentarlo");
                    }
                    else
                    {
                        if (control.AgregarClubDeTareas(this.ClubDeTareas))
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
            else
                MessageBox.Show("No dejar campos vacios");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool validarCampos()
        {
            if (txtHora.Text != "" && txtFecha.Text != "" && cmbPsicoteraputa.SelectedIndex!=-1)
                return true;
            return false;
        }

        private void Llenar_ComboBox_Personal()
        {
            empleados = new List<string>();
            List<String> aux = new List<string>();
            try
            {
                List<Empleado> lista = new List<Empleado>();
                lista = control.ObtenerEmpleados();
                empleadosCount = lista.Count;
                foreach (Empleado e in lista)
                {
                    aux.Add("E: " + e.Nombre);
                    empleados.Add(e.Matricula);
                }
            }
            catch (Exception ex) { }
            try
            {
                List<Psicoterapeuta> lista = new List<Psicoterapeuta>();
                lista = control.ObtenerPsicoterapeutas();
                foreach (Psicoterapeuta e in lista)
                {
                    aux.Add("P: " + e.Nombre);
                    empleados.Add(e.Matricula);
                }
            }
            catch (Exception ex) { }
            cmbPsicoteraputa.Items.AddRange(aux.ToArray());
        }
    }
}
