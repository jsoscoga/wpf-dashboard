using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace dashboard.Model
{
    public class StationState
    {
        public string Station { get; set; }
        public Visibility HigherVisibility { get; set; }
        public Visibility TopVisibility { get; set; }
        public Visibility CenterVisibility { get; set; }
        public Visibility BottomVisibility { get; set; }
    }
}
