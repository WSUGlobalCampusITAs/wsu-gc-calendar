using System;
using System.Collections.Generic;


namespace Calendar_Converter.Model
{
    public class Break : Week
    {
        public static Break CreateWeek(DateTime Start, string BreakName)
        {
            return new Break { memBreakName = BreakName, memStart = Start, memisBreak = true };
        }
    }
}
