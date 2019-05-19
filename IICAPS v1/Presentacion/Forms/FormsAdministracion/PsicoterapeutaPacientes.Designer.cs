﻿namespace IICAPS_v1.Presentacion
{
    partial class PsicoterapeutaPacientes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PsicoterapeutaPacientes));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuTablaAlumnos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.consultarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificiarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.limpiarBusqueda = new System.Windows.Forms.LinkLabel();
            this.pictureBoxBuscar = new System.Windows.Forms.PictureBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNombrePaciente = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuTablaAlumnos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBuscar)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.menuTablaAlumnos;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(20, 70);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(814, 350);
            this.dataGridView1.TabIndex = 0;
            // 
            // menuTablaAlumnos
            // 
            this.menuTablaAlumnos.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuTablaAlumnos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.consultarToolStripMenuItem,
            this.modificiarToolStripMenuItem});
            this.menuTablaAlumnos.Name = "menuTablaAlumnos";
            this.menuTablaAlumnos.Size = new System.Drawing.Size(126, 48);
            // 
            // consultarToolStripMenuItem
            // 
            this.consultarToolStripMenuItem.Name = "consultarToolStripMenuItem";
            this.consultarToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.consultarToolStripMenuItem.Text = "Consultar";
            this.consultarToolStripMenuItem.Click += new System.EventHandler(this.consultarToolStripMenuItem_Click);
            // 
            // modificiarToolStripMenuItem
            // 
            this.modificiarToolStripMenuItem.Name = "modificiarToolStripMenuItem";
            this.modificiarToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.modificiarToolStripMenuItem.Text = "Modificar";
            this.modificiarToolStripMenuItem.Click += new System.EventHandler(this.modificiarToolStripMenuItem_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(634, 40);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(200, 24);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuscar_KeyUp);
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
            this.limpiarBusqueda.Location = new System.Drawing.Point(810, 42);
            this.limpiarBusqueda.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.limpiarBusqueda.Name = "limpiarBusqueda";
            this.limpiarBusqueda.Size = new System.Drawing.Size(21, 20);
            this.limpiarBusqueda.TabIndex = 39;
            this.limpiarBusqueda.TabStop = true;
            this.limpiarBusqueda.Text = "X";
            this.limpiarBusqueda.Visible = false;
            this.limpiarBusqueda.VisitedLinkColor = System.Drawing.Color.Black;
            this.limpiarBusqueda.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.limpiarBusqueda_LinkClicked);
            // 
            // pictureBoxBuscar
            // 
            this.pictureBoxBuscar.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxBuscar.Image")));
            this.pictureBoxBuscar.Location = new System.Drawing.Point(605, 40);
            this.pictureBoxBuscar.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBoxBuscar.Name = "pictureBoxBuscar";
            this.pictureBoxBuscar.Size = new System.Drawing.Size(24, 23);
            this.pictureBoxBuscar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBuscar.TabIndex = 38;
            this.pictureBoxBuscar.TabStop = false;
            // 
            // btnActualizar
            // 
            this.btnActualizar.AutoSize = true;
            this.btnActualizar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnActualizar.BackColor = System.Drawing.Color.Transparent;
            this.btnActualizar.FlatAppearance.BorderSize = 0;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.Location = new System.Drawing.Point(18, 40);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(30, 30);
            this.btnActualizar.TabIndex = 2;
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(16, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pacientes:";
            // 
            // lblNombrePaciente
            // 
            this.lblNombrePaciente.AutoSize = true;
            this.lblNombrePaciente.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.lblNombrePaciente.ForeColor = System.Drawing.Color.Black;
            this.lblNombrePaciente.Location = new System.Drawing.Point(145, 8);
            this.lblNombrePaciente.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNombrePaciente.Name = "lblNombrePaciente";
            this.lblNombrePaciente.Size = new System.Drawing.Size(174, 29);
            this.lblNombrePaciente.TabIndex = 41;
            this.lblNombrePaciente.Text = "Psicoterapeuta";
            // 
            // PsicoterapeutaPacientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(848, 437);
            this.Controls.Add(this.lblNombrePaciente);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.limpiarBusqueda);
            this.Controls.Add(this.pictureBoxBuscar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.dataGridView1);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(800, 450);
            this.Name = "PsicoterapeutaPacientes";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lista de Pacientes";
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuTablaAlumnos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBuscar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.PictureBox pictureBoxBuscar;
        private System.Windows.Forms.LinkLabel limpiarBusqueda;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.ContextMenuStrip menuTablaAlumnos;
        private System.Windows.Forms.ToolStripMenuItem modificiarToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNombrePaciente;
        private System.Windows.Forms.ToolStripMenuItem consultarToolStripMenuItem;
    }
}