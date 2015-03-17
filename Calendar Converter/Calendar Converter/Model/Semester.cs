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
