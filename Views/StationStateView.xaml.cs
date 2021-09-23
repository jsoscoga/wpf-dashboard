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
        public StationStateViewModel stationStateViewModel;
        public StationStateView(string station, bool topVisibility, bool centerVisibility, bool bottomVisibility)
        {
            stationStateViewModel = new StationStateViewModel() {
                Station = station,
                HigherVisibility = StationStateViewModel.SetVisibility(false),
                TopVisibility = StationStateViewModel.SetVisibility(topVisibility),
                CenterVisibility = StationStateViewModel.SetVisibility(centerVisibility),
                BottomVisibility = StationStateViewModel.SetVisibility(bottomVisibility)
            };
            InitializeComponent();
            DataContext = stationStateViewModel;
        }
    }
}
