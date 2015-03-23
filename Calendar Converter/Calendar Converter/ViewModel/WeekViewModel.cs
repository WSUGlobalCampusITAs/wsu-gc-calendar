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
using Calendar_Converter.Model;
using System;
using System.Collections.ObjectModel;
using Calendar_Converter.Properties;
using System.Windows.Media;
using MVVMObjectLibrary;

namespace Calendar_Converter.ViewModel
{
    public class WeekViewModel : ViewModelBase
    {
        #region Member Variables

        readonly SemesterLogic _semesters;
        private Brush _color;

        #endregion

        #region Constructors

        public WeekViewModel(SemesterLogic Semesters, bool IsOldWeek)
        {
            if (Semesters == null)
            {
                throw new ArgumentNullException("Semesters");
            }

            _semesters = Semesters;
            int OldOrNew = 0;

            if (!IsOldWeek)
            { OldOrNew = 1; }

            this.Days = new ObservableCollection<Day>(_semesters.Semesters[OldOrNew].Week(Settings.Default.CurrentWeek).Days);

            if (IsOldWeek)
            {
                this.Color = Settings.Default.OldSemesterColor;
            }
            else
            {
                this.Color = Settings.Default.NewSemesterColor;
            }
        }

        public WeekViewModel(SemesterLogic Semesters, bool IsOldWeek, int WeekNumber)
        {
            if (Semesters == null)
            {
                throw new ArgumentNullException("Semesters");
            }

            _semesters = Semesters;
            int OldOrNew = 0;

            if (!IsOldWeek)
            { OldOrNew = 1; }

            this.Days = new ObservableCollection<Day>(_semesters.Semesters[OldOrNew].Week(WeekNumber).Days);

            if (IsOldWeek)
            {
                this.Color = Settings.Default.OldSemesterColor;
            }
            else
            {
                this.Color = Settings.Default.NewSemesterColor;
            }

        }
        #endregion

        #region Properties

        public ObservableCollection<Day> Days { get; private set; }

        public Brush Color
        {
            get { return _color; }
            private set { _color = value; }
        }

        #endregion

        #region Member Methods

        protected override void OnDispose()
        {
            this.Days.Clear();
        }

        #endregion
    }
}
