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
        public StationStateView(string station, bool topVisibility, bool centerVisibility, bool bottomVisibility)
        {
            InitializeComponent();

            DataContext = new
            {
                Station = station,
                TopVisibility = SetVisibility(topVisibility),
                CenterVisibility = SetVisibility(centerVisibility),
                BottomVisibility = SetVisibility(bottomVisibility)
            };
        }

        public Visibility SetVisibility(bool isVisible)
        {
            return isVisible ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
