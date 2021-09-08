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
        private readonly OrderService _orderService;
        private readonly StationState[] stationStates;
        private readonly Timer intervalUpdate;
        private readonly decimal minutesSchedule = 516;

        private CountsViewModel countsViewModel = null;

        public MainWindow(StationStateService stationStateService, OrderService orderService)
        {
            InitializeComponent();
            //stationStateService = new StationStateService();
            stationStates = stationStateService.GetStationStates();
            foreach(var stationState in stationStates)
            {
                StationStatePanel.Children.Add(new StationStateTemplate(stationState.Station, stationState.Color).panel);
            }
            _orderService = orderService;
            //var order = orderService.GetFirst(2038);
            var order = orderService.GetActualOrder();
            UpdatingCountsViewModel(order);

            intervalUpdate = new Timer()
            {
                Interval = 5000,
                Enabled = true,
                AutoReset = true
            };
            intervalUpdate.Elapsed += OnIntervalElapsed;
            
            DataContext = countsViewModel;
        }

        private void OnIntervalElapsed(Object source, ElapsedEventArgs e)
        {
            UpdatingCountsViewModel(_orderService.GetActualOrder());
        }

        private void UpdatingCountsViewModel(Order order)
        {
            if (countsViewModel == null)
            {
                countsViewModel = new CountsViewModel();
            }
            countsViewModel.Plan = order.TargetAmount;
            countsViewModel.Real = order.RealSignals;
            //countsViewModel.TaktTime = new DateTime(new TimeSpan(order.TargetEndTime.Hour - order.TargetBeginTime.Hour, order.TargetEndTime.Minute - order.TargetBeginTime.Minute, order.TargetEndTime.Second - order.TargetBeginTime.Second).Ticks);
            countsViewModel.TaktTime = new DateTime(new TimeSpan(0, 0, (int)(minutesSchedule / countsViewModel.Plan * 60)).Ticks);
            countsViewModel.TotalStopTime = new DateTime(new TimeSpan(order.RealEndTime.Hour - order.RealBeginTime.Hour, order.RealEndTime.Minute - order.RealBeginTime.Minute, order.RealEndTime.Second - order.RealBeginTime.Second).Ticks);
            countsViewModel.StationsStopTime = new DateTime(new TimeSpan(order.TargetEndTime.Hour, order.TargetEndTime.Minute, order.TargetEndTime.Second).Ticks);
        }
    }
}
