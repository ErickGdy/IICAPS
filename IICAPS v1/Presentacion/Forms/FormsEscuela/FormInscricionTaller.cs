using IICAPS_v1.Control;
using IICAPS_v1.DataObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        Pago pago;
        public FormInscricionTaller(string idTaller, TallerAsistente asistente)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            List<String> auxNombres = new List<string>();
            auxId = new List<string>();
            try
            {
                foreach (Taller p in control.ObtenerTalleres())
                {
                    auxNombres.Add(p.Nombre);
                    auxId.Add(p.Id.ToString());
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
                cmbTalleres.SelectedIndex = auxId.IndexOf(asistente.Taller.ToString());
                txtNombre.Text = asistente.Nombre;
                txtTelefono.Text = asistente.Telefono;
                txtCorreo.Text = asistente.Correo;
                txtCurp.Text = asistente.Curp;
                txtRFC.Text = asistente.Rfc;
                txtAnticipo.Value = asistente.Pago;
                lblAnticipo.Text="Pago: $";
                txtAnticipo.ReadOnly = true;
                txtCosto.Value = asistente.Costo;
                txtObservaciones.Text = asistente.Observaciones;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
                asistenT.Taller = Convert.ToInt32(auxId.ElementAt(cmbTalleres.SelectedIndex));
                asistenT.Nombre = txtNombre.Text;
                asistenT.Telefono = txtTelefono.Text;
                asistenT.Correo = txtCorreo.Text;
                asistenT.Curp = txtCurp.Text;
                asistenT.Rfc = txtRFC.Text;
                asistenT.Pago = txtAnticipo.Value;
                asistenT.Costo = txtCosto.Value;
                asistenT.Observaciones = txtObservaciones.Text;
                try
                {
                if (control.RegistrarAsistenteTaller(asistenT))
                {
                    MessageBox.Show("Asistencia registrada exitosamente!");
                    this.Hide();
                    if (!txtAnticipo.ReadOnly)
                    {
                        FormPago fp = new FormPago(asistenT.Pago, "Pago de Taller", "Escuela");
                        fp.ShowDialog();
                        pago = fp.getPagos();
                        if (control.RegistrarPagoAsistenciaTaller(pago, control.ObtenerAsistentesTalleres(asistenT.Taller.ToString()).Last().ID.ToString()))
                        {
                            MessageBox.Show("Pago registrado exitosamente");
                            Thread t = new Thread(new ThreadStart(ThreadMethodDocumentos));
                            t.Start();
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
                    txtCosto.Value = taller.CostoPublico;
                    break;
                case 1:
                    txtCosto.Enabled = false;
                    txtCosto.Value = taller.CostoClientes;
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
                taller = control.ConsultarTaller(auxId.ElementAt(cmbTalleres.SelectedIndex));
                cmbTipoCosto_SelectedIndexChanged(null,null);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al obtener datos del taller");
            }
        }
        private void ThreadMethodDocumentos()
        {
            DocumentosWord word = new DocumentosWord(pago);
        }
    }
}
