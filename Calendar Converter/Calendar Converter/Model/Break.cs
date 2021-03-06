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


namespace Calendar_Converter.Model
{
    /// <summary>
    /// Break is the container class for Breaks
    /// </summary>
    public class Break : Week
    {
        /// <summary>
        /// static Create Week creates an object of type Break, overriding the Week data. 
        /// May in the future consider adding a Regular Week class, and changing the Week class to an
        /// abstract class. 
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="BreakName"></param>
        /// <returns></returns>
        public static Break CreateWeek(DateTime Start, string BreakName)
        {
            return new Break { memBreakName = BreakName, memStart = Start, memisBreak = true };
        }
    }
}
