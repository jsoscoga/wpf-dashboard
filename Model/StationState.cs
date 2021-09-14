﻿using System;
using System.Collections.Generic;
using System.Text;

namespace dashboard.Model
{
    public class StationState
    {
        public string Station { get; set; }
        //public string Color { get; set; }
        public DateTime StopTime { get; set; }
        public DateTime DateEnd { get; set; }
        public bool Closed { get; set; }
    }
}
