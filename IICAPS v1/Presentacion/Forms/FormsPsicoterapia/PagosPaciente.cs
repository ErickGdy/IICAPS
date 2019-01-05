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
using MySql.Data.MySqlClient;

namespace IICAPS_v1.Presentacion
{
    public partial class PagosPaciente : Form
    {

        private static PagosPaciente instance;
        ControlIicaps control;
        string ID_paciente;
        List<Sesion> sesionesPendientesDePago;
        public PagosPaciente(string paciente)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            try
            {
                this.ID_paciente = paciente;
                lblNombrePaciente.Text = control.consultarPaciente(ID_paciente).NombreCompleto();
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
                sesionesPendientesDePago = control.obtenerSesionesPendietesDePagoPaciente(ID_paciente);
                //sumar pendiente
                decimal totalPendiente = 0;
                if (sesionesPendientesDePago != null)
                    foreach (Sesion aux in sesionesPendientesDePago)
                    {
                        totalPendiente += aux.Pendiente;
                    }
                lblPagoPendiente.Text = totalPendiente.ToString();
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener datos del paciente");
            }
        }

        public static PagosPaciente getInstance(string paciente)
        {
            if (instance != null)
            {
                return instance = new PagosPaciente(paciente);
            }else
            {
                return instance;
            }
        }
        private void actualizarTablaSesiones(MySqlDataAdapter data)
        {
            try
            {
                DataTable dtDatos = new DataTable();
                //Con la informacion del adaptador se llena el datatable
                data.Fill(dtDatos);
                //Se asigna el datatable como origen de datos del datagridview
                dataGridViewSesiones.DataSource = dtDatos;
                //Actualiza el valor del ancho de la columnas
                int x = (dataGridViewSesiones.Width - 20) / (dataGridViewSesiones.Columns.Count-1);
                foreach (DataGridViewColumn aux in dataGridViewSesiones.Columns)
                {
                    aux.Width = x;
                }
                dataGridViewSesiones.Columns[0].Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void actualizarTablaPagos(MySqlDataAdapter data)
        {
            try
            {
                DataTable dtDatos = new DataTable();
                //Con la informacion del adaptador se llena el datatable
                data.Fill(dtDatos);
                //Se asigna el datatable como origen de datos del datagridview
                dataGridViewPagos.DataSource = dtDatos;
                //Actualiza el valor del ancho de la columnas
                int x = (dataGridViewPagos.Width - 20) / (dataGridViewPagos.Columns.Count-1);
                foreach (DataGridViewColumn aux in dataGridViewPagos.Columns)
                {
                    aux.Width = x;
                }
                dataGridViewPagos.Columns[0].Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FormPago fa = new FormPago(Convert.ToDecimal(lblPagoPendiente.Text), "Pago de Sesión", "Psicoterapia");
            fa.ShowDialog();
            Pago pago = fa.getPagos();
            try
            {
                if (pago != null)
                {
                    decimal cantidad = Convert.ToDecimal(pago.cantidad);
                    int posicion = 0;
                    List<Sesion> sesionesActualizadas = new List<Sesion>();
                    while (cantidad > 0)
                    {
                        Sesion aux = sesionesPendientesDePago.ElementAt(posicion);
                        if (aux.Pendiente > 0)
                        {
                            if (aux.Pendiente > cantidad)
                            {
                                aux.Pendiente -= cantidad;
                                aux.Pago += cantidad;
                                cantidad = 0;
                            }
                            else
                            {
                                cantidad -= aux.Pendiente;
                                aux.Pendiente = 0;
                                aux.Pago = aux.Costo;
                            }
                            sesionesActualizadas.Add(aux);
                        }
                        posicion++;
                    }
                    if (control.registrarPagoDeSesion(fa.getPagos(), sesionesActualizadas)) {
                        form_Closed(null, null);
                        actualizarDatos();
                        MessageBox.Show("Pago Registrado Exitosamente");
                    } else
                        MessageBox.Show("Error al registrar pago");
                }
                else
                    MessageBox.Show("Error al registrar pago");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void form_Closed(object sender, FormClosedEventArgs e)
        {
            actualizarTablaPagos(control.obtenerPagosPacienteTable(ID_paciente, txtBuscarPagos.Text));
            actualizarTablaSesiones(control.obtenerPagosSesionesPacienteTable(ID_paciente, txtBuscarPagos.Text));
        }
        
        //Metodos de control Pagos
        private void limpiarBusqueda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            limpiarBusquedaPagos.Visible = false;
            txtBuscarPagos.Text = "";
            try
            {
                actualizarTablaPagos(control.obtenerPagosPacienteTable(ID_paciente));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarTablaPagos(control.obtenerPagosPacienteTable(ID_paciente, txtBuscarPagos.Text));
        }
        private void Main_SizeChanged(object sender, EventArgs e)
        {
            int ancho = this.Width;
            
            //actualiza la posicion de los controles con respecto al tamaño de la ventana
            btnAgregar.Location = new Point (ancho - 170, btnAgregar.Location.Y);
            txtBuscarPagos.Location = new Point(ancho - 225, txtBuscarPagos.Location.Y);
            pictureBoxBuscar.Location = new Point(ancho - 255, pictureBoxBuscar.Location.Y);
            limpiarBusquedaPagos.Location = new Point(ancho - 50, limpiarBusquedaPagos.Location.Y);
            txtBuscarSesiones.Location = new Point(ancho - 225, txtBuscarSesiones.Location.Y);
            pictureBox1.Location = new Point(ancho - 255, pictureBox1.Location.Y);
            limpiarBusquedaSesiones.Location = new Point(ancho - 50, limpiarBusquedaSesiones.Location.Y);
            //Actualiza el tamaño de los paneles con respecto al tamaño de la ventana
            panelPagos.Size = new Size(ancho,(this.Height-panelDatos.Height-30)/2);
            panelSesiones.Size = new Size(ancho,(this.Height-panelDatos.Height-30)/2);
            panelDatos.Size = new Size(ancho,panelDatos.Height);
            //Actualiza el tamaño de las tablas con respecto al tamaño de la ventana
            dataGridViewPagos.Width = panelPagos.Width - 45;
            dataGridViewPagos.Height = panelPagos.Height - 55;
            dataGridViewSesiones.Width = panelSesiones.Width - 45;
            dataGridViewSesiones.Height = panelSesiones.Height - 55;
            //Actualiza la posicion del panel en relación a la ventana
            panelSesiones.Location = new Point(panelSesiones.Location.X, this.Height - panelSesiones.Height -35);

            //Actualiza el valor del ancho de la columnas
            if (dataGridViewPagos.Columns.Count != 0)
            {
                int x = (dataGridViewPagos.Width - 20) / (dataGridViewPagos.Columns.Count-1);
                foreach (DataGridViewColumn aux in dataGridViewPagos.Columns)
                {
                    aux.Width = x;
                }
            }
            if (dataGridViewSesiones.Columns.Count != 0)
            {
                int x = (dataGridViewSesiones.Width - 20) / (dataGridViewSesiones.Columns.Count - 1);
                foreach (DataGridViewColumn aux in dataGridViewSesiones.Columns)
                {
                    aux.Width = x;
                }
            }
            
        }
        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            string texto = txtBuscarPagos.Text;
            if (texto != "")
            {
                limpiarBusquedaPagos.Visible = true;
                if (texto.Length > 2)
                {
                    try
                    {
                        actualizarTablaPagos(control.obtenerPagosPacienteTable(ID_paciente, texto));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                limpiarBusquedaPagos.Visible = false;
                try
                {
                    actualizarTablaPagos(control.obtenerPagosPacienteTable(ID_paciente)) ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
        }
        
        //Metodos de control sesiones
        private void limpiarBusquedaSesiones_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            limpiarBusquedaSesiones.Visible = false;
            txtBuscarSesiones.Text = "";
            try
            {
                actualizarTablaSesiones(control.obtenerPagosSesionesPacienteTable(ID_paciente));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnActualizarSesiones_Click(object sender, EventArgs e)
        {
            actualizarTablaSesiones(control.obtenerPagosSesionesPacienteTable(ID_paciente, txtBuscarPagos.Text));
        }
        private void txtBuscarSesiones_KeyUp(object sender, KeyEventArgs e)
        {
            string texto = txtBuscarSesiones.Text;
            if (texto != "")
            {
                limpiarBusquedaSesiones.Visible = true;
                if (texto.Length > 2)
                {
                    try
                    {
                        actualizarTablaSesiones(control.obtenerPagosSesionesPacienteTable(ID_paciente, texto));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                limpiarBusquedaSesiones.Visible = false;
                try
                {
                    actualizarTablaSesiones(control.obtenerPagosSesionesPacienteTable(ID_paciente));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }


    }
}
