namespace IICAPS_v1.Presentacion.Mains.Escuela
{
    partial class MainCreditos
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainCreditos));
            this.dataGridViewCreditos = new System.Windows.Forms.DataGridView();
            this.menuTablaAlumnos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.consultarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darDeBajaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtBuscarCredito = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.limpiarBusqueda = new System.Windows.Forms.LinkLabel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.cancelarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCreditos)).BeginInit();
            this.menuTablaAlumnos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewCreditos
            // 
            this.dataGridViewCreditos.AllowUserToAddRows = false;
            this.dataGridViewCreditos.AllowUserToDeleteRows = false;
            this.dataGridViewCreditos.AllowUserToOrderColumns = true;
            this.dataGridViewCreditos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCreditos.ContextMenuStrip = this.menuTablaAlumnos;
            this.dataGridViewCreditos.Location = new System.Drawing.Point(237, 138);
            this.dataGridViewCreditos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewCreditos.MultiSelect = false;
            this.dataGridViewCreditos.Name = "dataGridViewCreditos";
            this.dataGridViewCreditos.ReadOnly = true;
            this.dataGridViewCreditos.RowHeadersVisible = false;
            this.dataGridViewCreditos.RowTemplate.Height = 24;
            this.dataGridViewCreditos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCreditos.Size = new System.Drawing.Size(764, 338);
            this.dataGridViewCreditos.TabIndex = 0;
            // 
            // menuTablaAlumnos
            // 
            this.menuTablaAlumnos.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuTablaAlumnos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.consultarToolStripMenuItem,
            this.modificarToolStripMenuItem,
            this.darDeBajaToolStripMenuItem,
            this.cancelarToolStripMenuItem});
            this.menuTablaAlumnos.Name = "menuTablaAlumnos";
            this.menuTablaAlumnos.Size = new System.Drawing.Size(211, 128);
            // 
            // consultarToolStripMenuItem
            // 
            this.consultarToolStripMenuItem.Name = "consultarToolStripMenuItem";
            this.consultarToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.consultarToolStripMenuItem.Text = "Consultar";
            this.consultarToolStripMenuItem.Click += new System.EventHandler(this.consultarToolStripMenuItem_Click);
            // 
            // modificarToolStripMenuItem
            // 
            this.modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
            this.modificarToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.modificarToolStripMenuItem.Text = "Modificar";
            this.modificarToolStripMenuItem.Click += new System.EventHandler(this.modificarToolStripMenuItem_Click);
            // 
            // darDeBajaToolStripMenuItem
            // 
            this.darDeBajaToolStripMenuItem.Name = "darDeBajaToolStripMenuItem";
            this.darDeBajaToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.darDeBajaToolStripMenuItem.Text = "Contrato";
            this.darDeBajaToolStripMenuItem.Click += new System.EventHandler(this.darDeBajaToolStripMenuItem_Click);
            // 
            // txtBuscarCredito
            // 
            this.txtBuscarCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarCredito.Location = new System.Drawing.Point(735, 101);
            this.txtBuscarCredito.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBuscarCredito.Name = "txtBuscarCredito";
            this.txtBuscarCredito.Size = new System.Drawing.Size(265, 28);
            this.txtBuscarCredito.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(229, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 39);
            this.label1.TabIndex = 3;
            this.label1.Text = "Creditos";
            // 
            // limpiarBusqueda
            // 
            this.limpiarBusqueda.AutoSize = true;
            this.limpiarBusqueda.BackColor = System.Drawing.Color.White;
            this.limpiarBusqueda.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.limpiarBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.limpiarBusqueda.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.limpiarBusqueda.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.limpiarBusqueda.LinkColor = System.Drawing.Color.Gray;
            this.limpiarBusqueda.Location = new System.Drawing.Point(971, 103);
            this.limpiarBusqueda.Name = "limpiarBusqueda";
            this.limpiarBusqueda.Size = new System.Drawing.Size(27, 25);
            this.limpiarBusqueda.TabIndex = 39;
            this.limpiarBusqueda.TabStop = true;
            this.limpiarBusqueda.Text = "X";
            this.limpiarBusqueda.Visible = false;
            this.limpiarBusqueda.VisitedLinkColor = System.Drawing.Color.Black;
            this.limpiarBusqueda.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.limpiarBusqueda_LinkClicked);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(696, 101);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 28);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 38;
            this.pictureBox2.TabStop = false;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(237, 101);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 30);
            this.button1.TabIndex = 40;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(173)))), ((int)(((byte)(73)))));
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAgregar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(36)))), ((int)(((byte)(28)))));
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(797, 55);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(204, 39);
            this.btnAgregar.TabIndex = 42;
            this.btnAgregar.Text = "Agregar Nuevo";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregarAlumno_Click);
            // 
            // cancelarToolStripMenuItem
            // 
            this.cancelarToolStripMenuItem.Name = "cancelarToolStripMenuItem";
            this.cancelarToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.cancelarToolStripMenuItem.Text = "Cancelar";
            this.cancelarToolStripMenuItem.Click += new System.EventHandler(this.cancelarToolStripMenuItem_Click);
            // 
            // MainCreditos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1333, 571);
            this.ControlBox = false;
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.limpiarBusqueda);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBuscarCredito);
            this.Controls.Add(this.dataGridViewCreditos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainCreditos";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainAlumnos";
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCreditos)).EndInit();
            this.menuTablaAlumnos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewCreditos;
        private System.Windows.Forms.TextBox txtBuscarCredito;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.LinkLabel limpiarBusqueda;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip menuTablaAlumnos;
        private System.Windows.Forms.ToolStripMenuItem consultarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darDeBajaToolStripMenuItem;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.ToolStripMenuItem cancelarToolStripMenuItem;
    }
}