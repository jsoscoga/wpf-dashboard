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
            string query = @"SELECT id,slaveId,datStart,datEnd,duration,channel1,channel2,channel3,channel4,error
	            FROM dbo.slaveData 
	            where datStart between @actualDate and @nextDate
                and CONCAT(channel1, channel2, channel3) != '000'
	            ORDER BY slaveId, datStart";

            using (var connection = new SqlConnection(connectionString))
            {
                var result = connection.Query<SlaveData>(query, new { actualDate = DateTime.Now.ToString("yyyy-MM-dd"), nextDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") });
                return result.AsList();
            }
        }

        public int GetDurationSum(IEnumerable<SlaveData> slaveData) => slaveData.Sum(sD => sD.Duration);
    }
}
