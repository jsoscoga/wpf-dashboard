using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace dashboard.Objects
{
    public class StationStateTemplate
    {
        public StackPanel panel;
        private TextBlock station;
        private Border color;
        public StationStateTemplate(string name, string color)
        {
            CreatePanel();
            CreateObjectsInPanel(name, color);
        }

        private void CreateObjectsInPanel(string name, string color)
        {
            panel.Children.Add(new TextBlock()
            {
                Text = name,
                HorizontalAlignment = HorizontalAlignment.Center,
                Foreground = Brushes.White,
                FontSize = 25
            });
            panel.Children.Add(new Border()
            {
                BorderBrush = GetColorBrush(color),
                BorderThickness = new Thickness(50, 80, 50, 80)
            });
        }

        private SolidColorBrush GetColorBrush(string color)
        {
            SolidColorBrush colorToReturn = new SolidColorBrush();
            switch(color)
            {
                case "OrangeRed":
                    colorToReturn = Brushes.OrangeRed;
                    break;
                case "Green":
                    colorToReturn = Brushes.Green;
                    break;
                case "Blue":
                    colorToReturn = Brushes.Blue;
                    break;
                case "Yellow":
                    colorToReturn = Brushes.Yellow;
                    break;
            }

            return colorToReturn;
        }

        private void CreatePanel()
        {
            panel = new StackPanel()
            {
                Orientation = Orientation.Vertical
            };
            
        }
    }
}
