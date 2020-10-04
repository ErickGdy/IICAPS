﻿using System;
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

namespace IICAPS_v1.Presentacion.Mains
{
    public partial class MainPrestamos : Form
    {

        private static MainPrestamos instance;
        ControlIicaps control;
        public MainPrestamos()
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            try
            {
                actualizarTabla(control.ObtenerPrestamoLibrosTable());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static MainPrestamos getInstance()
        {
            if (instance == null)
                instance = new MainPrestamos();
            return instance;
        }

        private void actualizarTabla(SqlDataAdapter data)
        {
            try
            {
                DataTable dtDatos = new DataTable();
                //Con la informacion del adaptador se llena el datatable
                data.Fill(dtDatos);
                //Se asigna el datatable como origen de datos del datagridview
                dataGridView1.DataSource = dtDatos;
                //Actualiza el valor del ancho de la columnas
                int x = (dataGridView1.Width - 20) / (dataGridView1.Columns.Count-1);
                foreach (DataGridViewColumn aux in dataGridView1.Columns)
                {
                    aux.Width = x;
                }
                dataGridView1.Columns[0].Visible = false; ;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                FormPrestamo fa = new FormPrestamo();
                fa.FormClosed += new FormClosedEventHandler(form_Closed);
                fa.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al intentar abrir el formulario");
            }
        }

        private void form_Closed(object sender, FormClosedEventArgs e)
        {
            actualizarTabla(control.ObtenerPrestamoLibrosTable(txtBuscar.Text));
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                Prestamo Prestamo = control.ConsultarPrestamoLibreria(id);
                Prestamo.DetallesPrestamo = control.ConsultarPrestamoLibreria_DetallesDePrestamo(id);
                FormPrestamo fa = new FormPrestamo(Prestamo, true);
                fa.FormClosed += new FormClosedEventHandler(form_Closed);
                fa.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                Prestamo Prestamo = control.ConsultarPrestamoLibreria(id);
                Prestamo.DetallesPrestamo = control.ConsultarPrestamoLibreria_DetallesDePrestamo(id);
                FormPrestamo fa = new FormPrestamo(Prestamo, false);
                fa.FormClosed += new FormClosedEventHandler(form_Closed);
                fa.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                DialogResult dialogresult = MessageBox.Show("¿Desea cancelar préstamo?", "Cancelar préstamo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if(dialogresult == DialogResult.OK)
                {
                    if (control.CancelarPrestamoLibreria(id, control.ConsultarPrestamoLibreria_DetallesDePrestamo(id)))
                    {
                        MessageBox.Show("Préstamo eliminado");
                        actualizarTabla(control.ObtenerPrestamoLibrosTable());
                    }
                    else
                        MessageBox.Show("Error al cancelar préstamo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void limpiarBusqueda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            limpiarBusqueda.Visible = false;
            txtBuscar.Text = "";
            try
            {
                actualizarTabla(control.ObtenerPrestamoLibrosTable());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarTabla(control.ObtenerPrestamoLibrosTable(txtBuscar.Text));
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
                        actualizarTabla(control.ObtenerPrestamoLibrosTable(texto));
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
                    actualizarTabla(control.ObtenerPrestamoLibrosTable());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            int ancho = this.Width;
            //Actualiza el tamaño de la tabla con respecto al tamaño de la ventana
            dataGridView1.Width = ancho - 195;
            dataGridView1.Height = this.Height - 130;
            //actualiza la posicion de los controles con respecto al tamaño de la ventana
            btnAgregar.Location = new Point(ancho - 169, btnAgregar.Location.Y);
            txtBuscar.Location = new Point(ancho - 216, txtBuscar.Location.Y);
            pictureBoxBuscar.Location = new Point(ancho - 245, pictureBoxBuscar.Location.Y);
            limpiarBusqueda.Location = new Point(ancho - 39, limpiarBusqueda.Location.Y);
            //Actualiza el valor del ancho de la columnas
            if (dataGridView1.Columns.Count != 0)
            {
                int x = (dataGridView1.Width - 20) / (dataGridView1.Columns.Count-1);
                foreach (DataGridViewColumn aux in dataGridView1.Columns)
                {
                    aux.Width = x;
                }
            }
        }

    }
}