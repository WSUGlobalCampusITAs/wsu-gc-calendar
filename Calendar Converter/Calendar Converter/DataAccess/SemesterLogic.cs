﻿//  Copyright 2014 Washington State University

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
using Calendar_Converter.Model;
using System.Collections.ObjectModel;
using Calendar_Converter.Properties;

namespace Calendar_Converter.DataAccess
{
    public class SemesterLogic
    {
        private List<Semester> memSemesters;

        public SemesterLogic()
        {
            memSemesters = new List<Semester>();
        }

        public List<Semester> Semesters
        {
            get { return memSemesters; }
        }

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
    }
}
