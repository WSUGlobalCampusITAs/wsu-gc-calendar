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

using Calendar_Converter.DataAccess;
using Calendar_Converter.Properties;
using MVVMObjectLibrary;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Calendar_Converter.ViewModel
{
    /// <summary>
    /// Main View View Model provides the data handling for the Main View, this includes
    /// providing Observable Collections, as well as Command Properties
    /// </summary>
    public class MainViewViewModel : ViewModelBase
    {
        #region Member Variables

        private SemesterLogic _semesters;
        private ObservableCollection<ViewModelBase> _currentWeeks;
        private int _numberOfWeeks;
        private DateTime _oldStart;
        private DateTime _newStart;
        private readonly ICommand _updateCommand;
        private readonly ICommand _nextCommand;
        private readonly ICommand _previousCommand;
        private readonly ICommand _fullSemesterCommand;
        private bool _breaksChecked;

        #endregion

        #region Constructors
        public MainViewViewModel()
        {
            _semesters = new SemesterLogic();
            _numberOfWeeks = (int)Settings.Default.NumberOfWeeks;
            _oldStart = Settings.Default.OldStart;
            _newStart = Settings.Default.NewStart;
            _updateCommand = new RelayCommand(Update, new Predicate<object>(i => (OldDate != null) && (NewDate != null)));
            _nextCommand = new RelayCommand(Next, new Predicate<object>(i => Settings.Default.CurrentWeek < _numberOfWeeks - 1));
            _previousCommand = new RelayCommand(Previous, new Predicate<object>(i => Settings.Default.CurrentWeek > 0));
            _fullSemesterCommand = new RelayCommand(FullCalendar, new Predicate<object>(i => (_semesters.Semesters.Count > 1)));
            _breaksChecked = Settings.Default.IncludeBreaks;
        }

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

        #endregion 

        #region Properties

            #region Data

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
                if (_oldStart == DateTime.MinValue)
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

            #endregion

            #region Command

        public ICommand UpdateCommand { get { return _updateCommand; } }
        public ICommand NextCommand { get { return _nextCommand; } }
        public ICommand PreviousCommand { get { return _previousCommand; } }
        public ICommand FullSemesterCommand { get { return _fullSemesterCommand; } }

            #endregion

        #endregion

        #region Member Methods

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

        #endregion  
    }
}
