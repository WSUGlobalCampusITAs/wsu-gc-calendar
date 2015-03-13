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

        private readonly ICommand _updateCommand;

        public MainWindowViewModel()
        {
            _semesters = new SemesterLogic();
            _updateCommand = new RelayCommand(Update);
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
            _semesters.NewSemesters(Settings.Default.OldStart, Settings.Default.NewStart, (int)Settings.Default.NumberOfWeeks, Settings.Default.IncludeBreaks);
            SingleWeek.Add(new WeekViewModel(_semesters, false));
            SingleWeek.Add(new WeekViewModel(_semesters, true));
        }

        public ICommand UpdateCommand { get { return _updateCommand; } }
    }
}
