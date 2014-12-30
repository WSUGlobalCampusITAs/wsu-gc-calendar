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
    class Semester
    {
        //Member Variable Declarations
        private DateTime memSemStart;
        private DateTime memSemEnd;
        private int memNumWeeks;
        private int memBreakWeek;
        private List<Week> memSemesterWeeks;
        private bool memIsBreak;

        /// <summary>
        /// Constructor for the Semester Class. This constructor creates a 
        /// Semester Class object, and procedes to populate it with the weeks of the
        /// semester. It also caluclates where breaks are located and sets them to their
        /// respective places. 
        /// </summary>
        /// <param name="SemesterStart"></param>
        /// <param name="NumberofWeeks"></param>
        /// <param name="Break"></param>
        public Semester(DateTime SemesterStart, int NumberofWeeks, bool Break)
        {
            memSemStart = SemesterStart;
            memSemEnd = SemesterStart.AddDays(NumberofWeeks * 7.0 - 1);
            memNumWeeks = NumberofWeeks;

            //First check to see if the breaks are to be taken. 
            if (Break)
            {
                DateTime BreakDate;
                //Next check to see if November is contained within Semester.
                if (memSemStart.Month <= 11 && memSemEnd.Month >= 11)
                {
                    BreakDate = Thanksgiving(memSemStart.Year);
                }
                else
                {
                    //If the semester contains spring break use the 10th week for spring break. 
                    BreakDate = new DateTime(memSemStart.Year, 1, 1);
                    BreakDate = BreakDate.AddDays(11 * 7 - 1); //11 weeks from start of year contains the week of spring break
                }
                    memBreakWeek = 1;
                    DateTime BreakFinder = memSemStart.Date;
                    //Calculate the break week by finding which week Thanksgiving falls in. 
                    while (BreakFinder.DayOfYear < memSemEnd.DayOfYear)
                    {
                        if (BreakFinder.DayOfYear >= BreakDate.DayOfYear)
                        {
                            memBreakWeek--;
                            break;
                        }
                        memBreakWeek++;
                        BreakFinder = BreakFinder.AddDays(7);

                    }
                    if (memBreakWeek >= NumberofWeeks)
                    {
                        memBreakWeek = -1;
                    }
                
            }
            memSemesterWeeks = new List<Week>();
            memIsBreak = Break;
            for (int i = 0; i < memNumWeeks; i++)
            {
                if ((i + 1) == memBreakWeek)
                {
                    memSemesterWeeks.Add(new Week(memSemStart, (i + 1), true));
                }
                else
                {
                    memSemesterWeeks.Add(new Week(memSemStart, (i + 1), false));
                }
            }
            
        }

        //Setters and Getters
        public DateTime SemesterStart
        {
            get { return memSemStart; }
        }
        public DateTime SemesterEnd
        {
            get { return memSemEnd; }
        }

        /// <summary>
        /// This method was referenced from http://solidcoding.blogspot.com/2007/11/c30-extension-method-to-check-holidays.html
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private static DateTime Thanksgiving(int year)
        {
            DateTime thanksgiving = DateTime.MinValue;
            List<DateTime> november = new List<DateTime>();
            for (int count = 1; count < 31; count++)
            {
                november.Add(new DateTime(year, 11, count));
            }
            DayOfWeek thursdays = DayOfWeek.Thursday;

            //Use LINQ query to find all the thursdays of the month of November.
            //Thanksgiving is on the 4th thursday
            //Use the combination of Take and Last to get the 4th thursday of the month
            thanksgiving = (from d in november where d.DayOfWeek == thursdays orderby d.Day ascending select d).Take(4).Last();
            return thanksgiving;
        }

        public List<string> GetWeek(int WeekNumber)
        {
            return memSemesterWeeks[WeekNumber - 1].Dates;
        }

        public List<DateTime> GetDates(int WeekNumber)
        {
            return memSemesterWeeks[WeekNumber - 1].DateList;
        }

        public List<Week> Weeks
        {
            get { return memSemesterWeeks; }
        }
    }
}
