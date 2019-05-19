using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IICAPS_v1.Presentacion.Forms.FromsControl
{
    public partial class MessageBoxCustom : Form
    {
        public enum Icono {
            Error,
            Success,
            Info,
            Warning
        };
        public enum Opcion {
            Ok = 0,
            OkCancel = 1,
            YesNo = 2,
            RetryCancel = 3
        };

        public enum OpcionResult
        {
            Ok = 0,
            Cancel = 1,
            Yes = 2,
            No = 3,
            Retry = 4
        };

        MessageBoxCustom.Icono iconoG;
        MessageBoxCustom.Opcion opcionG;
        public static MessageBoxCustom.OpcionResult opcionResultante;
        private MessageBoxCustom(string mensaje, string titulo, Opcion opcion, Icono icono)
        {
            InitializeComponent();
            this.opcionG = opcion;
            this.iconoG = icono;
            if (titulo != null)
                lblTitulo.Text = titulo;
            if (mensaje != null)
                lblMensaje.Text = mensaje;
            if (opcion != Opcion.Ok)
            {
                //increase panel rows count by one
                panelBotones.ColumnCount++;
                //add a new RowStyle as a copy of the previous one
                ColumnStyle estiloBoton = new ColumnStyle(panelBotones.ColumnStyles[0].SizeType, panelBotones.ColumnStyles[0].Width);
                panelBotones.ColumnStyles.Add(estiloBoton);
                panelBotones.ColumnStyles[2] = new ColumnStyle(panelBotones.ColumnStyles[1].SizeType, panelBotones.ColumnStyles[1].Width);
                //add the control
                Button btnOpcion2 = new Button();
                btnOpcion2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
                btnOpcion2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
                btnOpcion2.Name = "btnOpcion2";
                btnOpcion2.Size = new System.Drawing.Size(114, 38);
                btnOpcion2.TabIndex = 0;
                btnOpcion2.UseVisualStyleBackColor = true;
                btnOpcion2.Click += new System.EventHandler(this.BtnSecundario_Click);
                panelBotones.Controls.Add(btnOpcion2, 2, 0);
                switch (opcion)
                {
                    case Opcion.OkCancel:
                        btnOpcion2.Text = "Cancelar";
                        BtnPrincipal.Text = "Ok";
                        break;
                    case Opcion.YesNo:
                        btnOpcion2.Text = "No";
                        BtnPrincipal.Text = "Se";
                        break;
                    case Opcion.RetryCancel:
                        btnOpcion2.Text = "Cancelar";
                        BtnPrincipal.Text = "Reintentar";
                        break;
                    default:
                        break;
                }
            }
            pbIcono.Image = imageList1.Images[(int)icono];
            pbIcono.SizeMode = PictureBoxSizeMode.Zoom;
            AñadirAccionMover();
        }
        private MessageBoxCustom(string mensaje)
        {
            InitializeComponent();
            this.opcionG = Opcion.Ok;
            lblMensaje.Text = mensaje;
            panelMensaje.GetControlFromPosition(0, 0).Dispose(); AñadirAccionMover();
        }
        public static OpcionResult ShowMe(string mensaje)
        {
            new MessageBoxCustom(mensaje).ShowDialog();
            return opcionResultante;
        }
        public static OpcionResult ShowMe(string mensaje, string titulo, Opcion opciones, Icono icono)
        {
            new MessageBoxCustom(mensaje, titulo, opciones, icono).ShowDialog();
            return opcionResultante;
        }
        private void BtnPrincipal_Click(object sender, EventArgs e)
        {
            switch (opcionG)
            {
                case Opcion.Ok:
                    opcionResultante = OpcionResult.Ok;
                    break;
                case Opcion.OkCancel:
                    opcionResultante = OpcionResult.Ok;
                    break;
                case Opcion.YesNo:
                    opcionResultante = OpcionResult.Yes;
                    break;
                case Opcion.RetryCancel:
                    opcionResultante = OpcionResult.Retry;
                    break;
                default:
                    break;
            }
            Dispose();
        }
        private void BtnSecundario_Click(object sender, EventArgs e)
        {
            switch (opcionG)
            {
                case Opcion.OkCancel:
                    opcionResultante = OpcionResult.Cancel;
                    break;
                case Opcion.YesNo:
                    opcionResultante = OpcionResult.No;
                    break;
                case Opcion.RetryCancel:
                    opcionResultante = OpcionResult.Cancel;
                    break;
                default:
                    break;
            }
            Dispose();
        }
        private void AñadirAccionMover() {
            foreach (System.Windows.Forms.Control item in this.Controls)
            {
                item.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelMove_MouseDown);
                item.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelMove_MouseMove);
                item.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelMove_MouseUp);
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
