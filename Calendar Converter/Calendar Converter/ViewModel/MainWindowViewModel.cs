//  Copyright 2014 Washington State University

//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at

//     http://www.apache.org/licenses/LICENSE-2.0

//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

//   Supervisor             Celisse Ellis
//   Design Lead            David Lystad
//   Programming Lead       David Lystad

using System.Collections.ObjectModel;
using Calendar_Converter.DataAccess;
using System.Windows.Input;
using Calendar_Converter.Properties;
using MVVMObjectLibrary;

namespace Calendar_Converter.ViewModel
{
    /// <summary>
    /// Class  
    ///     MainWindow View Model
    /// Parent Class
    ///     ViewModelBase
    /// Interfaces Used
    ///     INotify
    ///     IDisposable
    /// Description 
    ///     This class serves as the main ViewModel for the Calendar Converter Application. 
    ///     It instantiates the other ViewModels, as well as the DataAccess repository. It makes use of various
    ///     Commands, and Observable Collections to allow objects to be passed to the views via data bindings. It 
    ///     is only referenced by the Views, but does not itself call any view.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {

    #region Member Variables
        private ViewModelBase _currentViewModel;
        private SemesterLogic _semesters;
        private ViewModelBase _mainView;
        private bool _isChecked;
        private ICommand _settingsCommand;
    #endregion

    #region Default Constructor
        public MainWindowViewModel()
        {
            _semesters = new SemesterLogic();
            _mainView = new MainViewViewModel(_semesters);
            _currentViewModel = _mainView;
            _currentViewModel.PropertyChanged += MainWindowViewModel_PropertyChanged;
            _settingsCommand = new RelayCommand(SettingsViewShow);
            Settings.Default.Reload();
        }
    #endregion

    #region Event Handlers
        /// <summary>
        /// MainWindowViewModel_PropertyChanged event allows INotify to communicate that the MainView has clicked
        /// Open Full Calendar, which prompts the system to close the Main View, and open the Full Calendar View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainWindowViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "OpenFullCalendar") { OpenFullCalendar(this); }
        }

        /// <summary>
        /// Settings View Hide provides event handling from the Settings View Model, indicating that the user is finished
        /// modifying settings. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsViewHide(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CloseFullCalendar(this);
        }

        /// <summary>
        /// This event handler is called when the Semester View indicates that it needs to close. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FullCalendar_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CloseFullCalendar(this);
        }  

    #endregion

    #region Properties
        /// <summary>
        /// The CurrentViewModel Property provides the data for the MainWindow that allows the various User Controls to 
        /// be populated, and displayed. 
        /// </summary>
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }

    #endregion

    #region Member Methods
        /// <summary>
        /// The three private methods contained are used to open and close View Models to allow them
        /// to be properly displayed on the MainWindow View. 
        /// </summary>
        /// <param name="mainWindowViewModel"></param>
        private void CloseFullCalendar(MainWindowViewModel mainWindowViewModel)
        {        
            this.CurrentViewModel = _mainView;
            Settings.Default.IncludeBreaks = _isChecked;
        }        

        private void OpenFullCalendar(object obj)
        {
            this.CurrentViewModel = new SemestersViewModel(_semesters);
           this.CurrentViewModel.PropertyChanged += FullCalendar_PropertyChanged;
        }
  
        private void SettingsViewShow(object obj)
        {
            this.CurrentViewModel = new SettingsViewModel();
            this.CurrentViewModel.PropertyChanged += SettingsViewHide;
        }

        /// <summary>
        /// OnDispose Overrides the default OnDispose, and causes the collections 
        /// instantiated to be cleared. 
        /// </summary>
        protected override void OnDispose()
        {
        }
        #endregion
    
    }
}
