using dashboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dashboard.Service
{
    public class StationStateService
    {
        public StationStatus[] GetDummyStationStates()
        {
            StationStatus[] stationStates = new StationStatus[]
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

        public IEnumerable<StationStatus> GetFromSlaveData(IEnumerable<SlaveData> slaveData)
        {
            List<StationStatus> stationStates = new List<StationStatus>();

            while (slaveData.Any(sD => !string.Concat(sD.Channel1, sD.Channel2, sD.Channel3).Equals("000")))
            {
                AddStationStates(slaveData, ref stationStates);
                var lastStationState = stationStates.Last();
                var slaveDataToExclude = slaveData.Where(sD => sD.SlaveId == lastStationState.SlaveId &&
                    sD.DatStart >= lastStationState.DateStart && sD.DatEnd <= lastStationState.DateEnd);
                slaveData = slaveData.Except(slaveDataToExclude);
            }

            return stationStates;
        }

        private void AddStationStates(IEnumerable<SlaveData> slaveData, ref List<StationStatus> stationStates)
        {
            var firstState = slaveData.Where(sD => !string.Concat(sD.Channel1, sD.Channel2, sD.Channel3).Equals("000")).OrderBy(sD => sD.DatStart).First();
            int slave = firstState.SlaveId;
            var actualState = firstState;
            while (slaveData.Any(sD => sD.SlaveId == slave && sD.DatStart.Equals(actualState.DatEnd) &&
                !string.Concat(sD.Channel1, sD.Channel2, sD.Channel3).Equals("000")))
            {
                actualState = slaveData.First(sD => sD.SlaveId == slave && sD.DatStart.Equals(actualState.DatEnd));
            }
            var closedSlaveData = slaveData.FirstOrDefault(sD => sD.SlaveId == slave && sD.DatStart.Equals(actualState.DatEnd));

            bool closedState = closedSlaveData != null;
            stationStates.Add(new StationStatus()
            {
                Id = stationStates.Count + 1,
                SlaveId = slave,
                Station = actualState.Name,
                DateStart = firstState.DatStart,
                DateEnd = actualState.DatEnd,
                Closed = closedState,
                BottomVisibility = !closedState && actualState.Channel1 > 0,
                CenterVisibility = !closedState && actualState.Channel2 > 0,
                TopVisibility = !closedState && actualState.Channel3 > 0
            });
        }

        public void InspectStationStates(ref IEnumerable<StationStatus> stationStates, ref IEnumerable<ScheduleStations> schedules)
        {
            foreach (var stationState in stationStates)
            {
                stationState.ScopesStations = stationStates.Where(sS => sS.SlaveId != stationState.SlaveId && ((sS.DateStart >= stationState.DateStart && sS.DateStart < stationState.DateEnd) ||
                    (sS.DateEnd >= stationState.DateStart && sS.DateEnd < stationState.DateEnd)))
                    .Select(sS => sS);
            }

            while (stationStates.Any())
            {
                ScheduleStations schedule = new ScheduleStations()
                {
                    Closed = false,
                    IdStationStates = new List<int>(),
                    StationStates = new List<StationStatus>()
                };
                PreOrderStationStateFetch(stationStates.First(), ref schedule);
                schedule.Closed = schedule.StationStates.All(sS => sS.Closed);
                schedules = schedules.Append(schedule);
                stationStates = stationStates.Except(schedule.StationStates);
            }
        }

        private void PreOrderStationStateFetch(StationStatus stationState, ref ScheduleStations schedule)
        {
            if (schedule.IdStationStates.Contains(stationState.Id))
            {
                return;
            }
            schedule.IdStationStates = schedule.IdStationStates.Append(stationState.Id);
            schedule.StationStates = schedule.StationStates.Append(stationState);
            foreach (var sS in stationState.ScopesStations)
            {
                PreOrderStationStateFetch(sS, ref schedule);
            }
        }
    }
}
