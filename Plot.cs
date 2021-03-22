using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuroTraining
{
    public class Xaxis
    {
        public Path xaxis_path { get; set; }
        public Xaxis(double margin, double width, double height, int count)
        {
            double xmin = margin;
            double xmax = width - margin;
            double ymin = height - margin;
            double stepX = 0;
            if (count > 1)
            {
                stepX = (width - 2 * margin) / (count - 1);
            }
            else if (count == 1)
            {
                stepX = (width - 2 * margin) / count;
            }
            GeometryGroup xaxis_geom = new GeometryGroup();
            xaxis_geom.Children.Add(new LineGeometry(new System.Windows.Point(xmin, ymin), new System.Windows.Point(xmax, ymin)));
            for (double x = xmin; x <= xmax; x += stepX)
            {
                xaxis_geom.Children.Add(new LineGeometry(new System.Windows.Point(x, ymin), new System.Windows.Point(x, ymin + 0.5 * margin)));
            }
            Path xaxis_path_Tmp = new Path();
            xaxis_path_Tmp.StrokeThickness = 0.5;
            xaxis_path_Tmp.Stroke = Brushes.Black;
            xaxis_path_Tmp.Data = xaxis_geom;
            xaxis_path = xaxis_path_Tmp;
        }
    }
    public class YaxisSchulter
    {
        public Path yaxis_path { get; set; }
        public YaxisSchulter(double margin, double height, int count)
        {
            double xmin = margin;
            double ymin = height - margin;
            double ymax = margin;
            double range = height - 2 * margin;
            double stepY = 0;
            if (count > 1)
            {
                stepY = range / 10.5;
            }
            else if (count == 1)
            {
                stepY = range / (10.5 - 1);
            }
            GeometryGroup yaxis_geom = new GeometryGroup();
            yaxis_geom.Children.Add(new LineGeometry(new System.Windows.Point(xmin, ymin), new System.Windows.Point(xmin, ymax)));
            for (double y = ymin; y >= ymax; y -= stepY)
            {
                yaxis_geom.Children.Add(new LineGeometry(new System.Windows.Point(xmin - 0.5 * margin, y), new System.Windows.Point(xmin, y)));
            }
            Path yaxis_path_Tmp = new Path();
            yaxis_path_Tmp.StrokeThickness = 0.5;
            yaxis_path_Tmp.Stroke = Brushes.Black;
            yaxis_path_Tmp.Data = yaxis_geom;
            yaxis_path = yaxis_path_Tmp;
        }
    }
    public class YaxisRememberNumber
    {
        public Path yaxis_path { get; set; }
        public YaxisRememberNumber(double margin, double height)
        {
            double xmin = margin;
            double ymin = height - margin;
            double ymax = margin;
            double range = height - 2 * margin;
            double stepY = range / 10;
            GeometryGroup yaxis_geom = new GeometryGroup();
            yaxis_geom.Children.Add(new LineGeometry(new System.Windows.Point(xmin, ymin), new System.Windows.Point(xmin, ymax)));
            for (double y = ymin; y >= ymax; y -= stepY)
            {
                yaxis_geom.Children.Add(new LineGeometry(new System.Windows.Point(xmin - 0.5 * margin, y), new System.Windows.Point(xmin, y)));
            }
            Path yaxis_path_Tmp = new Path();
            yaxis_path_Tmp.StrokeThickness = 0.5;
            yaxis_path_Tmp.Stroke = Brushes.Black;
            yaxis_path_Tmp.Data = yaxis_geom;
            yaxis_path = yaxis_path_Tmp;
        }
    }
    public class Plot
    {
        public Polyline polyline { get; set; }
        public List<ResultPoint> ResultPoints { get; set; }
        public void MakePlotElements(double margin, double Width, double Height, List<int> Results)
        {
            List <ResultPoint> ResultPointsTmp = new List<ResultPoint>();
            List<int> l = new List<int>(Results);
            l.Sort();
            double Max = l[l.Count - 1];
            var Results1 = new List<ResultPoint>();
            double xmin = margin;
            double ymax = margin;
            double range = Height - 2 * margin;
            double factor = Max * 1.05;
            double stepX = 0;
            if (Results.Count > 1)
            {
                stepX = (Width - 2 * margin) / (Results.Count() - 1);
            }
            else if (Results.Count == 1)
            {
                stepX = (Width - 2 * margin) / Results.Count();
            }

            //Data set (draw points)
            for (int i = 0; i < Results.Count; i++)
            {
                var point = new ResultPoint(stepX * i + margin, ymax + range - Results[i] * range / factor);
                ResultPointsTmp.Add(point);
                //List of points for the real line
                var res = new ResultPoint(i, Results[i]);
                Results1.Add(res);
            }
            ResultPoints = new List<ResultPoint>(ResultPointsTmp);

            //Draw the real line
            if (Results1 != null)
            {
                PointCollection linePoints = new PointCollection();
                for (int i = 0; i < Results1.Count; i++)
                {
                    linePoints.Add(new System.Windows.Point(stepX * Results1[i].X + xmin, ymax + range - Results1[i].Y * range / factor));
                }
                polyline = new Polyline();
                polyline.StrokeThickness = 0.5;
                polyline.Stroke = Brushes.Blue;
                polyline.Points = linePoints;
            }
        }
    }
}
