using dashboard.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace dashboard.Service
{
    public class StationStateService
    {
        public StationState[] GetStationStates()
        {
            StationState[] stationStates = new StationState[]
            {
                new StationState() { Color = "OrangeRed", Station = "E1"},
                new StationState() { Color = "Green", Station = "E2"},
                new StationState() { Color = "Blue", Station = "E3"},
                new StationState() { Color = "Yellow", Station = "E4"},
                new StationState() { Color = "OrangeRed", Station = "E5"},
                new StationState() { Color = "Green", Station = "E6"},
                new StationState() { Color = "Blue", Station = "E7"},
            };

            return stationStates;
        }
    }
}
