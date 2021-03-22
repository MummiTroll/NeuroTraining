using System;
using System.Windows;
using System.Windows.Controls;

namespace NeuroTraining
{
    public class MyTextBox : TextBox
    {
        public MyTextBox()
        {
            Width = 18;
            Height = 32;
            FontSize = 20;
            VerticalContentAlignment = VerticalAlignment.Center;
            HorizontalContentAlignment = HorizontalAlignment.Center;
        }
    }
}
