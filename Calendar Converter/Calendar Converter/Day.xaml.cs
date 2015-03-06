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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calendar_Converter
{
	/// <summary>
	/// Interaction logic for Day.xaml
	/// </summary>
	public partial class Day : UserControl
	{
		private DateTime memDate;
		private string memDateFormat;
        private string memDayOfWeek;
		
		public Day(DateTime SemesterDate)
		{
			this.InitializeComponent();
            this.Date = SemesterDate;
		}

        public Day(DateTime SemesterDate, string Break)
        {
            this.InitializeComponent();
            memDateFormat = Break;
            memDate = SemesterDate;
            this.tblkDate.Text = memDateFormat;
            memDayOfWeek = memDate.DayOfWeek.ToString();
            this.tblkDayOfWeek.Text = memDayOfWeek;
        }

        public Day(string SemesterDate, string DayofWeek)
        {
            this.InitializeComponent();
            memDateFormat = SemesterDate;
            memDayOfWeek = DayofWeek;
            this.tblkDate.Text = memDateFormat;
            this.tblkDayOfWeek.Text = memDayOfWeek;
        }
		
		public DateTime Date
		{
            get { return memDate; }
            set
            {
                memDate = value;
                memDateFormat = memDate.Date.ToShortDateString();
                this.tblkDate.Text = memDateFormat;
                memDayOfWeek = memDate.DayOfWeek.ToString();
                this.tblkDayOfWeek.Text = memDayOfWeek;

            }
		}
		
	}
}