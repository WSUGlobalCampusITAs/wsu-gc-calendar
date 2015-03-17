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
using Calendar_Converter.Model;
using Calendar_Converter.Properties;

namespace Calendar_Converter.DataAccess
{
    public class BreakDates
    {
        public static List<Break> NewBreak(DateTime Year)
        {
            List<Break> Breaks = new List<Break>();

            Breaks.Add(SpringBreak(Year));
            Breaks.Add(FallBreak(Year));

            return Breaks;    
        }

        private static Break SpringBreak(DateTime Year)
        {
            //Add database check later
            Break SpringBreak;

            DateTime StartofSemester = new DateTime(Year.Year, 1, 7); //Starting from the 1st possible semester Start

            while(StartofSemester.DayOfWeek != DayOfWeek.Monday)
            {
                StartofSemester = StartofSemester.AddDays(1);
            }

            SpringBreak = Break.CreateWeek(StartofSemester.AddDays((Settings.Default.SpringBreakWeekNum - 1) * 7), "Spring\nBreak");
            //To Calculate spring Break
            return SpringBreak;//11 weeks from start of year contains the week of spring break
        }

        private static Break FallBreak(DateTime Year)
        {
            DateTime thanksgiving = DateTime.MinValue;
            List<DateTime> november = new List<DateTime>();
            for (int count = 1; count < 31; count++)
            {
                november.Add(new DateTime(Year.Year, 11, count));
            }
            DayOfWeek thursdays = DayOfWeek.Thursday;

            //Use LINQ query to find all the thursdays of the month of November.
            //Thanksgiving is on the 4th thursday
            //Use the combination of Take and Last to get the 4th thursday of the month
            thanksgiving = (from d in november where d.DayOfWeek == thursdays orderby d.Day ascending select d).Take(4).Last();

            while (thanksgiving.DayOfWeek != DayOfWeek.Monday)
            {
                thanksgiving = thanksgiving.AddDays(-1); //This moves the date placement back to Monday to give correct start of week. 
            }

            Break vacation = Break.CreateWeek(thanksgiving, "Thanksgiving\nBreak");

            return vacation;
        }
    }
}
