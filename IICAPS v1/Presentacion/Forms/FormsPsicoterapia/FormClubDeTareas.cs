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
        List<int> psicoteraputasID;
        List<string> psicoteraputasNombres;
        public FormClubDeTareas(ClubDeTareas club)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            //LLENAR COMBOBOX PSICOTERAPUTAS
            if(club==null)
                this.ClubDeTareas = new ClubDeTareas();
            else
            {
                this.ClubDeTareas = club;
                txtFecha.Value = ClubDeTareas.fecha;
                txtCosto.Value = ClubDeTareas.Costo;
                txtObservaciones.Text = ClubDeTareas.Observaciones;
                cmbPsicoteraputa.SelectedIndex = psicoteraputasID.IndexOf(ClubDeTareas.Psicoterapeuta);
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                this.ClubDeTareas.fecha = txtFecha.Value;
                this.ClubDeTareas.Hora = new TimeSpan(txtFecha.Value.Hour,txtHora.Value.Minute,0);
                this.ClubDeTareas.Costo = txtCosto.Value;
                this.ClubDeTareas.Psicoterapeuta = psicoteraputasID.ElementAt(cmbPsicoteraputa.SelectedIndex);
                this.ClubDeTareas.Observaciones = txtObservaciones.Text;
                try
                {
                    if (this.ClubDeTareas.id != 0)
                    {
                        if (control.actualizarClubDeTareas(this.ClubDeTareas))
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
                        if (control.agregarClubDeTareas(this.ClubDeTareas))
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
    }
}
