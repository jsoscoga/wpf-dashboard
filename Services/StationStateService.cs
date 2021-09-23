using dashboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dashboard.Service
{
    public class StationStateService
    {
        public IEnumerable<StationState> GetGreenFromSlaveData(IEnumerable<SlaveData> slaveData)
        {
            List<StationState> stationStates = new List<StationState>();
            if (slaveData.Any(sD => sD.Channel4 > 0))
            {
                var greenSlaveData = slaveData.Where(sD => sD.Channel4 > 0);
                var slavesId = greenSlaveData.Select(sD => sD.SlaveId).Distinct();
                foreach (int slaveId in slavesId)
                {
                    var lastData = slaveData.OrderBy(sD => sD.DatStart).Last(sD => sD.SlaveId == slaveId);
                    if (lastData.Channel4 > 0)
                    {
                        stationStates.Add(new StationState()
                        {
                            Station = lastData.Name,
                            HigherVisibility = System.Windows.Visibility.Visible,
                            TopVisibility = System.Windows.Visibility.Hidden,
                            CenterVisibility = System.Windows.Visibility.Hidden,
                            BottomVisibility = System.Windows.Visibility.Hidden
                        });
                    }
                }
            }
            return stationStates;
        }
    }
}
