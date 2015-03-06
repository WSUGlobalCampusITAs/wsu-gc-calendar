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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Converter
{
    /// <summary>
    /// The Week class acts as a container class for the Weeks of a given semester. 
    /// This class allows specific weeks to be specified, added to and removed. 
    /// </summary>
    public class Week
    {
        //protected member variables
        protected int memWeekNumber {get; set;}
        protected DateTime memWeekStart { get; set; }
        protected DateTime memWeekEnd { get; set; }
        protected bool memIsBreak { get; set; }
        protected string memBreakName;

        /// <summary>
        /// Default constructor for the Week Class
        /// </summary>
        public Week()
        {
            memWeekNumber = 0;
            memWeekStart = new DateTime();
            memWeekEnd = new DateTime();
            memIsBreak = false;
        }

        /// <summary>
        /// Standard constructor for the Week Class, allowing specification of the start of a 
        /// semester, the specific week number, and if the week is a break. 
        /// </summary>
        /// <param name="SemesterStart"></param>
        /// <param name="WeekNumber"></param>
        /// <param name="IsBreak"></param>
        public Week(DateTime SemesterStart, int WeekNumber, bool IsBreak)
        {
            memWeekNumber = WeekNumber;
            memWeekStart = SemesterStart.Date.AddDays((WeekNumber - 1) * 7);
            memWeekEnd = memWeekStart.AddDays(6);
            memIsBreak = IsBreak;
            memBreakName = "Break";
        }

        //Public setters and getters 
        public int WeekNumber
        {
            get { return memWeekNumber; }
        }

        public DateTime WeekStart
        {
            get{ return memWeekStart; }
        }

        public DateTime WeekEnd
        {
            get { return memWeekEnd; }
        }

        public bool IsBreak
        {
            get { return memIsBreak; }
            set { memIsBreak = value; }
        }

        public List<string> Dates
        {
            get
            {
                DateTime currentDate = memWeekStart.Date;
                List<string> Dates = new List<string>();

                while(currentDate.CompareTo(memWeekEnd) <= 0)
                {
                    if (memIsBreak)
                    {
                        Dates.Add(memBreakName);
                        currentDate = currentDate.AddDays(1);
                    }
                    else
                    {
                        Dates.Add(currentDate.GetDateTimeFormats()[2]); //Outputs date in MM/DD/YY Format
                        currentDate = currentDate.AddDays(1);
                    }
                }

                return Dates;
            }
        }

        public List<DateTime> DateList
        {
            get
            {
                DateTime currentDate = memWeekStart.Date;
                List<DateTime> Dates = new List<DateTime>();

                while (currentDate.CompareTo(memWeekEnd) <= 0)
                {
                    Dates.Add(currentDate);
                    currentDate = currentDate.AddDays(1);
                }

                return Dates;
            }
        }
    }

    /// <summary>
    /// The Break class inherits from the week class to improve the flexibity of adding breaks 
    /// to the semester. This allows extra settings to be added, as well as future adjustments. 
    /// </summary>
    class Break : Week
    {

        public Break() { }

        /// <summary>
        /// The Standard constructor for the Break class allows a specified week to have a break name
        /// that can be specified by the user. 
        /// </summary>
        /// <param name="SemesterStart"></param>
        /// <param name="WeekNumber"></param>
        /// <param name="BreakName"></param>
        public Break(DateTime SemesterStart, int WeekNumber, string BreakName)
        {
            this.memBreakName = BreakName;
            this.memIsBreak = true;
            this.memWeekStart = SemesterStart.Date.AddDays((WeekNumber - 1) * 7);
            this.memWeekEnd = memWeekStart.AddDays(6);
        }

    }
}
