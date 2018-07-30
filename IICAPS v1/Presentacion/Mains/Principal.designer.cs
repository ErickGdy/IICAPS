namespace IICAPS.Presentacion.Mains
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
            this.btnMenuAlumnos = new System.Windows.Forms.Button();
            this.btnMenuEscuela = new System.Windows.Forms.Button();
            this.btnIndex = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.btnUsuario = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCambiarPass = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCerrarSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMenuEscuela = new System.Windows.Forms.Panel();
            this.btnMateriasEscuela = new System.Windows.Forms.Button();
            this.logoDisgrawMenuEscuela = new System.Windows.Forms.PictureBox();
            this.btnDocumentacionAlumno = new System.Windows.Forms.Button();
            this.btnCreditoAlumno = new System.Windows.Forms.Button();
            this.btnPagosAlumno = new System.Windows.Forms.Button();
            this.btnRegistroAlumno = new System.Windows.Forms.Button();
            this.btnListaAlumnosEscuela = new System.Windows.Forms.Button();
            this.btnCalificacionesEscuela = new System.Windows.Forms.Button();
            this.btnGruposEscuela = new System.Windows.Forms.Button();
            this.btnProgramasEscuela = new System.Windows.Forms.Button();
            this.btnAlumno = new System.Windows.Forms.Button();
            this.btnEscuela = new System.Windows.Forms.Button();
            this.panelMenuSuperior.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.panelMenuEscuela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoDisgrawMenuEscuela)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenuSuperior
            // 
            this.panelMenuSuperior.BackColor = System.Drawing.Color.White;
            this.panelMenuSuperior.Controls.Add(this.btnMenuPsicoterapia);
            this.panelMenuSuperior.Controls.Add(this.btnMenuPacientes);
            this.panelMenuSuperior.Controls.Add(this.btnMenuAdministracion);
            this.panelMenuSuperior.Controls.Add(this.btnMenuLibreria);
            this.panelMenuSuperior.Controls.Add(this.btnMenuAlumnos);
            this.panelMenuSuperior.Controls.Add(this.btnMenuEscuela);
            this.panelMenuSuperior.Controls.Add(this.btnIndex);
            this.panelMenuSuperior.Controls.Add(this.menuStrip);
            this.panelMenuSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelMenuSuperior.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.panelMenuSuperior.Name = "panelMenuSuperior";
            this.panelMenuSuperior.Size = new System.Drawing.Size(1435, 55);
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
            this.btnMenuPsicoterapia.Location = new System.Drawing.Point(527, 0);
            this.btnMenuPsicoterapia.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnMenuPsicoterapia.Name = "btnMenuPsicoterapia";
            this.btnMenuPsicoterapia.Size = new System.Drawing.Size(185, 55);
            this.btnMenuPsicoterapia.TabIndex = 10;
            this.btnMenuPsicoterapia.Text = "Psicoterapia";
            this.btnMenuPsicoterapia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMenuPsicoterapia.UseVisualStyleBackColor = false;
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
            this.btnMenuPacientes.Location = new System.Drawing.Point(709, 2);
            this.btnMenuPacientes.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnMenuPacientes.Name = "btnMenuPacientes";
            this.btnMenuPacientes.Size = new System.Drawing.Size(172, 54);
            this.btnMenuPacientes.TabIndex = 9;
            this.btnMenuPacientes.Text = "Pacientes";
            this.btnMenuPacientes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMenuPacientes.UseVisualStyleBackColor = false;
            // 
            // btnMenuAdministracion
            // 
            this.btnMenuAdministracion.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnMenuAdministracion.FlatAppearance.BorderSize = 0;
            this.btnMenuAdministracion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuAdministracion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuAdministracion.Image = ((System.Drawing.Image)(resources.GetObject("btnMenuAdministracion.Image")));
            this.btnMenuAdministracion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuAdministracion.Location = new System.Drawing.Point(1031, 4);
            this.btnMenuAdministracion.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnMenuAdministracion.Name = "btnMenuAdministracion";
            this.btnMenuAdministracion.Size = new System.Drawing.Size(209, 53);
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
            this.btnMenuLibreria.Location = new System.Drawing.Point(884, 2);
            this.btnMenuLibreria.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnMenuLibreria.Name = "btnMenuLibreria";
            this.btnMenuLibreria.Size = new System.Drawing.Size(144, 54);
            this.btnMenuLibreria.TabIndex = 6;
            this.btnMenuLibreria.Text = "Librería";
            this.btnMenuLibreria.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMenuLibreria.UseVisualStyleBackColor = false;
            // 
            // btnMenuAlumnos
            // 
            this.btnMenuAlumnos.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnMenuAlumnos.FlatAppearance.BorderSize = 0;
            this.btnMenuAlumnos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuAlumnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuAlumnos.Image = ((System.Drawing.Image)(resources.GetObject("btnMenuAlumnos.Image")));
            this.btnMenuAlumnos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuAlumnos.Location = new System.Drawing.Point(375, 1);
            this.btnMenuAlumnos.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnMenuAlumnos.Name = "btnMenuAlumnos";
            this.btnMenuAlumnos.Size = new System.Drawing.Size(149, 55);
            this.btnMenuAlumnos.TabIndex = 5;
            this.btnMenuAlumnos.Text = "Alumnos";
            this.btnMenuAlumnos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMenuAlumnos.UseVisualStyleBackColor = false;
            this.btnMenuAlumnos.Click += new System.EventHandler(this.btnMenuAlumnos_Click);
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
            this.btnMenuEscuela.Location = new System.Drawing.Point(235, 1);
            this.btnMenuEscuela.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnMenuEscuela.Name = "btnMenuEscuela";
            this.btnMenuEscuela.Size = new System.Drawing.Size(140, 54);
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
            this.btnIndex.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnIndex.Name = "btnIndex";
            this.btnIndex.Size = new System.Drawing.Size(213, 55);
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
            this.menuStrip.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1435, 55);
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
            this.btnUsuario.Size = new System.Drawing.Size(105, 45);
            this.btnUsuario.Text = "Usuario";
            // 
            // btnCambiarPass
            // 
            this.btnCambiarPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCambiarPass.Image = ((System.Drawing.Image)(resources.GetObject("btnCambiarPass.Image")));
            this.btnCambiarPass.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCambiarPass.Name = "btnCambiarPass";
            this.btnCambiarPass.Size = new System.Drawing.Size(268, 38);
            this.btnCambiarPass.Text = "Cambiar Contraseña";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(265, 6);
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarSesion.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrarSesion.Image")));
            this.btnCerrarSesion.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(268, 38);
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            // 
            // panelMenuEscuela
            // 
            this.panelMenuEscuela.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(173)))), ((int)(((byte)(73)))));
            this.panelMenuEscuela.Controls.Add(this.btnMateriasEscuela);
            this.panelMenuEscuela.Controls.Add(this.logoDisgrawMenuEscuela);
            this.panelMenuEscuela.Controls.Add(this.btnDocumentacionAlumno);
            this.panelMenuEscuela.Controls.Add(this.btnCreditoAlumno);
            this.panelMenuEscuela.Controls.Add(this.btnPagosAlumno);
            this.panelMenuEscuela.Controls.Add(this.btnRegistroAlumno);
            this.panelMenuEscuela.Controls.Add(this.btnListaAlumnosEscuela);
            this.panelMenuEscuela.Controls.Add(this.btnCalificacionesEscuela);
            this.panelMenuEscuela.Controls.Add(this.btnGruposEscuela);
            this.panelMenuEscuela.Controls.Add(this.btnProgramasEscuela);
            this.panelMenuEscuela.Controls.Add(this.btnAlumno);
            this.panelMenuEscuela.Controls.Add(this.btnEscuela);
            this.panelMenuEscuela.Location = new System.Drawing.Point(3, 54);
            this.panelMenuEscuela.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.panelMenuEscuela.Name = "panelMenuEscuela";
            this.panelMenuEscuela.Size = new System.Drawing.Size(209, 604);
            this.panelMenuEscuela.TabIndex = 42;
            this.panelMenuEscuela.Visible = false;
            // 
            // btnMateriasEscuela
            // 
            this.btnMateriasEscuela.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMateriasEscuela.FlatAppearance.BorderSize = 0;
            this.btnMateriasEscuela.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMateriasEscuela.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMateriasEscuela.ForeColor = System.Drawing.Color.White;
            this.btnMateriasEscuela.Location = new System.Drawing.Point(0, 117);
            this.btnMateriasEscuela.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMateriasEscuela.Name = "btnMateriasEscuela";
            this.btnMateriasEscuela.Size = new System.Drawing.Size(208, 39);
            this.btnMateriasEscuela.TabIndex = 11;
            this.btnMateriasEscuela.Text = "Materias";
            this.btnMateriasEscuela.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMateriasEscuela.UseVisualStyleBackColor = true;
            this.btnMateriasEscuela.Click += new System.EventHandler(this.btnMateriasEscuela_Click);
            // 
            // logoDisgrawMenuEscuela
            // 
            this.logoDisgrawMenuEscuela.Image = ((System.Drawing.Image)(resources.GetObject("logoDisgrawMenuEscuela.Image")));
            this.logoDisgrawMenuEscuela.Location = new System.Drawing.Point(5, 558);
            this.logoDisgrawMenuEscuela.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.logoDisgrawMenuEscuela.Name = "logoDisgrawMenuEscuela";
            this.logoDisgrawMenuEscuela.Size = new System.Drawing.Size(201, 43);
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
            this.btnDocumentacionAlumno.Location = new System.Drawing.Point(0, 450);
            this.btnDocumentacionAlumno.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnDocumentacionAlumno.Name = "btnDocumentacionAlumno";
            this.btnDocumentacionAlumno.Size = new System.Drawing.Size(208, 39);
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
            this.btnCreditoAlumno.Location = new System.Drawing.Point(0, 411);
            this.btnCreditoAlumno.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnCreditoAlumno.Name = "btnCreditoAlumno";
            this.btnCreditoAlumno.Size = new System.Drawing.Size(208, 39);
            this.btnCreditoAlumno.TabIndex = 8;
            this.btnCreditoAlumno.Text = "Credito";
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
            this.btnPagosAlumno.Location = new System.Drawing.Point(0, 372);
            this.btnPagosAlumno.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnPagosAlumno.Name = "btnPagosAlumno";
            this.btnPagosAlumno.Size = new System.Drawing.Size(208, 39);
            this.btnPagosAlumno.TabIndex = 7;
            this.btnPagosAlumno.Text = "Pagos";
            this.btnPagosAlumno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPagosAlumno.UseVisualStyleBackColor = true;
            // 
            // btnRegistroAlumno
            // 
            this.btnRegistroAlumno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRegistroAlumno.FlatAppearance.BorderSize = 0;
            this.btnRegistroAlumno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistroAlumno.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistroAlumno.ForeColor = System.Drawing.Color.White;
            this.btnRegistroAlumno.Location = new System.Drawing.Point(0, 332);
            this.btnRegistroAlumno.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnRegistroAlumno.Name = "btnRegistroAlumno";
            this.btnRegistroAlumno.Size = new System.Drawing.Size(208, 39);
            this.btnRegistroAlumno.TabIndex = 6;
            this.btnRegistroAlumno.Text = "Registro";
            this.btnRegistroAlumno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegistroAlumno.UseVisualStyleBackColor = true;
            this.btnRegistroAlumno.Click += new System.EventHandler(this.btnRegistroAlumno_Click);
            // 
            // btnListaAlumnosEscuela
            // 
            this.btnListaAlumnosEscuela.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnListaAlumnosEscuela.FlatAppearance.BorderSize = 0;
            this.btnListaAlumnosEscuela.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnListaAlumnosEscuela.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListaAlumnosEscuela.ForeColor = System.Drawing.Color.White;
            this.btnListaAlumnosEscuela.Location = new System.Drawing.Point(0, 235);
            this.btnListaAlumnosEscuela.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnListaAlumnosEscuela.Name = "btnListaAlumnosEscuela";
            this.btnListaAlumnosEscuela.Size = new System.Drawing.Size(208, 39);
            this.btnListaAlumnosEscuela.TabIndex = 5;
            this.btnListaAlumnosEscuela.Text = "Lista Alumnos";
            this.btnListaAlumnosEscuela.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnListaAlumnosEscuela.UseVisualStyleBackColor = true;
            // 
            // btnCalificacionesEscuela
            // 
            this.btnCalificacionesEscuela.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCalificacionesEscuela.FlatAppearance.BorderSize = 0;
            this.btnCalificacionesEscuela.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalificacionesEscuela.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalificacionesEscuela.ForeColor = System.Drawing.Color.White;
            this.btnCalificacionesEscuela.Location = new System.Drawing.Point(0, 196);
            this.btnCalificacionesEscuela.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnCalificacionesEscuela.Name = "btnCalificacionesEscuela";
            this.btnCalificacionesEscuela.Size = new System.Drawing.Size(208, 39);
            this.btnCalificacionesEscuela.TabIndex = 4;
            this.btnCalificacionesEscuela.Text = "Calificaciones";
            this.btnCalificacionesEscuela.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCalificacionesEscuela.UseVisualStyleBackColor = true;
            // 
            // btnGruposEscuela
            // 
            this.btnGruposEscuela.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnGruposEscuela.FlatAppearance.BorderSize = 0;
            this.btnGruposEscuela.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGruposEscuela.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGruposEscuela.ForeColor = System.Drawing.Color.White;
            this.btnGruposEscuela.Location = new System.Drawing.Point(0, 156);
            this.btnGruposEscuela.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGruposEscuela.Name = "btnGruposEscuela";
            this.btnGruposEscuela.Size = new System.Drawing.Size(208, 39);
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
            this.btnProgramasEscuela.Location = new System.Drawing.Point(0, 75);
            this.btnProgramasEscuela.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnProgramasEscuela.Name = "btnProgramasEscuela";
            this.btnProgramasEscuela.Size = new System.Drawing.Size(208, 39);
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
            this.btnAlumno.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlumno.ForeColor = System.Drawing.Color.White;
            this.btnAlumno.Location = new System.Drawing.Point(0, 274);
            this.btnAlumno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAlumno.Name = "btnAlumno";
            this.btnAlumno.Size = new System.Drawing.Size(208, 58);
            this.btnAlumno.TabIndex = 1;
            this.btnAlumno.Text = "Alumno";
            this.btnAlumno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAlumno.UseVisualStyleBackColor = true;
            this.btnAlumno.Click += new System.EventHandler(this.btnAlumno_Click);
            // 
            // btnEscuela
            // 
            this.btnEscuela.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEscuela.FlatAppearance.BorderSize = 0;
            this.btnEscuela.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscuela.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEscuela.ForeColor = System.Drawing.Color.White;
            this.btnEscuela.Location = new System.Drawing.Point(0, 18);
            this.btnEscuela.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEscuela.Name = "btnEscuela";
            this.btnEscuela.Size = new System.Drawing.Size(208, 58);
            this.btnEscuela.TabIndex = 0;
            this.btnEscuela.Text = "Escuela";
            this.btnEscuela.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEscuela.UseVisualStyleBackColor = true;
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1420, 745);
            this.Controls.Add(this.panelMenuEscuela);
            this.Controls.Add(this.panelMenuSuperior);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MinimumSize = new System.Drawing.Size(1434, 724);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenuSuperior;
        private System.Windows.Forms.Button btnIndex;
        private System.Windows.Forms.Button btnMenuEscuela;
        private System.Windows.Forms.Button btnMenuAdministracion;
        private System.Windows.Forms.Button btnMenuLibreria;
        private System.Windows.Forms.Button btnMenuAlumnos;
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
        private System.Windows.Forms.Button btnListaAlumnosEscuela;
        private System.Windows.Forms.Button btnCalificacionesEscuela;
        private System.Windows.Forms.Button btnGruposEscuela;
        private System.Windows.Forms.Button btnProgramasEscuela;
        private System.Windows.Forms.Button btnAlumno;
        private System.Windows.Forms.Button btnEscuela;
        private System.Windows.Forms.Button btnMateriasEscuela;
    }
}