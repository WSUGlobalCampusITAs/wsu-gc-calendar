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
        private ViewModelBase _fullCalendar;
        private SemesterLogic _semesters;
        private bool _isChecked;


        public MainWindowViewModel()
        {
            _semesters = new SemesterLogic();
            _currentViewModel = new ObservableCollection<ViewModelBase>();
            _currentViewModel.Add(new MainViewViewModel(_semesters));
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
            _currentViewModel.Add(new MainViewViewModel(_semesters));
            Settings.Default.IncludeBreaks = _isChecked;
            
        }

        public ObservableCollection<ViewModelBase> CurrentViewModel
        {
            get { return _currentViewModel; }
        }

        public void OpenFullCalendar(object obj)
        {
            _isChecked = Settings.Default.IncludeBreaks;
           
            _fullCalendar = new SemestersViewModel(_semesters);
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
