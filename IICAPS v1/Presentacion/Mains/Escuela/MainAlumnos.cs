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
    public partial class MainAlumnos : Form
    {

        private static MainAlumnos instance;
        ControlIicaps control;
        public MainAlumnos()
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            try
            {
                actualizarTablaAlumnos(control.obtenerAlumnosTable());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static MainAlumnos getInstance()
        {
            if (instance == null)
                instance = new MainAlumnos();
            return instance;
        }

        private void actualizarTablaAlumnos(MySqlDataAdapter data)
        {
            try
            {
                DataTable dtDatos = new DataTable();
                //Con la informacion del adaptador se llena el datatable
                data.Fill(dtDatos);
                //Se asigna el datatable como origen de datos del datagridview
                dataGridViewAlumnos.DataSource = dtDatos;
                //Actualiza el valor de la etiqueta donde se muestra el total de productos
                dataGridViewAlumnos.Columns[0].Width = 50;
                dataGridViewAlumnos.Columns[1].Width = 50;
                dataGridViewAlumnos.Columns[2].Width = 50;
                dataGridViewAlumnos.Columns[3].Width = 50;
                dataGridViewAlumnos.Columns[4].Width = 50;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btnAgregarAlumno_Click(object sender, EventArgs e)
        {
            Alumnos fa = new Alumnos(null);
            fa.FormClosed += new FormClosedEventHandler(form_Closed);
            fa.Show();
        }

        private void form_Closed(object sender, FormClosedEventArgs e)
        {
            actualizarTablaAlumnos(control.obtenerAlumnosTable(txtBuscarAlumno.Text));
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            consultarAlumno();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String rfc = dataGridViewAlumnos.CurrentRow.Cells[0].Value.ToString();
                Alumno alumno = control.consultarAlumno(rfc);
                Alumnos fa = new Alumnos(alumno);
                fa.FormClosed += new FormClosedEventHandler(form_Closed);
                fa.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void darDeBajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String rfc = dataGridViewAlumnos.CurrentRow.Cells[0].Value.ToString();
                DialogResult dialogresult = MessageBox.Show("¿Desea dar de baja el alumno?", "Baja de Alumno", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if(dialogresult == DialogResult.OK)
                {
                    if (control.darDeBajaAlumno(rfc))
                    {
                        MessageBox.Show("Alumno dado de baja");
                        actualizarTablaAlumnos(control.obtenerAlumnosTable());
                    }
                    else
                        MessageBox.Show("Error al dar la baja del alumno");
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
            txtBuscarAlumno.Text = "";
            try
            {
                actualizarTablaAlumnos(control.obtenerAlumnosTable());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void consultarAlumno()
        {
            try
            {
                String rfc = dataGridViewAlumnos.CurrentRow.Cells[0].Value.ToString();
                Alumno alumno = control.consultarAlumno(rfc);
                Alumnos fa = new Alumnos(alumno, true);
                fa.FormClosed += new FormClosedEventHandler(form_Closed);
                fa.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            actualizarTablaAlumnos(control.obtenerAlumnosTable(txtBuscarAlumno.Text));
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            int ancho = this.Width;
            //Actualiza el tamaño de la tabla con respecto al tamaño de la ventana
            dataGridViewAlumnos.Width = ancho - 195;
            dataGridViewAlumnos.Height = this.Height - 130;
            //actualiza la posicion de los controles con respecto al tamaño de la ventana
            btnAgregar.Location = new Point(ancho - 169, btnAgregar.Location.Y);
            txtBuscarAlumno.Location = new Point(ancho - 216, txtBuscarAlumno.Location.Y);
            pictureBox2.Location = new Point(ancho - 245, pictureBox2.Location.Y);
            limpiarBusqueda.Location = new Point(ancho - 39, limpiarBusqueda.Location.Y);
            //Actualiza el valor del ancho de la columnas
            int x = (dataGridViewAlumnos.Width - 20) / dataGridViewAlumnos.Columns.Count;
            foreach (DataGridViewColumn aux in dataGridViewAlumnos.Columns)
            {
                aux.Width = x;
            }
        }


    }
}
