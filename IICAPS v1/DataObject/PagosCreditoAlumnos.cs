﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.DataObject
{
    public class PagosCreditoAlumnos
    {
        public int id { get; set; }
        public int creditoAlumno { get; set; }
        public string alumno { get; set; }
        public DateTime fechaPago { get; set; }
        public double cantidad { get; set; }
        public string observaciones { get; set; }
    }
}
