﻿namespace IICAPS.Presentacion.Mains
{
    partial class Principal
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
            Login lg = new Login();
            lg.Show();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.panelMenuSuperior = new System.Windows.Forms.Panel();
            this.btnMenuPsicoterapia = new System.Windows.Forms.Button();
            this.btnMenuPacientes = new System.Windows.Forms.Button();
            this.btnMenuAdministracion = new System.Windows.Forms.Button();
            this.btnMenuLibreria = new System.Windows.Forms.Button();
            this.btnMenuMaestros = new System.Windows.Forms.Button();
            this.btnMenuEscuela = new System.Windows.Forms.Button();
            this.btnIndex = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.btnUsuario = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCambiarPass = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCerrarSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMenuEscuela = new System.Windows.Forms.Panel();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnMateriasEscuela = new System.Windows.Forms.Button();
            this.logoDisgrawMenuEscuela = new System.Windows.Forms.PictureBox();
            this.btnDocumentacionAlumno = new System.Windows.Forms.Button();
            this.btnCreditoAlumno = new System.Windows.Forms.Button();
            this.btnPagosAlumno = new System.Windows.Forms.Button();
            this.btnRegistroAlumno = new System.Windows.Forms.Button();
            this.btnTalleres = new System.Windows.Forms.Button();
            this.btnGruposEscuela = new System.Windows.Forms.Button();
            this.btnProgramasEscuela = new System.Windows.Forms.Button();
            this.btnAlumno = new System.Windows.Forms.Button();
            this.btnEscuela = new System.Windows.Forms.Button();
            this.panelMenuPsicoterapia = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnPsicoterapeutasPsicoterapia = new System.Windows.Forms.Button();
            this.logoDisgrawMenuPsicoterapia = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnPagosPacientePsicoterapia = new System.Windows.Forms.Button();
            this.btnRegistroPacientePsicoterapia = new System.Windows.Forms.Button();
            this.btnNominaPsicoterapia = new System.Windows.Forms.Button();
            this.btnClubDeTareasPsicoterapia = new System.Windows.Forms.Button();
            this.btnAgendaPsicoterapia = new System.Windows.Forms.Button();
            this.btnPacientesPsicoterapia = new System.Windows.Forms.Button();
            this.btnPsicoterapia = new System.Windows.Forms.Button();
            this.panelMenuSuperior.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.panelMenuEscuela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoDisgrawMenuEscuela)).BeginInit();
            this.panelMenuPsicoterapia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoDisgrawMenuPsicoterapia)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenuSuperior
            // 
            this.panelMenuSuperior.BackColor = System.Drawing.Color.White;
            this.panelMenuSuperior.Controls.Add(this.btnMenuPsicoterapia);
            this.panelMenuSuperior.Controls.Add(this.btnMenuPacientes);
            this.panelMenuSuperior.Controls.Add(this.btnMenuAdministracion);
            this.panelMenuSuperior.Controls.Add(this.btnMenuLibreria);
            this.panelMenuSuperior.Controls.Add(this.btnMenuMaestros);
            this.panelMenuSuperior.Controls.Add(this.btnMenuEscuela);
            this.panelMenuSuperior.Controls.Add(this.btnIndex);
            this.panelMenuSuperior.Controls.Add(this.menuStrip);
            this.panelMenuSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelMenuSuperior.Margin = new System.Windows.Forms.Padding(4);
            this.panelMenuSuperior.Name = "panelMenuSuperior";
            this.panelMenuSuperior.Size = new System.Drawing.Size(1076, 45);
            this.panelMenuSuperior.TabIndex = 0;
            // 
            // btnMenuPsicoterapia
            // 
            this.btnMenuPsicoterapia.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnMenuPsicoterapia.FlatAppearance.BorderSize = 0;
            this.btnMenuPsicoterapia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuPsicoterapia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuPsicoterapia.Image = ((System.Drawing.Image)(resources.GetObject("btnMenuPsicoterapia.Image")));
            this.btnMenuPsicoterapia.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuPsicoterapia.Location = new System.Drawing.Point(395, 0);
            this.btnMenuPsicoterapia.Margin = new System.Windows.Forms.Padding(4);
            this.btnMenuPsicoterapia.Name = "btnMenuPsicoterapia";
            this.btnMenuPsicoterapia.Size = new System.Drawing.Size(139, 45);
            this.btnMenuPsicoterapia.TabIndex = 10;
            this.btnMenuPsicoterapia.Text = "Psicoterapia";
            this.btnMenuPsicoterapia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMenuPsicoterapia.UseVisualStyleBackColor = false;
            this.btnMenuPsicoterapia.Click += new System.EventHandler(this.btnMenuPsicoterapia_Click);
            // 
            // btnMenuPacientes
            // 
            this.btnMenuPacientes.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnMenuPacientes.FlatAppearance.BorderSize = 0;
            this.btnMenuPacientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuPacientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuPacientes.ForeColor = System.Drawing.Color.Black;
            this.btnMenuPacientes.Image = ((System.Drawing.Image)(resources.GetObject("btnMenuPacientes.Image")));
            this.btnMenuPacientes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuPacientes.Location = new System.Drawing.Point(532, 2);
            this.btnMenuPacientes.Margin = new System.Windows.Forms.Padding(4);
            this.btnMenuPacientes.Name = "btnMenuPacientes";
            this.btnMenuPacientes.Size = new System.Drawing.Size(129, 44);
            this.btnMenuPacientes.TabIndex = 9;
            this.btnMenuPacientes.Text = "Pacientes";
            this.btnMenuPacientes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMenuPacientes.UseVisualStyleBackColor = false;
            this.btnMenuPacientes.Click += new System.EventHandler(this.btnMenuPacientes_Click);
            // 
            // btnMenuAdministracion
            // 
            this.btnMenuAdministracion.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnMenuAdministracion.FlatAppearance.BorderSize = 0;
            this.btnMenuAdministracion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuAdministracion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuAdministracion.Image = ((System.Drawing.Image)(resources.GetObject("btnMenuAdministracion.Image")));
            this.btnMenuAdministracion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuAdministracion.Location = new System.Drawing.Point(773, 3);
            this.btnMenuAdministracion.Margin = new System.Windows.Forms.Padding(4);
            this.btnMenuAdministracion.Name = "btnMenuAdministracion";
            this.btnMenuAdministracion.Size = new System.Drawing.Size(157, 43);
            this.btnMenuAdministracion.TabIndex = 7;
            this.btnMenuAdministracion.Text = "Administración";
            this.btnMenuAdministracion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMenuAdministracion.UseVisualStyleBackColor = false;
            // 
            // btnMenuLibreria
            // 
            this.btnMenuLibreria.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnMenuLibreria.FlatAppearance.BorderSize = 0;
            this.btnMenuLibreria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuLibreria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuLibreria.Image = ((System.Drawing.Image)(resources.GetObject("btnMenuLibreria.Image")));
            this.btnMenuLibreria.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuLibreria.Location = new System.Drawing.Point(663, 2);
            this.btnMenuLibreria.Margin = new System.Windows.Forms.Padding(4);
            this.btnMenuLibreria.Name = "btnMenuLibreria";
            this.btnMenuLibreria.Size = new System.Drawing.Size(108, 44);
            this.btnMenuLibreria.TabIndex = 6;
            this.btnMenuLibreria.Text = "Librería";
            this.btnMenuLibreria.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMenuLibreria.UseVisualStyleBackColor = false;
            // 
            // btnMenuMaestros
            // 
            this.btnMenuMaestros.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnMenuMaestros.FlatAppearance.BorderSize = 0;
            this.btnMenuMaestros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuMaestros.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuMaestros.Image = ((System.Drawing.Image)(resources.GetObject("btnMenuMaestros.Image")));
            this.btnMenuMaestros.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuMaestros.Location = new System.Drawing.Point(281, 1);
            this.btnMenuMaestros.Margin = new System.Windows.Forms.Padding(4);
            this.btnMenuMaestros.Name = "btnMenuMaestros";
            this.btnMenuMaestros.Size = new System.Drawing.Size(112, 45);
            this.btnMenuMaestros.TabIndex = 5;
            this.btnMenuMaestros.Text = "Maestros";
            this.btnMenuMaestros.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMenuMaestros.UseVisualStyleBackColor = false;
            this.btnMenuMaestros.Click += new System.EventHandler(this.btnMenuMaestros_Click);
            // 
            // btnMenuEscuela
            // 
            this.btnMenuEscuela.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnMenuEscuela.FlatAppearance.BorderSize = 0;
            this.btnMenuEscuela.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuEscuela.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuEscuela.ForeColor = System.Drawing.Color.Black;
            this.btnMenuEscuela.Image = ((System.Drawing.Image)(resources.GetObject("btnMenuEscuela.Image")));
            this.btnMenuEscuela.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuEscuela.Location = new System.Drawing.Point(176, 1);
            this.btnMenuEscuela.Margin = new System.Windows.Forms.Padding(4);
            this.btnMenuEscuela.Name = "btnMenuEscuela";
            this.btnMenuEscuela.Size = new System.Drawing.Size(105, 44);
            this.btnMenuEscuela.TabIndex = 2;
            this.btnMenuEscuela.Text = "Escuela";
            this.btnMenuEscuela.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMenuEscuela.UseVisualStyleBackColor = false;
            this.btnMenuEscuela.Click += new System.EventHandler(this.btnMenuEscuela_Click);
            // 
            // btnIndex
            // 
            this.btnIndex.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnIndex.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnIndex.BackgroundImage")));
            this.btnIndex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnIndex.FlatAppearance.BorderSize = 0;
            this.btnIndex.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIndex.Location = new System.Drawing.Point(0, 0);
            this.btnIndex.Margin = new System.Windows.Forms.Padding(4);
            this.btnIndex.Name = "btnIndex";
            this.btnIndex.Size = new System.Drawing.Size(160, 45);
            this.btnIndex.TabIndex = 0;
            this.btnIndex.UseVisualStyleBackColor = false;
            this.btnIndex.Click += new System.EventHandler(this.btnIndex_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.AutoSize = false;
            this.menuStrip.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnUsuario});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1076, 45);
            this.menuStrip.TabIndex = 8;
            this.menuStrip.Text = "menuStrip1";
            // 
            // btnUsuario
            // 
            this.btnUsuario.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnUsuario.AutoSize = false;
            this.btnUsuario.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCambiarPass,
            this.toolStripSeparator1,
            this.btnCerrarSesion});
            this.btnUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsuario.Image = ((System.Drawing.Image)(resources.GetObject("btnUsuario.Image")));
            this.btnUsuario.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnUsuario.Name = "btnUsuario";
            this.btnUsuario.Size = new System.Drawing.Size(94, 45);
            this.btnUsuario.Text = "Usuario";
            // 
            // btnCambiarPass
            // 
            this.btnCambiarPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCambiarPass.Image = ((System.Drawing.Image)(resources.GetObject("btnCambiarPass.Image")));
            this.btnCambiarPass.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCambiarPass.Name = "btnCambiarPass";
            this.btnCambiarPass.Size = new System.Drawing.Size(233, 38);
            this.btnCambiarPass.Text = "Cambiar Contraseña";
            this.btnCambiarPass.Click += new System.EventHandler(this.cambiarContraseñaToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(230, 6);
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarSesion.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrarSesion.Image")));
            this.btnCerrarSesion.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(233, 38);
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.Click += new System.EventHandler(this.cerrarSesiónToolStripMenuItem_Click);
            // 
            // panelMenuEscuela
            // 
            this.panelMenuEscuela.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(173)))), ((int)(((byte)(73)))));
            this.panelMenuEscuela.Controls.Add(this.btnImprimir);
            this.panelMenuEscuela.Controls.Add(this.btnMateriasEscuela);
            this.panelMenuEscuela.Controls.Add(this.logoDisgrawMenuEscuela);
            this.panelMenuEscuela.Controls.Add(this.btnDocumentacionAlumno);
            this.panelMenuEscuela.Controls.Add(this.btnCreditoAlumno);
            this.panelMenuEscuela.Controls.Add(this.btnPagosAlumno);
            this.panelMenuEscuela.Controls.Add(this.btnRegistroAlumno);
            this.panelMenuEscuela.Controls.Add(this.btnTalleres);
            this.panelMenuEscuela.Controls.Add(this.btnGruposEscuela);
            this.panelMenuEscuela.Controls.Add(this.btnProgramasEscuela);
            this.panelMenuEscuela.Controls.Add(this.btnAlumno);
            this.panelMenuEscuela.Controls.Add(this.btnEscuela);
            this.panelMenuEscuela.Location = new System.Drawing.Point(2, 44);
            this.panelMenuEscuela.Margin = new System.Windows.Forms.Padding(4);
            this.panelMenuEscuela.Name = "panelMenuEscuela";
            this.panelMenuEscuela.Size = new System.Drawing.Size(157, 491);
            this.panelMenuEscuela.TabIndex = 42;
            this.panelMenuEscuela.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.Location = new System.Drawing.Point(0, 368);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(4);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(156, 32);
            this.btnImprimir.TabIndex = 12;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnMateriasEscuela
            // 
            this.btnMateriasEscuela.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMateriasEscuela.FlatAppearance.BorderSize = 0;
            this.btnMateriasEscuela.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMateriasEscuela.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMateriasEscuela.ForeColor = System.Drawing.Color.White;
            this.btnMateriasEscuela.Location = new System.Drawing.Point(0, 95);
            this.btnMateriasEscuela.Name = "btnMateriasEscuela";
            this.btnMateriasEscuela.Size = new System.Drawing.Size(156, 32);
            this.btnMateriasEscuela.TabIndex = 11;
            this.btnMateriasEscuela.Text = "Materias";
            this.btnMateriasEscuela.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMateriasEscuela.UseVisualStyleBackColor = true;
            this.btnMateriasEscuela.Click += new System.EventHandler(this.btnMateriasEscuela_Click);
            // 
            // logoDisgrawMenuEscuela
            // 
            this.logoDisgrawMenuEscuela.Image = ((System.Drawing.Image)(resources.GetObject("logoDisgrawMenuEscuela.Image")));
            this.logoDisgrawMenuEscuela.Location = new System.Drawing.Point(4, 453);
            this.logoDisgrawMenuEscuela.Margin = new System.Windows.Forms.Padding(4);
            this.logoDisgrawMenuEscuela.Name = "logoDisgrawMenuEscuela";
            this.logoDisgrawMenuEscuela.Size = new System.Drawing.Size(151, 35);
            this.logoDisgrawMenuEscuela.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoDisgrawMenuEscuela.TabIndex = 10;
            this.logoDisgrawMenuEscuela.TabStop = false;
            // 
            // btnDocumentacionAlumno
            // 
            this.btnDocumentacionAlumno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDocumentacionAlumno.FlatAppearance.BorderSize = 0;
            this.btnDocumentacionAlumno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDocumentacionAlumno.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDocumentacionAlumno.ForeColor = System.Drawing.Color.White;
            this.btnDocumentacionAlumno.Location = new System.Drawing.Point(0, 337);
            this.btnDocumentacionAlumno.Margin = new System.Windows.Forms.Padding(4);
            this.btnDocumentacionAlumno.Name = "btnDocumentacionAlumno";
            this.btnDocumentacionAlumno.Size = new System.Drawing.Size(156, 32);
            this.btnDocumentacionAlumno.TabIndex = 9;
            this.btnDocumentacionAlumno.Text = "Documentación";
            this.btnDocumentacionAlumno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDocumentacionAlumno.UseVisualStyleBackColor = true;
            this.btnDocumentacionAlumno.Click += new System.EventHandler(this.btnDocumentacionAlumno_Click);
            // 
            // btnCreditoAlumno
            // 
            this.btnCreditoAlumno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCreditoAlumno.FlatAppearance.BorderSize = 0;
            this.btnCreditoAlumno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreditoAlumno.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreditoAlumno.ForeColor = System.Drawing.Color.White;
            this.btnCreditoAlumno.Location = new System.Drawing.Point(0, 305);
            this.btnCreditoAlumno.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreditoAlumno.Name = "btnCreditoAlumno";
            this.btnCreditoAlumno.Size = new System.Drawing.Size(156, 32);
            this.btnCreditoAlumno.TabIndex = 8;
            this.btnCreditoAlumno.Text = "Creditos";
            this.btnCreditoAlumno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCreditoAlumno.UseVisualStyleBackColor = true;
            this.btnCreditoAlumno.Click += new System.EventHandler(this.btnCreditoAlumno_Click);
            // 
            // btnPagosAlumno
            // 
            this.btnPagosAlumno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPagosAlumno.FlatAppearance.BorderSize = 0;
            this.btnPagosAlumno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagosAlumno.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPagosAlumno.ForeColor = System.Drawing.Color.White;
            this.btnPagosAlumno.Location = new System.Drawing.Point(0, 273);
            this.btnPagosAlumno.Margin = new System.Windows.Forms.Padding(4);
            this.btnPagosAlumno.Name = "btnPagosAlumno";
            this.btnPagosAlumno.Size = new System.Drawing.Size(156, 32);
            this.btnPagosAlumno.TabIndex = 7;
            this.btnPagosAlumno.Text = "Pagos";
            this.btnPagosAlumno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPagosAlumno.UseVisualStyleBackColor = true;
            this.btnPagosAlumno.Click += new System.EventHandler(this.btnPagosAlumno_Click);
            // 
            // btnRegistroAlumno
            // 
            this.btnRegistroAlumno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRegistroAlumno.FlatAppearance.BorderSize = 0;
            this.btnRegistroAlumno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistroAlumno.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistroAlumno.ForeColor = System.Drawing.Color.White;
            this.btnRegistroAlumno.Location = new System.Drawing.Point(0, 241);
            this.btnRegistroAlumno.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegistroAlumno.Name = "btnRegistroAlumno";
            this.btnRegistroAlumno.Size = new System.Drawing.Size(156, 32);
            this.btnRegistroAlumno.TabIndex = 6;
            this.btnRegistroAlumno.Text = "Registro";
            this.btnRegistroAlumno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegistroAlumno.UseVisualStyleBackColor = true;
            this.btnRegistroAlumno.Click += new System.EventHandler(this.btnRegistroAlumno_Click);
            // 
            // btnTalleres
            // 
            this.btnTalleres.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTalleres.FlatAppearance.BorderSize = 0;
            this.btnTalleres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTalleres.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTalleres.ForeColor = System.Drawing.Color.White;
            this.btnTalleres.Location = new System.Drawing.Point(-2, 163);
            this.btnTalleres.Name = "btnTalleres";
            this.btnTalleres.Size = new System.Drawing.Size(156, 26);
            this.btnTalleres.TabIndex = 5;
            this.btnTalleres.Text = "Talleres";
            this.btnTalleres.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTalleres.UseVisualStyleBackColor = true;
            this.btnTalleres.Click += new System.EventHandler(this.btnTalleres_Click);
            // 
            // btnGruposEscuela
            // 
            this.btnGruposEscuela.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnGruposEscuela.FlatAppearance.BorderSize = 0;
            this.btnGruposEscuela.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGruposEscuela.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGruposEscuela.ForeColor = System.Drawing.Color.White;
            this.btnGruposEscuela.Location = new System.Drawing.Point(0, 127);
            this.btnGruposEscuela.Name = "btnGruposEscuela";
            this.btnGruposEscuela.Size = new System.Drawing.Size(156, 32);
            this.btnGruposEscuela.TabIndex = 3;
            this.btnGruposEscuela.Text = "Grupos";
            this.btnGruposEscuela.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGruposEscuela.UseVisualStyleBackColor = true;
            this.btnGruposEscuela.Click += new System.EventHandler(this.btnGruposEscuela_Click);
            // 
            // btnProgramasEscuela
            // 
            this.btnProgramasEscuela.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnProgramasEscuela.FlatAppearance.BorderSize = 0;
            this.btnProgramasEscuela.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProgramasEscuela.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProgramasEscuela.ForeColor = System.Drawing.Color.White;
            this.btnProgramasEscuela.Location = new System.Drawing.Point(0, 61);
            this.btnProgramasEscuela.Name = "btnProgramasEscuela";
            this.btnProgramasEscuela.Size = new System.Drawing.Size(156, 32);
            this.btnProgramasEscuela.TabIndex = 2;
            this.btnProgramasEscuela.Text = "Programas";
            this.btnProgramasEscuela.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProgramasEscuela.UseVisualStyleBackColor = true;
            this.btnProgramasEscuela.Click += new System.EventHandler(this.btnProgramasEscuela_Click);
            // 
            // btnAlumno
            // 
            this.btnAlumno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAlumno.FlatAppearance.BorderSize = 0;
            this.btnAlumno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlumno.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.btnAlumno.ForeColor = System.Drawing.Color.White;
            this.btnAlumno.Location = new System.Drawing.Point(0, 194);
            this.btnAlumno.Name = "btnAlumno";
            this.btnAlumno.Size = new System.Drawing.Size(156, 47);
            this.btnAlumno.TabIndex = 1;
            this.btnAlumno.Text = "Alumnos";
            this.btnAlumno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAlumno.UseVisualStyleBackColor = true;
            this.btnAlumno.Click += new System.EventHandler(this.btnAlumno_Click);
            // 
            // btnEscuela
            // 
            this.btnEscuela.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEscuela.FlatAppearance.BorderSize = 0;
            this.btnEscuela.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscuela.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.btnEscuela.ForeColor = System.Drawing.Color.White;
            this.btnEscuela.Location = new System.Drawing.Point(0, 15);
            this.btnEscuela.Name = "btnEscuela";
            this.btnEscuela.Size = new System.Drawing.Size(156, 47);
            this.btnEscuela.TabIndex = 0;
            this.btnEscuela.Text = "Escuela";
            this.btnEscuela.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEscuela.UseVisualStyleBackColor = true;
            // 
            // panelMenuPsicoterapia
            // 
            this.panelMenuPsicoterapia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(56)))), ((int)(((byte)(61)))));
            this.panelMenuPsicoterapia.Controls.Add(this.button1);
            this.panelMenuPsicoterapia.Controls.Add(this.btnPsicoterapeutasPsicoterapia);
            this.panelMenuPsicoterapia.Controls.Add(this.logoDisgrawMenuPsicoterapia);
            this.panelMenuPsicoterapia.Controls.Add(this.button3);
            this.panelMenuPsicoterapia.Controls.Add(this.button4);
            this.panelMenuPsicoterapia.Controls.Add(this.btnPagosPacientePsicoterapia);
            this.panelMenuPsicoterapia.Controls.Add(this.btnRegistroPacientePsicoterapia);
            this.panelMenuPsicoterapia.Controls.Add(this.btnNominaPsicoterapia);
            this.panelMenuPsicoterapia.Controls.Add(this.btnClubDeTareasPsicoterapia);
            this.panelMenuPsicoterapia.Controls.Add(this.btnAgendaPsicoterapia);
            this.panelMenuPsicoterapia.Controls.Add(this.btnPacientesPsicoterapia);
            this.panelMenuPsicoterapia.Controls.Add(this.btnPsicoterapia);
            this.panelMenuPsicoterapia.Location = new System.Drawing.Point(2, 44);
            this.panelMenuPsicoterapia.Margin = new System.Windows.Forms.Padding(4);
            this.panelMenuPsicoterapia.Name = "panelMenuPsicoterapia";
            this.panelMenuPsicoterapia.Size = new System.Drawing.Size(157, 491);
            this.panelMenuPsicoterapia.TabIndex = 43;
            this.panelMenuPsicoterapia.Visible = false;
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(0, 368);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 32);
            this.button1.TabIndex = 12;
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnPsicoterapeutasPsicoterapia
            // 
            this.btnPsicoterapeutasPsicoterapia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPsicoterapeutasPsicoterapia.FlatAppearance.BorderSize = 0;
            this.btnPsicoterapeutasPsicoterapia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPsicoterapeutasPsicoterapia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPsicoterapeutasPsicoterapia.ForeColor = System.Drawing.Color.White;
            this.btnPsicoterapeutasPsicoterapia.Location = new System.Drawing.Point(0, 95);
            this.btnPsicoterapeutasPsicoterapia.Name = "btnPsicoterapeutasPsicoterapia";
            this.btnPsicoterapeutasPsicoterapia.Size = new System.Drawing.Size(156, 32);
            this.btnPsicoterapeutasPsicoterapia.TabIndex = 11;
            this.btnPsicoterapeutasPsicoterapia.Text = "Psicoterapeutas";
            this.btnPsicoterapeutasPsicoterapia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPsicoterapeutasPsicoterapia.UseVisualStyleBackColor = true;
            // 
            // logoDisgrawMenuPsicoterapia
            // 
            this.logoDisgrawMenuPsicoterapia.Image = ((System.Drawing.Image)(resources.GetObject("logoDisgrawMenuPsicoterapia.Image")));
            this.logoDisgrawMenuPsicoterapia.Location = new System.Drawing.Point(4, 453);
            this.logoDisgrawMenuPsicoterapia.Margin = new System.Windows.Forms.Padding(4);
            this.logoDisgrawMenuPsicoterapia.Name = "logoDisgrawMenuPsicoterapia";
            this.logoDisgrawMenuPsicoterapia.Size = new System.Drawing.Size(151, 35);
            this.logoDisgrawMenuPsicoterapia.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoDisgrawMenuPsicoterapia.TabIndex = 10;
            this.logoDisgrawMenuPsicoterapia.TabStop = false;
            // 
            // button3
            // 
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(0, 337);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(156, 32);
            this.button3.TabIndex = 9;
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(0, 305);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(156, 32);
            this.button4.TabIndex = 8;
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // btnPagosPacientePsicoterapia
            // 
            this.btnPagosPacientePsicoterapia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPagosPacientePsicoterapia.FlatAppearance.BorderSize = 0;
            this.btnPagosPacientePsicoterapia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagosPacientePsicoterapia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPagosPacientePsicoterapia.ForeColor = System.Drawing.Color.White;
            this.btnPagosPacientePsicoterapia.Location = new System.Drawing.Point(0, 273);
            this.btnPagosPacientePsicoterapia.Margin = new System.Windows.Forms.Padding(4);
            this.btnPagosPacientePsicoterapia.Name = "btnPagosPacientePsicoterapia";
            this.btnPagosPacientePsicoterapia.Size = new System.Drawing.Size(156, 32);
            this.btnPagosPacientePsicoterapia.TabIndex = 7;
            this.btnPagosPacientePsicoterapia.Text = "Pagos";
            this.btnPagosPacientePsicoterapia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPagosPacientePsicoterapia.UseVisualStyleBackColor = true;
            // 
            // btnRegistroPacientePsicoterapia
            // 
            this.btnRegistroPacientePsicoterapia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRegistroPacientePsicoterapia.FlatAppearance.BorderSize = 0;
            this.btnRegistroPacientePsicoterapia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistroPacientePsicoterapia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistroPacientePsicoterapia.ForeColor = System.Drawing.Color.White;
            this.btnRegistroPacientePsicoterapia.Location = new System.Drawing.Point(0, 241);
            this.btnRegistroPacientePsicoterapia.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegistroPacientePsicoterapia.Name = "btnRegistroPacientePsicoterapia";
            this.btnRegistroPacientePsicoterapia.Size = new System.Drawing.Size(156, 32);
            this.btnRegistroPacientePsicoterapia.TabIndex = 6;
            this.btnRegistroPacientePsicoterapia.Text = "Registro";
            this.btnRegistroPacientePsicoterapia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegistroPacientePsicoterapia.UseVisualStyleBackColor = true;
            // 
            // btnNominaPsicoterapia
            // 
            this.btnNominaPsicoterapia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnNominaPsicoterapia.FlatAppearance.BorderSize = 0;
            this.btnNominaPsicoterapia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNominaPsicoterapia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNominaPsicoterapia.ForeColor = System.Drawing.Color.White;
            this.btnNominaPsicoterapia.Location = new System.Drawing.Point(4, 163);
            this.btnNominaPsicoterapia.Name = "btnNominaPsicoterapia";
            this.btnNominaPsicoterapia.Size = new System.Drawing.Size(150, 26);
            this.btnNominaPsicoterapia.TabIndex = 5;
            this.btnNominaPsicoterapia.Text = "Nomina";
            this.btnNominaPsicoterapia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNominaPsicoterapia.UseVisualStyleBackColor = true;
            // 
            // btnClubDeTareasPsicoterapia
            // 
            this.btnClubDeTareasPsicoterapia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnClubDeTareasPsicoterapia.FlatAppearance.BorderSize = 0;
            this.btnClubDeTareasPsicoterapia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClubDeTareasPsicoterapia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClubDeTareasPsicoterapia.ForeColor = System.Drawing.Color.White;
            this.btnClubDeTareasPsicoterapia.Location = new System.Drawing.Point(0, 127);
            this.btnClubDeTareasPsicoterapia.Name = "btnClubDeTareasPsicoterapia";
            this.btnClubDeTareasPsicoterapia.Size = new System.Drawing.Size(156, 32);
            this.btnClubDeTareasPsicoterapia.TabIndex = 3;
            this.btnClubDeTareasPsicoterapia.Text = "Club de Tareas";
            this.btnClubDeTareasPsicoterapia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClubDeTareasPsicoterapia.UseVisualStyleBackColor = true;
            // 
            // btnAgendaPsicoterapia
            // 
            this.btnAgendaPsicoterapia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAgendaPsicoterapia.FlatAppearance.BorderSize = 0;
            this.btnAgendaPsicoterapia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgendaPsicoterapia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgendaPsicoterapia.ForeColor = System.Drawing.Color.White;
            this.btnAgendaPsicoterapia.Location = new System.Drawing.Point(0, 61);
            this.btnAgendaPsicoterapia.Name = "btnAgendaPsicoterapia";
            this.btnAgendaPsicoterapia.Size = new System.Drawing.Size(156, 32);
            this.btnAgendaPsicoterapia.TabIndex = 2;
            this.btnAgendaPsicoterapia.Text = "Agenda";
            this.btnAgendaPsicoterapia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgendaPsicoterapia.UseVisualStyleBackColor = true;
            // 
            // btnPacientesPsicoterapia
            // 
            this.btnPacientesPsicoterapia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPacientesPsicoterapia.FlatAppearance.BorderSize = 0;
            this.btnPacientesPsicoterapia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPacientesPsicoterapia.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.btnPacientesPsicoterapia.ForeColor = System.Drawing.Color.White;
            this.btnPacientesPsicoterapia.Location = new System.Drawing.Point(0, 194);
            this.btnPacientesPsicoterapia.Name = "btnPacientesPsicoterapia";
            this.btnPacientesPsicoterapia.Size = new System.Drawing.Size(156, 47);
            this.btnPacientesPsicoterapia.TabIndex = 1;
            this.btnPacientesPsicoterapia.Text = "Pacientes";
            this.btnPacientesPsicoterapia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPacientesPsicoterapia.UseVisualStyleBackColor = true;
            // 
            // btnPsicoterapia
            // 
            this.btnPsicoterapia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPsicoterapia.FlatAppearance.BorderSize = 0;
            this.btnPsicoterapia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPsicoterapia.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.btnPsicoterapia.ForeColor = System.Drawing.Color.White;
            this.btnPsicoterapia.Location = new System.Drawing.Point(0, 15);
            this.btnPsicoterapia.Name = "btnPsicoterapia";
            this.btnPsicoterapia.Size = new System.Drawing.Size(156, 47);
            this.btnPsicoterapia.TabIndex = 0;
            this.btnPsicoterapia.Text = "Psicoterapia";
            this.btnPsicoterapia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPsicoterapia.UseVisualStyleBackColor = true;
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1072, 605);
            this.Controls.Add(this.panelMenuPsicoterapia);
            this.Controls.Add(this.panelMenuEscuela);
            this.Controls.Add(this.panelMenuSuperior);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1026, 595);
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IICAPS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.SizeChanged += new System.EventHandler(this.Principal_SizeChanged);
            this.panelMenuSuperior.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panelMenuEscuela.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoDisgrawMenuEscuela)).EndInit();
            this.panelMenuPsicoterapia.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoDisgrawMenuPsicoterapia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenuSuperior;
        private System.Windows.Forms.Button btnIndex;
        private System.Windows.Forms.Button btnMenuEscuela;
        private System.Windows.Forms.Button btnMenuAdministracion;
        private System.Windows.Forms.Button btnMenuLibreria;
        private System.Windows.Forms.Button btnMenuMaestros;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem btnUsuario;
        private System.Windows.Forms.ToolStripMenuItem btnCambiarPass;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem btnCerrarSesion;
        private System.Windows.Forms.Button btnMenuPsicoterapia;
        private System.Windows.Forms.Button btnMenuPacientes;
        private System.Windows.Forms.Panel panelMenuEscuela;
        private System.Windows.Forms.PictureBox logoDisgrawMenuEscuela;
        private System.Windows.Forms.Button btnDocumentacionAlumno;
        private System.Windows.Forms.Button btnCreditoAlumno;
        private System.Windows.Forms.Button btnPagosAlumno;
        private System.Windows.Forms.Button btnRegistroAlumno;
        private System.Windows.Forms.Button btnTalleres;
        private System.Windows.Forms.Button btnGruposEscuela;
        private System.Windows.Forms.Button btnProgramasEscuela;
        private System.Windows.Forms.Button btnAlumno;
        private System.Windows.Forms.Button btnEscuela;
        private System.Windows.Forms.Button btnMateriasEscuela;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Panel panelMenuPsicoterapia;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnPsicoterapeutasPsicoterapia;
        private System.Windows.Forms.PictureBox logoDisgrawMenuPsicoterapia;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnPagosPacientePsicoterapia;
        private System.Windows.Forms.Button btnRegistroPacientePsicoterapia;
        private System.Windows.Forms.Button btnNominaPsicoterapia;
        private System.Windows.Forms.Button btnClubDeTareasPsicoterapia;
        private System.Windows.Forms.Button btnAgendaPsicoterapia;
        private System.Windows.Forms.Button btnPacientesPsicoterapia;
        private System.Windows.Forms.Button btnPsicoterapia;
    }
}