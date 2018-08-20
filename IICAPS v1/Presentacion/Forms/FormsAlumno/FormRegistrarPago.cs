﻿using IICAPS_v1.Control;
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
    public partial class FormRegistrarPago : Form
    {
        ControlIicaps control;
        Pago pagos;
        bool modificacion = false;
        public FormRegistrarPago(Pago pago)
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
            foreach (string c in control.obtenerConceptosDePago())
            {
                auxConcepto.Add(c);
            }
            cmbConcepto.Items.AddRange(auxConcepto.ToArray());
            foreach (Programa p in control.obtenerProgramas())
            {
                auxPrograma.Add(p.Nombre);
                auxIDPrograma.Add(p.Codigo.ToString());
            }
            cmbIDPrograma.Items.AddRange(auxIDPrograma.ToArray());
            cmbPrograma.Items.AddRange(auxPrograma.ToArray());
            foreach (Empleados e in control.obtenerEmpleados())
            {
                auxRecibio.Add(e.nombre);
                auxIDRecibio.Add(e.correo);
            }
            cmbIDRecibio.Items.AddRange(auxIDRecibio.ToArray());
            cmbRecibio.Items.AddRange(auxRecibio.ToArray());
            if (pago != null)
            {
                modificacion = true;
                pagos = pago;
                cmbPrograma.SelectedItem = control.obtenerProgramaAlumno(pago.alumnoID);
                cmbAlumno.SelectedItem = pago.alumnoID;
                cmbRecibio.SelectedItem = pago.recibio;
                cmbIDPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
                cmbIDAlumno.SelectedIndex = cmbAlumno.SelectedIndex;
                cmbIDRecibio.SelectedIndex = cmbRecibio.SelectedIndex;
                cmbConcepto.SelectedItem = pago.concepto;
                numericUpDown1.Value = Convert.ToDecimal(pago.cantidad);
                txtObservaciones.Text = pago.observaciones;
            }
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
            Pago p = new Pago();
            p.alumnoID = cmbIDAlumno.SelectedItem.ToString();
            p.cantidad = Convert.ToDouble(numericUpDown1.Value);
            p.concepto = cmbConcepto.SelectedItem.ToString();
            p.observaciones = txtObservaciones.Text;
            p.recibio = cmbIDRecibio.SelectedItem.ToString();
            p.fechaPago = DateTime.Now;
            //if (modificacion)
            //{
            //    p.alumnoID = cmbIDAlumno.SelectedItem.ToString();
            //    if (control.actualizarEntregaDocumentos(doc))
            //    {
            //        DocumentosWord word = new DocumentosWord(doc);
            //        return true;
            //    }
            //    else
            //        throw new Exception("Error al actualizar los datos de la entrega de documentos");
            //}
            //else
            //{
                if (control.agregarPago(p))
                {
                    DocumentosWord word = new DocumentosWord(p);
                    return true;
                }
                else
                    throw new Exception("Error al agregar los datos de la entrega de documentos");
            //}
        }

        private void cmbPrograma_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIDPrograma.SelectedIndex = cmbPrograma.SelectedIndex;
            List<String> auxAlumno = new List<string>();
            List<String> auxIDAlumno = new List<string>();
            foreach (Alumno a in control.obtenerAlumnosByPrograma(cmbIDPrograma.SelectedItem.ToString()))
            {
                auxAlumno.Add(a.nombre);
                auxIDAlumno.Add(a.rfc.ToString());
            }
            cmbIDAlumno.Items.AddRange(auxIDAlumno.ToArray());
            cmbAlumno.Items.AddRange(auxAlumno.ToArray());
            cmbAlumno.SelectedIndex = 0;
        }
    }
}
