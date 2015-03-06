using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Calendar_Converter
{
	public partial class SemesterCalendar
	{
		public SemesterCalendar()
		{
			this.InitializeComponent();

			// Insert code required on object creation below this point.
		}

        public SemesterCalendar(Semester Old, Semester New)
        {
            this.InitializeComponent();
            this.Start(Old, New);
        }
        
        public void Start(Semester Old, Semester New)
        {
            foreach(Week week in Old.Weeks)
            {
                int i = 0;
                foreach(string date in week.Dates)
                {
                    UGoldSem.Children.Add(new Day(date, week.DateList[i].DayOfWeek.ToString()));
                    i++;
                }
            }
            foreach (Week week in New.Weeks)
            {
                int i = 0;
                foreach (string date in week.Dates)
                {
                    UGnewSem.Children.Add(new Day(date, week.DateList[i].DayOfWeek.ToString()));
                    i++;
                }
            }
            this.Visibility = System.Windows.Visibility.Visible;
        }
	}
}