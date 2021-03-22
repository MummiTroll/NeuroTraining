using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuroTraining
{
    public class MyEllipse
    {
        public Action<object, MouseButtonEventArgs> MouseDown;
        public Ellipse ellipse = new Ellipse
        {
            Fill = Brushes.DarkOrange,
        };
        public UIElement UiElement { get; set; }
        public string Name { get; set; }
        public Point coordinates { get; set; }
        public double dX { get; set; } = 1;
        public double dY { get; set; } = 1;
        public double aX { get; set; } = 1;
        public double aY { get; set; } = 1;
        public MyEllipse()
        {
            UiElement = ellipse;
        }
    }
}
