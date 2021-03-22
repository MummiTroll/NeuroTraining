using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;

namespace NeuroTraining.ViewModels
{
    public class SchulteViewModel : INotifyPropertyChanged
    {
        #region Objects
        public Time time = new Time();
        public Results results = new Results();
        public MainWindow MainWindow { get; set; }
        public Random r = new Random();
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
        public string timeValue { get; set; } = "0";
        public string TimeValue
        {
            get
            {
                return timeValue;
            }
            set
            {
                if (timeValue != value)
                {
                    timeValue = value;
                    OnPropertyChange(nameof(TimeValue));
                }
            }
        }
        private string next { get; set; }
        public string Next
        {
            get
            {
                return next;
            }
            set
            {
                if (next != value)
                {
                    next = value;
                    OnPropertyChange(nameof(Next));
                }
            }
        }
        private string current { get; set; }
        public string Current
        {
            get
            {
                return current;
            }
            set
            {
                if (current != value)
                {
                    current = value;
                    OnPropertyChange(nameof(Current));
                }
            }
        }
        private string best { get; set; }
        public string Best
        {
            get
            {
                return best;
            }
            set
            {
                if (best != value)
                {
                    best = value;
                    OnPropertyChange(nameof(Best));
                }
            }
        }
        private string setFieldSize { get; set; } = "";
        public string SetFieldSize
        {
            get
            {
                return setFieldSize.ToString();
            }
            set
            {
                if (setFieldSize != value)
                {
                    setFieldSize = value;
                    OnPropertyChange(nameof(SetFieldSize));
                }
            }
        }
        public bool fourchecked { get; set; }
        public bool FourChecked
        {
            get
            {
                return fourchecked;
            }
            set
            {
                if (fourchecked != value)
                {
                    fourchecked = value;
                    OnPropertyChange(nameof(FourChecked));
                }
            }
        }
        public bool fivechecked { get; set; } = true;
        public bool FiveChecked
        {
            get
            {
                return fivechecked;
            }
            set
            {
                if (fivechecked != value)
                {
                    fivechecked = value;
                    OnPropertyChange(nameof(FiveChecked));
                }
            }
        }
        public bool sixchecked { get; set; }
        public bool SixChecked
        {
            get
            {
                return sixchecked;
            }
            set
            {
                if (sixchecked != value)
                {
                    sixchecked = value;
                    OnPropertyChange(nameof(SixChecked));
                }
            }
        }
        public bool sevenchecked { get; set; }
        public bool SevenChecked
        {
            get
            {
                return sevenchecked;
            }
            set
            {
                if (sevenchecked != value)
                {
                    sevenchecked = value;
                    OnPropertyChange(nameof(SevenChecked));
                }
            }
        }
        private int fieldSize { get; set; } = 0;
        public int FieldSize
        {
            get
            {
                return fieldSize;
            }
            set
            {
                if (fieldSize != value)
                {
                    fieldSize = value;
                    OnPropertyChange(nameof(FieldSize));
                }
            }
        }
        private string clickedNumber { get; set; }
        public string ClickedNumber
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
        private Brush br { get; set; }
        public Brush brush
        {
            get
            {
                return br;
            }
            set
            {
                if (br != value)
                {
                    br = value;
                    OnPropertyChange(nameof(brush));
                }
            }
        }
        private int clickCount { get; set; }
        public int ClickCount
        {
            get
            {
                return clickCount;
            }
            set
            {
                if (clickCount != value)
                {
                    clickCount = value;
                    OnPropertyChange(nameof(ClickCount));
                }
            }
        }
        private int allNumbers { get; set; }
        public int AllNumbers
        {
            get
            {
                return allNumbers;
            }
            set
            {
                if (allNumbers != value)
                {
                    allNumbers = value;
                    OnPropertyChange(nameof(AllNumbers));
                }
            }
        }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string root
        {
            get
            {
                string root = System.IO.Path.GetPathRoot(System.Reflection.Assembly.GetEntryAssembly().Location);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                return root;
            }
        }
        private bool disable { get; set; } = true;
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
        private bool disableRnd { get; set; } = false;
        public bool DisableRnd
        {
            get
            {
                return disableRnd;
            }
            set
            {
                if (disableRnd != value)
                {
                    disableRnd = value;
                    OnPropertyChange(nameof(DisableRnd));
                }
            }
        }
        public List<int> Results { get; set; }
        public List<bool> Radiobuttons
        {
            get
            {
                return new List<bool>() { FourChecked, FiveChecked, SixChecked, SevenChecked };
            }
        }
        private bool rndOnce { get; set; } = false;
        public bool RndOnce
        {
            get
            {
                return rndOnce;
            }
            set
            {
                if (rndOnce != value)
                {
                    rndOnce = value;
                    OnPropertyChange(nameof(RndOnce));
                }
            }
        }
        private string symbol { get; set; }
        public string Symbol
        {
            get
            {
                return symbol;
            }
            set
            {
                if (symbol != value)
                {
                    symbol = value;
                    OnPropertyChange(nameof(Symbol));
                }
            }
        }
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
        private Visibility labelSchulte { get; set; } = Visibility.Visible;
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
        private Visibility labelEvenNumbers { get; set; } = Visibility.Hidden;
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
            get { return gameVis; }
            set
            {
                if (gameVis != value)
                {
                    gameVis = value;
                    OnPropertyChange(nameof(GameVis));
                }
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
        //private Visibility progressBar2 { get; set; } = Visibility.Hidden;
        //public Visibility ProgressBar2
        //{
        //    get
        //    { return progressBar2; }
        //    set
        //    {
        //        if (progressBar2 != value)
        //        {
        //            progressBar2 = value;
        //        }
        //        OnPropertyChange(nameof(ProgressBar2));
        //    }
        //}
        #endregion

        #region Number slots
        private Visibility numbersSlots { get; set; } = Visibility.Hidden;
        public Visibility NumbersSlots
        {
            get
            {
                return numbersSlots;
            }
            set
            {
                if (numbersSlots != value)
                {
                    numbersSlots = value;
                    OnPropertyChange(nameof(NumbersSlots));
                }
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
        private Visibility rememberNumberResults { get; set; } = Visibility.Hidden;
        public Visibility RememberNumberResults
        {
            get
            { return rememberNumberResults; }
            set
            {
                if (rememberNumberResults != value)
                {
                    rememberNumberResults = value;
                }
                OnPropertyChange(nameof(RememberNumberResults));
            }
        }
        #endregion
        #endregion

        #region Commands
        public ICommand Start { get; set; }
        public ICommand ToStart { get; set; }
        public ICommand OpenSet { get; set; }
        public ICommand OpenResults { get; set; }
        public ICommand Clear_FieldSizeBox { get; set; }
        public ICommand Exit { get; set; }
        #endregion
        public SchulteViewModel(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            Start = new Command(() => StartGame(), () => CanApp());
            ToStart = new Command(() => BackToStart(), () => CanApp());
            OpenSet = new Command(() => OpenCloseSettings(), () => CanApp());
            OpenResults = new Command(() => OpenGameResults(), () => CanApp());
            Clear_FieldSizeBox = new Command(() => ClearFieldSizeBox(), () => CanApp());
            Exit = new Command(() => ExitApp(), () => CanApp());
        }
        private void OpenCloseSettings()
        {
            StartImage = Visibility.Hidden;
            SchulteGameVis = Visibility.Hidden;
            StopGameVis = Visibility.Hidden;
            if (SchulteSettingsPanel == Visibility.Visible)
            {
                SchulteSettingsPanel = Visibility.Hidden;
            }
            else
            {
                SchulteSettingsPanel = Visibility.Visible;
            }
            timer.IsEnabled = false;
            TimeValue = string.Empty;
            Next = string.Empty;
            Current = string.Empty;
            Best = string.Empty;
        }
        private void BackToStart()
        {
            StartImage = Visibility.Visible;
            SchulteGameVis = Visibility.Hidden; 
            GameVis = Visibility.Hidden;
            SchulteSettingsPanel = Visibility.Hidden;
            SchulteVarPanel = Visibility.Hidden;
            MainWindow.MainViewModel.LabelSchulte = Visibility.Hidden;
            timer.IsEnabled = false;
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
        public void StartGame()
        {
            MainWindow.Screen.Children.Clear();
            Disable = false;
            DisableRnd = false;
            SchulteSettingsPanel = Visibility.Hidden;
            SchulteGameVis = Visibility.Visible;
            StopGameVis = Visibility.Visible;
            StartImage = Visibility.Hidden;
            GameVis = Visibility.Hidden;
            NumbersSlots = Visibility.Hidden;
            SchulteVarPanel = Visibility.Visible;
            ClickCount = 1;
            if (!String.IsNullOrEmpty(SetFieldSize) && !String.IsNullOrWhiteSpace(SetFieldSize) && Double.TryParse(SetFieldSize, out double k))
            {
                FieldSize = (int)Math.Round(Double.Parse(SetFieldSize), 0);
            }
            else
            {
                FieldSize = Radiobuttons.IndexOf(true) + 4;
            }
            if (RndOnce == true) { Symbol = "."; } else { Symbol = ";"; }
            ResetButtons(20);
            if (RndOnce == true) { DisableRnd = true; }
            Next = (ClickCount).ToString();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += GameTickTimer_Tick;
            timer.IsEnabled = true;
            StartTime = DateTime.Now;
        }
        void ClickEvent(object sender, RoutedEventArgs e)
        {
            if (Disable == false)
            {
                Button button = sender as Button;
                Click(button);
            }
        }
        private async Task Click(Button button)
        {
            ClickedNumber = button.Content.ToString();
            Disable = true;
            if (ClickedNumber == Next)
            {
                button.Background = Brushes.ForestGreen;
                ClickCount += 1;
                Next = ClickCount.ToString();
            }
            else
            {
                button.Background = Brushes.Red;
            }
            //Debug.WriteLine("AllNumbers: " + AllNumbers);
            //Debug.WriteLine("ClickCount: " + ClickCount);
            await Task.Delay(300);
            button.Background = Brushes.White;
            Disable = false;
            ResetButtons(400);
            if (ClickCount == AllNumbers+1)
            {
                timer.IsEnabled = false;
                Current = TimeValue;
                Next = "";
                Results = new List<int>(results.LoadResults("ResultsSchulte.res", Symbol + FieldSize));
                if (Results.Contains(-1)) { Results.Remove(-1); }
                Results.Add((DateTime.Now - StartTime).Seconds);
                results.SaveResults(Results, "ResultsSchulte.res", Symbol + FieldSize);
                Disable = true;
                MainWindow.Screen.Children.Clear();
                ResultPlot();
                FindBestTime();
            }
        }
        public async Task ResetButtons(int delay)
        {
            await Task.Delay(delay);
            if (Disable == false && DisableRnd == false && FieldSize < 16)
            {
                double top = 0;
                double left = 0;
                int RowsCount = 0;
                int ColumnsCount = 0;
                const double f = 12.227;
                const double g = -25.133;
                AllNumbers = (int)Math.Pow(FieldSize, 2);
                List<int> lib = Enumerable.Range(1, AllNumbers).ToList();
                for (double i = 0; i < (int)Math.Pow(FieldSize, 2); i++)
                {
                    MyButton newBtn = new MyButton(FieldSize, MainWindow.Screen.Width, MainWindow.Screen.Height);
                    int e = r.Next(0, lib.Count);
                    newBtn.Content = lib[e].ToString();
                    newBtn.Name = "Button" + lib[e].ToString();
                    lib.Remove(lib[e]);
                    newBtn.Click += new RoutedEventHandler(ClickEvent);
                    top = newBtn.Height * RowsCount;
                    left = newBtn.Width * ColumnsCount;
                    MainWindow.Screen.Children.Add(newBtn);
                    Canvas.SetTop(newBtn, top);
                    Canvas.SetLeft(newBtn, left);
                    if ((i + 1) % FieldSize == 0)
                    {
                        RowsCount += 1;
                        ColumnsCount = 0;
                    }
                    else
                    {
                        ColumnsCount += 1;
                    }
                }
            }
        }
        private void OpenGameResults()
        {
            StartImage = Visibility.Hidden;
            SchulteSettingsPanel = Visibility.Hidden;
            StopGameVis = Visibility.Hidden;
            if (SchulteGameVis == Visibility.Visible)
            {
                SchulteGameVis = Visibility.Hidden;
            }
            else
            {
                SchulteGameVis = Visibility.Visible;

                timer.IsEnabled = false;
                TimeValue = string.Empty;
                if (RndOnce == true) { Symbol = "."; } else { Symbol = ";"; }
                if (!String.IsNullOrEmpty(SetFieldSize))
                {
                    FieldSize = Int32.Parse(SetFieldSize);
                }
                else
                {
                    FieldSize = Radiobuttons.IndexOf(true) + 4;
                }
                MainWindow.Screen.Children.Clear();
                Results = results.LoadResults("ResultsSchulte.res", Symbol + FieldSize);
                if (Results.Contains(-1)) { Results.Remove(-1); }
                if (Results.Count > 0)
                {
                    FindBestTime();
                    ResultPlot();
                }
                else
                {
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = "No result for this game setting yet...";
                    textBlock.FontFamily = new FontFamily("Old English Text MT");
                    textBlock.FontSize = 26;
                    textBlock.Foreground = Brushes.Brown;
                    MainWindow.Screen.Children.Add(textBlock);
                    Canvas.SetLeft(textBlock, -40);
                    Canvas.SetTop(textBlock, MainWindow.Screen.Height / 3.5);
                }
            }
        }
        private void FindBestTime()
        {
            List<int> list = new List<int>(Results);
            list.Sort();
            double best = list[0];
            Best = time.TransformSecondsToTimeValue(best);
        }
        public void ResultPlot()
        {
            MainWindow.Screen.Children.Clear();
            const double margin = 5;
            Plot plot = new Plot();
            Xaxis xaxis = new Xaxis(margin, MainWindow.Screen.Width, MainWindow.Screen.Height, Results.Count);
            YaxisSchulter yaxis = new YaxisSchulter(margin, MainWindow.Screen.Height, Results.Count);
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
            TimeValue = string.Empty;
            Next = string.Empty;
        }
        public void GameTickTimer_Tick(object sender, EventArgs e)
        {
            TimeValue = time.MakeTime(StartTime);
        }
        public void CloseSettingsPane()
        {
            if (SchulteSettingsPanel == Visibility.Visible)
            {
                SchulteSettingsPanel = Visibility.Hidden;
            }
            else
            {
                SchulteSettingsPanel = Visibility.Visible;
            }
        }
        public void ClearFieldSizeBox()
        {
            SetFieldSize = string.Empty;
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
