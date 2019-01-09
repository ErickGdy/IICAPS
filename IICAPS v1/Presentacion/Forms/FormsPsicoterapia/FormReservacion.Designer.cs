namespace IICAPS_v1.Presentacion
{
    partial class FormReservacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReservacion));
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHoras = new System.Windows.Forms.NumericUpDown();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbConcepto = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbReservante = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCodigo_Reservacion = new System.Windows.Forms.Label();
            this.lblReservacionT = new System.Windows.Forms.Label();
            this.datePicker_Fecha = new System.Windows.Forms.DateTimePicker();
            this.datePicker_Hora = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMinutos = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbUbicacion = new System.Windows.Forms.ComboBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAgenda = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.btnDisponible = new System.Windows.Forms.Button();
            this.btnOcupado = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinutos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(186, 445);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(107, 38);
            this.btnAceptar.TabIndex = 9;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(69, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 20);
            this.label5.TabIndex = 37;
            this.label5.Text = "Horas:";
            // 
            // txtHoras
            // 
            this.txtHoras.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtHoras.Location = new System.Drawing.Point(133, 16);
            this.txtHoras.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.txtHoras.Name = "txtHoras";
            this.txtHoras.Size = new System.Drawing.Size(59, 26);
            this.txtHoras.TabIndex = 3;
            this.txtHoras.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtHoras.ValueChanged += new System.EventHandler(this.datePicker_Hora_ValueChanged);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(322, 445);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(107, 38);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(-1, 488);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(635, 115);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 63;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-16, -10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(650, 135);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 62;
            this.pictureBox1.TabStop = false;
            // 
            // cmbConcepto
            // 
            this.cmbConcepto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConcepto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmbConcepto.FormattingEnabled = true;
            this.cmbConcepto.Location = new System.Drawing.Point(155, 286);
            this.cmbConcepto.Name = "cmbConcepto";
            this.cmbConcepto.Size = new System.Drawing.Size(311, 28);
            this.cmbConcepto.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(60, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 72;
            this.label3.Text = "Concepto:";
            // 
            // cmbReservante
            // 
            this.cmbReservante.BackColor = System.Drawing.SystemColors.Window;
            this.cmbReservante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReservante.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmbReservante.FormattingEnabled = true;
            this.cmbReservante.Location = new System.Drawing.Point(155, 334);
            this.cmbReservante.Name = "cmbReservante";
            this.cmbReservante.Size = new System.Drawing.Size(371, 28);
            this.cmbReservante.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(45, 337);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 20);
            this.label7.TabIndex = 74;
            this.label7.Text = "Reservante:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(85, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 79;
            this.label1.Text = "Fecha:";
            // 
            // lblCodigo_Reservacion
            // 
            this.lblCodigo_Reservacion.AutoSize = true;
            this.lblCodigo_Reservacion.BackColor = System.Drawing.Color.White;
            this.lblCodigo_Reservacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigo_Reservacion.ForeColor = System.Drawing.Color.Black;
            this.lblCodigo_Reservacion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCodigo_Reservacion.Location = new System.Drawing.Point(501, 125);
            this.lblCodigo_Reservacion.Name = "lblCodigo_Reservacion";
            this.lblCodigo_Reservacion.Size = new System.Drawing.Size(117, 20);
            this.lblCodigo_Reservacion.TabIndex = 77;
            this.lblCodigo_Reservacion.Text = "XXXXXXXXX";
            // 
            // lblReservacionT
            // 
            this.lblReservacionT.AutoSize = true;
            this.lblReservacionT.BackColor = System.Drawing.Color.White;
            this.lblReservacionT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReservacionT.ForeColor = System.Drawing.Color.Black;
            this.lblReservacionT.Location = new System.Drawing.Point(451, 125);
            this.lblReservacionT.Name = "lblReservacionT";
            this.lblReservacionT.Size = new System.Drawing.Size(53, 20);
            this.lblReservacionT.TabIndex = 76;
            this.lblReservacionT.Text = "Folio:";
            // 
            // datePicker_Fecha
            // 
            this.datePicker_Fecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.datePicker_Fecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePicker_Fecha.Location = new System.Drawing.Point(156, 152);
            this.datePicker_Fecha.Name = "datePicker_Fecha";
            this.datePicker_Fecha.Size = new System.Drawing.Size(106, 26);
            this.datePicker_Fecha.TabIndex = 1;
            this.datePicker_Fecha.ValueChanged += new System.EventHandler(this.datePicker_Fecha_ValueChanged);
            // 
            // datePicker_Hora
            // 
            this.datePicker_Hora.CustomFormat = "HH:mm \'hrs\'";
            this.datePicker_Hora.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.datePicker_Hora.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePicker_Hora.Location = new System.Drawing.Point(345, 152);
            this.datePicker_Hora.Name = "datePicker_Hora";
            this.datePicker_Hora.ShowUpDown = true;
            this.datePicker_Hora.Size = new System.Drawing.Size(119, 26);
            this.datePicker_Hora.TabIndex = 2;
            this.datePicker_Hora.ValueChanged += new System.EventHandler(this.datePicker_Hora_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(286, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 20);
            this.label2.TabIndex = 81;
            this.label2.Text = "Hora:";
            // 
            // txtMinutos
            // 
            this.txtMinutos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtMinutos.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.txtMinutos.Location = new System.Drawing.Point(281, 18);
            this.txtMinutos.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.txtMinutos.Name = "txtMinutos";
            this.txtMinutos.Size = new System.Drawing.Size(59, 26);
            this.txtMinutos.TabIndex = 4;
            this.txtMinutos.ValueChanged += new System.EventHandler(this.datePicker_Hora_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(198, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 20);
            this.label8.TabIndex = 84;
            this.label8.Text = "Minutos:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtMinutos);
            this.groupBox1.Controls.Add(this.txtHoras);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.groupBox1.Location = new System.Drawing.Point(80, 185);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(384, 54);
            this.groupBox1.TabIndex = 85;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tiempo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(58, 249);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 20);
            this.label4.TabIndex = 87;
            this.label4.Text = "Ubicación:";
            // 
            // cmbUbicacion
            // 
            this.cmbUbicacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUbicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmbUbicacion.FormattingEnabled = true;
            this.cmbUbicacion.Location = new System.Drawing.Point(155, 246);
            this.cmbUbicacion.Name = "cmbUbicacion";
            this.cmbUbicacion.Size = new System.Drawing.Size(311, 28);
            this.cmbUbicacion.TabIndex = 5;
            this.cmbUbicacion.SelectedIndexChanged += new System.EventHandler(this.cmbUbicacion_SelectedIndexChanged);
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(56)))), ((int)(((byte)(61)))));
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblFecha.ForeColor = System.Drawing.Color.White;
            this.lblFecha.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFecha.Location = new System.Drawing.Point(527, 9);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(90, 17);
            this.lblFecha.TabIndex = 65;
            this.lblFecha.Text = "10/12/2010";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(56)))), ((int)(((byte)(61)))));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(469, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 17);
            this.label6.TabIndex = 64;
            this.label6.Text = "Fecha:";
            // 
            // btnAgenda
            // 
            this.btnAgenda.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAgenda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnAgenda.Image = ((System.Drawing.Image)(resources.GetObject("btnAgenda.Image")));
            this.btnAgenda.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgenda.Location = new System.Drawing.Point(473, 153);
            this.btnAgenda.Name = "btnAgenda";
            this.btnAgenda.Size = new System.Drawing.Size(145, 28);
            this.btnAgenda.TabIndex = 11;
            this.btnAgenda.Text = "Consultar Agenda";
            this.btnAgenda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgenda.UseVisualStyleBackColor = false;
            this.btnAgenda.Click += new System.EventHandler(this.btnAgenda_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(19, 378);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 20);
            this.label9.TabIndex = 89;
            this.label9.Text = "Observaciones:";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtObservaciones.Location = new System.Drawing.Point(155, 374);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(374, 60);
            this.txtObservaciones.TabIndex = 8;
            // 
            // btnDisponible
            // 
            this.btnDisponible.BackColor = System.Drawing.Color.White;
            this.btnDisponible.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisponible.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnDisponible.ForeColor = System.Drawing.Color.LimeGreen;
            this.btnDisponible.Image = ((System.Drawing.Image)(resources.GetObject("btnDisponible.Image")));
            this.btnDisponible.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisponible.Location = new System.Drawing.Point(472, 246);
            this.btnDisponible.Name = "btnDisponible";
            this.btnDisponible.Size = new System.Drawing.Size(102, 28);
            this.btnDisponible.TabIndex = 91;
            this.btnDisponible.Text = "Disponible";
            this.btnDisponible.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDisponible.UseVisualStyleBackColor = false;
            // 
            // btnOcupado
            // 
            this.btnOcupado.BackColor = System.Drawing.Color.White;
            this.btnOcupado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOcupado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnOcupado.ForeColor = System.Drawing.Color.Red;
            this.btnOcupado.Image = ((System.Drawing.Image)(resources.GetObject("btnOcupado.Image")));
            this.btnOcupado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOcupado.Location = new System.Drawing.Point(472, 246);
            this.btnOcupado.Name = "btnOcupado";
            this.btnOcupado.Size = new System.Drawing.Size(102, 28);
            this.btnOcupado.TabIndex = 92;
            this.btnOcupado.Text = "Ocupado";
            this.btnOcupado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOcupado.UseVisualStyleBackColor = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label10.Location = new System.Drawing.Point(153, 318);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(193, 13);
            this.label10.TabIndex = 93;
            this.label10.Text = "(\"P:\" Psicoterapeutas, \"E:\" Empleados)";
            // 
            // FormReservacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(629, 597);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtObservaciones);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnAgenda);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbUbicacion);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.datePicker_Hora);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.datePicker_Fecha);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCodigo_Reservacion);
            this.Controls.Add(this.lblReservacionT);
            this.Controls.Add(this.cmbReservante);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbConcepto);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnDisponible);
            this.Controls.Add(this.btnOcupado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormReservacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registrar Reservación";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormReservacion_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.txtHoras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinutos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown txtHoras;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ComboBox cmbConcepto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbReservante;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCodigo_Reservacion;
        private System.Windows.Forms.Label lblReservacionT;
        private System.Windows.Forms.DateTimePicker datePicker_Fecha;
        private System.Windows.Forms.DateTimePicker datePicker_Hora;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtMinutos;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbUbicacion;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAgenda;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Button btnDisponible;
        private System.Windows.Forms.Button btnOcupado;
        private System.Windows.Forms.Label label10;
    }
}