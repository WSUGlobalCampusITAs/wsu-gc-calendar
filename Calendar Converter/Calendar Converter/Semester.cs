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
        private bool memIsBreak;
        private List<Week> memWeeks;

        //Constructor for Semester Class
        public Semester(DateTime SemesterStart, int NumberofWeeks, bool Break)
        {
            memSemStart = SemesterStart;
            memSemEnd = SemesterStart.AddDays(NumberofWeeks * 7.0 - 1);
            memNumWeeks = NumberofWeeks;
            memWeeks = new List<Week>();
            memIsBreak = Break;
            for(int i = 0; i < memNumWeeks; i++)
            {
                memWeeks.Add(new Week(memSemStart, (i + 1), false));
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

        public List<string> GetWeek(int WeekNumber)
        {
            return memWeeks[WeekNumber - 1].Dates;
        }
    }
}
