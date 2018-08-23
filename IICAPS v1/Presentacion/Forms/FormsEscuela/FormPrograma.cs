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

namespace IICAPS_v1.Presentacion
{
    public partial class FormPrograma : Form
    {
        ControlIicaps control;
        bool modificacion;
        MateriasDialog md;
        List<string> listaMateriasListBox = new List<string>();
        public FormPrograma(Programa programa)
        {
            InitializeComponent();
            control = ControlIicaps.getInstance();
            List<String> auxNombres = new List<string>();
            cmbModalidad.SelectedIndex = 0;
            if (programa != null)
            {
                modificacion = true;
                txtCodigo.Text = programa.Codigo;
                txtCEIFRHS.Text = programa.CEIFRHS;
                txtRVOE.Text = programa.RVOE;
                txtNombre.Text = programa.Nombre;
                txtNivel.SelectedItem = programa.Nivel;
                txtDuracion.Text = programa.Duracion;
                txtHorario.Text = programa.Horario;
                cmbModalidad.SelectedValue = programa.Modalidad;
                txtObjetivo.Text = programa.Objetivo;
                txtReqEspecialidad.Text = programa.RequisitosEspecialidad;
                txtReqTitulacion.Text = programa.RequisitosTitulacion;
                txtReqDiplomado.Text = programa.RequisitosDiplomado;
                txtPerfilIngreso.Text = programa.PerfilIngreso;
                txtPerfilEgreso.Text = programa.PerfilEgreso;
                txtProcesoSeleccion.Text = programa.ProcesoSeleccion;
                txtCostoInscripcion.Value = programa.CostoInscripcionSemestral;
                txtCostoMensual.Value = programa.CostoMensualidad;
                txtCostoCurso.Value = programa.CostoCursoPropedeutico;
                List<int> ids = new List<int>();
                if (programa.MapaCurricular != null)
                {
                    foreach (Materia m in programa.MapaCurricular)
                    {
                        dataGridViewMaterias.Rows.Insert(dataGridViewMaterias.RowCount, m.id, m.nombre, m.semestre);
                        ids.Add(m.id);
                    }
                }
                try
                {
                    foreach (Materia mate in control.obtenerMaterias())
                    {
                        if(!ids.Contains(mate.id))
                            auxNombres.Add(mate.id + " - " + mate.nombre);
                    }
                    cmbMaterias.Items.AddRange(auxNombres.ToArray());
                    cmbMaterias.SelectedIndex = 0;
                    txtNivel.SelectedIndex = 0;
                    cmbModalidad.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    foreach (Materia m in control.obtenerMaterias())
                    {
                        auxNombres.Add(m.id + " - " + m.nombre);
                    }
                    cmbMaterias.Items.AddRange(auxNombres.ToArray());
                    cmbMaterias.SelectedIndex = 0;
                    txtNivel.SelectedIndex = 0;
                    cmbModalidad.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            foreach (string s in cmbMaterias.Items)
            {
                listaMateriasListBox.Add(s);
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validarCampos())
                {
                    Programa p = new Programa();
                    p.Codigo = txtCodigo.Text;
                    p.RVOE = txtRVOE.Text;
                    p.CEIFRHS = txtCEIFRHS.Text;
                    p.Nivel = txtNivel.SelectedItem.ToString();
                    p.Nombre = txtNombre.Text;
                    p.Duracion = txtDuracion.Text;
                    p.Horario = txtHorario.Text;
                    p.Modalidad = cmbModalidad.SelectedItem.ToString();
                    p.Objetivo = txtObjetivo.Text;
                    p.RequisitosEspecialidad = txtReqEspecialidad.Text;
                    p.RequisitosTitulacion = txtReqTitulacion.Text;
                    p.RequisitosDiplomado = txtReqDiplomado.Text;
                    p.PerfilIngreso = txtPerfilIngreso.Text;
                    p.PerfilEgreso = txtPerfilEgreso.Text;
                    p.ProcesoSeleccion = txtProcesoSeleccion.Text;
                    p.CostoInscripcionSemestral = txtCostoInscripcion.Value;
                    p.CostoMensualidad = txtCostoMensual.Value;
                    p.CostoCursoPropedeutico = txtCostoCurso.Value;
                    List<Materia> aux = new List<Materia>();
                    for (int i = 0; i < dataGridViewMaterias.RowCount; i++)
                    {
                        DataGridViewCellCollection cells = dataGridViewMaterias.Rows[i].Cells;
                        Materia m = new Materia();                        
                        aux.Add(control.consultarMateria(cells[0].Value.ToString()));
                    }
                    p.MapaCurricular = aux;
                    try
                    {
                        if (modificacion)
                        {
                            if (control.actualizarPrograma(p))
                            {
                                MessageBox.Show("Datos actualizados exitosamente!");
                                Close();
                                Dispose();
                            }
                            else
                                MessageBox.Show("Error al guardar datos, verifique los campos y vuelva a intentarlo");
                        }
                        else
                        {
                            if (control.agregarPrograma(p))
                            {
                                MessageBox.Show("Datos guardados exitosamente!");
                                Close();
                                Dispose();
                            }
                            else
                                MessageBox.Show("Error al guardar datos, verifique los campos y vuelva a intentarlo");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                    MessageBox.Show("No dejar campos vacios");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool validarCampos()
        {
            if (txtNombre.Text != "" && txtCostoInscripcion.Value > 0 && txtCodigo.Text != "" && txtNombre.Text != "" && txtDuracion.Text != "" && txtHorario.Text != "" &&
            txtObjetivo.Text != "" && txtReqEspecialidad.Text != "" && txtReqTitulacion.Text != "" && txtReqDiplomado.Text != "" && txtPerfilIngreso.Text != "" && txtPerfilEgreso.Text != "" &&
            txtProcesoSeleccion.Text != "" && txtCostoInscripcion.Value > 0 && txtCostoMensual.Value > 0 && txtCostoCurso.Value > 0)
                return true;
            return false;
        }

        private void btnMateriaNueva_Click(object sender, EventArgs e)
        {
            md = new MateriasDialog();
            md.FormClosed += new FormClosedEventHandler(form_Closed);
            md.ShowDialog();
        }

        private void form_Closed(object sender, FormClosedEventArgs e)
        {
            Materia materia = md.getMateria();
            if (materia != null)
            {
                int n = control.obtenerUltimoIDMateria();
                dataGridViewMaterias.Rows.Insert(dataGridViewMaterias.RowCount, n, materia.nombre, materia.semestre);
                cmbMaterias.Items.Add(n + " - " + materia.nombre);
                cmbMaterias.Refresh();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(cmbMaterias.SelectedItem!=null)
            try
            {
                string materia = cmbMaterias.SelectedItem.ToString();
                string id = materia.Substring(0, materia.IndexOf(" - "));
                foreach(DataGridViewRow row in dataGridViewMaterias.Rows)
                {
                    if (dataGridViewMaterias.Rows[row.Index].Cells[0].Value.ToString() == id)
                        return;
                }
                Materia m = control.consultarMateria(id);
                dataGridViewMaterias.Rows.Insert(dataGridViewMaterias.RowCount, m.id, m.nombre, m.semestre);
                cmbMaterias.Items.RemoveAt(cmbMaterias.SelectedIndex);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            listaMateriasListBox.Clear();
            foreach (string s in cmbMaterias.Items)
            {
                listaMateriasListBox.Add(s);
            }
        }

        private void btnNext1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabControl1.SelectedIndex + 1);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabControl1.SelectedIndex - 1);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                cmbMaterias.Items.Add(dataGridViewMaterias.CurrentRow.Cells[0].Value.ToString() + " - " + dataGridViewMaterias.CurrentRow.Cells[1].Value.ToString());
                dataGridViewMaterias.Rows.Remove(dataGridViewMaterias.CurrentRow);
                listaMateriasListBox.Clear();
                foreach (string s in cmbMaterias.Items)
                {
                    listaMateriasListBox.Add(s);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewMaterias_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridViewMaterias.Sort(dataGridViewMaterias.Columns[2], ListSortDirection.Ascending);
            dataGridViewMaterias.Sort(dataGridViewMaterias.Columns[0], ListSortDirection.Ascending);
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                string texto = txtBuscar.Text;
                if (texto != "")
                    limpiarBusqueda.Visible = true;
                else
                    limpiarBusqueda.Visible = false;
                cmbMaterias.Items.Clear();
                cmbMaterias.Items.AddRange(listaMateriasListBox.ToArray());
                List<string> aux = new List<string>();
                foreach (string s in cmbMaterias.Items)
                {
                    if (!s.ToUpper().Contains(txtBuscar.Text.ToUpper()))
                    {
                        aux.Add(s);
                    }
                }
                foreach (string x in aux)
                {
                    cmbMaterias.Items.Remove(x);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void limpiarBusqueda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            limpiarBusqueda.Visible = false;
            txtBuscar.Text = "";
            try
            {
                cmbMaterias.Items.AddRange(listaMateriasListBox.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
