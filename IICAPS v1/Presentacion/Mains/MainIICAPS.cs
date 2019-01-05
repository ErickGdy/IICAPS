using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IICAPS_v1.Control;
using IICAPS_v1.DataObject;
using IICAPS_v1.Presentacion;
using MySql.Data.MySqlClient;

namespace IICAPS_v1.Presentacion.Mains
{ 
    public partial class MainIICAPS : Form
    {

        private static MainIICAPS instance;
        public MainIICAPS()
        {
            InitializeComponent();
        }

        public static MainIICAPS getInstance()
        {
            if (instance == null)
                instance = new MainIICAPS();
            return instance;
        }

        private void MainIICAPS_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(this.Width-145,this.Height - 60);
        }
    }
}
