﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IICAPS_v1.Presentacion
{
    public partial class ImpresionDocumentos : Form
    {
        public ImpresionDocumentos()
        {
            InitializeComponent();
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }
    }
}
