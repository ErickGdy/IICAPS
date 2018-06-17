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

namespace IICAPS.Presentacion.Mains.Escuela
{
    public partial class EscuelaMain : Form
    {
        private static EscuelaMain instance;
        public EscuelaMain()
        {
            InitializeComponent();
        }
        public static EscuelaMain getInstance()
        {
            if (instance == null)
            {
                instance = new EscuelaMain();
            }
            return instance;
        }

     


    }
}
