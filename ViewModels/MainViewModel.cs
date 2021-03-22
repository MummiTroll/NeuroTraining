using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using NeuroTraining.Enums;

namespace NeuroTraining.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Objects
        public MainWindow MainWindow { get; set; }
        public SchulteViewModel SchulteViewModel { get; set; }
        public RememberNumberViewModel RememberNumberViewModel { get; set; }
        public EvenNumbersViewModel EvenNumbersViewModel { get; set; }
        public ConcentrationViewModel ConcentrationViewModel { get; set; }
        Time time = new Time();
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
        private DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Next
        {
            get
            {
                return SchulteViewModel.Next;
            }
            set
            {
                if (SchulteViewModel.Next != value)
                {
                    SchulteViewModel.Next = value;
                    OnPropertyChange(nameof(Next));
                }
            }
        }
        public string Current
        {
            get
            {
                return SchulteViewModel.Current;
            }
            set
            {
                if (SchulteViewModel.Current != value)
                {
                    SchulteViewModel.Current = value;
                    OnPropertyChange(nameof(Current));
                }
            }
        }
        public string Best
        {
            get
            {
                return SchulteViewModel.Best;
            }
            set
            {
                if (SchulteViewModel.Best != value)
                {
                    SchulteViewModel.Best = value;
                    OnPropertyChange(nameof(Best));
                }
            }
        }
        public string SetFieldSize
        {
            get
            {
                return SchulteViewModel.SetFieldSize.ToString();
            }
            set
            {
                if (SchulteViewModel.SetFieldSize != value)
                {
                    SchulteViewModel.SetFieldSize = value;
                    OnPropertyChange(nameof(SetFieldSize));
                }
            }
        }
        public bool FourChecked
        {
            get
            {
                return SchulteViewModel.FourChecked;
            }
            set
            {
                if (SchulteViewModel.FourChecked != value)
                {
                    SchulteViewModel.FourChecked = value;
                    OnPropertyChange(nameof(FourChecked));
                }
            }
        }
        public bool FiveChecked
        {
            get
            {
                return SchulteViewModel.FiveChecked;
            }
            set
            {
                if (SchulteViewModel.FiveChecked != value)
                {
                    SchulteViewModel.FiveChecked = value;
                    OnPropertyChange(nameof(FiveChecked));
                }
            }
        }
        public bool SixChecked
        {
            get
            {
                return SchulteViewModel.SixChecked;
            }
            set
            {
                if (SchulteViewModel.SixChecked != value)
                {
                    SchulteViewModel.SixChecked = value;
                    OnPropertyChange(nameof(SixChecked));
                }
            }
        }
        public bool SevenChecked
        {
            get
            {
                return SchulteViewModel.SevenChecked;
            }
            set
            {
                if (SchulteViewModel.SevenChecked != value)
                {
                    SchulteViewModel.SevenChecked = value;
                    OnPropertyChange(nameof(SevenChecked));
                }
            }
        }
        public int FieldSize
        {
            get
            {
                return SchulteViewModel.FieldSize;
            }
            set
            {
                if (SchulteViewModel.FieldSize != value)
                {
                    SchulteViewModel.FieldSize = value;
                    OnPropertyChange(nameof(FieldSize));
                }
            }
        }
        public string ClickedNumber
        {
            get
            {
                return SchulteViewModel.ClickedNumber;
            }
            set
            {
                if (SchulteViewModel.ClickedNumber != value)
                {
                    SchulteViewModel.ClickedNumber = value;
                    OnPropertyChange(nameof(ClickedNumber));
                }
            }
        }
        public List<bool> Radiobuttons
        {
            get
            {
                return new List<bool>() { FourChecked, FiveChecked, SixChecked, SevenChecked };
            }
        }
        private MainWindowFunctions visible = MainWindowFunctions.Normalize;
        public MainWindowFunctions Visible
        {
            get { return this.visible; }
            set
            {
                this.visible = value;

                OnPropertyChange(nameof(Visible));
                OnPropertyChange(nameof(MaximizeButton));
                OnPropertyChange(nameof(NormalizeButton));
            }
        }
        public Visibility MaximizeButton => Visible == MainWindowFunctions.Normalize ? Visibility.Visible : Visibility.Collapsed;
        public Visibility NormalizeButton => Visible == MainWindowFunctions.Maximize ? Visibility.Visible : Visibility.Collapsed;
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
        private Visibility labelMain { get; set; } = Visibility.Visible;
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
        private Visibility startVis { get; set; } = Visibility.Hidden;
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
        private Visibility settingsButtonVisibility { get; set; } = Visibility.Hidden;
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
        private Visibility rememberNumberSlots { get; set; } = Visibility.Hidden;
        public Visibility RememberNumberSlots
        {
            get
            {
                return rememberNumberSlots;
            }
            set
            {
                if (rememberNumberSlots != value)
                {
                    rememberNumberSlots = value;
                    OnPropertyChange(nameof(RememberNumberSlots));
                }
            }
        }
        private Visibility v12 { get; set; } = Visibility.Collapsed;
        public Visibility V12
        {
            get { return v12; }
            set
            {
                if (v12 != value)
                {
                    v12 = value;
                    OnPropertyChange(nameof(V12));
                }
            }
        }
        private Visibility v11 { get; set; } = Visibility.Collapsed;
        public Visibility V11
        {
            get { return v11; }
            set
            {
                if (v11 != value)
                {
                    v11 = value;
                    OnPropertyChange(nameof(V11));
                }
            }
        }
        private Visibility v10 { get; set; } = Visibility.Collapsed;
        public Visibility V10
        {
            get { return v10; }
            set
            {
                if (v10 != value)
                {
                    v10 = value;
                    OnPropertyChange(nameof(V10));
                }
            }
        }
        private Visibility v9 { get; set; } = Visibility.Collapsed;
        public Visibility V9
        {
            get { return v9; }
            set
            {
                if (v9 != value)
                {
                    v9 = value;
                    OnPropertyChange(nameof(V9));
                }
            }
        }
        private Visibility v8 { get; set; } = Visibility.Collapsed;
        public Visibility V8
        {
            get { return v8; }
            set
            {
                if (v8 != value)
                {
                    v8 = value;
                    OnPropertyChange(nameof(V8));
                }
            }
        }
        private Visibility v7 { get; set; } = Visibility.Collapsed;
        public Visibility V7
        {
            get { return v7; }
            set
            {
                if (v7 != value)
                {
                    v7 = value;
                    OnPropertyChange(nameof(V7));
                }
            }
        }
        private Visibility v6 { get; set; } = Visibility.Collapsed;
        public Visibility V6
        {
            get { return v6; }
            set
            {
                if (v6 != value)
                {
                    v6 = value;
                    OnPropertyChange(nameof(V6));
                }
            }
        }
        private Visibility v5 { get; set; } = Visibility.Collapsed;
        public Visibility V5
        {
            get { return v5; }
            set
            {
                if (v5 != value)
                {
                    v5 = value;
                    OnPropertyChange(nameof(V5));
                }
            }
        }
        private Visibility v4 { get; set; }
        public Visibility V4
        {
            get { return v4; }
            set
            {
                if (v4 != value)
                {
                    v4 = value;
                    OnPropertyChange(nameof(V4));
                }
            }
        }
        private Visibility v3 { get; set; }
        public Visibility V3
        {
            get { return v3; }
            set
            {
                if (v3 != value)
                {
                    v3 = value;
                    OnPropertyChange(nameof(V3));
                }
            }
        }
        private Visibility v2 { get; set; }
        public Visibility V2
        {
            get { return v2; }
            set
            {
                if (v2 != value)
                {
                    v2 = value;
                    OnPropertyChange(nameof(V2));
                }
            }
        }
        private Visibility v1 { get; set; }
        public Visibility V1
        {
            get { return v1; }
            set
            {
                if (v1 != value)
                {
                    v1 = value;
                    OnPropertyChange(nameof(V1));
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

        #region Commands
        public ICommand MaximizeIt { get; set; }
        public ICommand NormalizeIt { get; set; }
        public ICommand MinimizeIt { get; set; }
        public ICommand CloseIt { get; set; }
        public ICommand StartNumbers { get; set; }
        public ICommand StartSchulte { get; set; }
        public ICommand EvenNumbers { get; set; }
        public ICommand Concentration { get; set; }
        public ICommand Start { get; set; }
        public ICommand ToStart { get; set; }
        public ICommand Exit { get; set; }
        #endregion
        public MainViewModel(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            SchulteViewModel = new SchulteViewModel(mainWindow);
            RememberNumberViewModel = new RememberNumberViewModel(mainWindow);
            EvenNumbersViewModel = new EvenNumbersViewModel(mainWindow);
            ConcentrationViewModel = new ConcentrationViewModel(mainWindow);
            StartNumbers = new Command(() => StartRememberNumber(), () => CanApp());
            StartSchulte = new Command(() => StartSchulteTable(), () => CanApp());
            EvenNumbers = new Command(() => StartEvenNumbers(), () => CanApp());
            Concentration = new Command(() => StartConcentration(), () => CanApp());
            Start = new Command(() => SchulteViewModel.StartGame(), () => CanApp());
            Exit = new Command(() => ExitApp(), () => CanApp());

            //LabelRememberNumber = Visibility.Hidden;
            //LabelMain = Visibility.Visible;
            //LabelSchulte = Visibility.Hidden;
            //LabelEvenNumbers = Visibility.Hidden;
            //LabelConcentration = Visibility.Hidden;
        }
        public string timeValue { get; set; }
        public string TimeValue
        {
            get
            {
                return timeValue;
            }
            set
            {
                if (timeValue != SchulteViewModel.TimeValue)
                {
                    timeValue = SchulteViewModel.TimeValue;
                    OnPropertyChange(nameof(SchulteViewModel.TimeValue));
                }
            }
        }
        private void StartRememberNumber()
        {
            MainWindow.Screen.Children.Clear();
            LabelRememberNumber = Visibility.Visible;

            Image numbers = new Image()
            {
                Source = new BitmapImage(new Uri(@"Resources\RememberNumbers2.jpg", UriKind.Relative))
            };
            MainWindow.StartImage.Children.Clear();
            MainWindow.StartImage.Children.Add(numbers);
            numbers.Width = MainWindow.StartImage.Width;
            numbers.Height = MainWindow.StartImage.Height;
            Canvas.SetTop(numbers, 0);
            Canvas.SetLeft(numbers, 0);
            MainWindow.DataContext = MainWindow.DC[1];
        }
        private void StartSchulteTable()
        {
            LabelSchulte = Visibility.Visible;
            //MainWindow.NumbersViewModel.NumbersSlots = Visibility.Hidden;

            Image schulte = new Image()
            {
                Source = new BitmapImage(new Uri(@"Resources\SchulteTable.png", UriKind.Relative))
            };
            MainWindow.StartImage.Children.Clear();
            MainWindow.StartImage.Children.Add(schulte);
            schulte.Width = MainWindow.StartImage.Width;
            schulte.Height = MainWindow.StartImage.Height;
            Canvas.SetTop(schulte, 0);
            Canvas.SetLeft(schulte, 0);
            MainWindow.Screen.Children.Clear();
            MainWindow.DataContext = MainWindow.DC[2];
        }
        private void StartEvenNumbers()
        {
            LabelEvenNumbers = Visibility.Visible;
            Image numbers = new Image()
            {
                Source = new BitmapImage(new Uri(@"Resources\EvenNumbers.png", UriKind.Relative))
            };
            MainWindow.StartImage.Children.Clear();
            MainWindow.StartImage.Children.Add(numbers);
            numbers.Width = MainWindow.StartImage.Width;
            numbers.Height = MainWindow.StartImage.Height;
            Canvas.SetTop(numbers, 0);
            Canvas.SetLeft(numbers, 0);
            MainWindow.DataContext = MainWindow.DC[3];
        }
        private void StartConcentration()
        {
            LabelConcentration = Visibility.Visible;
            Image concentration = new Image()
            {
                Source = new BitmapImage(new Uri(@"Resources\Concentration.png", UriKind.Relative))
            };
            MainWindow.StartImage.Children.Clear();
            MainWindow.StartImage.Children.Add(concentration);
            concentration.Width = MainWindow.StartImage.Width;
            concentration.Height = MainWindow.StartImage.Height;
            Canvas.SetTop(concentration, 0);
            Canvas.SetLeft(concentration, 0);
            MainWindow.Screen.Children.Clear();
            MainWindow.DataContext = MainWindow.DC[4];
        }
        private void ExitApp()
        {
            System.Windows.Application.Current.Shutdown();
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
