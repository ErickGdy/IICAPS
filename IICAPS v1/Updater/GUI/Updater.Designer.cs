using System.Windows.Forms;

namespace Updater_IICAPS
{
    partial class Updater
    {

        #region Windows Form Designer generated code

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private ProgressBar DWProgressBar;
        private Label DWPorcentaje;
        private Label lblMensaje;
        private Label DWTamano;

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

        /// <summary>
        /// Required method for Designer support
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Updater));
            this.DWProgressBar = new System.Windows.Forms.ProgressBar();
            this.DWPorcentaje = new System.Windows.Forms.Label();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.DWTamano = new System.Windows.Forms.Label();
            this.pbDisgraw = new System.Windows.Forms.PictureBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lblAvisoDePrivacidad = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pbIsotipo = new System.Windows.Forms.PictureBox();
            this.pbEslogan = new System.Windows.Forms.PictureBox();
            this.pictureLoading = new System.Windows.Forms.PictureBox();
            this.lblDWName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbDisgraw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIsotipo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEslogan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // DWProgressBar
            // 
            this.DWProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DWProgressBar.BackColor = System.Drawing.Color.LightPink;
            this.DWProgressBar.Location = new System.Drawing.Point(54, 293);
            this.DWProgressBar.Name = "DWProgressBar";
            this.DWProgressBar.Size = new System.Drawing.Size(276, 10);
            this.DWProgressBar.TabIndex = 1;
            this.DWProgressBar.Visible = false;
            // 
            // DWPorcentaje
            // 
            this.DWPorcentaje.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.DWPorcentaje.AutoSize = true;
            this.DWPorcentaje.BackColor = System.Drawing.Color.Transparent;
            this.DWPorcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DWPorcentaje.ForeColor = System.Drawing.Color.White;
            this.DWPorcentaje.Location = new System.Drawing.Point(335, 293);
            this.DWPorcentaje.Name = "DWPorcentaje";
            this.DWPorcentaje.Size = new System.Drawing.Size(23, 13);
            this.DWPorcentaje.TabIndex = 2;
            this.DWPorcentaje.Text = "0%";
            this.DWPorcentaje.Visible = false;
            // 
            // lblMensaje
            // 
            this.lblMensaje.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMensaje.BackColor = System.Drawing.Color.Transparent;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.ForeColor = System.Drawing.Color.White;
            this.lblMensaje.Location = new System.Drawing.Point(17, 263);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(351, 27);
            this.lblMensaje.TabIndex = 3;
            this.lblMensaje.Tag = "";
            this.lblMensaje.Text = "Buscando Actualizaciones";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DWTamano
            // 
            this.DWTamano.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DWTamano.BackColor = System.Drawing.Color.Transparent;
            this.DWTamano.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DWTamano.ForeColor = System.Drawing.Color.White;
            this.DWTamano.Location = new System.Drawing.Point(91, 312);
            this.DWTamano.Name = "DWTamano";
            this.DWTamano.Size = new System.Drawing.Size(199, 16);
            this.DWTamano.TabIndex = 4;
            this.DWTamano.Text = "0 MB\'s / 0 MB\'s";
            this.DWTamano.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DWTamano.Visible = false;
            // 
            // pbDisgraw
            // 
            this.pbDisgraw.BackColor = System.Drawing.Color.Transparent;
            this.pbDisgraw.Image = ((System.Drawing.Image)(resources.GetObject("pbDisgraw.Image")));
            this.pbDisgraw.Location = new System.Drawing.Point(20, 373);
            this.pbDisgraw.Name = "pbDisgraw";
            this.pbDisgraw.Size = new System.Drawing.Size(97, 38);
            this.pbDisgraw.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDisgraw.TabIndex = 5;
            this.pbDisgraw.TabStop = false;
            // 
            // pbLogo
            // 
            this.pbLogo.BackColor = System.Drawing.Color.Transparent;
            this.pbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(20, 26);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(130, 128);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 6;
            this.pbLogo.TabStop = false;
            // 
            // lblAvisoDePrivacidad
            // 
            this.lblAvisoDePrivacidad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAvisoDePrivacidad.BackColor = System.Drawing.Color.Transparent;
            this.lblAvisoDePrivacidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.lblAvisoDePrivacidad.ForeColor = System.Drawing.Color.Thistle;
            this.lblAvisoDePrivacidad.Location = new System.Drawing.Point(121, 373);
            this.lblAvisoDePrivacidad.Name = "lblAvisoDePrivacidad";
            this.lblAvisoDePrivacidad.Size = new System.Drawing.Size(246, 37);
            this.lblAvisoDePrivacidad.TabIndex = 7;
            this.lblAvisoDePrivacidad.Tag = "";
            this.lblAvisoDePrivacidad.Text = "aaviso de privacidad aviso de privacidad aviso de privacidad aviso de privacidad " +
    "aviso de privacidad aviso de privacidad viso de privacidad ";
            this.lblAvisoDePrivacidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Raleway", 14F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(16, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(352, 84);
            this.label1.TabIndex = 8;
            this.label1.Text = "Sistema de Gestión de Recursos Institucionales IICAPS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbIsotipo
            // 
            this.pbIsotipo.BackColor = System.Drawing.Color.Transparent;
            this.pbIsotipo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbIsotipo.Image = ((System.Drawing.Image)(resources.GetObject("pbIsotipo.Image")));
            this.pbIsotipo.Location = new System.Drawing.Point(156, 55);
            this.pbIsotipo.Name = "pbIsotipo";
            this.pbIsotipo.Size = new System.Drawing.Size(202, 54);
            this.pbIsotipo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbIsotipo.TabIndex = 9;
            this.pbIsotipo.TabStop = false;
            // 
            // pbEslogan
            // 
            this.pbEslogan.BackColor = System.Drawing.Color.Transparent;
            this.pbEslogan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbEslogan.Image = ((System.Drawing.Image)(resources.GetObject("pbEslogan.Image")));
            this.pbEslogan.Location = new System.Drawing.Point(164, 115);
            this.pbEslogan.Name = "pbEslogan";
            this.pbEslogan.Size = new System.Drawing.Size(189, 15);
            this.pbEslogan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbEslogan.TabIndex = 10;
            this.pbEslogan.TabStop = false;
            // 
            // pictureLoading
            // 
            this.pictureLoading.BackColor = System.Drawing.Color.Transparent;
            this.pictureLoading.Image = ((System.Drawing.Image)(resources.GetObject("pictureLoading.Image")));
            this.pictureLoading.Location = new System.Drawing.Point(267, 265);
            this.pictureLoading.Name = "pictureLoading";
            this.pictureLoading.Size = new System.Drawing.Size(23, 23);
            this.pictureLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureLoading.TabIndex = 11;
            this.pictureLoading.TabStop = false;
            // 
            // lblDWName
            // 
            this.lblDWName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDWName.BackColor = System.Drawing.Color.Transparent;
            this.lblDWName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblDWName.ForeColor = System.Drawing.Color.White;
            this.lblDWName.Location = new System.Drawing.Point(22, 335);
            this.lblDWName.Name = "lblDWName";
            this.lblDWName.Size = new System.Drawing.Size(338, 17);
            this.lblDWName.TabIndex = 12;
            this.lblDWName.Text = "iicaps updater";
            this.lblDWName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDWName.Visible = false;
            // 
            // Updater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LavenderBlush;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(385, 428);
            this.Controls.Add(this.lblDWName);
            this.Controls.Add(this.DWPorcentaje);
            this.Controls.Add(this.pictureLoading);
            this.Controls.Add(this.pbEslogan);
            this.Controls.Add(this.pbIsotipo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAvisoDePrivacidad);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.pbDisgraw);
            this.Controls.Add(this.DWTamano);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.DWProgressBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Updater";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Updater IICAPS";
            this.TopMost = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelMove_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelMove_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelMove_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbDisgraw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIsotipo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEslogan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private PictureBox pbDisgraw;
        private PictureBox pbLogo;
        private Label lblAvisoDePrivacidad;
        private Label label1;
        private PictureBox pbIsotipo;
        private PictureBox pbEslogan;
        private PictureBox pictureLoading;
        private Label lblDWName;
    }
}