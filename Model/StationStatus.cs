﻿using System;
using System.Collections.Generic;
using System.Text;

namespace dashboard.Model
{
    public class StationStatus
    {
        public int Id { get; set; }
        public string Station { get; set; }
        public int SlaveId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool Closed { get; set; }
        public bool TopVisibility { get; set; }
        public bool CenterVisibility { get; set; }
        public bool BottomVisibility { get; set; }
        public IEnumerable<StationStatus> ScopesStations { get; set; }
    }
}
