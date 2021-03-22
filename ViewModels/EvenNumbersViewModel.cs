using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;
using System.Diagnostics;
using System.Linq;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

namespace NeuroTraining.ViewModels
{
    public class EvenNumbersViewModel : INotifyPropertyChanged
    {
        #region Objects
        public MainWindow MainWindow { get; set; }
        public Random r = new Random();
        public Settings settings = new Settings();
        public Results results = new Results();
        public static DispatcherTimer timer = new DispatcherTimer();
        #endregion

        #region Properties
        private int border_Thickness { get; set; }
        public int Border_Thickness
        {
            get { return border_Thickness; }
            set
            {
                if (border_Thickness != value)
                {
                    border_Thickness = value;
                    OnPropertyChange(nameof(Border_Thickness));
                }
            }
        }
        public string Total_Time
        {
            get
            {
                return settings.LoadSettings("SettingsEvenNumbers.ini")[0];
            }
            set
            {
                var list = new List<string>(settings.LoadSettings("SettingsEvenNumbers.ini"));
                if (!String.IsNullOrEmpty(value))
                {
                    list[0] = value;
                }
                else
                {
                    list[0] = "100";
                }
                string s = list.Aggregate((string a, string b) => (a + "\n" + b));
                File.WriteAllText(dir + "SettingsEvenNumbers.ini", s, Encoding.UTF8);
                OnPropertyChange(nameof(Total_Time));
            }
        }
        public string NOfButtons
        {
            get
            {
                return settings.LoadSettings("SettingsEvenNumbers.ini")[1];
            }
            set
            {
                var list = new List<string>(settings.LoadSettings("SettingsEvenNumbers.ini"));
                if (!String.IsNullOrEmpty(value))
                {
                    list[1] = value;
                }
                else
                {
                    list[1] = "40";
                }
                string s = list.Aggregate((string a, string b) => (a + "\n" + b));
                File.WriteAllText(dir + "SettingsEvenNumbers.ini", s, Encoding.UTF8);
                OnPropertyChange(nameof(NOfButtons));
            }
        }
        private int clickedNumber { get; set; }
        public int ClickedNumber
        {
            get
            {
                return clickedNumber;
            }
            set
            {
                if (clickedNumber != value)
                {
                    clickedNumber = value;
                    OnPropertyChange(nameof(ClickedNumber));
                }
            }
        }
        private int countTotal { get; set; }
        public int CountTotal
        {
            get
            {
                return countTotal;
            }
            set
            {
                if (countTotal != value)
                {
                    countTotal = value;
                    OnPropertyChange(nameof(CountTotal));
                }
            }
        }
        private int countPerPanel { get; set; }
        public int CountPerPanel
        {
            get
            {
                return countPerPanel;
            }
            set
            {
                if (countPerPanel != value)
                {
                    countPerPanel = value;
                    OnPropertyChange(nameof(CountPerPanel));
                }
            }
        }
        private string foundNumbers { get; set; }
        public string FoundNumbers
        {
            get
            {
                return foundNumbers;
            }
            set
            {
                if (foundNumbers != value)
                {
                    foundNumbers = value;
                    OnPropertyChange(nameof(FoundNumbers));
                }
            }
        }
        private DateTime startTime { get; set; }
        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                if (startTime != value)
                {
                    startTime = value;
                    OnPropertyChange(nameof(StartTime));
                }
            }
        }
        private double value1 { get; set; } = 0;
        public double Value1
        {
            get
            {
                return value1;
            }
            set
            {
                if (value1 != value)
                {
                    value1 = value;
                    OnPropertyChange(nameof(Value1));
                }
            }
        }
        private string currentEven { get; set; }
        public string CurrentEven
        {
            get
            {
                return currentEven;
            }
            set
            {
                if (currentEven != value)
                {
                    currentEven = value;
                    OnPropertyChange(nameof(CurrentEven));
                }
            }
        }
        private string bestEven { get; set; }
        public string BestEven
        {
            get
            {
                return bestEven;
            }
            set
            {
                if (bestEven != value)
                {
                    bestEven = value;
                    OnPropertyChange(nameof(BestEven));
                }
            }
        }
        public List<int> Results { get; set; }
        public string dir
        {
            get
            {
                string dir = System.IO.Path.GetPathRoot(System.Reflection.Assembly.GetEntryAssembly().Location) + @"_NeuroTraining\";
                GC.Collect();
                GC.WaitForPendingFinalizers();
                return dir;
            }
        }
        private bool disable { get; set; } = false;
        public bool Disable
        {
            get
            {
                return disable;
            }
            set
            {
                if (disable != value)
                {
                    disable = value;
                    OnPropertyChange(nameof(Disable));
                }
            }
        }
        public int Rows { get; set; }
        public int Columns { get; set; }
        #endregion

        #region Commands
        public ICommand Start { get; set; }
        public ICommand ToStart { get; set; }
        public ICommand OpenSet { get; set; }
        public ICommand OpenResults { get; set; }
        public ICommand Exit { get; set; }
        #endregion

        #region Visibilities

        #region Start panel
        private Visibility startPanel { get; set; } = Visibility.Hidden;
        public Visibility StartPanel
        {
            get
            {
                return startPanel;
            }
            set
            {
                if (startPanel != value)
                {
                    startPanel = value;
                    OnPropertyChange(nameof(StartPanel));
                }
            }
        }
        private Visibility startImage { get; set; } = Visibility.Visible;
        public Visibility StartImage
        {
            get
            { return startImage; }
            set
            {
                if (startImage != value)
                {
                    startImage = value;
                }
                OnPropertyChange(nameof(StartImage));
            }
        }
        #endregion

        #region Game labels
        private Visibility labelMain { get; set; } = Visibility.Hidden;
        public Visibility LabelMain
        {
            get
            { return labelMain; }
            set
            {
                if (labelMain != value)
                {
                    labelMain = value;
                }
                OnPropertyChange(nameof(LabelMain));
            }
        }
        private Visibility labelRememberNumber { get; set; } = Visibility.Hidden;
        public Visibility LabelRememberNumber
        {
            get
            { return labelRememberNumber; }
            set
            {
                if (labelRememberNumber != value)
                {
                    labelRememberNumber = value;
                }
                OnPropertyChange(nameof(LabelRememberNumber));
            }
        }
        private Visibility labelSchulte { get; set; } = Visibility.Hidden;
        public Visibility LabelSchulte
        {
            get
            { return labelSchulte; }
            set
            {
                if (labelSchulte != value)
                {
                    labelSchulte = value;
                }
                OnPropertyChange(nameof(LabelSchulte));
            }
        }
        private Visibility labelEvenNumbers { get; set; } = Visibility.Visible;
        public Visibility LabelEvenNumbers
        {
            get
            { return labelEvenNumbers; }
            set
            {
                if (labelEvenNumbers != value)
                {
                    labelEvenNumbers = value;
                }
                OnPropertyChange(nameof(LabelEvenNumbers));
            }
        }
        private Visibility labelConcentration { get; set; } = Visibility.Hidden;
        public Visibility LabelConcentration
        {
            get
            { return labelConcentration; }
            set
            {
                if (labelConcentration != value)
                {
                    labelConcentration = value;
                }
                OnPropertyChange(nameof(LabelConcentration));
            }
        }
        #endregion

        #region Control buttons
        private Visibility startVis { get; set; } = Visibility.Visible;
        public Visibility StartVis
        {
            get
            { return startVis; }
            set
            {
                if (startVis != value)
                {
                    startVis = value;
                }
                OnPropertyChange(nameof(StartVis));
            }
        }
        private Visibility settingsButtonVisibility { get; set; } = Visibility.Visible;
        public Visibility SettingsButtonVisibility
        {
            get
            { return settingsButtonVisibility; }
            set
            {
                if (settingsButtonVisibility != value)
                {
                    settingsButtonVisibility = value;
                }
                OnPropertyChange(nameof(SettingsButtonVisibility));
            }
        }
        private Visibility stopGameVis { get; set; } = Visibility.Hidden;
        public Visibility StopGameVis
        {
            get
            {
                return stopGameVis;
            }
            set
            {
                if (stopGameVis != value)
                {
                    stopGameVis = value;
                    OnPropertyChange(nameof(StopGameVis));
                }
            }
        }
        #endregion

        #region Game field
        private Visibility schulteGameVis { get; set; } = Visibility.Hidden;
        public Visibility SchulteGameVis
        {
            get
            {
                return schulteGameVis;
            }
            set
            {
                if (schulteGameVis != value)
                {
                    schulteGameVis = value;
                    OnPropertyChange(nameof(SchulteGameVis));
                }
            }
        }
        private Visibility gameVis { get; set; } = Visibility.Hidden;
        public Visibility GameVis
        {
            get
            { return gameVis; }
            set
            {
                if (gameVis != value)
                {
                    gameVis = value;
                }
                OnPropertyChange(nameof(GameVis));
            }
        }
        #endregion

        #region Borders
        private Visibility concentrationBorder { get; set; } = Visibility.Visible;
        public Visibility ConcentrationBorder
        {
            get
            {
                return concentrationBorder;
            }
            set
            {
                if (concentrationBorder != value)
                {
                    concentrationBorder = value;
                    OnPropertyChange(nameof(ConcentrationBorder));
                }
            }
        }
        #endregion

        #region Progress bars
        private Visibility progressBar1 { get; set; } = Visibility.Hidden;
        public Visibility ProgressBar1
        {
            get
            { return progressBar1; }
            set
            {
                if (progressBar1 != value)
                {
                    progressBar1 = value;
                }
                OnPropertyChange(nameof(ProgressBar1));
            }
        }
        #endregion

        #region Number slots
        private Visibility numbersSlots { get; set; } = Visibility.Hidden;
        public Visibility NumbersSlots
        {
            get
            { return numbersSlots; }
            set
            {
                if (numbersSlots != value)
                {
                    numbersSlots = value;
                }
                OnPropertyChange(nameof(NumbersSlots));
            }
        }
        #endregion

        #region Number buttons
        private Visibility numbersButtonsPanel { get; set; } = Visibility.Hidden;
        public Visibility NumbersButtonsPanel
        {
            get
            { return numbersButtonsPanel; }
            set
            {
                if (numbersButtonsPanel != value)
                {
                    numbersButtonsPanel = value;
                }
                OnPropertyChange(nameof(NumbersButtonsPanel));
            }
        }
        #endregion

        #region Settings panels
        private Visibility rememberNumberSettingsPanel { get; set; } = Visibility.Hidden;
        public Visibility RememberNumberSettingsPanel
        {
            get
            { return rememberNumberSettingsPanel; }
            set
            {
                if (rememberNumberSettingsPanel != value)
                {
                    rememberNumberSettingsPanel = value;
                }
                OnPropertyChange(nameof(RememberNumberSettingsPanel));
            }
        }
        private Visibility schulteSettingsPanel { get; set; } = Visibility.Hidden;
        public Visibility SchulteSettingsPanel
        {
            get
            {
                return schulteSettingsPanel;
            }
            set
            {
                if (schulteSettingsPanel != value)
                {
                    schulteSettingsPanel = value;
                    OnPropertyChange(nameof(SchulteSettingsPanel));
                }
            }
        }
        private Visibility evenNumbersSettingsPanel { get; set; } = Visibility.Hidden;
        public Visibility EvenNumbersSettingsPanel
        {
            get
            { return evenNumbersSettingsPanel; }
            set
            {
                if (evenNumbersSettingsPanel != value)
                {
                    evenNumbersSettingsPanel = value;
                }
                OnPropertyChange(nameof(EvenNumbersSettingsPanel));
            }
        }
        private Visibility concentrationSettingsPanel { get; set; } = Visibility.Hidden;
        public Visibility ConcentrationSettingsPanel
        {
            get
            { return concentrationSettingsPanel; }
            set
            {
                if (concentrationSettingsPanel != value)
                {
                    concentrationSettingsPanel = value;
                }
                OnPropertyChange(nameof(ConcentrationSettingsPanel));
            }
        }
        #endregion

        #region Variables panels
        private Visibility schulteVarPanel { get; set; } = Visibility.Hidden;
        public Visibility SchulteVarPanel
        {
            get
            {
                return schulteVarPanel;
            }
            set
            {
                if (schulteVarPanel != value)
                {
                    schulteVarPanel = value;
                    OnPropertyChange(nameof(SchulteVarPanel));
                }
            }
        }
        private Visibility evennumbersVarPanel { get; set; } = Visibility.Hidden;
        public Visibility EvennumbersVarPanel
        {
            get
            { return evennumbersVarPanel; }
            set
            {
                if (evennumbersVarPanel != value)
                {
                    evennumbersVarPanel = value;
                }
                OnPropertyChange(nameof(EvennumbersVarPanel));
            }
        }
        private Visibility concentrationVarPanel { get; set; } = Visibility.Hidden;
        public Visibility ConcentrationVarPanel
        {
            get
            { return concentrationVarPanel; }
            set
            {
                if (concentrationVarPanel != value)
                {
                    concentrationVarPanel = value;
                }
                OnPropertyChange(nameof(ConcentrationVarPanel));
            }
        }
        private Visibility rememberNumberResults { get; set; } = Visibility.Collapsed;
        public Visibility RememberNumberResults
        {
            get { return rememberNumberResults; }
            set
            {
                if (rememberNumberResults != value)
                {
                    rememberNumberResults = value;
                    OnPropertyChange(nameof(RememberNumberResults));
                }
            }
        }
        #endregion
        #endregion
        public EvenNumbersViewModel(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            Start = new Command(() => StartGame(), () => CanApp());
            OpenResults = new Command(() => OpenGameResults(), () => CanApp());
            OpenSet = new Command(() => OpenCloseSettings(), () => CanApp());
            ToStart = new Command(() => BackToStart(), () => CanApp());
            Exit = new Command(() => ExitApp(), () => CanApp());
            StartImage = Visibility.Visible;
            EvenNumbersSettingsPanel = Visibility.Hidden;
        }
        public void BackToStart()
        {
            StopTimer();
            StartImage = Visibility.Visible;
            SchulteGameVis = Visibility.Hidden;
            EvenNumbersSettingsPanel = Visibility.Hidden;
            EvennumbersVarPanel = Visibility.Hidden;
            MainWindow.MainViewModel.LabelEvenNumbers = Visibility.Hidden;
            ProgressBar1 = Visibility.Hidden;
            CountTotal = -1;
            FoundNumbers = "-";
            Results = new List<int>();

            Image Neuro = new Image()
            {
                Source = new BitmapImage(new Uri(@"Resources\NeuroTraining4.jpg", UriKind.Relative))
            };
            MainWindow.StartImage.Children.Clear();
            MainWindow.StartImage.Children.Add(Neuro);
            Neuro.Width = MainWindow.StartImage.Width;
            Neuro.Height = MainWindow.StartImage.Height;
            Canvas.SetTop(Neuro, 0);
            Canvas.SetLeft(Neuro, 0);
            MainWindow.DataContext = MainWindow.DC[0];
        }
        private void StartGame()
        {
            Results = new List<int>();
            StartImage = Visibility.Hidden;
            SchulteGameVis = Visibility.Visible;
            ProgressBar1 = Visibility.Visible;
            SettingsButtonVisibility = Visibility.Visible;
            EvennumbersVarPanel = Visibility.Visible;
            EvenNumbersSettingsPanel = Visibility.Hidden;
            Disable = false;
            CountTotal = 0;
            Value1 = 0;
            ResetButtons();
            StartTime = DateTime.Now;
            StartTimer();
        }
        public void Timer_Tick(object sender, EventArgs e)
        {
            if (Value1 >= 99)
            {
                StopTimer();
                EndGame();
            }
            else
            {
                Value1 += 100 / Int32.Parse(Total_Time);
            }
        }
        void ClickEvent(object sender, RoutedEventArgs e)
        {
            if (Disable == false)
            {
                Button button = sender as Button;
                ClickedNumber = Int32.Parse(button.Content.ToString());
                if (ClickedNumber % 2 == 0)
                {
                    button.Background = Brushes.ForestGreen;
                    CountTotal += 1;
                    CountPerPanel += 1;
                }
                else
                {
                    if (CountTotal > 0) { CountTotal -= 1; }
                }
                FoundNumbers = CountTotal.ToString();
                if (CountPerPanel == 6)
                {
                    CountPerPanel = 0;
                    ResetButtons();
                }
            }
        }
        private void StartTimer()
        {
            if (timer != null)
            {
                timer.Interval = TimeSpan.FromMilliseconds(1000);
                timer.Tick += Timer_Tick;
                timer.Start();
            }
        }
        private void StopTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Tick -= Timer_Tick;
            }
        }
        private void EndGame()
        {
            SchulteGameVis = Visibility.Hidden;
            ProgressBar1 = Visibility.Hidden;
            MainWindow.Screen.Children.Clear();
            Disable = true;
            OpenGameResults();
            results.SaveResults(Results, "ResultsEvenNumbers.res", Total_Time + "-" + NOfButtons);
            CountTotal = -1;
            FoundNumbers = "-";
        }
        private void OpenGameResults()
        {
            StartPanel = Visibility.Hidden;
            StartImage = Visibility.Hidden;
            EvenNumbersSettingsPanel = Visibility.Hidden;
            ProgressBar1 = Visibility.Hidden;

            if (SchulteGameVis == Visibility.Visible)
            {
                SchulteGameVis = Visibility.Hidden;
            }
            else
            {
                SchulteGameVis = Visibility.Visible;
                if (!String.IsNullOrEmpty(Total_Time) && !String.IsNullOrEmpty(NOfButtons))
                {
                    Results = new List<int>(results.LoadResults("ResultsEvenNumbers.res", Total_Time + "-" + NOfButtons));
                }
                else
                {
                    Results = new List<int>(results.LoadResults("ResultsEvenNumbers.res", "100-40"));
                }


                if (Results.Count > 1 || (Results.Count == 1 && Results[0] != -1))
                {
                    if (Results.Contains(-1)) { Results.Remove(-1); }
                    if (CountTotal == -1)
                    {
                        CurrentEven = "-";
                    }
                    else
                    {
                        CurrentEven = CountTotal.ToString();
                        Results.Add(CountTotal);
                        if (Results.Contains(-1)) { Results.Remove(-1); }
                        List<int> l = new List<int>(Results);
                        l.Sort();
                        BestEven = l[l.Count - 1].ToString();
                    }
                    ResultPlot();
                }
                else if (Results.Count == 0 || (Results.Count == 1 && Results[0] == -1) && CountTotal > -1)
                {
                    Results.Add(CountTotal);
                    ResultPlot();
                }
                else if (Results.Count == 0 || (Results.Count == 1 && Results[0] == -1) && CountTotal == -1)
                {
                    BestEven = string.Empty;
                    CurrentEven = string.Empty;
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = "No results yet...";
                    textBlock.FontFamily = new FontFamily("Old English Text MT");
                    textBlock.FontSize = 26;
                    textBlock.Foreground = Brushes.Brown;
                    MainWindow.Screen.Children.Add(textBlock);
                    Canvas.SetLeft(textBlock, MainWindow.Screen.Width / 5);
                    Canvas.SetTop(textBlock, MainWindow.Screen.Height / 3.5);
                }
            }
        }
        public void ResetButtons()
        {
            if (Double.Parse(NOfButtons) >= 100) { NOfButtons = "100"; }
            double a = -0.0819;
            double b = 19.247;
            int Font = (int)(Double.Parse(NOfButtons) * a + b);
            MainWindow.Screen.Children.Clear();
            CountPerPanel = 0;
            if (Disable == false)
            {
                RowsColumns();
                double top = 0;
                double left = 0;
                int RowsCount = 0;
                int ColumnsCount = 0;
                List<int> lib = new List<int>();
                int CountEven = 0;
                while (lib.Count < 6)
                {
                    int e = r.Next(1000, 9999);
                    if (e % 2 == 0 && !lib.Contains(e))
                    {
                        lib.Add(e);
                    }
                }
                while (lib.Count < Double.Parse(NOfButtons))
                {
                    int e = r.Next(1000, 9999);
                    if (e % 2 != 0 && !lib.Contains(e))
                    {
                        lib.Add(e);
                    }
                }
                int count = lib.Count;
                for (int i = 0; i < count; i++)
                {
                    MyButtonEven newBtn = new MyButtonEven(Rows, Columns, MainWindow.Screen.Width, MainWindow.Screen.Height);
                    int e = r.Next(0, lib.Count);
                    newBtn.Content = lib[e].ToString();
                    newBtn.Name = "Button" + lib[e].ToString();

                    newBtn.FontSize = Font;

                    lib.Remove(lib[e]);
                    newBtn.Click += new RoutedEventHandler(ClickEvent);
                    top = newBtn.Height * RowsCount;
                    left = newBtn.Width * ColumnsCount;
                    MainWindow.Screen.Children.Add(newBtn);
                    Canvas.SetTop(newBtn, top);
                    Canvas.SetLeft(newBtn, left);
                    if (ColumnsCount == Columns - 1)
                    {
                        ColumnsCount = 0;
                        RowsCount += 1;
                    }
                    else
                    {
                        ColumnsCount += 1;
                    }
                }
            }
        }
        public void RowsColumns()
        {
            double buttons = Double.Parse(NOfButtons);
            Columns = (int)Math.Round(buttons / 10, 0);
            Rows = (int)Math.Round(buttons / Columns, 0);
        }
        public void ResultPlot()
        {
            ProgressBar1 = Visibility.Hidden;
            SchulteGameVis = Visibility.Visible;
            const double margin = 5;
            Plot plot = new Plot();
            Xaxis xaxis = new Xaxis(margin, MainWindow.Screen.Width, MainWindow.Screen.Height, Results.Count);
            //YaxisSchulter yaxis = new YaxisSchulter(margin, MainWindow.Screen.Height, Results.Count);
            YaxisRememberNumber yaxis = new YaxisRememberNumber(margin, MainWindow.Screen.Height);
            MainWindow.Screen.Children.Clear();
            plot.MakePlotElements(margin, MainWindow.Screen.Width, MainWindow.Screen.Height, Results);
            MainWindow.Screen.Children.Add(xaxis.xaxis_path);
            MainWindow.Screen.Children.Add(yaxis.yaxis_path);
            for (int i = 0; i < plot.ResultPoints.Count; i++)
            {
                MainWindow.Screen.Children.Add(plot.ResultPoints[i].UiElement);
                Canvas.SetLeft(plot.ResultPoints[i].UiElement, plot.ResultPoints[i].X - margin / 4);
                Canvas.SetTop(plot.ResultPoints[i].UiElement, plot.ResultPoints[i].Y - margin / 3);
            }
            MainWindow.Screen.Children.Add(plot.polyline);
        }
        public void OpenCloseSettings()
        {
            StartImage = Visibility.Hidden;
            SchulteGameVis = Visibility.Hidden;

            if (EvenNumbersSettingsPanel == Visibility.Visible)
            {
                EvenNumbersSettingsPanel = Visibility.Hidden;
            }
            else
            {
                EvenNumbersSettingsPanel = Visibility.Visible;
            }
            //timer.IsEnabled = false;
        }
        private void ExitApp()
        {
            Application.Current.Shutdown();
        }
        private bool CanApp()
        {
            return true;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}

