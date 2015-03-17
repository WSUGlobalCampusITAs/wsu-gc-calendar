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
using System.Collections.Generic;

namespace Calendar_Converter.Model
{
    /// <summary>
    /// Data Model for the Semester Week
    /// Contains a collection of days, and various properties. 
    /// </summary>
    public class Week
    {
        #region Member Variables

        protected DateTime memStart;
        protected bool memisBreak;
        protected List<Day> _days;
        protected string memBreakName;

        #endregion

        #region Constructors

        public static Week CreateWeek(DateTime Start)
        {
            return new Week { memStart = Start, memisBreak = false, memBreakName = null };

        }

        #endregion

        #region Properties

        public DateTime Start
        {
            get { return memStart; }
        }

        public DateTime End
        {
            get { return memStart.AddDays(6); }
        }

        public List<Day> Days
        {
            get
            {
                if(_days == null)
                {
                    _days = new List<Day>();

                    for(int i = 0; i < 7; i++)
                    {
                        _days.Add(new Day(memStart.AddDays(i)));
                        if(memisBreak)
                        {
                            _days[i].Date = memBreakName;
                        }
                    }
                }
                return _days;
            }
        }

        public bool IsBreak
        {
            get { return memisBreak; }
        }

        #endregion
    }
}
