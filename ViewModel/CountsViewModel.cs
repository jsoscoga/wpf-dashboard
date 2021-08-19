using dashboard.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace dashboard
{
    sealed class CountsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Counts counts;

        public int Plan { 
            get { return counts.Plan; }
            set
            {
                if (counts.Plan != value)
                {
                    counts.Plan = value;
                    OnPropertyChange("Plan");
                }
            }
        }
        public int TaktTime
        {
            get { return counts.TaktTime; }
            set
            {
                if (counts.TaktTime != value)
                {
                    counts.TaktTime = value;
                    OnPropertyChange("TaktTime");
                }
            }
        }
        public int Real { 
            get { return counts.Real; }
            set
            {
                if (counts.Real != value)
                {
                    counts.Real = value;
                    OnPropertyChange("Real");
                }
            }
        }
        public int TotalStopTime { 
            get { return counts.TotalStopTime; }
            set
            {
                if (counts.TotalStopTime != value)
                {
                    counts.TotalStopTime = value;
                    OnPropertyChange("TotalStopTime");
                }
            }
        }

        public CountsViewModel()
        {
            counts = new Counts
            {
                Plan = 0,
                TaktTime = 0,
                Real = 0,
                TotalStopTime = 0
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
