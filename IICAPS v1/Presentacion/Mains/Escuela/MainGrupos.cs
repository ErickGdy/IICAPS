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
using MySql.Data.MySqlClient;

namespace IICAPS_v1.Presentacion.Mains.Escuela
{
    public partial class MainGrupos : Form
    {

        private static MainGrupos instance;
        ControlIicaps control;
        public MainGrupos()
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            try
            {
                actualizarTabla(control.obtenerGruposTable());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static MainGrupos getInstance()
        {
            if (instance == null)
                instance = new MainGrupos();
            return instance;
        }

        private void actualizarTabla(MySqlDataAdapter data)
        {
            try
            {
                DataTable dtDatos = new DataTable();
                //Con la informacion del adaptador se llena el datatable
                data.Fill(dtDatos);
                //Se asigna el datatable como origen de datos del datagridview
                dataGridView1.DataSource = dtDatos;
                //Actualiza el valor del ancho de la columnas
                int x = (dataGridView1.Width - 20) / dataGridView1.Columns.Count;
                foreach (DataGridViewColumn aux in dataGridView1.Columns)
                {
                    aux.Width = x;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void btnAgregarAlumno_Click(object sender, EventArgs e)
        {
            FormGrupos fa = new FormGrupos(null);
            fa.FormClosed += new FormClosedEventHandler(form_Closed);
            fa.Show();
        }
        private void form_Closed(object sender, FormClosedEventArgs e)
        {
            actualizarTabla(control.obtenerGruposTable(txtBuscar.Text));
        }
        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                Grupo grupo = control.consultarGrupo(id);
                //MessageBox.Show("Reporte referente al grupo "+ grupo.codigo);
                DetalleGrupo fa = new DetalleGrupo(grupo);
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
                Grupo grupo = control.consultarGrupo(id);
                FormGrupos fa = new FormGrupos(grupo);
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
                DialogResult dialogresult = MessageBox.Show("¿Desea calcelar el grupo?", "Cancelar grupo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if(dialogresult == DialogResult.OK)
                {
                    if (control.desactivarGrupo(id))
                    {
                        MessageBox.Show("Grupo cancelado");
                        actualizarTabla(control.obtenerGruposTable());
                    }
                    else
                        MessageBox.Show("Error al cancelar grupo");
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
                actualizarTabla(control.obtenerGruposTable());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarTabla(control.obtenerGruposTable(txtBuscar.Text));
        }
        private void Main_SizeChanged(object sender, EventArgs e)
        {
            int ancho = this.Width;
            //Actualiza el tamaño de la tabla con respecto al tamaño de la ventana
            dataGridView1.Width= ancho - 195;
            dataGridView1.Height=this.Height - 130;
            //actualiza la posicion de los controles con respecto al tamaño de la ventana
            btnAgregar.Location = new Point (ancho - 169, btnAgregar.Location.Y);
            txtBuscar.Location = new Point (ancho - 216, txtBuscar.Location.Y);
            pictureBoxBuscar.Location = new Point (ancho - 245, pictureBoxBuscar.Location.Y);
            limpiarBusqueda.Location = new Point (ancho - 39, limpiarBusqueda.Location.Y);
            //Actualiza el valor del ancho de la columnas
            if (dataGridView1.Columns.Count != 0)
            {
                int x = (dataGridView1.Width - 20) / dataGridView1.Columns.Count;
                foreach (DataGridViewColumn aux in dataGridView1.Columns)
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
                        actualizarTabla(control.obtenerGruposTable(texto));
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
                    actualizarTabla(control.obtenerGruposTable());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
        }

        private void listasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                Grupo grupo = control.consultarGrupo(id);
                DetalleGrupoListas dtg = new DetalleGrupoListas(grupo);
                dtg.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void calificacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                Grupo grupo = control.consultarGrupo(id);
                DetalleGrupoCalificaciones dtg = new DetalleGrupoCalificaciones(grupo);
                dtg.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
