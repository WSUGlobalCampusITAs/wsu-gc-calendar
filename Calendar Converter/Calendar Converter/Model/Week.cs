using System;

namespace Calendar_Converter.Model
{
    public class Week
    {
        protected DateTime memStart;
        protected bool memisBreak;

        public static Week CreateWeek(DateTime Start)
        {
            return new Week { memStart = Start };
        }

        public DateTime Start
        {
            get { return memStart; }
        }

        public DateTime End
        {
            get { return memStart.AddDays(6); }
        }

    }
}
