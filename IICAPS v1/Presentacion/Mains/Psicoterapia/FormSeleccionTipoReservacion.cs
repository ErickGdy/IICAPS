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
    public partial class FormSeleccionTipoReservacion : Form
    {
        public string tipo="";
        public FormSeleccionTipoReservacion()
        {
            InitializeComponent();
            cmbTipo.SelectedIndex = 0;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            tipo = cmbTipo.SelectedItem.ToString();
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public string getTipo()
        {
            return tipo;
        }

    }
}
