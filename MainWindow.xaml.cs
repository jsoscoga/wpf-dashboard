using dashboard.Model;
using dashboard.Objects;
using dashboard.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CountsViewModel countsViewModel;
        private readonly StationState[] stationStates;
        private readonly Timer intervalUpdate;

        public MainWindow(StationStateService stationStateService, OrderService orderService)
        {
            InitializeComponent();
            //stationStateService = new StationStateService();
            stationStates = stationStateService.GetStationStates();
            foreach(var stationState in stationStates)
            {
                StationStatePanel.Children.Add(new StationStateTemplate(stationState.Station, stationState.Color).panel);
            }
            var order = orderService.GetFirst(2038);
            countsViewModel = new CountsViewModel()
            {
                Plan = order.OrderState,
                Real = order.SlaveId,
                TaktTime = new DateTime(new TimeSpan(order.TargetEndTime.Hour - order.TargetBeginTime.Hour, order.TargetEndTime.Minute - order.TargetBeginTime.Minute, order.TargetEndTime.Second - order.TargetBeginTime.Second).Ticks),
                TotalStopTime = new DateTime(new TimeSpan(order.RealEndTime.Hour - order.RealBeginTime.Hour, order.RealEndTime.Minute - order.RealBeginTime.Minute, order.RealEndTime.Second - order.RealBeginTime.Second).Ticks),
                StationsStopTime = new DateTime(new TimeSpan(order.TargetEndTime.Hour, order.TargetEndTime.Minute, order.TargetEndTime.Second).Ticks)
            };

            intervalUpdate = new Timer()
            {
                Interval = 1000,
                Enabled = true,
                AutoReset = true
            };
            intervalUpdate.Elapsed += OnIntervalElapsed;
            
            DataContext = countsViewModel;
        }

        private void OnIntervalElapsed(Object source, ElapsedEventArgs e)
        {
            countsViewModel.Plan++;
            countsViewModel.Real++;
            countsViewModel.StationsStopTime = countsViewModel.StationsStopTime.AddSeconds(1);
            countsViewModel.TaktTime = countsViewModel.TaktTime.AddSeconds(1);
            countsViewModel.TotalStopTime = countsViewModel.TotalStopTime.AddSeconds(1);
        }
    }
}
