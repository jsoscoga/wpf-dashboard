using dashboard.Model;
using dashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dashboard.Views
{
    /// <summary>
    /// Interaction logic for StationStateView.xaml
    /// </summary>
    public partial class StationStateView : UserControl
    {
        public StationStateViewModel _stationStateViewModel;

        public StationStateView(StationState stationState)
        {
            _stationStateViewModel = new StationStateViewModel()
            {
                Station = stationState.Station,
                HigherVisibility = stationState.HigherVisibility,
                TopVisibility = stationState.TopVisibility,
                CenterVisibility = stationState.CenterVisibility,
                BottomVisibility = stationState.BottomVisibility
            };
            InitializingComponent();
        }
        public StationStateView(string station, bool topVisibility, bool centerVisibility, bool bottomVisibility)
        {
            _stationStateViewModel = new StationStateViewModel() {
                Station = station,
                HigherVisibility = StationStateViewModel.SetVisibility(false),
                TopVisibility = StationStateViewModel.SetVisibility(topVisibility),
                CenterVisibility = StationStateViewModel.SetVisibility(centerVisibility),
                BottomVisibility = StationStateViewModel.SetVisibility(bottomVisibility)
            };
            InitializingComponent();
        }

        public StationStateView(string station, Visibility higherVisibility, Visibility topVisibility, Visibility centerVisibility, Visibility bottomVisibility)
        {
            _stationStateViewModel = new StationStateViewModel()
            {
                Station = station,
                HigherVisibility = higherVisibility,
                TopVisibility = topVisibility,
                CenterVisibility = centerVisibility,
                BottomVisibility = bottomVisibility,
            };
            InitializingComponent();
        }

        public void InitializingComponent()
        {
            InitializeComponent();
            DataContext = _stationStateViewModel;
        }
    }
}
