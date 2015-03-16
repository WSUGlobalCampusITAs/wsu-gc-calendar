using System;
using System.Collections.Generic;

namespace Calendar_Converter.Model
{
    public class Week
    {
        protected DateTime memStart;
        protected bool memisBreak;
        protected List<Day> _days;
        protected int _background;
        protected string memBreakName;

        public static Week CreateWeek(DateTime Start)
        {
            return new Week { memStart = Start, memisBreak = false, memBreakName = null };

        }

        public DateTime Start
        {
            get { return memStart; }
        }

        public DateTime End
        {
            get { return memStart.AddDays(6); }
        }

        public List<Day> Days
        {
            get
            {
                if(_days == null)
                {
                    _days = new List<Day>();

                    for(int i = 0; i < 7; i++)
                    {
                        _days.Add(new Day(memStart.AddDays(i)));
                        if(memisBreak)
                        {
                            _days[i].Date = memBreakName;
                        }
                    }
                }
                return _days;
            }
        }

        public int Color
        {
            get { return _background; }
        }

        public bool IsBreak
        {
            get { return memisBreak; }
        }

    }
}
