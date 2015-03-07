using System;

namespace Calendar_Converter.Model
{
    public class Break : Week
    {
        protected string memBreakName;

        public static Break CreateWeek(DateTime Start, string BreakName)
        {
            return new Break { memBreakName = BreakName, memStart = Start, memisBreak = true };
        }
    }
}
