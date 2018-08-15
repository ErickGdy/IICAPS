namespace IICAPS_v1.Presentacion
{
    partial class FormRegistrarPago
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegistrarPago));
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbIDAlumno = new System.Windows.Forms.ComboBox();
            this.cmbIDPrograma = new System.Windows.Forms.ComboBox();
            this.cmbAlumno = new System.Windows.Forms.ComboBox();
            this.cmbPrograma = new System.Windows.Forms.ComboBox();
            this.cmbConcepto = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbIDRecibio = new System.Windows.Forms.ComboBox();
            this.cmbRecibio = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(182, 381);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 28);
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(57, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 18);
            this.label5.TabIndex = 37;
            this.label5.Text = "Cantidad a pagar:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 18);
            this.label2.TabIndex = 35;
            this.label2.Text = "Inscrito en el Programa:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.Location = new System.Drawing.Point(182, 199);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 24);
            this.numericUpDown1.TabIndex = 3;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(314, 381);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 28);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(118, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 42;
            this.label1.Text = "Alumno:";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservaciones.Location = new System.Drawing.Point(182, 260);
            this.txtObservaciones.Margin = new System.Windows.Forms.Padding(2);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(240, 80);
            this.txtObservaciones.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(68, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 18);
            this.label4.TabIndex = 61;
            this.label4.Text = "Observaciones:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(-4, 414);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(570, 102);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 63;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-4, -4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(570, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 62;
            this.pictureBox1.TabStop = false;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(173)))), ((int)(((byte)(73)))));
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.Color.White;
            this.lblFecha.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFecha.Location = new System.Drawing.Point(459, 7);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(99, 20);
            this.lblFecha.TabIndex = 65;
            this.lblFecha.Text = "10/12/2010";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(173)))), ((int)(((byte)(73)))));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(394, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 20);
            this.label6.TabIndex = 64;
            this.label6.Text = "Fecha:";
            // 
            // cmbIDAlumno
            // 
            this.cmbIDAlumno.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbIDAlumno.FormattingEnabled = true;
            this.cmbIDAlumno.Location = new System.Drawing.Point(521, 167);
            this.cmbIDAlumno.Margin = new System.Windows.Forms.Padding(2);
            this.cmbIDAlumno.Name = "cmbIDAlumno";
            this.cmbIDAlumno.Size = new System.Drawing.Size(35, 26);
            this.cmbIDAlumno.TabIndex = 70;
            this.cmbIDAlumno.Visible = false;
            // 
            // cmbIDPrograma
            // 
            this.cmbIDPrograma.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.cmbIDPrograma.FormattingEnabled = true;
            this.cmbIDPrograma.Location = new System.Drawing.Point(522, 136);
            this.cmbIDPrograma.Margin = new System.Windows.Forms.Padding(2);
            this.cmbIDPrograma.Name = "cmbIDPrograma";
            this.cmbIDPrograma.Size = new System.Drawing.Size(35, 26);
            this.cmbIDPrograma.TabIndex = 69;
            this.cmbIDPrograma.Visible = false;
            // 
            // cmbAlumno
            // 
            this.cmbAlumno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlumno.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAlumno.FormattingEnabled = true;
            this.cmbAlumno.Location = new System.Drawing.Point(182, 167);
            this.cmbAlumno.Name = "cmbAlumno";
            this.cmbAlumno.Size = new System.Drawing.Size(335, 26);
            this.cmbAlumno.TabIndex = 68;
            // 
            // cmbPrograma
            // 
            this.cmbPrograma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrograma.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPrograma.FormattingEnabled = true;
            this.cmbPrograma.Location = new System.Drawing.Point(182, 136);
            this.cmbPrograma.Name = "cmbPrograma";
            this.cmbPrograma.Size = new System.Drawing.Size(335, 26);
            this.cmbPrograma.TabIndex = 67;
            this.cmbPrograma.SelectedIndexChanged += new System.EventHandler(this.cmbPrograma_SelectedIndexChanged);
            // 
            // cmbConcepto
            // 
            this.cmbConcepto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConcepto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbConcepto.FormattingEnabled = true;
            this.cmbConcepto.Location = new System.Drawing.Point(182, 229);
            this.cmbConcepto.Name = "cmbConcepto";
            this.cmbConcepto.Size = new System.Drawing.Size(335, 26);
            this.cmbConcepto.TabIndex = 71;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(103, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 18);
            this.label3.TabIndex = 72;
            this.label3.Text = "Concepto:";
            // 
            // cmbIDRecibio
            // 
            this.cmbIDRecibio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbIDRecibio.FormattingEnabled = true;
            this.cmbIDRecibio.Location = new System.Drawing.Point(523, 349);
            this.cmbIDRecibio.Margin = new System.Windows.Forms.Padding(2);
            this.cmbIDRecibio.Name = "cmbIDRecibio";
            this.cmbIDRecibio.Size = new System.Drawing.Size(34, 26);
            this.cmbIDRecibio.TabIndex = 75;
            this.cmbIDRecibio.Visible = false;
            // 
            // cmbRecibio
            // 
            this.cmbRecibio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecibio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRecibio.FormattingEnabled = true;
            this.cmbRecibio.Location = new System.Drawing.Point(182, 349);
            this.cmbRecibio.Name = "cmbRecibio";
            this.cmbRecibio.Size = new System.Drawing.Size(335, 26);
            this.cmbRecibio.TabIndex = 73;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(118, 352);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 18);
            this.label7.TabIndex = 74;
            this.label7.Text = "Recibió:";
            // 
            // FormRegistrarPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(567, 510);
            this.Controls.Add(this.cmbIDRecibio);
            this.Controls.Add(this.cmbRecibio);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbConcepto);
            this.Controls.Add(this.cmbIDAlumno);
            this.Controls.Add(this.cmbIDPrograma);
            this.Controls.Add(this.cmbAlumno);
            this.Controls.Add(this.cmbPrograma);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.txtObservaciones);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FormRegistrarPago";
            this.Text = "Registrar Pago";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbIDAlumno;
        private System.Windows.Forms.ComboBox cmbIDPrograma;
        private System.Windows.Forms.ComboBox cmbAlumno;
        private System.Windows.Forms.ComboBox cmbPrograma;
        private System.Windows.Forms.ComboBox cmbConcepto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbIDRecibio;
        private System.Windows.Forms.ComboBox cmbRecibio;
        private System.Windows.Forms.Label label7;
    }
}