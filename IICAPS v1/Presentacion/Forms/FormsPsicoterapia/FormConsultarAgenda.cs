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

namespace IICAPS_v1.Presentacion.Forms.FromsControl
{
    public partial class FormConsultarAgenda : Form
    {
        List<String> ubicaciones;
        List<Reservacion> reservaciones = new List<Reservacion>();
        List<TimeSpan> intervalos = new List<TimeSpan>();
        TimeSpan last_Intervalo;
        DateTime last_Fecha;
        ControlIicaps control;
        string ubicacion;
        DateTime fecha;
        TimeSpan hora;
        public FormConsultarAgenda(DateTime fecha)
        {
            InitializeComponent();
            try
            {
                intervalos.Add(new TimeSpan(0, 30, 0));
                intervalos.Add(new TimeSpan(1, 0, 0));
                cmbIntervalo.SelectedIndex = 0;
                control = ControlIicaps.getInstance();

                ubicaciones = control.consultarUbicaciones();
                if (fecha != null)
                    datePicker_Fecha.Value = fecha;
                FillDatos();
                Agregar_Columnas();
                Mostrar_Columnas();
                Filtro_Click(null, null);
                dataGridView1_CellContentClick(null,null);
            }
            catch (Exception e)
            {

            }
        }

        public void FillDatos()
        {
            reservaciones = control.obtenerReservaciones(datePicker_Fecha.Value);
            foreach (DataGridViewRow aux in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in aux.Cells)
                {
                    cell.Style.BackColor = Color.White;
                    cell.Tag = null;
                }
            }
        }

        private void Filtro_Click(object sender, EventArgs e)
        {
            if (last_Fecha.ToShortDateString() != datePicker_Fecha.Value.ToShortDateString() && last_Intervalo==intervalos.ElementAt(cmbIntervalo.SelectedIndex))
            {
                FillDatos();
            }else if (last_Fecha.ToShortDateString() == datePicker_Fecha.Value.ToShortDateString() && last_Intervalo != intervalos.ElementAt(cmbIntervalo.SelectedIndex))
            {
                Agregar_Columnas();
                Mostrar_Columnas();
            }
            else if (last_Fecha.ToShortDateString() != datePicker_Fecha.Value.ToShortDateString() && last_Intervalo != intervalos.ElementAt(cmbIntervalo.SelectedIndex))
            {
                FillDatos();
                Agregar_Columnas();
                Mostrar_Columnas();
            }
            else
            {
                Mostrar_Columnas();
                return;
            }
            Llenar_Agenda();
            last_Fecha = datePicker_Fecha.Value;
            last_Intervalo = intervalos.ElementAt(cmbIntervalo.SelectedIndex);
        }

