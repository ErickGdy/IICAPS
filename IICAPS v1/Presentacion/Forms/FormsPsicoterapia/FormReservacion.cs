using IICAPS_v1.Control;
using IICAPS_v1.DataObject;
using IICAPS_v1.Presentacion.Forms.FromsControl;
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
    public partial class FormReservacion : Form
    {
        ControlIicaps control = ControlIicaps.getInstance();
        Reservacion reservacion = new Reservacion();
        FormConsultarAgenda fa;

        List<String> empleados;
        int empleadosCount = 0;
        int psicoterapeutasCount = 0;

        List<Reservacion> reservaciones;
        bool consultar;

        public FormReservacion(Reservacion reservacion, bool agregar, bool cons, string concepto, string modulo)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            try
            {
                cmbUbicacion.Items.AddRange(control.consultarUbicaciones().ToArray());
                cmbUbicacion.SelectedIndex = 0;
                reservaciones = control.obtenerReservaciones(DateTime.Now);
                this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
                if (agregar)
                {
                    this.btnAceptar.Click -= new System.EventHandler(this.btnAceptar_Click);
                    this.btnAceptar.Click += new System.EventHandler(this.btnAceptarAgregar_Click);
                }
                consultar = cons;
                lblFecha.Text = DateTime.Now.ToShortDateString();
                List<String> aux = new List<string>();
                foreach (string c in control.obtenerConceptos("Reservacion", modulo))
                {
                    aux.Add(c);
                }
                cmbConcepto.Items.AddRange(aux.ToArray());
                cmbConcepto.Items.Add("Otro");
                Llenar_ComboBox_Personal();
                if (reservacion != null)
                {
                    this.reservacion = reservacion;
                    cmbReservante.SelectedIndex = empleados.IndexOf(reservacion.reservante);
                    cmbConcepto.SelectedItem = reservacion.concepto;
                    txtHoras.Value = Convert.ToDecimal(reservacion.duracion.Hours);
                    txtMinutos.Value = Convert.ToDecimal(reservacion.duracion.Minutes);
                    datePicker_Fecha.Text = reservacion.fecha.ToShortDateString();
                    datePicker_Hora.Value = new DateTime(2000, 1, 1, reservacion.hora_Inicio.Hours, reservacion.hora_Inicio.Minutes, 00);
                    txtObservaciones.Text = reservacion.observaciones;
                    cmbUbicacion.SelectedItem = reservacion.ubicacion;
                    btnDisponible.Visible = true;
                    lblCodigo_Reservacion.Text = reservacion.formatoFolio();
                    lblCodigo_Reservacion.Visible = true;
                    lblReservacionT.Visible = true;
                    if (consultar)
                    {
                        cmbReservante.Enabled = false;
                        cmbUbicacion.Enabled = false;
                        txtHoras.Enabled = false;
                        datePicker_Fecha.Enabled = false;
                        datePicker_Hora.Enabled = false;
                        txtMinutos.Enabled = false;
                        cmbConcepto.Enabled = false;
                        txtObservaciones.Enabled = false;
                        btnAceptar.Enabled = false;
                        cmbReservante.Enabled = false;
                    }
                }
                else
                {
                    datePicker_Fecha.MinDate = DateTime.Now;
                    lblCodigo_Reservacion.Text = control.obtenerUltimoIDReservaciones().ToString("00000000");
                    this.reservacion = new Reservacion();
                    if (concepto != null && concepto != "")
                    {
                        cmbConcepto.SelectedItem = concepto;
                        cmbConcepto.Enabled = false;
                    }
                    validarDisponibilidad();
                }
            }
            catch (Exception ex) { }
        }
        public FormReservacion(Reservacion reservacion, DateTime fecha, TimeSpan hora, string ubicacion, bool cons, string concepto, string modulo)
        {
            InitializeComponent();
            try
            {
                this.btnAceptar.Click += new System.EventHandler(this.btnAceptarAgregar_Click);
                consultar = cons;
                control = ControlIicaps.getInstance();
                lblFecha.Text = DateTime.Now.ToShortDateString();
                List<String> aux = new List<string>();
                foreach (string c in control.obtenerConceptos("Reservacion", modulo))
                {
                    aux.Add(c);
                }
                cmbConcepto.Items.AddRange(aux.ToArray());
                cmbConcepto.Items.Add("Otro");
                cmbUbicacion.Items.AddRange(control.consultarUbicaciones().ToArray());
                Llenar_ComboBox_Personal();
                //Validar datos y setearlos a los campos de texto
                if (fecha != null)
                {
                    fecha=new DateTime(fecha.Year, fecha.Month, fecha.Day, 23, 59, 59);
                    reservaciones = control.obtenerReservaciones(fecha);
                    datePicker_Fecha.Value = fecha;
                }
                else
                    reservaciones = control.obtenerReservaciones(DateTime.Now);
                if (hora != null)
                    datePicker_Hora.Value = new DateTime(2000, 1, 1, hora.Hours, hora.Minutes, 00);
                if (ubicacion != null)
                    cmbUbicacion.SelectedItem = ubicacion;

                if (reservacion != null)
                {
                    this.reservacion = reservacion;
                    cmbReservante.SelectedIndex = empleados.IndexOf(reservacion.reservante);
                    cmbConcepto.SelectedItem = reservacion.concepto;
                    txtHoras.Value = Convert.ToDecimal(reservacion.duracion.Hours);
                    txtMinutos.Value = Convert.ToDecimal(reservacion.duracion.Minutes);
                    datePicker_Fecha.Text = reservacion.fecha.ToShortDateString();
                    datePicker_Hora.Value = new DateTime(2000, 1, 1, reservacion.hora_Inicio.Hours, reservacion.hora_Inicio.Minutes, 00);
                    txtObservaciones.Text = reservacion.observaciones;
                    cmbUbicacion.SelectedItem = reservacion.ubicacion;
                    btnDisponible.Visible = true;
                    lblCodigo_Reservacion.Text = reservacion.formatoFolio();
                    lblCodigo_Reservacion.Visible = true;
                    lblReservacionT.Visible = true;
                    if (consultar)
                    {
                        cmbReservante.Enabled = false;
                        cmbUbicacion.Enabled = false;
                        txtHoras.Enabled = false;
                        datePicker_Fecha.Enabled = false;
                        datePicker_Hora.Enabled = false;
                        txtMinutos.Enabled = false;
                        cmbConcepto.Enabled = false;
                        txtObservaciones.Enabled = false;
                        btnAceptar.Enabled = false;
                        cmbReservante.Enabled = false;
                    }
                    btnAceptar.Enabled = true;
                }
                else
                {
                    datePicker_Fecha.MinDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);
                    lblCodigo_Reservacion.Text = control.obtenerUltimoIDReservaciones().ToString("00000000");
                    this.reservacion = new Reservacion();
                }
                if (concepto != null && concepto != "")
                {
                    cmbConcepto.SelectedItem = concepto;
                    cmbConcepto.Enabled = false;
                }
                validarDisponibilidad();
            }
            catch (Exception ex) { }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if(consultar)
                    Dispose();
                if (validarCampos()) {
                    reservacion.duracion = new TimeSpan(Convert.ToInt32(txtHoras.Value), Convert.ToInt32(txtMinutos.Value), 00);
                    reservacion.hora_Inicio = new TimeSpan(Convert.ToInt32(datePicker_Hora.Value.Hour), Convert.ToInt32(datePicker_Hora.Value.Minute), 00);
                    reservacion.hora_Fin = reservacion.hora_Inicio.Add(reservacion.duracion);
                    reservacion.fecha = datePicker_Fecha.Value;
                    reservacion.concepto = cmbConcepto.SelectedItem.ToString();
                    reservacion.ubicacion = cmbUbicacion.SelectedItem.ToString();
                    reservacion.observaciones = txtObservaciones.Text;
                    reservacion.reservante = empleados.ElementAt(cmbReservante.SelectedIndex);
                    reservacion.codigo_Reservacion = lblCodigo_Reservacion.Text;
                    Close();
                }else
                {
                    MessageBox.Show("No dejar campos vacios");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAceptarAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (consultar)
                    Dispose();
                if (validarCampos())
                {
                    reservacion.duracion = new TimeSpan(Convert.ToInt32(txtHoras.Value), Convert.ToInt32(txtMinutos.Value), 00);
                    reservacion.hora_Inicio = new TimeSpan(Convert.ToInt32(datePicker_Hora.Value.Hour), Convert.ToInt32(datePicker_Hora.Value.Minute), 00);
                    reservacion.hora_Fin = reservacion.hora_Inicio.Add(reservacion.duracion);
                    reservacion.fecha = datePicker_Fecha.Value;
                    reservacion.concepto = cmbConcepto.SelectedItem.ToString();
                    reservacion.ubicacion = cmbUbicacion.SelectedItem.ToString();
                    reservacion.observaciones = txtObservaciones.Text;
                    reservacion.reservante = empleados.ElementAt(cmbReservante.SelectedIndex);
                    reservacion.id_parent = "0";
                    reservacion.codigo_Reservacion = lblCodigo_Reservacion.Text;
                    if (reservacion.id == 0)
                    {
                        if (control.agregarReservacion(reservacion))
                        {
                            MessageBox.Show("Reservación registrada exitosamente");
                            Close();
                            Dispose();
                        }
                        else
                            MessageBox.Show("Error al agregar reservación");
                    }
                    else
                    {
                        if (control.actualizarReservacion(reservacion))
                        {
                            MessageBox.Show("Reservación actualizada exitosamente");
                            Close();
                            Dispose();
                        }
                        else
                            MessageBox.Show("Error al actualizar reservación");
                    }
                }
                else
                {
                    MessageBox.Show("No dejar campos vacios");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public Reservacion getReservacion()
        {
            return reservacion;
        }
        private bool validarCampos()
        {
            if (cmbConcepto.SelectedIndex >= 0 && cmbReservante.SelectedIndex >= 0 && cmbUbicacion.SelectedIndex >=0)
                return true;
            else
                return false;
        }
        private void btnAgenda_Click(object sender, EventArgs e)
        {
            fa = new FormConsultarAgenda(datePicker_Fecha.Value);
            fa.FormClosing += new FormClosingEventHandler(FormAgenda_FormClosing);
            fa.Show();
        }

        private void FormAgenda_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DateTime fecha_Aux = fa.getFecha();
                if (fecha_Aux != null)
                {
                    datePicker_Fecha.Value = fecha_Aux;
                }
                TimeSpan hora_aux = fa.getHora();
                if (fa.getHora() != null)
                {
                    datePicker_Hora.Value = new DateTime(2000,1,1, hora_aux.Hours, hora_aux.Minutes,0);
                }
                string ubicacion_aux = fa.getUbicacion(); 
                if (ubicacion_aux != null)
                {
                    cmbUbicacion.SelectedItem = ubicacion_aux;
                }
                fa.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        private void FormReservacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                fa.Dispose();
            }
            catch (Exception ex)
            {

            }
        }


        private bool validarDisponibilidad()
        {
            try
            {
                TimeSpan hora_Inicio = new TimeSpan(datePicker_Hora.Value.Hour, datePicker_Hora.Value.Minute, 0);
                TimeSpan hora_Fin = hora_Inicio.Add(new TimeSpan(Convert.ToInt32(txtHoras.Value), Convert.ToInt32(txtMinutos.Value), 0));
                if (reservaciones != null)
                    foreach (Reservacion aux in reservaciones)
                    {
                        if (aux.ubicacion == cmbUbicacion.SelectedItem.ToString())
                        {
                            hora_Inicio.CompareTo(aux.hora_Inicio);//>= 0
                            hora_Inicio.CompareTo(aux.hora_Fin);//<0
                            hora_Fin.CompareTo(aux.hora_Inicio);//>0
                            hora_Fin.CompareTo(aux.hora_Fin);//<= 0
                            if ((hora_Inicio.CompareTo(aux.hora_Inicio) >= 0 && hora_Inicio.CompareTo(aux.hora_Fin) < 0) || (hora_Fin.CompareTo(aux.hora_Inicio) > 0 && hora_Fin.CompareTo(aux.hora_Fin) <= 0))
                            {
                                btnDisponible.Visible = false;
                                return false;
                            }
                            if ((aux.hora_Inicio.CompareTo(hora_Inicio) >= 0 && aux.hora_Inicio.CompareTo(hora_Fin) < 0) || (aux.hora_Fin.CompareTo(hora_Inicio) > 0 && aux.hora_Fin.CompareTo(hora_Fin) <= 0))
                            {
                                btnDisponible.Visible = false;
                                return false;
                            }
                        }
                    }
                btnDisponible.Visible = true;
                return true;
            }
            catch (Exception ex)
            {
                btnDisponible.Visible = false;
                return false;
            }
        }

        private void datePicker_Hora_ValueChanged(object sender, EventArgs e)
        {
            if (validarDisponibilidad())
                btnAceptar.Enabled = true;
            else
                btnAceptar.Enabled = false;
        }

        private void datePicker_Fecha_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                reservaciones = control.obtenerReservaciones(datePicker_Fecha.Value);
                if (validarDisponibilidad())
                    btnAceptar.Enabled = true;
                else
                    btnAceptar.Enabled = false;
            }
            catch(Exception ex) { }
        }

        private void cmbUbicacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (validarDisponibilidad())
                btnAceptar.Enabled = true;
            else
                btnAceptar.Enabled = false;
        }
        private void Llenar_ComboBox_Personal()
        {
            empleados = new List<string>();
            List<String> aux = new List<string>();
            try
            {
                List<Empleado> lista = new List<Empleado>();
                lista = control.obtenerEmpleados();
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
                lista = control.obtenerPsicoterapeutas();
                foreach (Psicoterapeuta e in lista)
                {
                    aux.Add("P: "+e.Nombre);
                    empleados.Add(e.Matricula);
                }
            }
            catch (Exception ex) { }
            cmbReservante.Items.AddRange(aux.ToArray());
        }
    }
}
