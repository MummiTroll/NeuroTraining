using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media.Imaging;

namespace NeuroTraining.ViewModels
{
    public class RememberNumberViewModel : INotifyPropertyChanged
    {
        #region Objects
        public MainWindow MainWindow { get; set; }
        public Random r = new Random();
        public Settings settings = new Settings();
        public Results results = new Results();
        #endregion

        #region Properties
        public string Digits
        {
            get
            {
                return settings.LoadSettings("SettingsRememberNumber.ini")[0];
            }
            set
            {
                var list = new List<string>(settings.LoadSettings("SettingsRememberNumber.ini"));
                if (!String.IsNullOrEmpty(value))
                {
                    list[0] = value;
                }
                else
                {
                    list[0] = "4";
                }
                string s = list.Aggregate((string a, string b) => (a + "\n" + b));
                File.WriteAllText(dir + "SettingsRememberNumber.ini", s, Encoding.UTF8);
                OnPropertyChange(nameof(Digits));
            }
        }
        public string Exposure
        {
            get
            {
                return settings.LoadSettings("SettingsRememberNumber.ini")[1];
            }
            set
            {
                var list = new List<string>(settings.LoadSettings("SettingsRememberNumber.ini"));
                if (!String.IsNullOrEmpty(value))
                {
                    list[1] = value;
                }
                else
                {
                    list[1] = "2";
                }
                string s = list.Aggregate((string a, string b) => (a + "\n" + b + "\n"));
                File.WriteAllText(dir + "SettingsRememberNumber.ini", s, Encoding.UTF8);
                OnPropertyChange(nameof(Exposure));
            }
        }
        public string NOfTries
        {
            get
            {
                return settings.LoadSettings("SettingsRememberNumber.ini")[2];
            }
            set
            {
                var list = new List<string>(settings.LoadSettings("SettingsRememberNumber.ini"));
                if (!String.IsNullOrEmpty(value))
                {
                    list[2] = value;
                }
                else
                {
                    list[2] = "3";
                }
                string s = list.Aggregate((string a, string b) => (a + "\n" + b + "\n"));
                File.WriteAllText(dir + "SettingsRememberNumber.ini", s, Encoding.UTF8);
                OnPropertyChange(nameof(NOfTries));
            }
        }
        public string TotalTries
        {
            get
            {
                return settings.LoadSettings("SettingsRememberNumber.ini")[3];
            }
            set
            {
                var list = new List<string>(settings.LoadSettings("SettingsRememberNumber.ini"));
                if (!String.IsNullOrEmpty(value))
                {
                    list[3] = value;
                }
                else
                {
                    list[3] = "15";
                }
                string s = list.Aggregate((string a, string b) => (a + "\n" + b + "\n"));
                File.WriteAllText(dir + "SettingsRememberNumber.ini", s, Encoding.UTF8);
                OnPropertyChange(nameof(TotalTries));
            }
        }
        private string numToRemember { get; set; }
        public string NumToRemember
        {
            get
            {
                return numToRemember;
            }
            set
            {
                if (numToRemember != value)
                {
                    numToRemember = value;
                    OnPropertyChange(nameof(NumToRemember));
                }
            }
        }
        private string numTyped { get; set; }
        public string NumTyped
        {
            get
            {
                return numTyped;
            }
            set
            {
                if (numTyped != value)
                {
                    numTyped = value;
                    OnPropertyChange(nameof(NumTyped));
                }
            }
        }
        private int firstVisible { get; set; }
        public int FirstVisible
        {
            get
            {
                return firstVisible;
            }
            set
            {
                if (firstVisible != value)
                {
                    firstVisible = value;
                    OnPropertyChange(nameof(FirstVisible));
                }
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
        private bool disable1 { get; set; }
        public bool Disable1
        {
            get
            {
                return disable1;
            }
            set
            {
                if (disable1 != value)
                {
                    disable1 = value;
                    OnPropertyChange(nameof(Disable1));
                }
            }
        }
        private int value1 { get; set; }
        public int Value1
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
        public string TriesCount { get; set; }
        private string bestResult { get; set; }
        public string BestResult
        {
            get
            {
                return bestResult;
            }
            set
            {
                if (bestResult != value)
                {
                    bestResult = value;
                    OnPropertyChange(nameof(BestResult));
                }
            }
        }
        private string currentResult { get; set; }
        public string CurrentResult
        {
            get
            {
                return currentResult;
            }
            set
            {
                if (currentResult != value)
                {
                    currentResult = value;
                    OnPropertyChange(nameof(CurrentResult));
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
        private int Success { get; set; }
        private int NTries { get; set; }
        private int NTotalTries { get; set; }
        public string ClickedNumber { get; set; }
        public string ButtonName { get; set; }
        public TextBox wantedChildLast
        {
            get { return MainWindow.NumbersSlots.Children[FirstVisible - 1] as TextBox; }
        }
        private int CountForDecision { get; set; }
        #endregion

        #region Visibilities
        #region Labels
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
        private Visibility labelRememberNumber { get; set; } = Visibility.Visible;
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
        private Visibility rememberNumberResults { get; set; } = Visibility.Hidden;
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
        private Visibility rememberNumberSettingsPanel { get; set; } = Visibility.Hidden;
        public Visibility RememberNumberSettingsPanel
        {
            get { return rememberNumberSettingsPanel; }
            set
            {
                if (rememberNumberSettingsPanel != value)
                {
                    rememberNumberSettingsPanel = value;
                    OnPropertyChange(nameof(RememberNumberSettingsPanel));
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
        private Visibility numbersSlots { get; set; } = Visibility.Hidden;
        public Visibility NumbersSlots
        {
            get { return numbersSlots; }
            set
            {
                if (numbersSlots != value)
                {
                    numbersSlots = value;
                    OnPropertyChange(nameof(NumbersSlots));
                }
            }
        }
        private Visibility progressBar1 { get; set; } = Visibility.Hidden;
        public Visibility ProgressBar1
        {
            get { return progressBar1; }
            set
            {
                if (progressBar1 != value)
                {
                    progressBar1 = value;
                    OnPropertyChange(nameof(ProgressBar1));
                }
            }
        }
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
        #endregion

        #region Commands
        public ICommand Start { get; set; }
        public ICommand ToStart { get; set; }
        public ICommand OpenSet { get; set; }
        public ICommand OpenResults { get; set; }
        public ICommand Exit { get; set; }
        #endregion
        public RememberNumberViewModel(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            Start = new Command(() => StartGame(), () => CanApp());
            OpenSet = new Command(() => OpenSettings(), () => CanApp());
            OpenResults = new Command(() => OpenGameResults(), () => CanApp());
            ToStart = new Command(() => BackToStart(), () => CanApp());
            Exit = new Command(() => ExitApp(), () => CanApp());
        }
        public void BackToStart()
        {
            SchulteGameVis = Visibility.Hidden;
            NumbersSlots = Visibility.Hidden;
            ProgressBar1 = Visibility.Hidden;
            GameVis = Visibility.Hidden;
            RememberNumberSettingsPanel = Visibility.Hidden;
            MainWindow.MainViewModel.LabelRememberNumber= Visibility.Hidden;
            StartImage = Visibility.Visible;

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
        private async Task StartGame()
        {
            LabelRememberNumber = Visibility.Visible;
            LabelMain = Visibility.Hidden;
            StartImage = Visibility.Hidden;
            StartPanel = Visibility.Hidden;
            SchulteGameVis = Visibility.Hidden;
            SchulteVarPanel = Visibility.Hidden;

            CurrentResult = string.Empty;
            Success = 0;
            CountForDecision = 0;
            RememberNumberResults = Visibility.Hidden;
            RememberNumberSettingsPanel = Visibility.Hidden;
            GameVis = Visibility.Visible;
            NumbersSlots = Visibility.Visible;
            ProgressBar1 = Visibility.Visible;

            FirstVisible = Int32.Parse(Digits);
            NTries = Int32.Parse(NOfTries);
            NTotalTries = Int32.Parse(TotalTries);
            Value1 = 0;
            SetButtons();
            SetNumberSlots(FirstVisible);
            Action();
        }
        private async Task Action()
        {
            Disable = false;
            if (wantedChildLast.Text != "_")
            {
                CompareNumbers();
                await Task.Delay(1700);
                if (NTotalTries == 0)
                {
                    ManageResults();
                }
                else
                {
                    if (NTries == 0)
                    {
                        if (CountForDecision == Int32.Parse(NOfTries))
                        {
                            FirstVisible += 1;
                        }
                        else
                        {
                            if (FirstVisible > 4)
                            {
                                FirstVisible -= 1;
                            }
                        }
                        CountForDecision = 0;
                        NTries = Int32.Parse(NOfTries);
                    }
                }
                Value1 += (int)Math.Ceiling((double)(100 / NTotalTries));
                SetNumberSlots(FirstVisible);
            }
            MakeNumberToRemember();
        }
        public void SetButtons()
        {
            if (Disable == false)
            {
                double top = 0;
                double left = 0;
                int RowsCount = 0;
                int ColumnsCount = 0;
                for (int i = 0; i < 11; i++)
                {
                    MyButton newBtn = new MyButton(4,MainWindow.ButtonPanel.Width, MainWindow.ButtonPanel.Height);
                    newBtn.Name = "Button" + i.ToString();
                    newBtn.Click += new RoutedEventHandler(ClickEvent);
                    ImageBrush myBrush = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/NeuroTraining;component/Resources/Square_64.jpg", UriKind.RelativeOrAbsolute)));
                    newBtn.Content = i.ToString();
                    if (i == 10)
                    {
                        myBrush = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/NeuroTraining;component/Resources/Button_Square_Del_blue_64.jpg", UriKind.RelativeOrAbsolute)));
                        newBtn.Content = "";
                    }
                    newBtn.Background = myBrush;
                    if (RowsCount == 3)
                    {
                        top = newBtn.Height * RowsCount;
                        left = newBtn.Width * (ColumnsCount + 1);
                    }
                    else
                    {
                        top = newBtn.Height * RowsCount;
                        left = newBtn.Width * ColumnsCount;
                    }
                    MainWindow.ButtonPanel.Children.Add(newBtn);
                    Canvas.SetTop(newBtn, top);
                    Canvas.SetLeft(newBtn, left);
                    if ((i + 1) % 3 == 0)
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
        private void SetNumberSlots(int NumberOfSlots)
        {
            MainWindow.NumbersSlots.Children.Clear();
            for (int i = 0; i < NumberOfSlots; i++)
            {
                MyTextBox myTextBox = new MyTextBox();
                myTextBox.Name = "TextBox" + i.ToString();
                myTextBox.Text = "_";
                MainWindow.NumbersSlots.Children.Add(myTextBox);
            }
        }
        private async Task MakeNumberToRemember()
        {
            Disable = true;
            List<int> num = new List<int>();
            NumToRemember = string.Empty;
            DelBoxes();
            for (int i = 0; i < FirstVisible; i++)
            {
                string digit = r.Next(0, 9).ToString();
                NumToRemember += digit;
                TextBox wantedChild = MainWindow.NumbersSlots.Children[i] as TextBox;
                wantedChild.Text = digit;
            }
            NumTyped = string.Empty;
            await Task.Delay(1000);
            DelBoxes();
            Disable = false;
        }
        void ClickEvent(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ClickedNumber = button.Content.ToString();
            ButtonName = button.Name;
            TypeOrDelete();
        }
        private async Task TypeOrDelete()
        {
            if (Disable == false)
            {
                for (int i = 0; i < FirstVisible; i++)
                {
                    TextBox wantedChild = MainWindow.NumbersSlots.Children[i] as TextBox;
                    if (ButtonName == "Button10")
                    {
                        if (i < FirstVisible - 1)
                        {
                            TextBox wantedChild1 = MainWindow.NumbersSlots.Children[i + 1] as TextBox;
                            if (wantedChild.Text != "_" && wantedChild1.Text == "_")
                            {
                                wantedChild.Text = "_";
                                if (!String.IsNullOrEmpty(NumTyped)) { NumTyped = NumTyped.Substring(0, NumTyped.Length - 1); }
                                break;
                            }
                        }
                        else if (i == FirstVisible - 1)
                        {
                            if (wantedChild.Text != "_")
                            {
                                wantedChild.Text = "_";
                                if (!String.IsNullOrEmpty(NumTyped)) { NumTyped = NumTyped.Substring(0, NumTyped.Length - 1); }
                                break;
                            }
                        }
                        Disable1 = true;
                    }
                    else
                    {
                        if (wantedChild.Text == "_")
                        {
                            wantedChild.Text = ClickedNumber;
                            NumTyped += ClickedNumber;
                            Disable1 = false;
                            break;
                        }
                    }
                }

                Debug.WriteLine("NumToRemember.Length: " + NumToRemember.Length);
                Debug.WriteLine("NumTyped.Length: " + NumTyped.Length);

                if (NumTyped.Length == NumToRemember.Length && Disable1 == false)
                {
                    Action();
                }
            }
        }
        private async Task CompareNumbers()
        {
            Disable = true;
            List<int> green = new List<int>();
            List<int> red = new List<int>();
            for (int i = 0; i < NumTyped.Length; i++)
            {
                if (NumTyped[i] == NumToRemember[i])
                {
                    green.Add(i);
                }
                else
                {
                    red.Add(i);
                }
            }
            for (int i = 0; i < FirstVisible; i++)
            {
                TextBox wantedChild = MainWindow.NumbersSlots.Children[i] as TextBox;
                if (green.Contains(i))
                {
                    wantedChild.Background = Brushes.Green;
                }
                else if (red.Contains(i))
                {
                    wantedChild.Background = Brushes.Red;
                }
            }
            if (green.Count == FirstVisible)
            {
                Success += 1;
                CountForDecision += 1;
            }
            if (NTries > 0)
            {
                NTries -= 1;
            }
            else
            {
                NTries = 0;
            }
            NTotalTries -= 1;
            NumTyped = string.Empty;
            await Task.Delay(1500);
            DelBoxes();

            Disable = false;
        }
        private void ManageResults()
        {
            RememberNumberSettingsPanel = Visibility.Hidden;
            RememberNumberResults = Visibility.Visible;
            GameVis = Visibility.Hidden;
            NumbersSlots = Visibility.Hidden;
            ProgressBar1 = Visibility.Hidden;
            Results = new List<int>(results.LoadResults("ResultsRememberNumber.res", TotalTries));
            Results.Add(Success);
            if (Results.Contains(-1)) { Results.Remove(-1); }
            results.SaveResults(Results, "ResultsRememberNumber.res", TotalTries);
            ResultPlot();
            List<int> list = new List<int>(Results);
            list.Sort();
            BestResult = list[list.Count - 1].ToString();
            CurrentResult = Success.ToString();
        }
        public void OpenGameResults()
        {
            RememberNumberSettingsPanel = Visibility.Hidden;
            RememberNumberResults = Visibility.Visible;
            GameVis = Visibility.Hidden;
            NumbersSlots = Visibility.Hidden;
            ProgressBar1 = Visibility.Hidden;
            StartImage = Visibility.Hidden;
            StartPanel = Visibility.Hidden;
            if (SchulteGameVis == Visibility.Visible)
            {
                SchulteGameVis = Visibility.Hidden;
            }
            else
            {
                SchulteGameVis = Visibility.Visible;
                Results = new List<int>(results.LoadResults("ResultsRememberNumber.res", TotalTries));
                if (Results.Contains(-1)) { Results.Remove(-1); }
                if (Results.Count > 0)
                {
                    var list = new List<int>(Results);
                    CurrentResult = string.Empty;
                    ResultPlot();
                    list.Sort();
                    BestResult = list[list.Count - 1].ToString();
                }
                else
                {
                    BestResult = string.Empty;
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
        public void OpenSettings()
        {
            if (RememberNumberSettingsPanel == Visibility.Visible)
            {
                RememberNumberSettingsPanel = Visibility.Hidden;
            }
            else
            {
                RememberNumberSettingsPanel = Visibility.Visible;
            }
            SchulteGameVis = Visibility.Hidden;
            StartImage = Visibility.Hidden;
            StartPanel = Visibility.Hidden;
        }
        public void ResultPlot()
        {
            SchulteGameVis = Visibility.Visible;
            MainWindow.Screen.Children.Clear();
            const double margin = 5;
            Plot plot = new Plot();
            Xaxis xaxis = new Xaxis(margin, MainWindow.Screen.Width, MainWindow.Screen.Height, Results.Count);
            YaxisRememberNumber yaxis = new YaxisRememberNumber(margin, MainWindow.Screen.Height);
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
        private void DelBoxes()
        {
            for (int i = 0; i < FirstVisible; i++)
            {
                TextBox wantedChild = MainWindow.NumbersSlots.Children[i] as TextBox;
                wantedChild.Text = "_";
                wantedChild.Background = Brushes.White;
            }
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
