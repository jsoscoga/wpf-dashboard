﻿using System;
using System.Collections.Generic;
using System.Text;

namespace dashboard.Model
{
    public class ScheduleStations
    {
        public IEnumerable<StationStatus> StationStates { get; set; }
        public IEnumerable<int> IdStationStates { get; set; }
        public bool Closed { get; set; }
    }
}
