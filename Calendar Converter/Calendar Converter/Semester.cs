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
    public class Semester
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
            string BreakName = "Break";
            //First check to see if the breaks are to be taken. 
            if (Break)
            {
                DateTime BreakDate = new DateTime();
                
                //Next check to see if November is contained within Semester.
                if (memSemStart.Month <= 11 && memSemEnd.Month >= 11)
                {
                    memNumWeeks++;
                    BreakDate = Thanksgiving(memSemStart.Year);
                    BreakName = "Thanksgiving Break";
                }
                else if (memSemStart.Month <= 3 && memSemEnd.Month >= 3)
                {
                    BreakName = "Spring Break";
                    memNumWeeks++;
                    if (Properties.Settings.Default.AutoCalcBreaks == true)
                    {
                        BreakDate = new DateTime(memSemStart.Year, 1, 1);
                        BreakDate = BreakDate.AddDays(11 * 7 - 1); //11 weeks from start of year contains the week of spring break
                    }
                    else
                    {
                        Load_Spring_Break(); //Method to load break data from XML File
                    }
                }
                    memBreakWeek = 0;
                    DateTime BreakFinder = memSemStart.Date;
                    //Calculate the break week by finding which week Thanksgiving falls in. 
                    while (BreakFinder.DayOfYear < memSemEnd.DayOfYear)
                    {
                        if (BreakFinder.DayOfYear >= BreakDate.DayOfYear)
                        {
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
                    memSemesterWeeks.Add(new Break(memSemStart, (i+1), BreakName));
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

        public int NumWeeks
        {
            get { return memNumWeeks; }
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

        /// <summary>
        /// Method for returning the dates of a given week in a string list format
        /// </summary>
        /// <param name="WeekNumber"></param>
        /// <returns></returns>
        public List<string> GetWeek(int WeekNumber)
        {
            return memSemesterWeeks[WeekNumber - 1].Dates;
        }

        /// <summary>
        /// Method for returning the dates of a given week in a DateTime format. 
        /// </summary>
        /// <param name="WeekNumber"></param>
        /// <returns></returns>
        public List<DateTime> GetDates(int WeekNumber)
        {
            return memSemesterWeeks[WeekNumber - 1].DateList;
        }

        /// <summary>
        /// Public getter for returning the collection of weeks within a semester. 
        /// </summary>
        public List<Week> Weeks
        {
            get { return memSemesterWeeks; }
        }

        private void Load_Spring_Break()
        { 
            //Need to add Reading Spring Break from XML File
            throw (new NotImplementedException());
        }
    }
}
