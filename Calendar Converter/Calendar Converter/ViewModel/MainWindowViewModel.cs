using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Calendar_Converter.DataAccess;
using System.Windows.Input;
using Calendar_Converter.Properties;
using Calendar_Converter.Model;

namespace Calendar_Converter.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        readonly SemesterLogic _semesters;
        ObservableCollection<ViewModelBase> _currentWeeks;
        private int NumberOfWeeks;
        private SemestersViewModel _fullCalendar;

        private readonly ICommand _updateCommand;
        private readonly ICommand _nextCommand;
        private readonly ICommand _previousCommand;
        private readonly ICommand _fullCalendarCommand;

        public MainWindowViewModel()
        {
            _semesters = new SemesterLogic();
            NumberOfWeeks = (int)Settings.Default.NumberOfWeeks;
            _updateCommand = new RelayCommand(Update);
            _nextCommand = new RelayCommand(Next, new Predicate<object>(i => Settings.Default.CurrentWeek < NumberOfWeeks - 1));
            _previousCommand = new RelayCommand(Previous, new Predicate<object>(i => Settings.Default.CurrentWeek > 0));
            _fullCalendarCommand = new RelayCommand(FullCalendar);
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
            if(Settings.Default.IncludeBreaks)
            {
                NumberOfWeeks++;
            }
            _semesters.NewSemesters(Settings.Default.OldStart, Settings.Default.NewStart, NumberOfWeeks, Settings.Default.IncludeBreaks);
            Settings.Default.CurrentWeek = 0;
            SingleWeek.Add(new WeekViewModel(_semesters, true));
            SingleWeek.Add(new WeekViewModel(_semesters, false));
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
            _fullCalendar = new SemestersViewModel(_semesters);
        }

        public ICommand UpdateCommand { get { return _updateCommand; } }
        public ICommand NextCommand { get { return _nextCommand; } }
        public ICommand PreviousCommand { get { return _previousCommand; } }
    }
}
