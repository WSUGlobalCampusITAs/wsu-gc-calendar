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

        //Constructor for Semester Class
        public Semester(DateTime SemesterStart, int NumberofWeeks)
        {
            memSemStart = SemesterStart;
            memSemEnd = SemesterStart.AddDays(NumberofWeeks * 7.0);
            memNumWeeks = NumberofWeeks;
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
    }
}
