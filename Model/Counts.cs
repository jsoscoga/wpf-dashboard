using System;
using System.Collections.Generic;
using System.Text;

namespace dashboard.Model
{
    sealed class Counts
    {
        public int Plan { get; set; }
        public int TaktTime { get; set; }
        public int Real { get; set; }
        public int TotalStopTime { get; set; }
    }
}