        private void Agregar_Columnas()
        {
            TimeSpan inicio = new TimeSpan(0,0,0);
            TimeSpan fin = new TimeSpan(23, 59, 59);
            dataGridView1.Columns.Clear();
            while (inicio <= fin)
            {
                DataGridViewColumn column = new DataGridViewColumn();
                DataGridViewCell cellType = new DataGridViewTextBoxCell();
                cellType.Style.WrapMode = DataGridViewTriState.True;
                column.CellTemplate = cellType;
                column.Name = inicio.Hours.ToString("00") + ":" + inicio.Minutes.ToString("00");
                column.HeaderText = inicio.Hours.ToString("00") + ":" + inicio.Minutes.ToString("00");
                column.Visible = false;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns.Add(column);
                inicio = inicio.Add(intervalos.ElementAt(cmbIntervalo.SelectedIndex));
            }
            dataGridView1.Rows.Clear();
            for (int i = 0; i < ubicaciones.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell.Value = ubicaciones.ElementAt(i);
                dataGridView1.Rows[i].Height = 80;
            }
            dataGridView1.RowHeadersWidth = 100;
            dataGridView1.ColumnHeadersHeight = 25;

        }
        private void Mostrar_Columnas()
        {
            TimeSpan inicio = filtroHoraInicio.Value.TimeOfDay;
            TimeSpan fin = filtroHoraFin.Value.TimeOfDay;
            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                if(columna.Name == inicio.Hours.ToString("00") + ":" + inicio.Minutes.ToString("00"))
                {
                    columna.Visible = true;
                    if(inicio <= fin)
                        inicio = inicio.Add(intervalos.ElementAt(cmbIntervalo.SelectedIndex));
                }
                else
                columna.Visible = false;
            }
            dataGridView1.ColumnHeadersHeight = 25;
        }
        public void Llenar_Agenda()
        {
            foreach (DataGridViewRow filas in dataGridView1.Rows)
            {
                foreach (DataGridViewCell celdas in filas.Cells)
                {
                    celdas.Value = null;
                } 
            }
            TimeSpan intervalo = intervalos.ElementAt(cmbIntervalo.SelectedIndex);
            if(reservaciones!=null)
            foreach (Reservacion r in reservaciones)
            {
                string columna;
                TimeSpan hora = new TimeSpan(r.hora_Inicio.Hours, 0, 0);
                Color columnaColor;
                while (true)
                {
                    if (hora == r.hora_Inicio)
                    {
                        columna = r.hora_Inicio.Hours.ToString("00") + ":00";
                        columnaColor = Color.Red;
                        break;
                    }
                    else if (hora.Add(intervalo) == r.hora_Inicio)
                    {
                        columna = r.hora_Inicio.Hours.ToString("00") + ":" + r.hora_Inicio.Minutes.ToString("00");
                        columnaColor = Color.Red;
                        break;
                    }
                    else if (hora < r.hora_Inicio && hora.Add(intervalo) > r.hora_Inicio)
                    {
                        columna = hora.Hours.ToString("00") + ":" + hora.Minutes.ToString("00");
                        columnaColor = Color.MistyRose;
                        break;
                    }
                    else
                        hora = hora.Add(intervalo);
                }
                if (dataGridView1.Rows[ubicaciones.IndexOf(r.ubicacion)].Cells[dataGridView1.Columns[columna].Index].Value != null)
                {
                    dataGridView1.Rows[ubicaciones.IndexOf(r.ubicacion)].Cells[dataGridView1.Columns[columna].Index].Value += "\n --------------------- \n" + r.agendaText();
                    dataGridView1.Rows[ubicaciones.IndexOf(r.ubicacion)].Cells[dataGridView1.Columns[columna].Index].Style.BackColor = Color.Red;
                    dataGridView1.Rows[ubicaciones.IndexOf(r.ubicacion)].Cells[dataGridView1.Columns[columna].Index].Tag += "," + r.codigo_Reservacion;
                }
                else
                {
                    dataGridView1.Rows[ubicaciones.IndexOf(r.ubicacion)].Cells[dataGridView1.Columns[columna].Index].Value = r.agendaText();
                    dataGridView1.Rows[ubicaciones.IndexOf(r.ubicacion)].Cells[dataGridView1.Columns[columna].Index].Style.BackColor = columnaColor;
                    dataGridView1.Rows[ubicaciones.IndexOf(r.ubicacion)].Cells[dataGridView1.Columns[columna].Index].Tag = r.codigo_Reservacion;
                }
                TimeSpan newHora = new TimeSpan(Convert.ToInt32(columna.Substring(0,2)), Convert.ToInt32(columna.Substring(3)),0).Add(intervalos.ElementAt(cmbIntervalo.SelectedIndex));
                while (r.hora_Fin > newHora)
                {
                    columna = newHora.Hours.ToString("00") + ":" + newHora.Minutes.ToString("00");
                    if (dataGridView1.Rows[ubicaciones.IndexOf(r.ubicacion)].Cells[dataGridView1.Columns[columna].Index].Value != null)
                    {
                        dataGridView1.Rows[ubicaciones.IndexOf(r.ubicacion)].Cells[dataGridView1.Columns[columna].Index].Value += "\n --------------------- \n" + r.agendaText();
                        dataGridView1.Rows[ubicaciones.IndexOf(r.ubicacion)].Cells[dataGridView1.Columns[columna].Index].Style.BackColor = Color.Red;
                        dataGridView1.Rows[ubicaciones.IndexOf(r.ubicacion)].Cells[dataGridView1.Columns[columna].Index].Tag += "," + r.codigo_Reservacion;
                    }
                    else
                    {
                        dataGridView1.Rows[ubicaciones.IndexOf(r.ubicacion)].Cells[dataGridView1.Columns[columna].Index].Value = r.agendaText();
                        dataGridView1.Rows[ubicaciones.IndexOf(r.ubicacion)].Cells[dataGridView1.Columns[columna].Index].Style.BackColor = Color.Red;
                        dataGridView1.Rows[ubicaciones.IndexOf(r.ubicacion)].Cells[dataGridView1.Columns[columna].Index].Tag = r.codigo_Reservacion;
                    }
                    if (r.hora_Fin.Subtract(newHora) < intervalo)
                    {
                        if (dataGridView1.Rows[ubicaciones.IndexOf(r.ubicacion)].Cells[dataGridView1.Columns[columna].Index].Value.ToString().IndexOf("-----") == -1)
                            dataGridView1.Rows[ubicaciones.IndexOf(r.ubicacion)].Cells[dataGridView1.Columns[columna].Index].Style.BackColor = Color.MistyRose;
                        break;
                    }
                    newHora = newHora.Add(intervalos.ElementAt(cmbIntervalo.SelectedIndex));

                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DateTime aux = new DateTime(last_Fecha.Year, last_Fecha.Month, last_Fecha.Day, Convert.ToInt32(dataGridView1.CurrentCell.OwningColumn.HeaderCell.Value.ToString().Substring(0, 2)), Convert.ToInt32(dataGridView1.CurrentCell.OwningColumn.HeaderCell.Value.ToString().Substring(3)), 0);

                if(DateTime.Now > last_Fecha)
                    seleccionarFechaToolStripMenuItem.Visible = false;
                else
                    seleccionarFechaToolStripMenuItem.Visible = true;
                if (dataGridView1.CurrentCell.Tag != null)
                {
                    string id = dataGridView1.CurrentCell.Tag.ToString();
                    if (id.Contains(","))
                        id = id.Substring(0, id.IndexOf(","));
                    lblCodigo.Text = id;
                    Reservacion res = control.consultarReservacion(id);
                    lblConcepto.Text = res.concepto;
                    lblDuracion.Text = res.duracion.Hours + ":" + res.duracion.Minutes + " hrs";
                    lblHora.Text = res.hora_Inicio.Hours + ":" + res.hora_Inicio.Minutes + " hrs";
                    lblFecha.Text = res.fecha.ToShortDateString();
                    lblReservante.Text = res.reservante;
                    lblUbicacion.Text = res.ubicacion;
                    seleccionarFechaToolStripMenuItem.Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void FormConsultarAgenda_SizeChanged(object sender, EventArgs e)
        {
            int ancho = this.Width;
            int alto = this.Height;
            //Actualiza el tamaño de la tabla con respecto al tamaño de la ventana
            dataGridView1.Width = ancho - 220;
            dataGridView1.Height = alto - 95;
            panel1.Height = alto-15;
        }

        private void seleccionarFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.fecha = datePicker_Fecha.Value;
                this.hora = new TimeSpan(Convert.ToInt32(dataGridView1.CurrentCell.OwningColumn.HeaderText.Substring(0, 2)), Convert.ToInt32(dataGridView1.CurrentCell.OwningColumn.HeaderText.Substring(3)), 0);
                this.ubicacion = dataGridView1.CurrentCell.OwningRow.HeaderCell.Value.ToString();
                this.Close();
            }
            catch (Exception ex)
            {

            }
        }
        public DateTime getFecha()
        {
            return this.fecha;
        }
        public string getUbicacion()
        {
            return this.ubicacion;
        }
        public TimeSpan getHora()
        {
            return this.hora;
        }
    }
}
