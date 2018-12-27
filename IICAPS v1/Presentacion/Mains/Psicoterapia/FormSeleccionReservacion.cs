using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IICAPS_v1.Presentacion.Mains.Psicoterapia
{
    public partial class FormSeleccionReservacion : Form
    {
        public string codigo_reservacion="";
        public FormSeleccionReservacion(List<string> reservaciones)
        {
            InitializeComponent();
            cmbTipo.Items.AddRange(reservaciones.ToArray());
            cmbTipo.SelectedIndex = 0;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            codigo_reservacion = cmbTipo.SelectedItem.ToString();
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public string getCodigo_Reservacion()
        {
            return codigo_reservacion;
        }

    }
}
