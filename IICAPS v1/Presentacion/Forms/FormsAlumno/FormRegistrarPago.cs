﻿using IICAPS_v1.Control;
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
    public partial class FormRegistrarPago : Form
    {
        ControlIicaps control;
        PagoAlumno pagos;
        PagoAlumno p;
        public FormRegistrarPago(PagoAlumno pago, bool consultar)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            List<String> auxPrograma = new List<string>();
            List<String> auxIDPrograma = new List<string>();
            List<String> auxAlumno = new List<string>();
            List<String> auxIDAlumno = new List<string>();
            List<String> auxRecibio = new List<string>();
            List<String> auxIDRecibio = new List<string>();
            List<String> auxConcepto = new List<string>();
            try
            {
                auxConcepto.Add("Pago de Adeudo General");
                foreach (string c in control.ObtenerConceptosDePagoAlumno("Escuela"))
                {
                    auxConcepto.Add(c);
                }
                cmbConcepto.Items.AddRange(auxConcepto.ToArray());
            }
            catch (Exception ex) { }
            try
            {
                foreach (Programa p in control.ObtenerProgramas())
                {
                    auxPrograma.Add(p.Nombre);
                    auxIDPrograma.Add(p.Codigo.ToString());
                }
                cmbIDPrograma.Items.AddRange(auxIDPrograma.ToArray());
                cmbPrograma.Items.AddRange(auxPrograma.ToArray());
            }
            catch (Exception ex) { }
            try
            {
                foreach (Empleado e in control.ObtenerEmpleados())
                {
                    auxRecibio.Add(e.Nombre);
                    auxIDRecibio.Add(e.Matricula);
                }
                cmbIDRecibio.Items.AddRange(auxIDRecibio.ToArray());
                cmbRecibio.Items.AddRange(auxRecibio.ToArray());
            }
            catch (Exception ex) { }
            try
            {
                if (pago != null)
                {
                    pagos = pago;
                    cmbIDPrograma.SelectedItem = control.ObtenerProgramaAlumno(pago.AlumnoID);
                    cmbIDRecibio.SelectedItem = pago.Recibio;
                    cmbPrograma.SelectedIndex = cmbIDPrograma.SelectedIndex;
                    cmbIDAlumno.SelectedItem = pago.AlumnoID;
                    cmbAlumno.SelectedIndex = cmbIDAlumno.SelectedIndex;
                    cmbRecibio.SelectedIndex = cmbIDRecibio.SelectedIndex;
                    cmbConcepto.SelectedItem = pago.Concepto;
                    cmbAlumno.Enabled = false; 
                    cmbPrograma.Enabled = false;
                    numericUpDown1.Maximum = Convert.ToDecimal(pago.Cantidad);
                    numericUpDown1.Value = Convert.ToDecimal(pago.Cantidad);
                    txtObservaciones.Text = pago.Observaciones;
                    if (consultar)
                    {
                        cmbPrograma.Enabled = false;
                        cmbAlumno.Enabled = false;
                        cmbRecibio.Enabled = false;
                        numericUpDown1.Enabled = false;
                        cmbConcepto.Enabled = false;
                        txtObservaciones.Enabled = false;
                        btnAceptar.Enabled = false;
                    }
                }
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
                if (!agregarPago())
                    MessageBox.Show("Error al guardar los datos de la entrega de documentos");
                else
                {
                    MessageBox.Show("Datos del pago guardados exitosamente");
                    Close();
                    Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool agregarPago()
        {
            cmbIDPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
            cmbIDAlumno.SelectedIndex = cmbAlumno.SelectedIndex;
            cmbIDRecibio.SelectedIndex = cmbRecibio.SelectedIndex;
            p = new PagoAlumno();
            p.AlumnoID = cmbIDAlumno.SelectedItem.ToString();
            p.Cantidad = Convert.ToDouble(numericUpDown1.Value);
            p.Concepto = cmbConcepto.SelectedItem.ToString();
            p.Observaciones = txtObservaciones.Text;
            p.Recibio = cmbIDRecibio.SelectedItem.ToString();
            p.FechaPago = DateTime.Now;
            List<Cobro> pagoPendientes = control.ConsultarCobrosDeAlumno(p.AlumnoID);
            if (pagoPendientes != null) {
                decimal cantidad = Convert.ToDecimal(p.Cantidad);
                int posicion = 0;
                List<Cobro> cobrosActualizados = new List<Cobro>();
                while (cantidad > 0)
                {
                    Cobro aux = pagoPendientes.ElementAt(posicion);
                    if (aux.Restante > 0)
                    {
                        if (aux.Restante > cantidad)
                        {
                            aux.Restante -= cantidad;
                            aux.Pago += cantidad;
                            cantidad = 0;
                        }
                        else
                        {
                            cantidad -= aux.Restante;
                            aux.Restante = 0;
                            aux.Pago = aux.Cantidad;
                        }
                        cobrosActualizados.Add(aux);
                    }
                    posicion++;
                }
                if (control.AgregarPagoAlumno(p, cobrosActualizados))
                {
                    Thread t = new Thread(new ThreadStart(ThreadMethodDocumentos));
                    t.Start();
                    return true;
                }
                else
                    throw new Exception("Error al agregar pago del alumno");
            }
            return false;
            
        }
        private void ThreadMethodDocumentos()
        {
            DocumentosWord word = new DocumentosWord(p);
        }

        private void cmbPrograma_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIDPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
            List<String> auxAlumno = new List<string>();
            List<String> auxIDAlumno = new List<string>();
            foreach (Alumno a in control.ObtenerAlumnosByPrograma(cmbIDPrograma.SelectedItem.ToString()))
            {
                auxAlumno.Add(a.Nombre);
                auxIDAlumno.Add(a.Rfc.ToString());
            }
            cmbIDAlumno.Items.AddRange(auxIDAlumno.ToArray());
            cmbAlumno.Items.AddRange(auxAlumno.ToArray());
            cmbAlumno.SelectedIndex = 0;
        }
    }
}
