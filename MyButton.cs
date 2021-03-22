using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NeuroTraining
{
    public class MyButton : Button
    {
        const double f = 12.227;
        const double g = -25.133;
        public MyButton(int fieldSize, double width, double height)
        {
            Width = width / fieldSize;
            Height = height / fieldSize;
            BorderThickness = new Thickness(1);
            FontSize = (int)(Math.Log(height / fieldSize) * f + g);
            Background = Brushes.White;
        }
    }
    public class MyButtonEven : Button
    {
        public MyButtonEven(int Rows, int Columns, double width, double height)
        {
            Width = width / Columns;
            Height = height / Rows;
            BorderThickness = new Thickness(0.5);
            FontSize = 14;
            Background = Brushes.White;
        }
    }
}
