using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace dashboard.Service
{
    public abstract class WermawinService
    {
        public string connectionString;
        public WermawinService(IConfiguration Configuration)
        {
            connectionString = Configuration.GetConnectionString("uno");
        }
    }
}
