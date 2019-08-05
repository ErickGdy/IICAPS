namespace IICAPS_v1.Presentacion
{
    partial class FormVenta
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
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVenta));
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.NumericUpDown();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbAlumno = new System.Windows.Forms.ComboBox();
            this.txtBuscarRFC = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLibro = new System.Windows.Forms.TextBox();
            this.cmbLibros = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupAlumno = new System.Windows.Forms.GroupBox();
            this.checkPublico = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Col_Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Precio_Unitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAgregarLibro = new System.Windows.Forms.Button();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_Credito = new System.Windows.Forms.RadioButton();
            this.rb_Contado = new System.Windows.Forms.RadioButton();
            this.txtCambio = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbEmpleados = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPago = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupAlumno.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPago)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.btnAceptar.Location = new System.Drawing.Point(415, 486);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(117, 44);
            this.btnAceptar.TabIndex = 7;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.BtnAceptar_click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(68, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 18);
            this.label5.TabIndex = 37;
            this.label5.Text = "Cantidad:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 18);
            this.label2.TabIndex = 35;
            this.label2.Text = "Buscar RFC:";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidad.Location = new System.Drawing.Point(153, 221);
            this.txtCantidad.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(39, 24);
            this.txtCantidad.TabIndex = 3;
            this.txtCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Location = new System.Drawing.Point(573, 486);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(117, 44);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(62, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 18);
            this.label1.TabIndex = 42;
            this.label1.Text = "Alumno:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(-5, 544);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1404, 98);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 63;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-40, -15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1109, 147);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 62;
            this.pictureBox1.TabStop = false;
            // 
            // lblFecha
            // 
            this.lblFecha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFecha.AutoSize = true;
            this.lblFecha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(81)))), ((int)(((byte)(43)))));
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.Color.White;
            this.lblFecha.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFecha.Location = new System.Drawing.Point(951, 9);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(99, 20);
            this.lblFecha.TabIndex = 65;
            this.lblFecha.Text = "10/12/2010";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(81)))), ((int)(((byte)(43)))));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(886, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 20);
            this.label6.TabIndex = 64;
            this.label6.Text = "Fecha:";
            // 
            // cmbAlumno
            // 
            this.cmbAlumno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlumno.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAlumno.FormattingEnabled = true;
            this.cmbAlumno.Location = new System.Drawing.Point(132, 50);
            this.cmbAlumno.Name = "cmbAlumno";
            this.cmbAlumno.Size = new System.Drawing.Size(292, 26);
            this.cmbAlumno.TabIndex = 2;
            // 
            // txtBuscarRFC
            // 
            this.txtBuscarRFC.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarRFC.Location = new System.Drawing.Point(132, 18);
            this.txtBuscarRFC.Margin = new System.Windows.Forms.Padding(2);
            this.txtBuscarRFC.Name = "txtBuscarRFC";
            this.txtBuscarRFC.Size = new System.Drawing.Size(254, 24);
            this.txtBuscarRFC.TabIndex = 76;
            this.txtBuscarRFC.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuscarRFC_KeyUp);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(798, 402);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 18);
            this.label4.TabIndex = 78;
            this.label4.Text = "Total a pagar: $";
            // 
            // txtLibro
            // 
            this.txtLibro.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtLibro.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLibro.Location = new System.Drawing.Point(153, 146);
            this.txtLibro.Margin = new System.Windows.Forms.Padding(2);
            this.txtLibro.Name = "txtLibro";
            this.txtLibro.Size = new System.Drawing.Size(254, 24);
            this.txtLibro.TabIndex = 82;
            this.txtLibro.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtLibro_KeyUp);
            // 
            // cmbLibros
            // 
            this.cmbLibros.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbLibros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLibros.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLibros.FormattingEnabled = true;
            this.cmbLibros.Location = new System.Drawing.Point(153, 181);
            this.cmbLibros.Name = "cmbLibros";
            this.cmbLibros.Size = new System.Drawing.Size(349, 26);
            this.cmbLibros.TabIndex = 79;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(96, 184);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 18);
            this.label8.TabIndex = 81;
            this.label8.Text = "Libro:";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(39, 149);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 18);
            this.label9.TabIndex = 80;
            this.label9.Text = "Buscar Libro:";
            // 
            // groupAlumno
            // 
            this.groupAlumno.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupAlumno.Controls.Add(this.txtBuscarRFC);
            this.groupAlumno.Controls.Add(this.cmbAlumno);
            this.groupAlumno.Controls.Add(this.label1);
            this.groupAlumno.Controls.Add(this.label2);
            this.groupAlumno.Location = new System.Drawing.Point(598, 153);
            this.groupAlumno.Name = "groupAlumno";
            this.groupAlumno.Size = new System.Drawing.Size(446, 85);
            this.groupAlumno.TabIndex = 83;
            this.groupAlumno.TabStop = false;
            this.groupAlumno.Text = "Datos del Alumno";
            // 
            // checkPublico
            // 
            this.checkPublico.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkPublico.AutoSize = true;
            this.checkPublico.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkPublico.Location = new System.Drawing.Point(710, 134);
            this.checkPublico.Name = "checkPublico";
            this.checkPublico.Size = new System.Drawing.Size(155, 24);
            this.checkPublico.TabIndex = 84;
            this.checkPublico.Text = "Publico General";
            this.checkPublico.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_Count,
            this.Col_Descripcion,
            this.Col_Precio_Unitario,
            this.Col_Cantidad,
            this.Col_Total});
            this.dataGridView1.Location = new System.Drawing.Point(11, 265);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(572, 110);
            this.dataGridView1.TabIndex = 85;
            // 
            // Col_Count
            // 
            this.Col_Count.HeaderText = "#";
            this.Col_Count.Name = "Col_Count";
            this.Col_Count.ReadOnly = true;
            this.Col_Count.Width = 30;
            // 
            // Col_Descripcion
            // 
            this.Col_Descripcion.HeaderText = "Descripción";
            this.Col_Descripcion.Name = "Col_Descripcion";
            this.Col_Descripcion.ReadOnly = true;
            this.Col_Descripcion.Width = 280;
            // 
            // Col_Precio_Unitario
            // 
            this.Col_Precio_Unitario.HeaderText = "Precio Unitario";
            this.Col_Precio_Unitario.Name = "Col_Precio_Unitario";
            this.Col_Precio_Unitario.ReadOnly = true;
            // 
            // Col_Cantidad
            // 
            this.Col_Cantidad.HeaderText = "Cantidad";
            this.Col_Cantidad.Name = "Col_Cantidad";
            this.Col_Cantidad.Width = 70;
            // 
            // Col_Total
            // 
            this.Col_Total.HeaderText = "Total";
            this.Col_Total.Name = "Col_Total";
            this.Col_Total.ReadOnly = true;
            this.Col_Total.Width = 70;
            // 
            // btnAgregarLibro
            // 
            this.btnAgregarLibro.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAgregarLibro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(151)))), ((int)(((byte)(92)))));
            this.btnAgregarLibro.FlatAppearance.BorderSize = 0;
            this.btnAgregarLibro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarLibro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarLibro.ForeColor = System.Drawing.Color.White;
            this.btnAgregarLibro.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarLibro.Image")));
            this.btnAgregarLibro.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregarLibro.Location = new System.Drawing.Point(199, 218);
            this.btnAgregarLibro.Name = "btnAgregarLibro";
            this.btnAgregarLibro.Size = new System.Drawing.Size(106, 30);
            this.btnAgregarLibro.TabIndex = 86;
            this.btnAgregarLibro.Text = "Agregar";
            this.btnAgregarLibro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarLibro.UseVisualStyleBackColor = false;
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(925, 399);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(2);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(97, 24);
            this.txtTotal.TabIndex = 87;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotal.TextChanged += new System.EventHandler(this.txtTotal_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.rb_Credito);
            this.groupBox1.Controls.Add(this.rb_Contado);
            this.groupBox1.Location = new System.Drawing.Point(598, 296);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 75);
            this.groupBox1.TabIndex = 88;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de venta";
            // 
            // rb_Credito
            // 
            this.rb_Credito.AutoSize = true;
            this.rb_Credito.Location = new System.Drawing.Point(40, 42);
            this.rb_Credito.Name = "rb_Credito";
            this.rb_Credito.Size = new System.Drawing.Size(58, 17);
            this.rb_Credito.TabIndex = 1;
            this.rb_Credito.TabStop = true;
            this.rb_Credito.Text = "Crédito";
            this.rb_Credito.UseVisualStyleBackColor = true;
            // 
            // rb_Contado
            // 
            this.rb_Contado.AutoSize = true;
            this.rb_Contado.Location = new System.Drawing.Point(40, 19);
            this.rb_Contado.Name = "rb_Contado";
            this.rb_Contado.Size = new System.Drawing.Size(77, 17);
            this.rb_Contado.TabIndex = 0;
            this.rb_Contado.TabStop = true;
            this.rb_Contado.Text = "Al Contado";
            this.rb_Contado.UseVisualStyleBackColor = true;
            // 
            // txtCambio
            // 
            this.txtCambio.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCambio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCambio.Location = new System.Drawing.Point(925, 456);
            this.txtCambio.Margin = new System.Windows.Forms.Padding(2);
            this.txtCambio.Name = "txtCambio";
            this.txtCambio.ReadOnly = true;
            this.txtCambio.Size = new System.Drawing.Size(97, 24);
            this.txtCambio.TabIndex = 90;
            this.txtCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(839, 459);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 18);
            this.label3.TabIndex = 89;
            this.label3.Text = "Cambio: $";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(858, 430);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 18);
            this.label7.TabIndex = 91;
            this.label7.Text = "Pago: $";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox2.Controls.Add(this.cmbEmpleados);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(598, 240);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(446, 50);
            this.groupBox2.TabIndex = 84;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Quien Recibe";
            // 
            // cmbEmpleados
            // 
            this.cmbEmpleados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpleados.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmpleados.FormattingEnabled = true;
            this.cmbEmpleados.Location = new System.Drawing.Point(132, 15);
            this.cmbEmpleados.Name = "cmbEmpleados";
            this.cmbEmpleados.Size = new System.Drawing.Size(292, 26);
            this.cmbEmpleados.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(42, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 18);
            this.label10.TabIndex = 42;
            this.label10.Text = "Empleado:";
            // 
            // txtPago
            // 
            this.txtPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtPago.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtPago.Location = new System.Drawing.Point(925, 425);
            this.txtPago.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.txtPago.Name = "txtPago";
            this.txtPago.Size = new System.Drawing.Size(97, 26);
            this.txtPago.TabIndex = 93;
            this.txtPago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtObservaciones);
            this.groupBox3.Location = new System.Drawing.Point(767, 296);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(277, 75);
            this.groupBox3.TabIndex = 94;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Observaciones";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(6, 21);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(265, 46);
            this.txtObservaciones.TabIndex = 0;
            // 
            // FormVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1062, 644);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.txtPago);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtCambio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.btnAgregarLibro);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.checkPublico);
            this.Controls.Add(this.groupAlumno);
            this.Controls.Add(this.txtLibro);
            this.Controls.Add(this.cmbLibros);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormVenta";
            this.Text = "44";
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupAlumno.ResumeLayout(false);
            this.groupAlumno.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPago)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtCantidad;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbAlumno;
        private System.Windows.Forms.TextBox txtBuscarRFC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLibro;
        private System.Windows.Forms.ComboBox cmbLibros;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupAlumno;
        private System.Windows.Forms.CheckBox checkPublico;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Count;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Precio_Unitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Total;
        private System.Windows.Forms.Button btnAgregarLibro;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb_Credito;
        private System.Windows.Forms.RadioButton rb_Contado;
        private System.Windows.Forms.TextBox txtCambio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbEmpleados;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown txtPago;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtObservaciones;
    }
}