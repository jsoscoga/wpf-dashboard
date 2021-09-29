using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace dashboard.Service
{
    public abstract class WermawinService
    {
        public string connectionString;
        public string macIds;
        public WermawinService(IConfiguration Configuration)
        {
            connectionString = Configuration.GetConnectionString("uno");
            macIds = Configuration["MacIds"];
        }

        public IEnumerable<string> GetMacIds() => macIds.Split(",");
    }
}
