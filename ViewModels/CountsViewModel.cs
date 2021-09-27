using dashboard.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace dashboard
{
    public class CountsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Counts counts;

        private List<StationStatus> stationStates;

        public decimal Plan { 
            get { return counts.Plan; }
            set
            {
                counts.Plan = value;
                OnPropertyChange("Plan");
            }
        }
        public DateTime TaktTime
        {
            get { return counts.TaktTime; }
            set
            {
                counts.TaktTime = value;
                OnPropertyChange("TaktTime");
            }
        }
        public decimal Real { 
            get { return counts.Real; }
            set
            {
                counts.Real = value;
                OnPropertyChange("Real");
            }
        }
        public DateTime TotalStopTime { 
            get { return counts.TotalStopTime; }
            set
            {
                counts.TotalStopTime = value;
                OnPropertyChange("TotalStopTime");
            }
        }

        public DateTime StationsStopTime
        {
            get { return counts.StationsStopTime; }
            set
            {
                counts.StationsStopTime = value;
                OnPropertyChange("StationsStopTime");
            }
        }

        public List<StationStatus> StationStates 
        { 
            get => stationStates;
            set
            {
                stationStates = value;
                OnPropertyChange("stationStates");
            }
        }

        public CountsViewModel()
        {
            counts = new Counts
            {
                Plan = 0,
                TaktTime = new DateTime(0),
                Real = 0,
                TotalStopTime = new DateTime(0),
                StationsStopTime = new DateTime(0)
            };
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
