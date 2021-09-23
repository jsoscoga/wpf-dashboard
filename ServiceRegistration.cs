using dashboard.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace dashboard
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<StationStateService>();
            services.AddTransient<StationStatusService>();
            services.AddTransient<OrderService>();
            services.AddTransient<SlaveDataService>();
        }
    }
}
