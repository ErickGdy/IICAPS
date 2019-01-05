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

namespace IICAPS.Presentacion.Mains.Libreria
{
    public partial class LibreriaMain : Form
    {
        private static LibreriaMain instance;
        public LibreriaMain()
        {
            InitializeComponent();
        }
        public static LibreriaMain getInstance()
        {
            if (instance == null)
            {
                instance = new LibreriaMain();
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
