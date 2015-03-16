using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calendar_Converter.Properties;

namespace Calendar_Converter.Model
{
    public class Day
    {
        private DateTime _date;
        private string output;

        public Day(DateTime Date)
        {
            _date = Date;
            output = _date.GetDateTimeFormats()[Settings.Default.DateFormatString];
        }

        public string DayOfWeek
        {
            get { return _date.DayOfWeek.ToString(); }
        }

        public string Date
        {
            get { return output; }
            set { output = value; }
        }
    }
}
