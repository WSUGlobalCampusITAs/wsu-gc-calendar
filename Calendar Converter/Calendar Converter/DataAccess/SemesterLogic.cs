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
using Calendar_Converter.Model;

namespace Calendar_Converter.DataAccess
{
    /// <summary>
    /// The SemesterLogic Class provides the repository used in displaying Semester Data within the Calendar Converter
    /// </summary>
    public class SemesterLogic
    {
        #region Member Variables
        private List<Semester> memSemesters;
        #endregion

        #region Constructors
        public SemesterLogic()
        {
            memSemesters = new List<Semester>();
        }
        #endregion

        #region Properties
        public List<Semester> Semesters
        {
            get { return memSemesters; }
        }
        #endregion

        #region Member Methods

        /// <summary>
        /// NewSemesters method populates the Semester database with the given variables.
        /// This is accomplished by creating the individual semesters, and giving them their 
        /// neccessary data. The included data is a method call to Populate Weeks. Following the creation
        /// of the semesters, the weeks are modified to remove any breaks in the old semester, as well as add
        /// break weeks from the new semester to the old one. 
        /// </summary>
        /// <param name="OldStart"></param>
        /// <param name="NewStart"></param>
        /// <param name="Length"></param>
        /// <param name="UseBreaks"></param>
        public void NewSemesters(DateTime OldStart, DateTime NewStart, int Length, bool UseBreaks)
        {
            memSemesters = new List<Semester>();
            memSemesters.Add(Semester.CreateSemester(OldStart, Length, UseBreaks, PopulateWeeks(OldStart, Length, UseBreaks)));

            memSemesters.Add(Semester.CreateSemester(NewStart, Length, UseBreaks, PopulateWeeks(NewStart, Length, UseBreaks)));

            for (int i = 0; i < Length; i++)
            {
                if (memSemesters[0].Week(i).IsBreak)
                {
                    memSemesters[0].Weeks.Remove(memSemesters[0].Week(i));
                    memSemesters[0].Weeks.Add(Week.CreateWeek(memSemesters[0].Weeks[memSemesters[0].Weeks.Count - 1].End.AddDays(1))); //Adds week to end to replace the missing week. 
                }
            }

            for (int i = 0; i < Length; i++)
            {
                if (memSemesters[1].Week(i).IsBreak)
                {
                    memSemesters[0].Weeks.Insert(i, memSemesters[1].Week(i));
                    memSemesters[0].Weeks.Remove(memSemesters[0].Weeks[memSemesters[0].Weeks.Count - 1]);   //Removes week off the end to keep the count consistent
                }
            }
        }

        /// <summary>
        /// PopulateWeeks Method creates a collection of weeks, that can be passed to a semester data class given the appropriate
        /// weeks for the given semester. This includes adding regular weeks, as well as adding any breaks. 
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="Length"></param>
        /// <param name="HasBreaks"></param>
        /// <returns></returns>
        private List<Week> PopulateWeeks(DateTime Start, int Length, bool HasBreaks)
        {
            List<Week> Weeks = new List<Week>();
            List<Break> Breaks = new List<Break>();

            while (Start.DayOfWeek != DayOfWeek.Monday)
            {
                Start = Start.AddDays(-1); //This moves the date placement back to Monday to give correct start of week. 
            }

            if(HasBreaks)
            {
                Breaks = BreakDates.NewBreak(Start);
            }

            for(int i = 0; i < Length; i++)
            {
                bool isBreak = false;
                foreach(Break _break in Breaks)
                {
                    if(_break.Start == Start.AddDays(7*i))
                    {
                        Weeks.Add(_break);
                        isBreak = true;
                    }
                }
                if (!isBreak)
                {
                    Weeks.Add(Week.CreateWeek(Start.AddDays(7 * i)));
                }
            }

            return Weeks;
        }
        #endregion
    }
}
