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

namespace IICAPS_v1.Presentacion.Mains.Psicoterapia
{
    public partial class MainAgenda : Form
    {

        private static MainAgenda instance;
        ControlIicaps control;
        List<String> ubicaciones;
        List<Reservacion> reservaciones = new List<Reservacion>();
        List<TimeSpan> intervalos = new List<TimeSpan>();
        TimeSpan last_Intervalo;
        DateTime last_Fecha;
        private DateTime fecha;
        private TimeSpan hora;
        private string ubicacion;

        public MainAgenda()
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            try
            {
                intervalos.Add(new TimeSpan(0, 30, 0));
                intervalos.Add(new TimeSpan(1, 0, 0));
                cmbIntervalo.SelectedIndex = 0;
                ubicaciones = control.consultarUbicaciones();
                FillDatos();
                Agregar_Columnas();
                Mostrar_Columnas();
                Filtro_Click(null, null);
                dataGridView1_CellContentClick(null,null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static MainAgenda getInstance()
        {
            if (instance == null)
                instance = new MainAgenda();
            return instance;
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
            if (last_Fecha.ToShortDateString() != datePicker_Fecha.Value.ToShortDateString() && last_Intervalo == intervalos.ElementAt(cmbIntervalo.SelectedIndex))
            {
                FillDatos();
            }
            else if (last_Fecha.ToShortDateString() == datePicker_Fecha.Value.ToShortDateString() && last_Intervalo != intervalos.ElementAt(cmbIntervalo.SelectedIndex))
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
            TimeSpan inicio = new TimeSpan(0, 0, 0);
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
                if (columna.Name == inicio.Hours.ToString("00") + ":" + inicio.Minutes.ToString("00"))
                {
                    columna.Visible = true;
                    if (inicio <= fin)
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
            if(reservaciones!= null)
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

                TimeSpan newHora = new TimeSpan(Convert.ToInt32(columna.Substring(0, 2)), Convert.ToInt32(columna.Substring(3)), 0).Add(intervalos.ElementAt(cmbIntervalo.SelectedIndex));
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

                modificarToolStripMenuItem.Visible = false;
                consultarToolStripMenuItem.Visible = false;
                if (DateTime.Now.CompareTo(aux) > 0)
                {
                    agendarAquiToolStripMenuItem.Visible = false;
                }
                else
                    agendarAquiToolStripMenuItem.Visible = true;

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
                    modificarToolStripMenuItem.Visible = true;
                    consultarToolStripMenuItem.Visible = true;
                    agendarAquiToolStripMenuItem.Visible = false;
                }

            }
            catch (Exception ex)
            {
                
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FormReservacion frs = new FormReservacion(null,true, false, null, "Psicoterapia");
            frs.BringToFront();
            frs.FormClosed += new FormClosedEventHandler(form_Closed);
            frs.Show();
        }
        public void form_Closed(object sender, FormClosedEventArgs e)
        {
            FillDatos();
            Filtro_Click(null, null);
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String ids = dataGridView1.CurrentCell.Tag.ToString();
                List<string> idL = new List<string>();
                if (ids.Contains(","))
                {
                    while (ids.Contains(",")) {
                        idL.Add(ids.Substring(0, ids.IndexOf(",")));
                        ids = ids.Substring(ids.IndexOf(",")+1);
                    }
                    idL.Add(ids);
                    FormSeleccionReservacion fsr = new FormSeleccionReservacion(idL);
                    fsr.ShowDialog();
                    Reservacion reservacion = control.consultarReservacion(fsr.getCodigo_Reservacion());
                    FormReservacion frs = new FormReservacion(reservacion, false, true, "Otro", "Psicoterapia");
                    frs.BringToFront();
                    frs.FormClosed += new FormClosedEventHandler(form_Closed);
                    frs.Show();
                } else {
                    Reservacion reservacion = control.consultarReservacion(ids);
                    FormReservacion frs = new FormReservacion(reservacion, false, true, "Otro", "Psicoterapia");
                    frs.BringToFront();
                    frs.FormClosed += new FormClosedEventHandler(form_Closed);
                    frs.Show();
                }
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
                String ids = dataGridView1.CurrentCell.Tag.ToString();
                List<string> idL = new List<string>();
                if (ids.Contains(","))
                {
                    while (ids.Contains(","))
                    {
                        idL.Add(ids.Substring(0, ids.IndexOf(",")));
                        ids = ids.Substring(ids.IndexOf(","));
                    }
                    idL.Add(ids);
                    FormSeleccionReservacion fsr = new FormSeleccionReservacion(idL);
                    fsr.ShowDialog();
                    ids = fsr.getCodigo_Reservacion();
                }
                Reservacion reservacion = control.consultarReservacion(ids);
                FormReservacion frs = new FormReservacion(reservacion, false, false, "Otro", "Psicoterapia");
                frs.BringToFront();
                frs.FormClosed += new FormClosedEventHandler(form_Closed);
                frs.Show();
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
                DialogResult dialogresult = MessageBox.Show("¿Desea cancelar reservación?", "Cancelar reservación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogresult == DialogResult.OK)
                {
                    String ids = dataGridView1.CurrentCell.Tag.ToString();
                    List<string> idL = new List<string>();
                    if (ids.Contains(","))
                    {
                        while (ids.Contains(","))
                        {
                            idL.Add(ids.Substring(0, ids.IndexOf(",")));
                            ids = ids.Substring(ids.IndexOf(","));
                        }
                        idL.Add(ids);
                        FormSeleccionReservacion fsr = new FormSeleccionReservacion(idL);
                        fsr.ShowDialog();
                        ids = fsr.getCodigo_Reservacion();
                    }
                    if (control.cancelarReservacion(ids))
                    {
                        MessageBox.Show("Reservacion cancelada");
                        Filtro_Click(null,null);
                    }
                    else
                        MessageBox.Show("Error al cancelar reservación");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Main_SizeChanged(object sender, EventArgs e)
        {
            int ancho = this.Width;
            //Actualiza el tamaño de la tabla con respecto al tamaño de la ventana
            dataGridView1.Width = ancho - 315;
            dataGridView1.Height = this.Height - 130;
            panel1.Height = this.Height - 130;
            //actualiza la posicion de los controles con respecto al tamaño de la ventana
            btnAgregar.Location = new Point(ancho - 169, btnAgregar.Location.Y);
            panel1.Location = new Point(ancho - 135, panel1.Location.Y);
        }

        private void agendarAquiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.fecha = datePicker_Fecha.Value;
                this.hora = new TimeSpan(Convert.ToInt32(dataGridView1.CurrentCell.OwningColumn.HeaderText.Substring(0, 2)), Convert.ToInt32(dataGridView1.CurrentCell.OwningColumn.HeaderText.Substring(3)), 0);
                this.ubicacion = dataGridView1.CurrentCell.OwningRow.HeaderCell.Value.ToString();
                btnAgregar_Click(null, null);
            }
            catch (Exception ex) { }
        }
    }
}
