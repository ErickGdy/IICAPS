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
            this.pictureBox1000 = new System.Windows.Forms.PictureBox();
            this.pictureBox1200 = new System.Windows.Forms.PictureBox();
            this.pictureBox1500 = new System.Windows.Forms.PictureBox();
            this.pictureBox2000 = new System.Windows.Forms.PictureBox();
            this.layoutVentana = new System.Windows.Forms.TableLayoutPanel();
            this.layoutButtons.SuspendLayout();
            this.layoutForm.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1000)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1200)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1500)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2000)).BeginInit();
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
            this.layoutButtons.Location = new System.Drawing.Point(189, 502);
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
            this.layoutForm.Controls.Add(this.groupBox1, 0, 6);
            this.layoutForm.Controls.Add(this.label6, 0, 5);
            this.layoutForm.Controls.Add(this.txtPagoClase, 1, 5);
            this.layoutForm.Controls.Add(this.label5, 0, 4);
            this.layoutForm.Controls.Add(this.label4, 0, 3);
            this.layoutForm.Controls.Add(this.label3, 0, 2);
            this.layoutForm.Controls.Add(this.label2, 0, 1);
            this.layoutForm.Controls.Add(this.txtPagoTaller, 1, 4);
            this.layoutForm.Controls.Add(this.txtPagoSesion, 1, 3);
            this.layoutForm.Controls.Add(this.txtCosto_Credito_Especialidad_Diplomado, 1, 1);
            this.layoutForm.Controls.Add(this.txtCosto_Credito_Maestria, 1, 2);
            this.layoutForm.Controls.Add(this.pictureBox1000, 0, 0);
            this.layoutForm.Location = new System.Drawing.Point(148, 48);
            this.layoutForm.Name = "layoutForm";
            this.layoutForm.RowCount = 7;
            this.layoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.layoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.layoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.layoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.layoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.layoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.layoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutForm.Size = new System.Drawing.Size(1049, 448);
            this.layoutForm.TabIndex = 42;
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
            this.groupBox1.Location = new System.Drawing.Point(3, 357);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1043, 88);
            this.groupBox1.TabIndex = 102;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(280, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 98;
            this.label1.Text = "Ubicación:";
            // 
            // txtUbicacion
            // 
            this.txtUbicacion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUbicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUbicacion.Location = new System.Drawing.Point(193, 39);
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
            this.btnAgregarUbicacion.Location = new System.Drawing.Point(487, 14);
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
            this.listUbicaciones.Location = new System.Drawing.Point(533, 14);
            this.listUbicaciones.Name = "listUbicaciones";
            this.listUbicaciones.Size = new System.Drawing.Size(294, 69);
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
            this.btnRemoverUbicacion.Location = new System.Drawing.Point(487, 53);
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
            this.label6.Location = new System.Drawing.Point(269, 322);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(252, 20);
            this.label6.TabIndex = 97;
            this.label6.Text = "Porcentaje de Pago por Clase:";
            // 
            // txtPagoClase
            // 
            this.txtPagoClase.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPagoClase.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtPagoClase.Location = new System.Drawing.Point(526, 319);
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
            this.label5.Location = new System.Drawing.Point(270, 278);
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
            this.label4.Location = new System.Drawing.Point(259, 234);
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
            this.label3.Location = new System.Drawing.Point(257, 190);
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
            this.label2.Location = new System.Drawing.Point(139, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(382, 20);
            this.label2.TabIndex = 92;
            this.label2.Text = "Costo de Crédito para Especialidad/Diplomado";
            // 
            // txtPagoTaller
            // 
            this.txtPagoTaller.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPagoTaller.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtPagoTaller.Location = new System.Drawing.Point(526, 275);
            this.txtPagoTaller.Margin = new System.Windows.Forms.Padding(2);
            this.txtPagoTaller.Name = "txtPagoTaller";
            this.txtPagoTaller.Size = new System.Drawing.Size(285, 26);
            this.txtPagoTaller.TabIndex = 91;
            // 
            // txtPagoSesion
            // 
            this.txtPagoSesion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPagoSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtPagoSesion.Location = new System.Drawing.Point(526, 231);
            this.txtPagoSesion.Margin = new System.Windows.Forms.Padding(2);
            this.txtPagoSesion.Name = "txtPagoSesion";
            this.txtPagoSesion.Size = new System.Drawing.Size(285, 26);
            this.txtPagoSesion.TabIndex = 89;
            // 
            // txtCosto_Credito_Especialidad_Diplomado
            // 
            this.txtCosto_Credito_Especialidad_Diplomado.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCosto_Credito_Especialidad_Diplomado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtCosto_Credito_Especialidad_Diplomado.Location = new System.Drawing.Point(526, 143);
            this.txtCosto_Credito_Especialidad_Diplomado.Margin = new System.Windows.Forms.Padding(2);
            this.txtCosto_Credito_Especialidad_Diplomado.Name = "txtCosto_Credito_Especialidad_Diplomado";
            this.txtCosto_Credito_Especialidad_Diplomado.Size = new System.Drawing.Size(285, 26);
            this.txtCosto_Credito_Especialidad_Diplomado.TabIndex = 83;
            // 
            // txtCosto_Credito_Maestria
            // 
            this.txtCosto_Credito_Maestria.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCosto_Credito_Maestria.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtCosto_Credito_Maestria.Location = new System.Drawing.Point(526, 187);
            this.txtCosto_Credito_Maestria.Margin = new System.Windows.Forms.Padding(2);
            this.txtCosto_Credito_Maestria.Name = "txtCosto_Credito_Maestria";
            this.txtCosto_Credito_Maestria.Size = new System.Drawing.Size(285, 26);
            this.txtCosto_Credito_Maestria.TabIndex = 85;
            // 
            // pictureBox1000
            // 
            this.pictureBox1000.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.layoutForm.SetColumnSpan(this.pictureBox1000, 12);
            this.pictureBox1000.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1000.Image")));
            this.pictureBox1000.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1000.Name = "pictureBox1000";
            this.pictureBox1000.Size = new System.Drawing.Size(1043, 125);
            this.pictureBox1000.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1000.TabIndex = 98;
            this.pictureBox1000.TabStop = false;
            this.pictureBox1000.Visible = false;
            // 
            // pictureBox1200
            // 
            this.pictureBox1200.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1200.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1200.Image")));
            this.pictureBox1200.Location = new System.Drawing.Point(239, 568);
            this.pictureBox1200.Name = "pictureBox1200";
            this.pictureBox1200.Size = new System.Drawing.Size(166, 14);
            this.pictureBox1200.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1200.TabIndex = 99;
            this.pictureBox1200.TabStop = false;
            this.pictureBox1200.Visible = false;
            // 
            // pictureBox1500
            // 
            this.pictureBox1500.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1500.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1500.Image")));
            this.pictureBox1500.Location = new System.Drawing.Point(165, 568);
            this.pictureBox1500.Name = "pictureBox1500";
            this.pictureBox1500.Size = new System.Drawing.Size(57, 10);
            this.pictureBox1500.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1500.TabIndex = 100;
            this.pictureBox1500.TabStop = false;
            this.pictureBox1500.Visible = false;
            // 
            // pictureBox2000
            // 
            this.pictureBox2000.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox2000.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2000.Image")));
            this.pictureBox2000.Location = new System.Drawing.Point(12, 564);
            this.pictureBox2000.Name = "pictureBox2000";
            this.pictureBox2000.Size = new System.Drawing.Size(147, 22);
            this.pictureBox2000.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2000.TabIndex = 101;
            this.pictureBox2000.TabStop = false;
            this.pictureBox2000.Visible = false;
            // 
            // layoutVentana
            // 
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
            this.layoutVentana.Size = new System.Drawing.Size(1200, 544);
            this.layoutVentana.TabIndex = 102;
            // 
            // FormParametrosGenerales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1212, 598);
            this.ControlBox = false;
            this.Controls.Add(this.layoutVentana);
            this.Controls.Add(this.pictureBox2000);
            this.Controls.Add(this.pictureBox1500);
            this.Controls.Add(this.pictureBox1200);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1000)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1200)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1500)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2000)).EndInit();
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
        private System.Windows.Forms.PictureBox pictureBox1000;
        private System.Windows.Forms.PictureBox pictureBox1200;
        private System.Windows.Forms.PictureBox pictureBox1500;
        private System.Windows.Forms.PictureBox pictureBox2000;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtUbicacion;
        private System.Windows.Forms.Button btnAgregarUbicacion;
        private System.Windows.Forms.ListBox listUbicaciones;
        private System.Windows.Forms.Button btnRemoverUbicacion;
        private System.Windows.Forms.TableLayoutPanel layoutVentana;
        private System.Windows.Forms.Label label1;
    }
}