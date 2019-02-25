using IICAPS.Presentacion;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Updater_IICAPS.Control;

namespace Updater_IICAPS
{
    public partial class Updater : Form
    {
        public static Updater instance; 
        NetworkCredential credenciales = new NetworkCredential("iic2ps1d", "ConejoVolador10");
        long fileSize = 0;
        Control_Updater control_updater;
        string pathUpdater;
        public Updater()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            //AGREGAR METODO PARA PODER MOVER LA VENTANA CON EL CURSOR, MANTENIENDO EL CLICK SOBRE CUALQUIER CONTROL
            foreach (System.Windows.Forms.Control item in this.Controls)
            {
                item.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelMove_MouseDown);
                item.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelMove_MouseMove);
                item.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelMove_MouseUp);
            }
            this.Visible = true;
            new Thread(obtenerPath).Start();
        }
        public static Updater getInstance()
        {
            if (instance == null)
            {
                instance = new Updater();
                return instance;
            }
            else
            {
                return instance;
            }
        }

        private void obtenerPath()
        {
            try
            {
                control_updater = Control_Updater.getInstance();
                control_updater.controlDeVersion();
                string patht = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf(@"\"));
                string[] directorios = Directory.GetFiles(patht, "Updater IICAPS.exe?", SearchOption.AllDirectories);
                while (directorios.Length <= 0)
                {
                    patht = patht.Substring(0, patht.LastIndexOf(@"\"));
                    directorios = Directory.GetFiles(patht, "Updater IICAPS.exe?", SearchOption.AllDirectories);
                }
                pathUpdater = directorios[0];
                for (int i = 1; i < directorios.Length; i++)
                {
                    if (File.GetLastWriteTime(pathUpdater) < File.GetLastWriteTime(directorios[i]))
                    {
                        pathUpdater = directorios[i];
                    }
                }
                actualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se ha encontrado el directorio de instalación, es necesario reinstalar el programa ", "Error al encontrar archivos de configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Application.Exit();
        }
        private void actualizar()
        {
            try
            {
                //Process.Start(pathUpdater);

                if (control_updater.leerVersion(pathUpdater.Substring(0, pathUpdater.LastIndexOf(@"\"))) != control_updater.version_updater)
                {
                    descargarActualizacionUpdater();
                    lblMensaje.Text = "Descargando actualizaciones";
                    DWPorcentaje.Visible = true;
                    DWProgressBar.Visible = true;
                    DWTamano.Visible = true;
                    lblDWName.Text = "IICAPS Updater";
                    lblDWName.Visible = true;
                    pictureLoading.Location = new Point(pictureLoading.Location.X + 10, pictureLoading.Location.Y);
                    pictureLoading.Visible = true;
                }
                else
                {
                    if (control_updater.leerVersion(Environment.CurrentDirectory) == control_updater.version_app)
                    {
                        DWPorcentaje.Visible = false;
                        DWProgressBar.Visible = false;
                        DWTamano.Visible = false;
                        pictureLoading.Visible = false;
                        lblDWName.Visible = false;
                        pictureLoading.Visible = false;
                        lblMensaje.Text = "Sistema Actualizado";
                        Thread.Sleep(2000);
                        lblMensaje.Text = "Abriendo ERP IICAPS";
                        Thread.Sleep(3000);
                        this.Close();
                        Application.Run(new Login());
                    }
                    else
                        client_DownloadFileCompletedParche1(null, null);
                }
            }
            catch (Exception ex)
            {
                DWPorcentaje.Visible = false;
                DWProgressBar.Visible = false;
                DWTamano.Visible = false;
                lblMensaje.Text = "Descarga fallida";
                pictureLoading.Visible = false;
                lblDWName.Visible = false;
                DialogResult result = MessageBox.Show("No ha sido posible actualizar, ¿Desea reintentando descarga?", "Actualización Fallida", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Retry)
                    actualizar();
                else
                {
                    lblMensaje.Text = "Actualización Fallida";
                    Thread.Sleep(2000);
                    lblMensaje.Text = "Abriendo ERP IICAPS";
                    Thread.Sleep(3000);
                    this.Close();
                    Application.Run(new Login());
                }
            }
        }


        private void descargarActualizacionUpdater()
        {
            string urlPach1 = @"www.iicaps.edu.mx/PatchERP/Updater_IICAPS.exe";
            WebClient client = new WebClient();
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChangedParche1);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompletedParche1);
            client.Credentials = credenciales;

            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(@"ftp://"+urlPach1));
            request.Proxy = null;
            request.Credentials = credenciales;
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            fileSize = response.ContentLength;

            client.DownloadFileAsync(new Uri(@"http://" + urlPach1), pathUpdater);

        }

        void client_DownloadProgressChangedParche1(object sender, DownloadProgressChangedEventArgs e)
        {
            DWProgressBar.Value = Convert.ToInt32((float)(e.BytesReceived /(float)fileSize) *100);
            DWPorcentaje.Text = DWProgressBar.Value.ToString() + "%";
            //DWarchiuvo.Text = "...Patch-4.MPQ";
            DWTamano.Text = string.Format("{0} MB's / {1} MB's",
            (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
            (fileSize / 1024d / 1024d).ToString("0.00"));
        }

        void client_DownloadFileCompletedParche1(object sender, AsyncCompletedEventArgs e)
        {
            DWPorcentaje.Visible = false;
            DWProgressBar.Visible = false;
            DWTamano.Visible = false;
            pictureLoading.Visible = false;
            lblDWName.Visible = false;
            pictureLoading.Visible = false;
            lblMensaje.Text = "Sistema Actualizado";
            Thread.Sleep(2000);
            lblMensaje.Text = "Abriendo IICAPS UPDATER";
            Thread.Sleep(3000);
            try
            {
                //Process.Start(pathUpdater);
                Process updater_Process = new Process();
                updater_Process.StartInfo.FileName = pathUpdater;
                updater_Process.StartInfo.UseShellExecute = false;
                updater_Process.StartInfo.CreateNoWindow = false;
                updater_Process.StartInfo.WorkingDirectory = pathUpdater.Substring(0, pathUpdater.LastIndexOf(@"\"));
                updater_Process.Start();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No ha sido posible abrir updater";
                Thread.Sleep(2000);
                lblMensaje.Text = "Abriendo ERP IICAPS";
                Thread.Sleep(2000);
                Login lg = new Login();
                Thread.Sleep(3000);
            }
        }




        //METODOS DE DISEÑO GRAFICO DE INTERFAZ

        private bool Drag;
        private int MouseX;
        private int MouseY;

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        private bool m_aeroEnabled;

        private const int CS_DROPSHADOW = 0x20000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]

        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0; DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        }; DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;
                default: break;
            }
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT) m.Result = (IntPtr)HTCAPTION;
        }
        private void PanelMove_MouseDown(object sender, MouseEventArgs e)
        {
            Drag = true;
            MouseX = Cursor.Position.X - this.Left;
            MouseY = Cursor.Position.Y - this.Top;
        }
        private void PanelMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                this.Top = Cursor.Position.Y - MouseY;
                this.Left = Cursor.Position.X - MouseX;
            }
        }
        private void PanelMove_MouseUp(object sender, MouseEventArgs e)
        {
            Drag = false;
        }


    }
}
