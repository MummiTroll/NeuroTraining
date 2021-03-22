using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuroTraining
{
    public class ResultPoint
    {
        public UIElement UiElement = new Ellipse
        {
            Width = 3,
            Height = 3,
            Fill = Brushes.Blue
        };
        public double X { get; set; }
        public double Y { get; set; }
        public ResultPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
