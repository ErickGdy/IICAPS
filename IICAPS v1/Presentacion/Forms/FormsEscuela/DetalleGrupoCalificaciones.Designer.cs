namespace IICAPS_v1.Presentacion.Mains.Escuela
{
    partial class DetalleGrupoCalificaciones
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            instance = null;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetalleGrupoCalificaciones));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnBuscarLista = new System.Windows.Forms.Button();
            this.lblNombreGrupo = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.imprimirListaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.capturarCalificacionesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbMaterias = new System.Windows.Forms.ComboBox();
            this.lblMaterias = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.MyPrintDocument = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Location = new System.Drawing.Point(13, 146);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(757, 328);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnBuscarLista
            // 
            this.btnBuscarLista.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(173)))), ((int)(((byte)(73)))));
            this.btnBuscarLista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBuscarLista.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(36)))), ((int)(((byte)(28)))));
            this.btnBuscarLista.FlatAppearance.BorderSize = 0;
            this.btnBuscarLista.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarLista.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarLista.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarLista.Image")));
            this.btnBuscarLista.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarLista.Location = new System.Drawing.Point(479, 79);
            this.btnBuscarLista.Margin = new System.Windows.Forms.Padding(2);
            this.btnBuscarLista.Name = "btnBuscarLista";
            this.btnBuscarLista.Size = new System.Drawing.Size(136, 28);
            this.btnBuscarLista.TabIndex = 2;
            this.btnBuscarLista.Text = "Buscar Lista";
            this.btnBuscarLista.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscarLista.UseVisualStyleBackColor = false;
            this.btnBuscarLista.Click += new System.EventHandler(this.btnBuscarLista_Click);
            // 
            // lblNombreGrupo
            // 
            this.lblNombreGrupo.AutoSize = true;
            this.lblNombreGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreGrupo.ForeColor = System.Drawing.Color.Black;
            this.lblNombreGrupo.Location = new System.Drawing.Point(103, 28);
            this.lblNombreGrupo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNombreGrupo.Name = "lblNombreGrupo";
            this.lblNombreGrupo.Size = new System.Drawing.Size(238, 31);
            this.lblNombreGrupo.TabIndex = 41;
            this.lblNombreGrupo.Text = "Nombre de grupo";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imprimirListaToolStripMenuItem1,
            this.capturarCalificacionesToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 29);
            this.menuStrip1.TabIndex = 42;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // imprimirListaToolStripMenuItem1
            // 
            this.imprimirListaToolStripMenuItem1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.imprimirListaToolStripMenuItem1.Enabled = false;
            this.imprimirListaToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("imprimirListaToolStripMenuItem1.Image")));
            this.imprimirListaToolStripMenuItem1.Name = "imprimirListaToolStripMenuItem1";
            this.imprimirListaToolStripMenuItem1.Size = new System.Drawing.Size(254, 25);
            this.imprimirListaToolStripMenuItem1.Text = "Imprimir Lista de Calificaciones";
            this.imprimirListaToolStripMenuItem1.Click += new System.EventHandler(this.imprimirListaToolStripMenuItem_Click);
            // 
            // capturarCalificacionesToolStripMenuItem1
            // 
            this.capturarCalificacionesToolStripMenuItem1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.capturarCalificacionesToolStripMenuItem1.Enabled = false;
            this.capturarCalificacionesToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("capturarCalificacionesToolStripMenuItem1.Image")));
            this.capturarCalificacionesToolStripMenuItem1.Name = "capturarCalificacionesToolStripMenuItem1";
            this.capturarCalificacionesToolStripMenuItem1.Size = new System.Drawing.Size(197, 25);
            this.capturarCalificacionesToolStripMenuItem1.Text = "Capturar Calificaciones";
            this.capturarCalificacionesToolStripMenuItem1.Click += new System.EventHandler(this.capturarCalificacionesToolStripMenuItem_Click);
            // 
            // cmbMaterias
            // 
            this.cmbMaterias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaterias.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmbMaterias.FormattingEnabled = true;
            this.cmbMaterias.Location = new System.Drawing.Point(103, 80);
            this.cmbMaterias.Name = "cmbMaterias";
            this.cmbMaterias.Size = new System.Drawing.Size(371, 28);
            this.cmbMaterias.TabIndex = 1;
            // 
            // lblMaterias
            // 
            this.lblMaterias.AutoSize = true;
            this.lblMaterias.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lblMaterias.ForeColor = System.Drawing.Color.Black;
            this.lblMaterias.Location = new System.Drawing.Point(20, 83);
            this.lblMaterias.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaterias.Name = "lblMaterias";
            this.lblMaterias.Size = new System.Drawing.Size(85, 24);
            this.lblMaterias.TabIndex = 46;
            this.lblMaterias.Text = "Materias:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(173)))), ((int)(((byte)(73)))));
            this.btnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(36)))), ((int)(((byte)(28)))));
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(634, 483);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(136, 45);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.Text = "GUARDAR";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Visible = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // MyPrintDocument
            // 
            this.MyPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.MyPrintDocument_PrintPage);
            // 
            // DetalleGrupoCalificaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 547);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.cmbMaterias);
            this.Controls.Add(this.lblMaterias);
            this.Controls.Add(this.lblNombreGrupo);
            this.Controls.Add(this.btnBuscarLista);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.White;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(800, 450);
            this.Name = "DetalleGrupoCalificaciones";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lista de alumnos";
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnBuscarLista;
        private System.Windows.Forms.Label lblNombreGrupo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem imprimirListaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem capturarCalificacionesToolStripMenuItem1;
        private System.Windows.Forms.ComboBox cmbMaterias;
        private System.Windows.Forms.Label lblMaterias;
        private System.Windows.Forms.Button btnGuardar;
        private System.Drawing.Printing.PrintDocument MyPrintDocument;
    }
}