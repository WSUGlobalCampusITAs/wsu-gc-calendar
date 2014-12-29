using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Converter
{
    class Week
    {
        private DateTime memWeekStart;
        private DateTime memWeekEnd;
        private int memWeekNumber;
        private bool memIsBreak;

        public Week(DateTime SemesterStart, int WeekNumber, bool IsBreak)
        {
            memWeekStart = SemesterStart.AddDays(WeekNumber * 7);
            memWeekEnd = memWeekStart.AddDays(7);
            memIsBreak = IsBreak;
            memWeekNumber = WeekNumber;
        }

        public DateTime WeekStart
        {
            get { return memWeekStart; }
        }
        public DateTime WeekEnd
        {
            get { return memWeekEnd; }
        }
        public int WeekNumber
        {
            get { return memWeekNumber; }
        }
        public bool Break
        {
            get { return memIsBreak; }
        }
    }
}
