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
        private ViewModelBase _fullCalendar;
        private SemesterLogic _semesters;


        public MainWindowViewModel()
        {
            _semesters = new SemesterLogic();
            _currentViewModel = new ObservableCollection<ViewModelBase>();
            _currentViewModel.Add(new MainViewViewModel(_semesters));
            _viewModels = new List<ViewModelBase>();
            _viewModels.Add(_currentViewModel[0]); //Starting View Model is the Main View, which also takes the 0 spot in the ViewModel List
            _fullCalendar = null;
            _currentViewModel[0].PropertyChanged += MainWindowViewModel_PropertyChanged;
        }

        void MainWindowViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "OpenFullCalendar") { OpenFullCalendar(this); }
        }

        private void CloseFullCalendar(MainWindowViewModel mainWindowViewModel)
        {
            _currentViewModel.Clear();
            _currentViewModel.Add(_viewModels[0]);
        }

        public ObservableCollection<ViewModelBase> CurrentViewModel
        {
            get { return _currentViewModel; }
        }

        public void OpenFullCalendar(object obj)
        {
            if (_fullCalendar == null)
            {
                _fullCalendar = new SemestersViewModel(_semesters);
                _viewModels.Add(_fullCalendar);
            }
            else
            {
                _fullCalendar = new SemestersViewModel(_semesters);
                _viewModels[1] = _fullCalendar; //_viewModels index 1 will be the permanent position for _fullCalendar
            }
            _fullCalendar.PropertyChanged += _fullCalendar_PropertyChanged;
            _currentViewModel.Clear();
            _currentViewModel.Add(_fullCalendar);
        }

        void _fullCalendar_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
             CloseFullCalendar(this); 
        }        
    }
}
