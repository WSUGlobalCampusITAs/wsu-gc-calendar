using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Converter.Model
{
    public class Semester
    {
        //Member Variables
        private DateTime memSemesterStart;
        private int memSemLength;
        private bool memHasBreak;
        private List<Week> memSemWeeks;

        /// <summary>
        /// Create Semester returns a new Semester Object for use with the given parameters
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="Length"></param>
        /// <param name="HasBreak"></param>
        /// <returns></returns>
        public static Semester CreateSemester(DateTime Start, int Length, bool HasBreak, List<Week> Weeks)
        {
            return new Semester { memSemesterStart = Start, memSemLength = Length, memHasBreak = HasBreak, memSemWeeks = Weeks};
        }

        public DateTime Start
        { get { return memSemesterStart; } }

        public int Length
        { get { return memSemLength; } }

        public bool HasBreak
        { get { return memHasBreak; } }

        public Week Week(int WeekNumber)
        {
            return memSemWeeks[WeekNumber];
        }

        public List<Week> Weeks
        {
            get { return memSemWeeks; }
            set { memSemWeeks = value; }
        }

    }
}
