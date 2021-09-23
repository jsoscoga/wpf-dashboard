using System;
using System.Collections.Generic;
using System.Text;

namespace dashboard.Model
{
    public class ScheduleStations
    {
        public IEnumerable<StationStatus> StationStatuses { get; set; }
        public IEnumerable<int> IdStationStatuses { get; set; }
        public bool Closed { get; set; }
    }
}
