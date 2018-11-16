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
        Taller taller;
        TallerAsistente asistenT;
        public FormInscricionTaller(string idTaller, TallerAsistente asistente)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            List<String> auxNombres = new List<string>();
            auxId = new List<string>();
            try
            {
                foreach (Taller p in control.obtenerTalleres())
                {
                    auxNombres.Add(p.nombre);
                    auxId.Add(p.id.ToString());
                }
                cmbTalleres.DataSource = auxNombres;
                if (idTaller != null)
                {
                    cmbTalleres.SelectedIndex = auxId.IndexOf(idTaller);
                    cmbTalleres.Enabled = false;
                }
            }
            catch (Exception ex)
            {
            }
            cmbTipoCosto.SelectedIndex = 0;
            if (asistente != null)
            {
                asistenT = asistente;
                cmbTalleres.SelectedIndex = auxId.IndexOf(asistente.taller.ToString());
                txtNombre.Text = asistente.nombre;
                txtTelefono.Text = asistente.telefono;
                txtCorreo.Text = asistente.correo;
                txtCurp.Text = asistente.curp;
                txtRFC.Text = asistente.rfc;
                txtAnticipo.Value = asistente.pago;
                lblAnticipo.Text="Pago: $";
                txtAnticipo.ReadOnly = true;
                txtCosto.Value = asistente.costo;
                txtObservaciones.Text = asistente.observaciones;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
                asistenT.taller = Convert.ToInt32(auxId.ElementAt(cmbTalleres.SelectedIndex));
                asistenT.nombre = txtNombre.Text;
                asistenT.telefono = txtTelefono.Text;
                asistenT.correo = txtCorreo.Text;
                asistenT.curp = txtCurp.Text;
                asistenT.rfc = txtRFC.Text;
                asistenT.pago = txtAnticipo.Value;
                asistenT.costo = txtCosto.Value;
                asistenT.observaciones = txtObservaciones.Text;
                try
                {
                if (control.registrarAsistenteTaller(asistenT))
                {
                    MessageBox.Show("Asistencia registrada exitosamente!");
                    this.Hide();
                    if (!txtAnticipo.ReadOnly)
                    {
                        FormPago fp = new FormPago(asistenT.pago, "Pago de Taller", "Escuela");
                        fp.ShowDialog();
                        Pago pago = fp.getPagos();
                        if (control.registrarPagoAsistenciaTaller(pago, control.obtenerAsistentesTalleres(asistenT.taller.ToString()).Last().ID.ToString()))
                        {
                            MessageBox.Show("Pago registrado exitosamente");
                            DocumentosWord word = new DocumentosWord(pago);
                        }
                        else
                            throw new Exception("Error al registrar pago");
                    }
                    Close();
                    Dispose();
                }
                else
                    MessageBox.Show("Error al guardar datos, verifique los campos y compruebe que el asistente no se encuentre registrado y vuelva a intentarlo");
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

        private void cmbTipoCosto_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbTipoCosto.SelectedIndex)
            {
                case 0:
                    txtCosto.Enabled = false;
                    txtCosto.Value = taller.costoPublico;
                    break;
                case 1:
                    txtCosto.Enabled = false;
                    txtCosto.Value = taller.costoClientes;
                    break;
                case 2:
                    txtCosto.Enabled = true;
                    break;
                default:
                    txtCosto.Enabled = false;
                    txtCosto.Value = 0;
                    break;
            }
        }

        private void cmbTalleres_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                taller = control.consultarTaller(auxId.ElementAt(cmbTalleres.SelectedIndex));
                cmbTipoCosto_SelectedIndexChanged(null,null);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al obtener datos del taller");
            }
        }

    }
}
