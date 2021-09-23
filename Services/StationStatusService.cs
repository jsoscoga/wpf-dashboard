using dashboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dashboard.Service
{
    public class StationStatusService
    {
        public StationStatus[] GetDummyStationStatuses()
        {
            StationStatus[] stationStatuss = new StationStatus[]
            {
                //new StationStatus() { Color = "OrangeRed", Station = "E1"},
                //new StationStatus() { Color = "Green", Station = "E2"},
                //new StationStatus() { Color = "Blue", Station = "E3"},
                //new StationStatus() { Color = "Yellow", Station = "E4"},
                //new StationStatus() { Color = "OrangeRed", Station = "E5"},
                //new StationStatus() { Color = "Green", Station = "E6"},
                //new StationStatus() { Color = "Blue", Station = "E7"},
            };

            return stationStatuss;
        }

        public IEnumerable<StationStatus> GetFromSlaveData(IEnumerable<SlaveData> slaveData)
        {
            List<StationStatus> stationStatuses = new List<StationStatus>();

            while (slaveData.Any(sD => !string.Concat(sD.Channel1, sD.Channel2, sD.Channel3).Equals("000")))
            {
                AddStationStatuses(slaveData, ref stationStatuses);
                var lastStationStatus = stationStatuses.Last();
                var slaveDataToExclude = slaveData.Where(sD => sD.SlaveId == lastStationStatus.SlaveId &&
                    sD.DatStart >= lastStationStatus.DateStart && sD.DatEnd <= lastStationStatus.DateEnd);
                slaveData = slaveData.Except(slaveDataToExclude);
            }

            return stationStatuses;
        }

        public void InspectStationStatuses(ref IEnumerable<StationStatus> stationStatuses, ref IEnumerable<ScheduleStations> schedules)
        {
            foreach (var stationStatus in stationStatuses)
            {
                stationStatus.ScopesStations = stationStatuses.Where(sS => sS.SlaveId != stationStatus.SlaveId && ((sS.DateStart >= stationStatus.DateStart && sS.DateStart < stationStatus.DateEnd) ||
                    (sS.DateEnd >= stationStatus.DateStart && sS.DateEnd < stationStatus.DateEnd)))
                    .Select(sS => sS);
            }

            while (stationStatuses.Any())
            {
                ScheduleStations schedule = new ScheduleStations()
                {
                    Closed = false,
                    IdStationStatuses = new List<int>(),
                    StationStatuses = new List<StationStatus>()
                };
                PreOrderStationStatusFetch(stationStatuses.First(), ref schedule);
                schedule.Closed = schedule.StationStatuses.All(sS => sS.Closed);
                schedules = schedules.Append(schedule);
                stationStatuses = stationStatuses.Except(schedule.StationStatuses);
            }
        }

        private void PreOrderStationStatusFetch(StationStatus stationStatus, ref ScheduleStations schedule)
        {
            if (schedule.IdStationStatuses.Contains(stationStatus.Id))
            {
                return;
            }
            schedule.IdStationStatuses = schedule.IdStationStatuses.Append(stationStatus.Id);
            schedule.StationStatuses = schedule.StationStatuses.Append(stationStatus);
            foreach (var sS in stationStatus.ScopesStations)
            {
                PreOrderStationStatusFetch(sS, ref schedule);
            }
        }

        private void AddStationStatuses(IEnumerable<SlaveData> slaveData, ref List<StationStatus> stationStatuses)
        {
            var firstStatus = slaveData.Where(sD => !string.Concat(sD.Channel1, sD.Channel2, sD.Channel3).Equals("000")).OrderBy(sD => sD.DatStart).First();
            int slave = firstStatus.SlaveId;
            var actualStatus = firstStatus;
            while (slaveData.Any(sD => sD.SlaveId == slave && sD.DatStart.Equals(actualStatus.DatEnd) &&
                !string.Concat(sD.Channel1, sD.Channel2, sD.Channel3).Equals("000")))
            {
                actualStatus = slaveData.First(sD => sD.SlaveId == slave && sD.DatStart.Equals(actualStatus.DatEnd));
            }
            var closedSlaveData = slaveData.FirstOrDefault(sD => sD.SlaveId == slave && sD.DatStart.Equals(actualStatus.DatEnd));

            bool closedStatus = closedSlaveData != null;
            stationStatuses.Add(new StationStatus()
            {
                Id = stationStatuses.Count + 1,
                SlaveId = slave,
                Station = actualStatus.Name,
                DateStart = firstStatus.DatStart,
                DateEnd = actualStatus.DatEnd,
                Closed = closedStatus,
                BottomVisibility = !closedStatus && actualStatus.Channel1 > 0,
                CenterVisibility = !closedStatus && actualStatus.Channel2 > 0,
                TopVisibility = !closedStatus && actualStatus.Channel3 > 0
            });
        }
    }
}
