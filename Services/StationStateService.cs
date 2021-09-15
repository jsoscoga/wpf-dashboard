using dashboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dashboard.Service
{
    public class StationStateService
    {
        public StationState[] GetDummyStationStates()
        {
            StationState[] stationStates = new StationState[]
            {
                //new StationState() { Color = "OrangeRed", Station = "E1"},
                //new StationState() { Color = "Green", Station = "E2"},
                //new StationState() { Color = "Blue", Station = "E3"},
                //new StationState() { Color = "Yellow", Station = "E4"},
                //new StationState() { Color = "OrangeRed", Station = "E5"},
                //new StationState() { Color = "Green", Station = "E6"},
                //new StationState() { Color = "Blue", Station = "E7"},
            };

            return stationStates;
        }

        public List<StationState> GetFromSlaveData(IEnumerable<SlaveData> slaveData)
        {
            List<StationState> stationStates = new List<StationState>();

            var slaves = slaveData.OrderBy(sD => sD.SlaveId).Select(sD => sD.SlaveId).Distinct().ToArray();
            foreach (int slave in slaves)
            {
                var slaveDataList = slaveData.Where(sD => sD.SlaveId == slave && !string.Concat(sD.Channel1, sD.Channel2, sD.Channel3).Equals("000"))
                    .OrderBy(sD => sD.DatStart);
                AddStationStates(slaveData, ref stationStates, slave, slaveDataList);
            }

            return stationStates;
        }

        private static void AddStationStates(IEnumerable<SlaveData> slaveData, ref List<StationState> stationStates, int slave, IOrderedEnumerable<SlaveData> slaveDataList)
        {
            var firstState = slaveDataList.First();
            var actualState = firstState;
            while (slaveDataList.Any(sD => sD.DatStart.Equals(actualState.DatEnd)))
            {
                actualState = slaveDataList.First(sD => sD.DatStart.Equals(actualState.DatEnd));
            }

            var closedSlaveData = slaveData.FirstOrDefault(sD => sD.SlaveId == slave && sD.DatStart.Equals(actualState.DatEnd));
            bool closedState = closedSlaveData != null;
            stationStates.Add(new StationState()
            {
                Station = slave.ToString(),
                DateStart = firstState.DatStart,
                DateEnd = actualState.DatEnd,
                Closed = closedState,
                BottomVisibility = !closedState && actualState.Channel1 > 0,
                CenterVisibility = !closedState && actualState.Channel2 > 0,
                TopVisibility = !closedState && actualState.Channel3 > 0
            });

            if (closedSlaveData != null)
            {
                var newSlaveDataList = slaveData.Where(sD => sD.SlaveId == slave && !string.Concat(sD.Channel1, sD.Channel2, sD.Channel3).Equals("000") && sD.DatStart > closedSlaveData.DatStart)
                    .OrderBy(sD => sD.DatStart);
                if (newSlaveDataList.Count() > 0)
                {
                    AddStationStates(slaveData, ref stationStates, slave, newSlaveDataList);
                }
            }
        }
    }
}
