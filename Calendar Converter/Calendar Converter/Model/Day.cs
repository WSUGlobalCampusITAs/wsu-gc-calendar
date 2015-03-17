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

using System;
using Calendar_Converter.Properties;

namespace Calendar_Converter.Model
{
    /// <summary>
    /// Day class provides the lowest level object for the Semester Repository
    /// </summary>
    public class Day
    {
        #region Member Variables

        private DateTime _date;
        private string _output;

        #endregion

        #region Constructors

        public Day(DateTime Date)
        {
            _date = Date;
            _output = _date.GetDateTimeFormats()[Settings.Default.DateFormatString];
        }

        #endregion

        #region Properties

        public string DayOfWeek
        {
            get { return _date.DayOfWeek.ToString(); }
        }

        public string Date //The Date Property provides a set feature to allow the string to changed to a BreakName if necessary. 
        {
            get { return _output; }
            set { _output = value; }
        }

        #endregion
    }
}
