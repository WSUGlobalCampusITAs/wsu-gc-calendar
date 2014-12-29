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

        //Constructor for Semester Class
        public Semester(DateTime SemesterStart, int NumberofWeeks, bool Break)
        {
            memSemStart = SemesterStart;
            memSemEnd = SemesterStart.AddDays(NumberofWeeks * 7.0 - 1);
            memNumWeeks = NumberofWeeks;
            if (Break)
            {
                if (memSemStart.Month <= 11 && memSemEnd.Month >= 11)
                {
                    memBreakWeek = 1;
                    DateTime BreakFinder = memSemStart.Date;
                    while (BreakFinder.DayOfYear < memSemEnd.DayOfYear)
                    {
                        if (BreakFinder.DayOfYear >= Thanksgiving(BreakFinder.Year).DayOfYear)
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
                else
                {
                    if (memSemStart.Month <= 3 && memSemEnd.Month >= 3)
                    {
                        memBreakWeek = 10;
                    }
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
    }
}
