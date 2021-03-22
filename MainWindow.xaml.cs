using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using NeuroTraining.ViewModels;
using NeuroTraining.Enums;

namespace NeuroTraining
{
    public partial class MainWindow : Window
    {
        #region Objects
        public MainViewModel MainViewModel { get; set; }
        public SchulteViewModel SchulteViewModel { get; set; }
        public RememberNumberViewModel NumbersViewModel { get; set; }
        public EvenNumbersViewModel EvenNumbersViewModel { get; set; }
        public ConcentrationViewModel ConcentrationViewModel { get; set; }
    #endregion
    #region Properties
    public List<Object> DC { get; set; }
        #endregion
        public MainWindow()
        {
            MainViewModel = new MainViewModel(this);
            SchulteViewModel = new SchulteViewModel(this);
            NumbersViewModel = new RememberNumberViewModel(this);
            EvenNumbersViewModel = new EvenNumbersViewModel(this);
            ConcentrationViewModel=new ConcentrationViewModel(this);
            InitializeComponent();
            DC = new List<object>() { MainViewModel, NumbersViewModel, SchulteViewModel, EvenNumbersViewModel, ConcentrationViewModel };
            this.DataContext = DC[0];
            MainViewModel.MaximizeIt = new Command(() => ReshapeWindow(MainWindowFunctions.Maximize), () => MainViewModel.CanApp());
            MainViewModel.NormalizeIt = new Command(() => ReshapeWindow(MainWindowFunctions.Normalize), () => MainViewModel.CanApp());
            MainViewModel.MinimizeIt = new Command(() => ReshapeWindow(MainWindowFunctions.Minimize), () => MainViewModel.CanApp());
            MainViewModel.CloseIt = new Command(() => ReshapeWindow(MainWindowFunctions.CloseWin), () => MainViewModel.CanApp());
            this.MouseDown += delegate { DragMove(); };
        }
        private void WindowContentRendered(object sender, EventArgs e)
        {
            Image Neuro = new Image()
            {
                Source = new BitmapImage(new Uri(@"Resources\NeuroTraining4.jpg", UriKind.Relative))
            };
            StartImage.Children.Clear();
            StartImage.Children.Add(Neuro);
            Neuro.Width = StartImage.Width;
            Neuro.Height = StartImage.Height;
            Canvas.SetTop(Neuro, 0);
            Canvas.SetLeft(Neuro, 0);
            MainViewModel.StartPanel = Visibility.Visible;
        }
        public void ReshapeWindow(MainWindowFunctions mode)
        {
            switch (mode)
            {
                case MainWindowFunctions.Maximize:
                    this.WindowState = WindowState.Maximized;
                    MainViewModel.Visible = mode;
                    break;
                case MainWindowFunctions.Normalize:
                    this.WindowState = WindowState.Normal;
                    MainViewModel.Visible = mode;
                    break;
                case MainWindowFunctions.Minimize:
                    this.WindowState = WindowState.Minimized;
                    break;
                case MainWindowFunctions.CloseWin:
                    Application.Current.Shutdown();
                    break;
                default:
                    throw new NotImplementedException(string.Format($"{mode.ToString()} not implemented"));
            }
        }
    }
}
