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
    public partial class FormInscricionTaller : Form
    {
        ControlIicaps control;
        List<String> auxId;
        public FormInscricionTaller(string id)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            List<String> auxNombres = new List<string>();
            auxId = new List<string>();
            foreach (Taller p in control.obtenerTalleres())
            {
                auxNombres.Add(p.nombre);
                auxId.Add(p.id.ToString());
            }
            cmbTalleres.DataSource = auxNombres;
            if (id != null)
            {
                cmbTalleres.SelectedIndex= auxId.IndexOf(id);
                cmbTalleres.Enabled = false;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            TallerAsistente asistente = new TallerAsistente();
            asistente.taller = Convert.ToInt32(auxId.ElementAt(cmbTalleres.SelectedIndex));
            asistente.nombre = txtNombre.Text;
            asistente.telefono = txtTelefono.Text;
            asistente.correo = txtCorreo.Text;
            asistente.pago = txtPago.Value;
            try
            {
                if (control.registrarAsistenteTaller(asistente))
                {
                    MessageBox.Show("Asistencia registrada exitosamente!");
                    Close();
                    Dispose();
                }
                else
                    MessageBox.Show("Error al guardar datos, verifique los campos y vuelva a intentarlo");
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
