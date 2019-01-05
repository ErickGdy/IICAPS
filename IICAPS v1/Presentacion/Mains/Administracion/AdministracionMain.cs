using IICAPS_v1.Presentacion;
using IICAPS_v1.Presentacion.Mains.Escuela;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IICAPS.Presentacion.Mains.Administracion
{
    public partial class AdministracionMain : Form
    {
        private static AdministracionMain instance;
        public AdministracionMain()
        {
            InitializeComponent();
        }
        public static AdministracionMain getInstance()
        {
            if (instance == null)
            {
                instance = new AdministracionMain();
            }
            return instance;
        }

        private void EscuelaMain_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(this.Width - 145, this.Height - 60);
            pictureBox2.Size = new Size(this.Width- 157, this.Height);
        }
    }
}
