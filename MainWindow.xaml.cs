﻿using dashboard.Model;
using dashboard.Objects;
using dashboard.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private StationStateService stationStateService;
        private readonly StationState[] stationStates;

        public MainWindow()
        {
            InitializeComponent();
            countsViewModel = new CountsViewModel();
            stationStateService = new StationStateService();
            stationStates = stationStateService.GetStationStates();
            foreach(var stationState in stationStates)
            {
                StationStatePanel.Children.Add((new StationStateTemplate(stationState.Station, stationState.Color)).panel);
            }
            //StationStatePanel.Children.Add(new TextBlock() { Text = "E1", Foreground = Brushes.White, FontSize = 25 });
            
            DataContext = countsViewModel;
        }
    }
}