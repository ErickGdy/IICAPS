using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IICAPS_v1.Control;
using IICAPS_v1.DataObject;
using IICAPS_v1.Presentacion;
using System.Data.SqlClient;

namespace IICAPS_v1.Presentacion
{
    public partial class NominaPsicoterapeuta : Form
    {

        private static NominaPsicoterapeuta instance;
        ControlIicaps control;
        string ID_psicoterapeuta;
        List<string> sesionesPendientesDePago;
        DateTime fechaInicio=new DateTime();
        DateTime fechaFin= DateTime.Now;
        public NominaPsicoterapeuta(string psicoterapeuta)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            try
            {
                this.ID_psicoterapeuta = psicoterapeuta;
                lblNombre.Text = control.ConsultarPsicoterapeuta(ID_psicoterapeuta).Nombre;
                actualizarDatos();
                form_Closed(null, null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void actualizarDatos()
        {
            try
            {
                fechaInicio = control.ConsultarUltimaFechaNomina(ID_psicoterapeuta);
                sesionesPendientesDePago = control.ObtenerConsultasPsicoterapeutaPendientes(ID_psicoterapeuta,fechaInicio,DateTime.Now);
                //sumar pendiente
                decimal totalPendiente = 0;
                if (sesionesPendientesDePago != null)
                    for (int i = 0; i < sesionesPendientesDePago.Count; i += 2) { 
                        if(sesionesPendientesDePago.ElementAt(i)=="s")
                            totalPendiente += Convert.ToDecimal(sesionesPendientesDePago.ElementAt(i+1))*(control.parametros_Generales.Porcentaje_Pago_Sesion/100);
                        if (sesionesPendientesDePago.ElementAt(i) == "e")
                            totalPendiente += Convert.ToDecimal(sesionesPendientesDePago.ElementAt(i + 1)) * (control.parametros_Generales.Porcentaje_Pago_Evaluacion / 100);
                    }
                totalPendiente = totalPendiente * (control.parametros_Generales.Porcentaje_Pago_Sesion / 100);
                lblPago.Text = totalPendiente.ToString("F2");
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener datos del psicoterapeuta");
            }
        }

        public static NominaPsicoterapeuta getInstance(string paciente)
        {
            if (instance != null)
            {
                return instance = new NominaPsicoterapeuta(paciente);
            }else
            {
                return instance;
            }
        }
        private void actualizarTablaPagos(SqlDataAdapter data)
        {
            try
            {
                DataTable dtDatos = new DataTable();
                //Con la informacion del adaptador se llena el datatable
                data.Fill(dtDatos);
                //Se asigna el datatable como origen de datos del datagridview
                dataGridView.DataSource = dtDatos;
                //Actualiza el valor del ancho de la columnas
                int x = (dataGridView.Width - 20) / (dataGridView.Columns.Count-1);
                foreach (DataGridViewColumn aux in dataGridView.Columns)
                {
                    aux.Width = x;
                }
                dataGridView.Columns[0].Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Nomina nomina = new Nomina();
            nomina.Psicoterapeutas = ID_psicoterapeuta;
            nomina.Total = Convert.ToDecimal(lblPago.Text);
            nomina.FechaFin = fechaFin;
            nomina.FechaInicio = fechaInicio;
            FormNomina fn = new FormNomina(nomina);
            fn.ShowDialog();
            form_Closed(null,null);
        }
        private void form_Closed(object sender, FormClosedEventArgs e)
        {
            actualizarTablaPagos(control.ObtenerNominaPacienteTable(ID_psicoterapeuta, txtBuscar.Text));
            actualizarDatos();

        }

        //Metodos de control 
        private void limpiarBusqueda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            limpiarBusqueda.Visible = false;
            txtBuscar.Text = "";
            try
            {
                actualizarTablaPagos(control.ObtenerNominaPacienteTable(ID_psicoterapeuta));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarTablaPagos(control.ObtenerNominaPacienteTable(ID_psicoterapeuta, txtBuscar.Text));
        }
        private void Main_SizeChanged(object sender, EventArgs e)
        {
            int ancho = this.Width;
            
            //actualiza la posicion de los controles con respecto al tamaño de la ventana
            btnAgregar.Location = new Point (ancho - 170, btnAgregar.Location.Y);
            txtBuscar.Location = new Point(ancho - 225, txtBuscar.Location.Y);
            pictureBoxBuscar.Location = new Point(ancho - 255, pictureBoxBuscar.Location.Y);
            limpiarBusqueda.Location = new Point(ancho - 50, limpiarBusqueda.Location.Y);
            //Actualiza el tamaño de los paneles con respecto al tamaño de la ventana
            panelTabla.Size = new Size(ancho,(this.Height-panelDatos.Height-30)/2);
            panelDatos.Size = new Size(ancho,panelDatos.Height);
            //Actualiza el tamaño de las tablas con respecto al tamaño de la ventana
            dataGridView.Width = panelTabla.Width - 45;
            dataGridView.Height = panelTabla.Height - 55;

            //Actualiza el valor del ancho de la columnas
            if (dataGridView.Columns.Count != 0)
            {
                int x = (dataGridView.Width - 20) / (dataGridView.Columns.Count-1);
                foreach (DataGridViewColumn aux in dataGridView.Columns)
                {
                    aux.Width = x;
                }
            }
        }
        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            string texto = txtBuscar.Text;
            if (texto != "")
            {
                limpiarBusqueda.Visible = true;
                if (texto.Length > 2)
                {
                    try
                    {
                        actualizarTablaPagos(control.ObtenerNominaPacienteTable(ID_psicoterapeuta, texto));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                limpiarBusqueda.Visible = false;
                try
                {
                    actualizarTablaPagos(control.ObtenerNominaPacienteTable(ID_psicoterapeuta)) ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
        }
       

    }
}
