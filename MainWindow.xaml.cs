using dashboard.Model;
using dashboard.Service;
using dashboard.Views;
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
        private readonly SlaveDataService _slaveDataService;
        private readonly StationStateService _stationStateService;

        private Timer intervalUpdate;
        private const decimal MINUTESSCHEDULE = 516;
        // Interval of 30 seg is mandatory
        private const int SECONDSFORINTERVAL = 10;

        private CountsViewModel countsViewModel = new CountsViewModel();

        public MainWindow(StationStateService stationStateService, OrderService orderService, SlaveDataService slaveDataService)
        {
            InitializeComponent();

            _stationStateService = stationStateService;
            _orderService = orderService;
            _slaveDataService = slaveDataService;

            InitializeData();
        }

        private void InitializeData()
        {
            if (_orderService.OrderExists())
            {
                UpdatingCountsViewModel();
                DataContext = countsViewModel;
            }

            intervalUpdate = new Timer()
            {
                Interval = SECONDSFORINTERVAL * 1000,
                Enabled = true,
                AutoReset = true
            };
            intervalUpdate.Elapsed += OnIntervalElapsed;
        }

        private void OnIntervalElapsed(Object source, ElapsedEventArgs e)
        {
            if (_orderService.OrderExists())
            {
                UpdatingCountsViewModel();
            }
        }

        private void UpdatingCountsViewModel()
        {
            Dispatcher.Invoke(() => UpdatingText.Visibility = Visibility.Visible);

            var order = _orderService.GetActualOrder();
            countsViewModel.Plan = order.TargetAmount;
            countsViewModel.Real = order.RealSignals;
            countsViewModel.TaktTime = new DateTime(new TimeSpan(0, 0, (int)(MINUTESSCHEDULE / countsViewModel.Plan * 60)).Ticks);

            var slaveData = _slaveDataService.GetByActualDate();
            int totalDuration = _slaveDataService.GetDurationSum(slaveData);
            countsViewModel.TotalStopTime = new DateTime(new TimeSpan(0, 0, totalDuration).Ticks);

            var stationStates = _stationStateService.GetFromSlaveData(slaveData);
            if (stationStates.Count > 0)
            {
                if (stationStates.Any(sD => !sD.Closed))
                {
                    var openStationStates = stationStates.Where(sD => !sD.Closed);
                    DateTime dateStart = openStationStates.Min(sD => sD.DateStart);
                    DateTime dateEnd = openStationStates.Max(sD => sD.DateEnd);
                    TimeSpan timeDiff = dateEnd - dateStart;
                    countsViewModel.StationsStopTime = new DateTime(timeDiff.Ticks);
                }
                Dispatcher.Invoke(() => StationStatePanel.Children.Clear());
                foreach (var stationState in stationStates)
                {
                    if (!stationState.Closed)
                    {
                        Dispatcher.Invoke(() => StationStatePanel.Children.Add(new StationStateView(stationState.Station, stationState.TopVisibility, stationState.CenterVisibility, stationState.BottomVisibility)));
                    }
                }
            }
            

            Dispatcher.Invoke(() => UpdatingText.Visibility = Visibility.Hidden);
        }
    }
}
