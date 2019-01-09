namespace IICAPS_v1.Presentacion
{
    partial class PagosPaciente
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PagosPaciente));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewPagos = new System.Windows.Forms.DataGridView();
            this.txtBuscarPagos = new System.Windows.Forms.TextBox();
            this.limpiarBusquedaPagos = new System.Windows.Forms.LinkLabel();
            this.pictureBoxBuscar = new System.Windows.Forms.PictureBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnActualizarPagos = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNombrePaciente = new System.Windows.Forms.Label();
            this.panelPagos = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panelSesiones = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnActualizarSesiones = new System.Windows.Forms.Button();
            this.limpiarBusquedaSesiones = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtBuscarSesiones = new System.Windows.Forms.TextBox();
            this.dataGridViewSesiones = new System.Windows.Forms.DataGridView();
            this.panelDatos = new System.Windows.Forms.Panel();
            this.lblPagoPendiente = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPagos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBuscar)).BeginInit();
            this.panelPagos.SuspendLayout();
            this.panelSesiones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSesiones)).BeginInit();
            this.panelDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewPagos
            // 
            this.dataGridViewPagos.AllowUserToAddRows = false;
            this.dataGridViewPagos.AllowUserToDeleteRows = false;
            this.dataGridViewPagos.AllowUserToOrderColumns = true;
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dataGridViewPagos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewPagos.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewPagos.Location = new System.Drawing.Point(20, 36);
            this.dataGridViewPagos.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewPagos.Name = "dataGridViewPagos";
            this.dataGridViewPagos.ReadOnly = true;
            this.dataGridViewPagos.RowHeadersVisible = false;
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dataGridViewPagos.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewPagos.RowTemplate.Height = 24;
            this.dataGridViewPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPagos.Size = new System.Drawing.Size(809, 200);
            this.dataGridViewPagos.TabIndex = 0;
            // 
            // txtBuscarPagos
            // 
            this.txtBuscarPagos.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarPagos.Location = new System.Drawing.Point(629, 6);
            this.txtBuscarPagos.Margin = new System.Windows.Forms.Padding(2);
            this.txtBuscarPagos.Name = "txtBuscarPagos";
            this.txtBuscarPagos.Size = new System.Drawing.Size(200, 24);
            this.txtBuscarPagos.TabIndex = 3;
            this.txtBuscarPagos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuscar_KeyUp);
            // 
            // limpiarBusquedaPagos
            // 
            this.limpiarBusquedaPagos.AutoSize = true;
            this.limpiarBusquedaPagos.BackColor = System.Drawing.Color.White;
            this.limpiarBusquedaPagos.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.limpiarBusquedaPagos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.limpiarBusquedaPagos.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.limpiarBusquedaPagos.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.limpiarBusquedaPagos.LinkColor = System.Drawing.Color.Gray;
            this.limpiarBusquedaPagos.Location = new System.Drawing.Point(805, 8);
            this.limpiarBusquedaPagos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.limpiarBusquedaPagos.Name = "limpiarBusquedaPagos";
            this.limpiarBusquedaPagos.Size = new System.Drawing.Size(21, 20);
            this.limpiarBusquedaPagos.TabIndex = 39;
            this.limpiarBusquedaPagos.TabStop = true;
            this.limpiarBusquedaPagos.Text = "X";
            this.limpiarBusquedaPagos.Visible = false;
            this.limpiarBusquedaPagos.VisitedLinkColor = System.Drawing.Color.Black;
            this.limpiarBusquedaPagos.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.limpiarBusqueda_LinkClicked);
            // 
            // pictureBoxBuscar
            // 
            this.pictureBoxBuscar.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxBuscar.Image")));
            this.pictureBoxBuscar.Location = new System.Drawing.Point(600, 6);
            this.pictureBoxBuscar.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBoxBuscar.Name = "pictureBoxBuscar";
            this.pictureBoxBuscar.Size = new System.Drawing.Size(24, 23);
            this.pictureBoxBuscar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBuscar.TabIndex = 38;
            this.pictureBoxBuscar.TabStop = false;
            // 
            // btnAgregar
            // 
            this.btnAgregar.AutoSize = true;
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(173)))), ((int)(((byte)(73)))));
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAgregar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(36)))), ((int)(((byte)(28)))));
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(682, 62);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(143, 32);
            this.btnAgregar.TabIndex = 1;
            this.btnAgregar.Text = "Agregar Pago";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnActualizarPagos
            // 
            this.btnActualizarPagos.AutoSize = true;
            this.btnActualizarPagos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnActualizarPagos.BackColor = System.Drawing.Color.Transparent;
            this.btnActualizarPagos.FlatAppearance.BorderSize = 0;
            this.btnActualizarPagos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarPagos.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizarPagos.Image")));
            this.btnActualizarPagos.Location = new System.Drawing.Point(18, 6);
            this.btnActualizarPagos.Name = "btnActualizarPagos";
            this.btnActualizarPagos.Size = new System.Drawing.Size(30, 30);
            this.btnActualizarPagos.TabIndex = 2;
            this.btnActualizarPagos.UseVisualStyleBackColor = false;
            this.btnActualizarPagos.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pagos:";
            // 
            // lblNombrePaciente
            // 
            this.lblNombrePaciente.AutoSize = true;
            this.lblNombrePaciente.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.lblNombrePaciente.ForeColor = System.Drawing.Color.Black;
            this.lblNombrePaciente.Location = new System.Drawing.Point(99, 11);
            this.lblNombrePaciente.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNombrePaciente.Name = "lblNombrePaciente";
            this.lblNombrePaciente.Size = new System.Drawing.Size(107, 29);
            this.lblNombrePaciente.TabIndex = 41;
            this.lblNombrePaciente.Text = "Paciente";
            // 
            // panelPagos
            // 
            this.panelPagos.BackColor = System.Drawing.SystemColors.Window;
            this.panelPagos.Controls.Add(this.label2);
            this.panelPagos.Controls.Add(this.btnActualizarPagos);
            this.panelPagos.Controls.Add(this.limpiarBusquedaPagos);
            this.panelPagos.Controls.Add(this.pictureBoxBuscar);
            this.panelPagos.Controls.Add(this.txtBuscarPagos);
            this.panelPagos.Controls.Add(this.dataGridViewPagos);
            this.panelPagos.Location = new System.Drawing.Point(0, 100);
            this.panelPagos.Name = "panelPagos";
            this.panelPagos.Size = new System.Drawing.Size(838, 251);
            this.panelPagos.TabIndex = 42;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(53, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 29);
            this.label2.TabIndex = 41;
            this.label2.Text = "Pagos realizados";
            // 
            // panelSesiones
            // 
            this.panelSesiones.BackColor = System.Drawing.SystemColors.Window;
            this.panelSesiones.Controls.Add(this.label3);
            this.panelSesiones.Controls.Add(this.btnActualizarSesiones);
            this.panelSesiones.Controls.Add(this.limpiarBusquedaSesiones);
            this.panelSesiones.Controls.Add(this.pictureBox1);
            this.panelSesiones.Controls.Add(this.txtBuscarSesiones);
            this.panelSesiones.Controls.Add(this.dataGridViewSesiones);
            this.panelSesiones.Location = new System.Drawing.Point(0, 353);
            this.panelSesiones.Name = "panelSesiones";
            this.panelSesiones.Size = new System.Drawing.Size(838, 251);
            this.panelSesiones.TabIndex = 43;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(53, 5);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 29);
            this.label3.TabIndex = 42;
            this.label3.Text = "Sesiones";
            // 
            // btnActualizarSesiones
            // 
            this.btnActualizarSesiones.AutoSize = true;
            this.btnActualizarSesiones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnActualizarSesiones.BackColor = System.Drawing.Color.Transparent;
            this.btnActualizarSesiones.FlatAppearance.BorderSize = 0;
            this.btnActualizarSesiones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarSesiones.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizarSesiones.Image")));
            this.btnActualizarSesiones.Location = new System.Drawing.Point(18, 6);
            this.btnActualizarSesiones.Name = "btnActualizarSesiones";
            this.btnActualizarSesiones.Size = new System.Drawing.Size(30, 30);
            this.btnActualizarSesiones.TabIndex = 4;
            this.btnActualizarSesiones.UseVisualStyleBackColor = false;
            this.btnActualizarSesiones.Click += new System.EventHandler(this.btnActualizarSesiones_Click);
            // 
            // limpiarBusquedaSesiones
            // 
            this.limpiarBusquedaSesiones.AutoSize = true;
            this.limpiarBusquedaSesiones.BackColor = System.Drawing.Color.White;
            this.limpiarBusquedaSesiones.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.limpiarBusquedaSesiones.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.limpiarBusquedaSesiones.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.limpiarBusquedaSesiones.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.limpiarBusquedaSesiones.LinkColor = System.Drawing.Color.Gray;
            this.limpiarBusquedaSesiones.Location = new System.Drawing.Point(805, 8);
            this.limpiarBusquedaSesiones.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.limpiarBusquedaSesiones.Name = "limpiarBusquedaSesiones";
            this.limpiarBusquedaSesiones.Size = new System.Drawing.Size(21, 20);
            this.limpiarBusquedaSesiones.TabIndex = 39;
            this.limpiarBusquedaSesiones.TabStop = true;
            this.limpiarBusquedaSesiones.Text = "X";
            this.limpiarBusquedaSesiones.Visible = false;
            this.limpiarBusquedaSesiones.VisitedLinkColor = System.Drawing.Color.Black;
            this.limpiarBusquedaSesiones.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.limpiarBusquedaSesiones_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(599, 6);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 38;
            this.pictureBox1.TabStop = false;
            // 
            // txtBuscarSesiones
            // 
            this.txtBuscarSesiones.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarSesiones.Location = new System.Drawing.Point(628, 6);
            this.txtBuscarSesiones.Margin = new System.Windows.Forms.Padding(2);
            this.txtBuscarSesiones.Name = "txtBuscarSesiones";
            this.txtBuscarSesiones.Size = new System.Drawing.Size(200, 24);
            this.txtBuscarSesiones.TabIndex = 5;
            this.txtBuscarSesiones.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuscarSesiones_KeyUp);
            // 
            // dataGridViewSesiones
            // 
            this.dataGridViewSesiones.AllowUserToAddRows = false;
            this.dataGridViewSesiones.AllowUserToDeleteRows = false;
            this.dataGridViewSesiones.AllowUserToOrderColumns = true;
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dataGridViewSesiones.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewSesiones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewSesiones.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewSesiones.Location = new System.Drawing.Point(20, 36);
            this.dataGridViewSesiones.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewSesiones.Name = "dataGridViewSesiones";
            this.dataGridViewSesiones.ReadOnly = true;
            this.dataGridViewSesiones.RowHeadersVisible = false;
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dataGridViewSesiones.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewSesiones.RowTemplate.Height = 24;
            this.dataGridViewSesiones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSesiones.Size = new System.Drawing.Size(809, 200);
            this.dataGridViewSesiones.TabIndex = 0;
            // 
            // panelDatos
            // 
            this.panelDatos.Controls.Add(this.lblPagoPendiente);
            this.panelDatos.Controls.Add(this.label4);
            this.panelDatos.Controls.Add(this.btnAgregar);
            this.panelDatos.Controls.Add(this.label1);
            this.panelDatos.Controls.Add(this.lblNombrePaciente);
            this.panelDatos.Location = new System.Drawing.Point(0, 1);
            this.panelDatos.Name = "panelDatos";
            this.panelDatos.Size = new System.Drawing.Size(838, 101);
            this.panelDatos.TabIndex = 42;
            // 
            // lblPagoPendiente
            // 
            this.lblPagoPendiente.AutoSize = true;
            this.lblPagoPendiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lblPagoPendiente.ForeColor = System.Drawing.Color.Black;
            this.lblPagoPendiente.Location = new System.Drawing.Point(205, 62);
            this.lblPagoPendiente.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPagoPendiente.Name = "lblPagoPendiente";
            this.lblPagoPendiente.Size = new System.Drawing.Size(45, 24);
            this.lblPagoPendiente.TabIndex = 43;
            this.lblPagoPendiente.Text = "0.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(16, 62);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 24);
            this.label4.TabIndex = 42;
            this.label4.Text = "Pendiente de pago: $";
            // 
            // PagosPaciente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(838, 604);
            this.Controls.Add(this.panelPagos);
            this.Controls.Add(this.panelSesiones);
            this.Controls.Add(this.panelDatos);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(800, 450);
            this.Name = "PagosPaciente";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pagos de Paciente";
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPagos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBuscar)).EndInit();
            this.panelPagos.ResumeLayout(false);
            this.panelPagos.PerformLayout();
            this.panelSesiones.ResumeLayout(false);
            this.panelSesiones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSesiones)).EndInit();
            this.panelDatos.ResumeLayout(false);
            this.panelDatos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPagos;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TextBox txtBuscarPagos;
        private System.Windows.Forms.PictureBox pictureBoxBuscar;
        private System.Windows.Forms.LinkLabel limpiarBusquedaPagos;
        private System.Windows.Forms.Button btnActualizarPagos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNombrePaciente;
        private System.Windows.Forms.Panel panelPagos;
        private System.Windows.Forms.Panel panelSesiones;
        private System.Windows.Forms.Button btnActualizarSesiones;
        private System.Windows.Forms.LinkLabel limpiarBusquedaSesiones;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtBuscarSesiones;
        private System.Windows.Forms.DataGridView dataGridViewSesiones;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelDatos;
        private System.Windows.Forms.Label lblPagoPendiente;
        private System.Windows.Forms.Label label4;
    }
}