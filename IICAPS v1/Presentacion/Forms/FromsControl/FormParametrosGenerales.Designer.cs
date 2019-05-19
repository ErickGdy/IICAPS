namespace IICAPS_v1.Presentacion
{
    partial class FormParametrosGenerales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormParametrosGenerales));
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.layoutButtons = new System.Windows.Forms.TableLayoutPanel();
            this.layoutForm = new System.Windows.Forms.TableLayoutPanel();
            this.txtSede = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDirector = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPorcentajeEvaluacion = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUbicacion = new System.Windows.Forms.TextBox();
            this.btnAgregarUbicacion = new System.Windows.Forms.Button();
            this.listUbicaciones = new System.Windows.Forms.ListBox();
            this.btnRemoverUbicacion = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPagoClase = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPagoTaller = new System.Windows.Forms.TextBox();
            this.txtPagoSesion = new System.Windows.Forms.TextBox();
            this.txtCosto_Credito_Especialidad_Diplomado = new System.Windows.Forms.TextBox();
            this.txtCosto_Credito_Maestria = new System.Windows.Forms.TextBox();
            this.pictureBoxHeader = new System.Windows.Forms.PictureBox();
            this.layoutVentana = new System.Windows.Forms.TableLayoutPanel();
            this.layoutButtons.SuspendLayout();
            this.layoutForm.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeader)).BeginInit();
            this.layoutVentana.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(504, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(107, 33);
            this.btnCancelar.TabIndex = 44;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(354, 3);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(107, 33);
            this.btnAceptar.TabIndex = 43;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // layoutButtons
            // 
            this.layoutButtons.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.layoutButtons.ColumnCount = 4;
            this.layoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.layoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.layoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutButtons.Controls.Add(this.btnCancelar, 2, 0);
            this.layoutButtons.Controls.Add(this.btnAceptar, 1, 0);
            this.layoutButtons.Location = new System.Drawing.Point(189, 607);
            this.layoutButtons.Name = "layoutButtons";
            this.layoutButtons.RowCount = 1;
            this.layoutButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutButtons.Size = new System.Drawing.Size(966, 39);
            this.layoutButtons.TabIndex = 45;
            // 
            // layoutForm
            // 
            this.layoutForm.ColumnCount = 2;
            this.layoutForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutForm.Controls.Add(this.txtSede, 1, 8);
            this.layoutForm.Controls.Add(this.label9, 0, 8);
            this.layoutForm.Controls.Add(this.txtDirector, 1, 7);
            this.layoutForm.Controls.Add(this.label8, 0, 7);
            this.layoutForm.Controls.Add(this.txtPorcentajeEvaluacion, 1, 5);
            this.layoutForm.Controls.Add(this.label7, 0, 5);
            this.layoutForm.Controls.Add(this.groupBox1, 0, 9);
            this.layoutForm.Controls.Add(this.label6, 0, 6);
            this.layoutForm.Controls.Add(this.txtPagoClase, 1, 6);
            this.layoutForm.Controls.Add(this.label5, 0, 4);
            this.layoutForm.Controls.Add(this.label4, 0, 3);
            this.layoutForm.Controls.Add(this.label3, 0, 2);
            this.layoutForm.Controls.Add(this.label2, 0, 1);
            this.layoutForm.Controls.Add(this.txtPagoTaller, 1, 4);
            this.layoutForm.Controls.Add(this.txtPagoSesion, 1, 3);
            this.layoutForm.Controls.Add(this.txtCosto_Credito_Especialidad_Diplomado, 1, 1);
            this.layoutForm.Controls.Add(this.txtCosto_Credito_Maestria, 1, 2);
            this.layoutForm.Location = new System.Drawing.Point(148, 48);
            this.layoutForm.Name = "layoutForm";
            this.layoutForm.RowCount = 10;
            this.layoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.71312F));
            this.layoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.706713F));
            this.layoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.706713F));
            this.layoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.706713F));
            this.layoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.706713F));
            this.layoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.706713F));
            this.layoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.706713F));
            this.layoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.706713F));
            this.layoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.706713F));
            this.layoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.63318F));
            this.layoutForm.Size = new System.Drawing.Size(1049, 553);
            this.layoutForm.TabIndex = 42;
            // 
            // txtSede
            // 
            this.txtSede.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSede.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtSede.Location = new System.Drawing.Point(526, 422);
            this.txtSede.Margin = new System.Windows.Forms.Padding(2);
            this.txtSede.Name = "txtSede";
            this.txtSede.Size = new System.Drawing.Size(285, 26);
            this.txtSede.TabIndex = 104;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(465, 425);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 20);
            this.label9.TabIndex = 104;
            this.label9.Text = "Sede:";
            // 
            // txtDirector
            // 
            this.txtDirector.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtDirector.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtDirector.Location = new System.Drawing.Point(526, 380);
            this.txtDirector.Margin = new System.Windows.Forms.Padding(2);
            this.txtDirector.Name = "txtDirector";
            this.txtDirector.Size = new System.Drawing.Size(285, 26);
            this.txtDirector.TabIndex = 103;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(443, 383);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 20);
            this.label8.TabIndex = 103;
            this.label8.Text = "Director:";
            // 
            // txtPorcentajeEvaluacion
            // 
            this.txtPorcentajeEvaluacion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPorcentajeEvaluacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtPorcentajeEvaluacion.Location = new System.Drawing.Point(526, 296);
            this.txtPorcentajeEvaluacion.Margin = new System.Windows.Forms.Padding(2);
            this.txtPorcentajeEvaluacion.Name = "txtPorcentajeEvaluacion";
            this.txtPorcentajeEvaluacion.Size = new System.Drawing.Size(285, 26);
            this.txtPorcentajeEvaluacion.TabIndex = 103;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(232, 299);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(289, 20);
            this.label7.TabIndex = 103;
            this.label7.Text = "Porcentaje de Pago por Evaluación";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.layoutForm.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtUbicacion);
            this.groupBox1.Controls.Add(this.btnAgregarUbicacion);
            this.groupBox1.Controls.Add(this.listUbicaciones);
            this.groupBox1.Controls.Add(this.btnRemoverUbicacion);
            this.groupBox1.Location = new System.Drawing.Point(3, 459);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1043, 91);
            this.groupBox1.TabIndex = 102;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ubicaciones";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(280, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 98;
            this.label1.Text = "Ubicación:";
            // 
            // txtUbicacion
            // 
            this.txtUbicacion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUbicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUbicacion.Location = new System.Drawing.Point(193, 41);
            this.txtUbicacion.Margin = new System.Windows.Forms.Padding(2);
            this.txtUbicacion.Name = "txtUbicacion";
            this.txtUbicacion.Size = new System.Drawing.Size(274, 24);
            this.txtUbicacion.TabIndex = 51;
            // 
            // btnAgregarUbicacion
            // 
            this.btnAgregarUbicacion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAgregarUbicacion.BackColor = System.Drawing.Color.Transparent;
            this.btnAgregarUbicacion.FlatAppearance.BorderSize = 0;
            this.btnAgregarUbicacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarUbicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarUbicacion.ForeColor = System.Drawing.Color.White;
            this.btnAgregarUbicacion.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarUbicacion.Image")));
            this.btnAgregarUbicacion.Location = new System.Drawing.Point(487, 18);
            this.btnAgregarUbicacion.Name = "btnAgregarUbicacion";
            this.btnAgregarUbicacion.Size = new System.Drawing.Size(31, 33);
            this.btnAgregarUbicacion.TabIndex = 52;
            this.btnAgregarUbicacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarUbicacion.UseVisualStyleBackColor = false;
            this.btnAgregarUbicacion.Click += new System.EventHandler(this.btnAgregarUbicacion_Click);
            // 
            // listUbicaciones
            // 
            this.listUbicaciones.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.listUbicaciones.FormattingEnabled = true;
            this.listUbicaciones.Location = new System.Drawing.Point(533, 8);
            this.listUbicaciones.Name = "listUbicaciones";
            this.listUbicaciones.Size = new System.Drawing.Size(294, 82);
            this.listUbicaciones.Sorted = true;
            this.listUbicaciones.TabIndex = 54;
            // 
            // btnRemoverUbicacion
            // 
            this.btnRemoverUbicacion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRemoverUbicacion.BackColor = System.Drawing.Color.Transparent;
            this.btnRemoverUbicacion.FlatAppearance.BorderSize = 0;
            this.btnRemoverUbicacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoverUbicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoverUbicacion.ForeColor = System.Drawing.Color.White;
            this.btnRemoverUbicacion.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoverUbicacion.Image")));
            this.btnRemoverUbicacion.Location = new System.Drawing.Point(487, 52);
            this.btnRemoverUbicacion.Name = "btnRemoverUbicacion";
            this.btnRemoverUbicacion.Size = new System.Drawing.Size(31, 34);
            this.btnRemoverUbicacion.TabIndex = 53;
            this.btnRemoverUbicacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoverUbicacion.UseVisualStyleBackColor = false;
            this.btnRemoverUbicacion.Click += new System.EventHandler(this.btnRemoverUbicacion_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(269, 341);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(252, 20);
            this.label6.TabIndex = 97;
            this.label6.Text = "Porcentaje de Pago por Clase:";
            // 
            // txtPagoClase
            // 
            this.txtPagoClase.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPagoClase.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtPagoClase.Location = new System.Drawing.Point(526, 338);
            this.txtPagoClase.Margin = new System.Windows.Forms.Padding(2);
            this.txtPagoClase.Name = "txtPagoClase";
            this.txtPagoClase.Size = new System.Drawing.Size(285, 26);
            this.txtPagoClase.TabIndex = 96;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(270, 257);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(251, 20);
            this.label5.TabIndex = 95;
            this.label5.Text = "Porcentaje de Pago por Taller:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(259, 215);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(262, 20);
            this.label4.TabIndex = 94;
            this.label4.Text = "Porcentaje de Pago por Sesión:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(257, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(264, 20);
            this.label3.TabIndex = 93;
            this.label3.Text = "Costo de Crédito para Maestría:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(139, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(382, 20);
            this.label2.TabIndex = 92;
            this.label2.Text = "Costo de Crédito para Especialidad/Diplomado";
            // 
            // txtPagoTaller
            // 
            this.txtPagoTaller.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPagoTaller.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtPagoTaller.Location = new System.Drawing.Point(526, 254);
            this.txtPagoTaller.Margin = new System.Windows.Forms.Padding(2);
            this.txtPagoTaller.Name = "txtPagoTaller";
            this.txtPagoTaller.Size = new System.Drawing.Size(285, 26);
            this.txtPagoTaller.TabIndex = 91;
            // 
            // txtPagoSesion
            // 
            this.txtPagoSesion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPagoSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtPagoSesion.Location = new System.Drawing.Point(526, 212);
            this.txtPagoSesion.Margin = new System.Windows.Forms.Padding(2);
            this.txtPagoSesion.Name = "txtPagoSesion";
            this.txtPagoSesion.Size = new System.Drawing.Size(285, 26);
            this.txtPagoSesion.TabIndex = 89;
            // 
            // txtCosto_Credito_Especialidad_Diplomado
            // 
            this.txtCosto_Credito_Especialidad_Diplomado.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCosto_Credito_Especialidad_Diplomado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtCosto_Credito_Especialidad_Diplomado.Location = new System.Drawing.Point(526, 128);
            this.txtCosto_Credito_Especialidad_Diplomado.Margin = new System.Windows.Forms.Padding(2);
            this.txtCosto_Credito_Especialidad_Diplomado.Name = "txtCosto_Credito_Especialidad_Diplomado";
            this.txtCosto_Credito_Especialidad_Diplomado.Size = new System.Drawing.Size(285, 26);
            this.txtCosto_Credito_Especialidad_Diplomado.TabIndex = 83;
            // 
            // txtCosto_Credito_Maestria
            // 
            this.txtCosto_Credito_Maestria.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCosto_Credito_Maestria.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtCosto_Credito_Maestria.Location = new System.Drawing.Point(526, 170);
            this.txtCosto_Credito_Maestria.Margin = new System.Windows.Forms.Padding(2);
            this.txtCosto_Credito_Maestria.Name = "txtCosto_Credito_Maestria";
            this.txtCosto_Credito_Maestria.Size = new System.Drawing.Size(285, 26);
            this.txtCosto_Credito_Maestria.TabIndex = 85;
            // 
            // pictureBoxHeader
            // 
            this.pictureBoxHeader.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBoxHeader.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxHeader.Image")));
            this.pictureBoxHeader.Location = new System.Drawing.Point(-572, 42);
            this.pictureBoxHeader.Name = "pictureBoxHeader";
            this.pictureBoxHeader.Size = new System.Drawing.Size(2486, 117);
            this.pictureBoxHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxHeader.TabIndex = 101;
            this.pictureBoxHeader.TabStop = false;
            // 
            // layoutVentana
            // 
            this.layoutVentana.BackColor = System.Drawing.Color.Transparent;
            this.layoutVentana.ColumnCount = 2;
            this.layoutVentana.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.layoutVentana.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutVentana.Controls.Add(this.layoutForm, 1, 1);
            this.layoutVentana.Controls.Add(this.layoutButtons, 1, 2);
            this.layoutVentana.Location = new System.Drawing.Point(0, 0);
            this.layoutVentana.Name = "layoutVentana";
            this.layoutVentana.RowCount = 3;
            this.layoutVentana.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.layoutVentana.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutVentana.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.layoutVentana.Size = new System.Drawing.Size(1200, 649);
            this.layoutVentana.TabIndex = 102;
            // 
            // FormParametrosGenerales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1212, 678);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBoxHeader);
            this.Controls.Add(this.layoutVentana);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormParametrosGenerales";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainAlumnos";
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.layoutButtons.ResumeLayout(false);
            this.layoutForm.ResumeLayout(false);
            this.layoutForm.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeader)).EndInit();
            this.layoutVentana.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.TableLayoutPanel layoutButtons;
        private System.Windows.Forms.TableLayoutPanel layoutForm;
        private System.Windows.Forms.TextBox txtPagoTaller;
        private System.Windows.Forms.TextBox txtPagoSesion;
        private System.Windows.Forms.TextBox txtCosto_Credito_Especialidad_Diplomado;
        private System.Windows.Forms.TextBox txtCosto_Credito_Maestria;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPagoClase;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxHeader;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtUbicacion;
        private System.Windows.Forms.Button btnAgregarUbicacion;
        private System.Windows.Forms.ListBox listUbicaciones;
        private System.Windows.Forms.Button btnRemoverUbicacion;
        private System.Windows.Forms.TableLayoutPanel layoutVentana;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPorcentajeEvaluacion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDirector;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSede;
        private System.Windows.Forms.Label label9;
    }
}