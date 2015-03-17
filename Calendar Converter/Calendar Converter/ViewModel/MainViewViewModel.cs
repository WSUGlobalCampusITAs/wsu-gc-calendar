using Calendar_Converter.DataAccess;
using Calendar_Converter.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calendar_Converter.ViewModel
{
    public class MainViewViewModel : ViewModelBase
    {
        readonly SemesterLogic _semesters;
        ObservableCollection<ViewModelBase> _currentWeeks;
        private int NumberOfWeeks;
        

        private readonly ICommand _updateCommand;
        private readonly ICommand _nextCommand;
        private readonly ICommand _previousCommand;
        private readonly ICommand _fullSemesterCommand;
        private bool _breaksChecked;

        public MainViewViewModel(SemesterLogic Semesters)
        {
            _semesters = Semesters;
            NumberOfWeeks = (int)Settings.Default.NumberOfWeeks;
            _updateCommand = new RelayCommand(Update, new Predicate<object>(i => (OldDate != null) && (NewDate != null)));
            _nextCommand = new RelayCommand(Next, new Predicate<object>(i => Settings.Default.CurrentWeek < NumberOfWeeks - 1));
            _previousCommand = new RelayCommand(Previous, new Predicate<object>(i => Settings.Default.CurrentWeek > 0));
            _fullSemesterCommand = new RelayCommand(FullCalendar, new Predicate<object>(i=> (_semesters.Semesters.Count > 1)));
            _breaksChecked = Settings.Default.IncludeBreaks;
            
        }


        public ObservableCollection<ViewModelBase> SingleWeek
        {
            get
            {
                if(_currentWeeks == null)
                {
                    _currentWeeks = new ObservableCollection<ViewModelBase>();             
                    Update(this);
                }
                return _currentWeeks;
            }
        }

        public void Update(object obj)
        {
            _currentWeeks.Clear();
            NumberOfWeeks = (int)Settings.Default.NumberOfWeeks;
            if(_breaksChecked)
            {
                    NumberOfWeeks++;
            }

            if ((OldDate != null) && (NewDate != null))
            {
                _semesters.NewSemesters(Settings.Default.OldStart, Settings.Default.NewStart, NumberOfWeeks, Settings.Default.IncludeBreaks);
                Settings.Default.CurrentWeek = 0;
                SingleWeek.Add(new WeekViewModel(_semesters, true));
                SingleWeek.Add(new WeekViewModel(_semesters, false));
            }
            
            
        }

        public void Next(object obj)
        {
            Settings.Default.CurrentWeek++;
            _currentWeeks.Clear();
            SingleWeek.Add(new WeekViewModel(_semesters, true));
            SingleWeek.Add(new WeekViewModel(_semesters, false));

        }

        public void Previous(object obj)
        {
            Settings.Default.CurrentWeek--;
            _currentWeeks.Clear();
            SingleWeek.Add(new WeekViewModel(_semesters, true));
            SingleWeek.Add(new WeekViewModel(_semesters, false));
        }

        public void FullCalendar(object obj)
        {
            this.OnPropertyChanged("OpenFullCalendar");
        }

        public bool BreaksChecked
        {
            get { return _breaksChecked; }
            set 
            { 
                _breaksChecked = value;
                Settings.Default.IncludeBreaks = value;
            }
        }

        public DateTime? OldDate
        {
            get 
            { 
                if(Settings.Default.OldStart == DateTime.MinValue)
                {
                    return null;
                }
                else
                {
                    return Settings.Default.OldStart as DateTime?;
                }
            }
            set { Settings.Default.OldStart = (DateTime)value; }
        }

        public DateTime? NewDate
        {
            get
            {
                if (Settings.Default.NewStart == DateTime.MinValue)
                {
                    return null;
                }
                else
                {
                    return Settings.Default.NewStart as DateTime?;
                }
            }
            set { Settings.Default.NewStart = (DateTime)value; }
        }
        public ICommand UpdateCommand { get { return _updateCommand; } }
        public ICommand NextCommand { get { return _nextCommand; } }
        public ICommand PreviousCommand { get { return _previousCommand; } }
        public ICommand FullSemesterCommand { get { return _fullSemesterCommand; } }
    }
}
