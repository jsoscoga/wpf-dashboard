using Dapper;
using dashboard.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace dashboard.Service
{
    public class SlaveDataService : WermawinService
    {
        public SlaveDataService(IConfiguration Configuration) : base(Configuration)
        {
        }

        public IReadOnlyList<SlaveData> GetByActualDate()
        {
            string query = @"SELECT d.id,d.slaveId,dev.name,dev.macId,d.datStart,d.datEnd,d.duration,d.channel1,d.channel2,d.channel3,d.channel4,d.error
                FROM dbo.slaveData d
                LEFT JOIN slaveDevice dev ON dev.id = d.slaveId
                WHERE d.datStart BETWEEN @actualDate AND @nextDate
                AND dev.macId in @macIds
                AND d.error = 0
                ORDER BY d.datStart";

            using (var connection = new SqlConnection(connectionString))
            {
                var result = connection.Query<SlaveData>(query, new { actualDate = DateTime.Now.ToString("yyyy-MM-dd"), nextDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"), macIds = GetMacIds() });
                return result.AsList();
            }
        }

        public int GetDurationSum(IEnumerable<SlaveData> slaveData) => slaveData.Where(sD => !string.Concat(sD.Channel1, sD.Channel2, sD.Channel3).Equals("000")).Sum(sD => sD.Duration);
    }
}
