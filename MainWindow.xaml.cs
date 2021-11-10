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
        private readonly StationStatusService _stationStatusService;

        private Timer intervalUpdate;
        private const decimal MINUTESSCHEDULE = 516;

        private CountsViewModel countsViewModel = new CountsViewModel();

        public MainWindow(IConfiguration configuration, StationStateService stationStateService, StationStatusService stationStatusService, OrderService orderService, SlaveDataService slaveDataService)
        {
            InitializeComponent();

            _stationStateService = stationStateService;
            _stationStatusService = stationStatusService;
            _orderService = orderService;
            _slaveDataService = slaveDataService;

            InitializeData(configuration);
        }

        private void InitializeData(IConfiguration configuration)
        {
            intervalUpdate = new Timer()
            {
                Interval = int.Parse(configuration["SecondsForInterval"]) * 1000,
                Enabled = true,
                AutoReset = true
            };
            intervalUpdate.Elapsed += OnIntervalElapsed;
        }

        private void OnIntervalElapsed(Object source, ElapsedEventArgs e)
        {
            try
            {
                if (_orderService.OrderExists())
                {
                    UpdatingCountsViewModel();
                    Dispatcher.Invoke(() => DataContext = countsViewModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en el programa. --" + ex.Message, "Error en el dashboard", MessageBoxButton.OK);
                intervalUpdate.Stop();
                Application.Current.MainWindow.Close();
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

            var stationStatuses = _stationStatusService.GetFromSlaveData(slaveData);
            var stationStatusesGreen = _stationStateService.GetGreenFromSlaveData(slaveData);
            ApplyingStationStateViewsColors(stationStatuses, stationStatusesGreen);

            Dispatcher.Invoke(() => UpdatingText.Visibility = Visibility.Hidden);
        }
        private void ApplyingStationStateViewsColors(IEnumerable<StationStatus> stationStatuses, IEnumerable<StationState> stationStatusesGreen)
        {
            Dispatcher.Invoke(() => StationStatePanel.Children.Clear());
            if (stationStatuses.Any())
            {
                IEnumerable<ScheduleStations> schedules = new List<ScheduleStations>();
                _stationStatusService.InspectStationStatuses(ref stationStatuses, ref schedules);
                if (schedules.Any(sD => !sD.Closed))
                {
                    var openStationStatuses = schedules.First(s => !s.Closed).StationStatuses;
                    DateTime dateStart = openStationStatuses.Min(sD => sD.DateStart);
                    DateTime dateEnd = openStationStatuses.Max(sD => sD.DateEnd);
                    TimeSpan timeDiff = dateEnd - dateStart;
                    countsViewModel.StationsStopTime = new DateTime(timeDiff.Ticks);

                    foreach (var stationStatus in openStationStatuses)
                    {
                        if (!stationStatus.Closed)
                        {
                            Dispatcher.Invoke(() => StationStatePanel.Children.Add(new StationStateView(stationStatus.Station, stationStatus.TopVisibility, stationStatus.CenterVisibility, stationStatus.BottomVisibility)));
                        }
                    }
                }
                else
                {
                    countsViewModel.StationsStopTime = new DateTime(0);
                }
            }
            if (stationStatusesGreen.Any())
            {
                foreach (var stationStatusGreen in stationStatusesGreen)
                {
                    StationStateView stationStateView = null;
                    Dispatcher.Invoke(() => stationStateView = StationStatePanel.Children.Cast<StationStateView>().FirstOrDefault(sSV => sSV._stationStateViewModel.Station.Equals(stationStatusGreen.Station)));
                    if (stationStateView == null)
                    {
                        Dispatcher.Invoke(() => StationStatePanel.Children.Add(new StationStateView(stationStatusGreen)));
                    }
                    else
                    {
                        Dispatcher.Invoke(() => stationStateView._stationStateViewModel.HigherVisibility = stationStatusGreen.HigherVisibility);
                    }
                }
            }
        }
    }
}
