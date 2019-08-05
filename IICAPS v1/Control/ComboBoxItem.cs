using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICAPS_v1.Control
{
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public object ValueItem { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
