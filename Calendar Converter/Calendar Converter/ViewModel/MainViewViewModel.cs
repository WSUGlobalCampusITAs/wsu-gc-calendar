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
        private int _numberOfWeeks;
        private DateTime _oldStart;
        private DateTime _newStart;
        

        private readonly ICommand _updateCommand;
        private readonly ICommand _nextCommand;
        private readonly ICommand _previousCommand;
        private readonly ICommand _fullSemesterCommand;
        private bool _breaksChecked;

        public MainViewViewModel(SemesterLogic Semesters)
        {
            _semesters = Semesters;
            _numberOfWeeks = (int)Settings.Default.NumberOfWeeks;
            _oldStart = Settings.Default.OldStart;
            _newStart = Settings.Default.NewStart;
            _updateCommand = new RelayCommand(Update, new Predicate<object>(i => (OldDate != null) && (NewDate != null)));
            _nextCommand = new RelayCommand(Next, new Predicate<object>(i => Settings.Default.CurrentWeek < _numberOfWeeks - 1));
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
            int i = 0;
            if(_breaksChecked)
            {
                    i++;
            }

            if ((OldDate != null) && (NewDate != null))
            {
                _semesters.NewSemesters(_oldStart, _newStart, _numberOfWeeks + i, _breaksChecked);
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
            }
        }

        public DateTime? OldDate
        {
            get 
            { 
                if(_oldStart == DateTime.MinValue)
                {
                    return null;
                }
                else
                {
                    return _oldStart as DateTime?;
                }
            }
            set { _oldStart = (DateTime)value; }
        }

        public DateTime? NewDate
        {
            get
            {
                if (_newStart == DateTime.MinValue)
                {
                    return null;
                }
                else
                {
                    return _newStart as DateTime?;
                }
            }
            set { _newStart = (DateTime)value; }
        }

        public int Weeks
        {
            get { return _numberOfWeeks; }
            set { _numberOfWeeks = value; }
        }
        public ICommand UpdateCommand { get { return _updateCommand; } }
        public ICommand NextCommand { get { return _nextCommand; } }
        public ICommand PreviousCommand { get { return _previousCommand; } }
        public ICommand FullSemesterCommand { get { return _fullSemesterCommand; } }
    }
}
