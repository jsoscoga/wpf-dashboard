using dashboard.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace dashboard.ViewModels
{
    public class StationStateViewModel : INotifyPropertyChanged
    {
        private StationState stationState;

        public string Station
        {
            get { return stationState.Station; }
            set
            {
                if (stationState.Station != value)
                {
                    stationState.Station = value;
                    OnPropertyChange("Station");
                }
            }
        }
        public Visibility HigherVisibility
        {
            get { return stationState.HigherVisibility; }
            set
            {
                if (stationState.HigherVisibility != value)
                {
                    stationState.HigherVisibility = value;
                    OnPropertyChange("HigherVisibility");
                }
            }
        }
        public Visibility TopVisibility 
        {
            get { return stationState.TopVisibility; }
            set
            {
                if (stationState.TopVisibility != value)
                {
                    stationState.TopVisibility = value;
                    OnPropertyChange("TopVisibility");
                }
            }
        }
        public Visibility CenterVisibility
        {
            get { return stationState.CenterVisibility; }
            set
            {
                if (stationState.CenterVisibility != value)
                {
                    stationState.CenterVisibility = value;
                    OnPropertyChange("CenterVisibility");
                }
            }
        }
        public Visibility BottomVisibility
        {
            get { return stationState.BottomVisibility; }
            set
            {
                if (stationState.BottomVisibility != value)
                {
                    stationState.BottomVisibility = value;
                    OnPropertyChange("BottomVisibility");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public StationStateViewModel()
        {
            stationState = new StationState();
        }

        public static Visibility SetVisibility(bool isVisible)
        {
            return isVisible ? Visibility.Visible : Visibility.Hidden;
        }

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
