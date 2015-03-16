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
        private ObservableCollection<ViewModelBase> _currentViewModel;
        private List<ViewModelBase> _viewModels;
        private readonly ICommand _fullCalendarCommand;
        private SemestersViewModel _fullCalendar;
        private SemesterLogic _semesters;

        public MainWindowViewModel()
        {
            _semesters = new SemesterLogic();
            _currentViewModel = new ObservableCollection<ViewModelBase>();
            _currentViewModel.Add(new MainViewViewModel(_semesters));
            _viewModels = new List<ViewModelBase>();
            _viewModels.Add(_currentViewModel[0]); //Starting View Model is the Main View, which also takes the 0 spot in the ViewModel List
            _fullCalendarCommand = new RelayCommand(FullCalendar);
        }

        public ObservableCollection<ViewModelBase> CurrentViewModel
        {
            get { return _currentViewModel; }
        }

        public void FullCalendar(object obj)
        {
            _fullCalendar = new SemestersViewModel(_semesters);
        }


        
    }
}
