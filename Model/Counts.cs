using System;
using System.Collections.Generic;
using System.Text;

namespace dashboard.Model
{
    sealed class Counts
    {
        public int Plan { get; set; }
        public int Real { get; set; }
        public DateTime TaktTime { get; set; }
        public DateTime TotalStopTime { get; set; }
        public DateTime StationsStopTime { get; set; }
    }
}
