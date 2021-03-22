using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;
using System.Threading;

namespace NeuroTraining.ViewModels
{
    public class ConcentrationViewModel : INotifyPropertyChanged
    {
        #region Objects
        public MainWindow MainWindow { get; set; }
        public Random r = new Random();
        public static DispatcherTimer timerOverall = new DispatcherTimer();
        public static DispatcherTimer timerRound = new DispatcherTimer();
        public Settings settings = new Settings();
        public Results results = new Results();
        public static CancellationTokenSource ts = new CancellationTokenSource();
        public CancellationToken ct = ts.Token;

        #endregion

        #region Commands
        public ICommand OpenSet { get; set; }
        public ICommand OpenResults { get; set; }
        public ICommand CloseIt { get; set; }
        public ICommand Start { get; set; }
        public ICommand ToStart { get; set; }
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
        private Visibility labelConcentration { get; set; } = Visibility.Visible;
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
        #endregion
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
        public double top { get; set; }
        public double left { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double xmin = 0;
        public double xmax { get; set; }
        public double ymin = 0;
        public double ymax { get; set; }
        public double side { get; set; }
        //public int NumberOfObjects { get; set; } = 1;
        public int ClickCount { get; set; }
        public int Rounds { get; set; }
        public int NOrange { get; set; } = 5;
        public int NGreen { get; set; } = 3;
        public int CorrectGuess { get; set; }
        public int TotalScore { get; set; } = 0;
        public List<Point> CurrentCoordinates { get; set; }
        public List<Point> CoordinatesCheck { get; set; }
        private string score { get; set; }
        public string Score
        {
            get { return score; }
            set
            {
                if (score != value)
                {
                    score = value;
                    OnPropertyChange(nameof(Score));
                }
            }
        }
        private string best { get; set; }
        public string Best
        {
            get { return best; }
            set
            {
                if (best != value)
                {
                    best = value;
                    OnPropertyChange(nameof(Best));
                }
            }
        }
        private double value1 { get; set; }
        public double Value1
        {
            get { return value1; }
            set
            {
                if (value1 != value)
                {
                    value1 = value;
                    OnPropertyChange(nameof(Value1));
                }
            }
        }
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
        public string Target_Points
        {
            get { return settings.LoadSettings("SettingsConcentration.ini")[0]; }
            set
            {
                var list = new List<string>(settings.LoadSettings("SettingsConcentration.ini"));
                if (!String.IsNullOrEmpty(value))
                {
                    list[0] = value;
                }
                else
                {
                    list[0] = "5";
                }
                string s = list.Aggregate((string a, string b) => a + "\n" + b);
                File.WriteAllText(dir + "SettingsConcentration.ini", s, Encoding.UTF8);
                OnPropertyChange(nameof(Target_Points));
            }
        }
        public string Other_Points
        {
            get { return settings.LoadSettings("SettingsConcentration.ini")[1]; }
            set
            {
                var list = new List<string>(settings.LoadSettings("SettingsConcentration.ini"));
                if (!String.IsNullOrEmpty(value))
                {
                    list[1] = value;
                }
                else
                {
                    list[1] = "9";
                }
                string s = list.Aggregate((string a, string b) => a + "\n" + b);
                File.WriteAllText(dir + "SettingsConcentration.ini", s, Encoding.UTF8);
                OnPropertyChange(nameof(Other_Points));
            }
        }
        public string Overall_Time
        {
            get { return settings.LoadSettings("SettingsConcentration.ini")[2]; }
            set
            {
                var list = new List<string>(settings.LoadSettings("SettingsConcentration.ini"));
                if (!String.IsNullOrEmpty(value))
                {
                    list[2] = value;
                }
                else
                {
                    list[2] = "100";
                }
                string s = list.Aggregate((string a, string b) => a + "\n" + b);
                File.WriteAllText(dir + "SettingsConcentration.ini", s, Encoding.UTF8);
                OnPropertyChange(nameof(Overall_Time));
            }
        }
        //public int TotalTime { get; set; }
        public string Initiation_Time
        {
            get { return settings.LoadSettings("SettingsConcentration.ini")[3]; }
            set
            {
                var list = new List<string>(settings.LoadSettings("SettingsConcentration.ini"));
                if (!String.IsNullOrEmpty(value))
                {
                    list[3] = value;
                }
                else
                {
                    list[3] = "4";
                }
                string s = list.Aggregate((string a, string b) => a + "\n" + b);
                File.WriteAllText(dir + "SettingsConcentration.ini", s, Encoding.UTF8);
                OnPropertyChange(nameof(Initiation_Time));
            }
        }
        public int Initiation { get; set; }
        public List<int> Results { get; set; }
        public List<MyEllipse> ListEllipses { get; set; }
        #endregion
        public ConcentrationViewModel(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            Start = new Command(() => StartGame(), () => CanApp());
            OpenResults = new Command(() => OpenGameResults(), () => CanApp());
            OpenSet = new Command(() => OpenSettings(), () => CanApp());
            ToStart = new Command(() => BackToStart(), () => CanApp());
            Exit = new Command(() => ExitApp(), () => CanApp());
        }
        public void BackToStart()
        {
            StartImage = Visibility.Visible;
            SchulteGameVis = Visibility.Hidden;
            ConcentrationSettingsPanel = Visibility.Hidden;
            MainWindow.MainViewModel.LabelConcentration = Visibility.Hidden;
            StopRoundTimer();
            //ts.Cancel();
            StopOverallTimer();
            ProgressBar1 = Visibility.Hidden;
            Border_Thickness = 0;
            ConcentrationVarPanel = Visibility.Hidden;
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
        public async Task StartGame()
        {
            StartImage = Visibility.Hidden;
            SchulteGameVis = Visibility.Visible;
            ProgressBar1 = Visibility.Visible;
            SettingsButtonVisibility = Visibility.Visible;
            ConcentrationVarPanel = Visibility.Visible;
            ConcentrationSettingsPanel = Visibility.Hidden;
            Border_Thickness = 1;
            const double a = -11.08;
            const double b = 66.1;
            xmax = Convert.ToInt32(MainWindow.Screen.Width);
            ymax = Convert.ToInt32(MainWindow.Screen.Height);
            Initiation = Int32.Parse(Initiation_Time) * 1000;
            Value1 = 0;
            NOrange = Int32.Parse(Other_Points);
            NGreen = Int32.Parse(Target_Points);
            TotalScore = 0;
            Rounds = 0;
            side = Math.Floor(Math.Log(NOrange + NGreen) * a + b);
            StartOverallTimer();
            CorrectGuess = 0;
            StartRound(ct);
        }
        private void OpenSettings()
        {
            SchulteGameVis = Visibility.Hidden;
            StartImage = Visibility.Hidden;
            timerRound.IsEnabled = false;
            timerOverall.IsEnabled = false;
            Value1 = 0;
            Border_Thickness = 0;

            if (ConcentrationSettingsPanel == Visibility.Visible)
            {
                ConcentrationSettingsPanel = Visibility.Hidden;
            }
            else
            {
                ConcentrationSettingsPanel = Visibility.Visible;
            }
        }
        public void EndGame()
        {
            if (Value1 >= 99)
            {
                StopRoundTimer();
                ts.Cancel();
                StopOverallTimer();
                MainWindow.Screen.Children.Clear();
                SchulteGameVis = Visibility.Hidden;
                ProgressBar1 = Visibility.Hidden;
                Score = TotalScore.ToString();
                OpenGameResults();
                results.SaveResults(Results, "ResultsConcentration.res", Overall_Time);
                Best = "-";
            }
            else
            {
                TotalScore += CorrectGuess;
                Score = TotalScore.ToString();
                if (Rounds == 2)
                {
                    Rounds = 0;
                    if (CorrectGuess == 2 * NGreen)
                    {
                        NOrange += 2;
                        NGreen += 1;
                    }
                    else
                    {
                        if (NGreen > 4)
                        {
                            NOrange -= 2;
                            NGreen -= 1;
                        }
                    }
                    CorrectGuess = 0;
                }
                StartRound(ct);
            }
        }
        private void OpenGameResults()
        {
            StartPanel = Visibility.Hidden;
            StartImage = Visibility.Hidden;
            ConcentrationSettingsPanel = Visibility.Hidden;
            ProgressBar1 = Visibility.Hidden;
            MainWindow.Screen.Children.Clear();
            if (SchulteGameVis == Visibility.Visible)
            {
                SchulteGameVis = Visibility.Hidden;
            }
            else
            {
                SchulteGameVis = Visibility.Visible;
                if (!String.IsNullOrEmpty(Overall_Time))
                {
                    Results = new List<int>(results.LoadResults("ResultsConcentration.res", Overall_Time));
                }
                else
                {
                    Results = new List<int>(results.LoadResults("ResultsConcentration.res", "100"));
                }
                if (Results.Count > 1 || (Results.Count == 1 && Results[0] != -1))
                {
                    if (Results.Contains(-1)) { Results.Remove(-1); }
                    if (TotalScore == 0)
                    {
                        Score = "-";
                    }
                    else
                    {
                        Results.Add(TotalScore);
                        Score = TotalScore.ToString();
                        List<int> l = new List<int>(Results);
                        l.Sort();
                        Best = l[l.Count - 1].ToString();
                    }
                    ResultPlot();
                }
                else if (Results.Count == 0 || (Results.Count == 1 && Results[0] == -1) && TotalScore > 0)
                {
                    Results.Add(TotalScore);
                    ResultPlot();
                }
                else if (Results.Count == 0 || (Results.Count == 1 && Results[0] == -1))
                {
                    Best = "-";
                    Score = "-";
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
        public async Task StartRound(CancellationToken ct)
        {
            if (ct.IsCancellationRequested)
            {
                ts.Cancel();
                return;
            }
            Rounds += 1;
            StartRoundTimer();
            ClickCount = 0;
            ResetOjects();
            await Task.Delay(Initiation);
            for (int i = 0; i < ListEllipses.Count; i++)
            {
                ListEllipses[i].ellipse.Fill = Brushes.LightGray;
            }
            await Task.Delay(2500);
            timerRound.Stop();
        }
        public void Timer_TickRound(object sender, EventArgs e)
        {
            Step();
        }
        public async Task Step()
        {
            await Task.Delay(100);
            MainWindow.Screen.Children.Clear();
            Navi();
        }
        public void Timer_TickOverall(object sender, EventArgs e)
        {
            Value1 += 100 / Int32.Parse(Overall_Time);
            if (Value1 >= 100)
            {
                EndGame();
            }
        }
        public void ResetOjects()
        {
            ListEllipses = new List<MyEllipse>();
            List<int> lib = new List<int>() { -1, 1 };
            CoordinatesCheck = new List<Point>();
            for (int i = 0; i < NOrange; i++)
            {
                GenerateXY();
                MyEllipse myEllipse = new MyEllipse();
                myEllipse.ellipse.Name = "Orange" + i.ToString();
                myEllipse.coordinates = new Point(X, Y);
                int a = r.Next(0, 2);
                myEllipse.ellipse.Width = side;
                myEllipse.ellipse.Height = side;
                myEllipse.dX = myEllipse.dX * lib[a];
                a = r.Next(0, 2);
                myEllipse.dY = myEllipse.dY * lib[a];
                //myEllipse.UiElement.MouseDown += Click_It;
                myEllipse.UiElement.MouseDown += new MouseButtonEventHandler(Click_It);
                ListEllipses.Add(myEllipse);
            }
            for (int i = 0; i < NGreen; i++)
            {
                GenerateXY();
                MyEllipse myEllipse = new MyEllipse();
                myEllipse.ellipse.Name = "Green" + (i + 5).ToString();
                myEllipse.coordinates = new Point(X, Y);
                int a = r.Next(0, 2);
                myEllipse.dX = myEllipse.dX * lib[a];
                a = r.Next(0, 2);
                myEllipse.dY = myEllipse.dY * lib[a];
                myEllipse.ellipse.Width = side;
                myEllipse.ellipse.Height = side;
                myEllipse.ellipse.Fill = Brushes.ForestGreen;
                myEllipse.UiElement.MouseDown += new MouseButtonEventHandler(Click_It);
                ListEllipses.Add(myEllipse);
            }
            MainWindow.Screen.Children.Clear();
            for (int i = 0; i < ListEllipses.Count; i++)
            {
                MainWindow.Screen.Children.Add(ListEllipses[i].UiElement);
                Canvas.SetTop(ListEllipses[i].UiElement, ListEllipses[i].coordinates.Y);
                Canvas.SetLeft(ListEllipses[i].UiElement, ListEllipses[i].coordinates.X);
            }
        }
        private void GenerateXY()
        {
            double x = r.Next(0, Convert.ToInt32(xmax - side));
            double y = r.Next(0, Convert.ToInt32(ymax - side));
            var point = new Point(x + side / 2, y + side / 2);
            int count = 0;
            if (CoordinatesCheck.Count != 0)
            {
                foreach (Point po in CoordinatesCheck)
                {
                    if (Math.Sqrt(Math.Pow(po.X - point.X, 2) + Math.Pow(po.Y - point.Y, 2)) <= 1.05 * side
                        || x <= xmin + 1 || x >= xmax - side - 1 || y <= ymin + 1 || y >= xmax - side - 1)
                    {
                        count += 1;
                        break;
                    }
                }
            }
            if (count == 0)
            {
                CoordinatesCheck.Add(point);
                X = x;
                Y = y;
            }
            else
            {
                GenerateXY();
            }
        }
        private void Click_It(object sender, MouseButtonEventArgs e)
        {
            ClickCount += 1;
            Ellipse myEllipse = sender as Ellipse;
            if (myEllipse.Name.Contains("Green"))
            {
                myEllipse.Fill = Brushes.Green;
                CorrectGuess += 1;
            }
            else
            {
                myEllipse.Fill = Brushes.Red;
            }
            if (ClickCount == NGreen)
            {
                EndGame();
            }
        }
        public void Navi()
        {
            CurrentCoordinates = new List<Point>();
            foreach (MyEllipse el in ListEllipses)
            {
                if ((el.coordinates.X <= xmin + 1 && el.coordinates.Y <= ymin + 1)
                    || (el.coordinates.X <= xmin + 1 && el.coordinates.Y >= ymax - side - 1)
                    || (el.coordinates.X >= xmax - side - 1 && el.coordinates.Y >= ymax - side - 1)
                    || (el.coordinates.X >= xmax - side - 1 && el.coordinates.Y <= ymin + 1))
                {
                    el.aX = -el.aX;
                    el.aY = -el.aY;
                }
                else if (el.coordinates.X <= xmin || el.coordinates.X >= xmax - side - 1)
                {
                    el.aX = -el.aX;
                }
                else if (el.coordinates.Y <= ymin + 1 || el.coordinates.Y >= ymax - side - 1)
                {
                    el.aY = -el.aY;
                }
                X = Convert.ToInt32(el.coordinates.X + el.dX * el.aX);
                Y = Convert.ToInt32(el.coordinates.Y + el.dY * el.aY);
                el.coordinates = new Point(X, Y);
                CurrentCoordinates.Add(new Point(X + side / 2, Y + side / 2));
                MainWindow.Screen.Children.Add(el.UiElement);
                Canvas.SetTop(el.UiElement, el.coordinates.Y);
                Canvas.SetLeft(el.UiElement, el.coordinates.X);
            }
            for (int i = 0; i < CurrentCoordinates.Count; i++)
            {
                for (int j = i + 1; j < CurrentCoordinates.Count; j++)
                {
                    if (Math.Sqrt(Math.Pow(CurrentCoordinates[i].X - CurrentCoordinates[j].X, 2)
                        + Math.Pow(CurrentCoordinates[i].Y - CurrentCoordinates[j].Y, 2)) <= side)
                    {
                        ListEllipses[i].aX *= -1;
                        ListEllipses[i].aY *= -1;
                        ListEllipses[j].aX *= -1;
                        ListEllipses[j].aY *= -1;
                    }
                }
            }
        }
        public void ResultPlot()
        {
            Border_Thickness = 0;
            const double margin = 5;
            Plot plot = new Plot();
            Xaxis xaxis = new Xaxis(margin, MainWindow.Screen.Width, MainWindow.Screen.Height, Results.Count);
            YaxisSchulter yaxis = new YaxisSchulter(margin, MainWindow.Screen.Height, Results.Count);
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
        private void StartRoundTimer()
        {
            if (timerRound != null)
            {
                timerRound.Interval = TimeSpan.FromMilliseconds(15);
                timerRound.Tick += Timer_TickRound;
                timerRound.Start();
            }
        }
        private void StopRoundTimer()
        {
            if (timerRound != null)
            {
                timerRound.Stop();
                timerRound.Tick -= Timer_TickRound;
            }
        }
        private void StartOverallTimer()
        {
            if (timerOverall != null)
            {
                timerOverall.Interval = TimeSpan.FromMilliseconds(1000);
                timerOverall.Tick += Timer_TickOverall;
                timerOverall.Start();
            }
        }
        private void StopOverallTimer()
        {
            if (timerOverall != null)
            {
                timerOverall.Stop();
                timerOverall.Tick -= Timer_TickOverall;
            }
        }
        private void ExitApp()
        {
            Application.Current.Shutdown();
        }
        public bool CanApp()
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
