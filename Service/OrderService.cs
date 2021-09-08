using Dapper;
using dashboard.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace dashboard.Service
{
    public class OrderService
    {
        private string connectionString;
        public OrderService(IConfiguration Configuration)
        {
            connectionString = Configuration.GetConnectionString("uno");
        }

        public Order GetFirst(int id)
        {
            string query = @"SELECT id,reference,description,slaveId,orderState,piecesPerSignal,timePerSignal,
                            targetBeginTime,targetEndTime,targetAmount,targetSetupTime,realBeginTime,realEndTime,realSignals,autoStopTime 
                            FROM [WERMAWIN].[dbo].[order]
                            where id = @id";
            Order order;

            using (var connection = new SqlConnection(connectionString))
            {
                order = connection.QueryFirstOrDefault<Order>(query, new { id = id });
            }

            return order;
        }

        public Order GetActualOrder()
        {
            string query = @"SELECT id,reference,description,slaveId,orderState,piecesPerSignal,timePerSignal,
                            targetBeginTime,targetEndTime,targetAmount,targetSetupTime,realBeginTime,realEndTime,realSignals,autoStopTime 
                            FROM [WERMAWIN].[dbo].[order]
                            where FORMAT(targetBeginTime, 'yyyy-MM-dd') = @actualDate";
            Order order;

            using (var connection = new SqlConnection(connectionString))
            {
                order = connection.QueryFirstOrDefault<Order>(query, new { actualDate = DateTime.Now.ToString("yyyy-MM-dd") });
            }

            return order;
        }
    }
}
