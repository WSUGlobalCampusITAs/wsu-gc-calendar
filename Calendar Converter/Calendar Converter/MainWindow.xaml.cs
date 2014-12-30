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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DateTime memOldSemStart;
        private DateTime memNewSemStart;
        private bool memBreak;
        private int memNumWeeks;
        private List<string> memNewSemList;
        private List<string> memOldSemList;
        private Semester memNewSem;
        private Semester memOldSem;
        private int memNewWeekNumber;
        private int memOldWeekNumber;
        private List<TextBlock> OldSemester;
        private List<TextBlock> NewSemester;
        private List<DateTime> memNewSemDates;
        private List<DateTime> memOldSemDates;
        /// <summary>
        /// Default Constructor for MainWindow
        /// </summary>
        public MainWindow()
        {
            memNewWeekNumber = 1;
            memOldWeekNumber = 1;
            InitializeComponent();

            OldSemester = new List<TextBlock>();
            NewSemester = new List<TextBlock>();

            OldSemester.Add(tblkOld0);
            OldSemester.Add(tblkOld1);
            OldSemester.Add(tblkOld2);
            OldSemester.Add(tblkOld3);
            OldSemester.Add(tblkOld4);
            OldSemester.Add(tblkOld5);
            OldSemester.Add(tblkOld6);

            NewSemester.Add(tblkNew0);
            NewSemester.Add(tblkNew1);
            NewSemester.Add(tblkNew2);
            NewSemester.Add(tblkNew3);
            NewSemester.Add(tblkNew4);
            NewSemester.Add(tblkNew5);
            NewSemester.Add(tblkNew6);

        }

        private void RadioButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
        	if(rbSkipbreak.IsChecked == true)
            {
                memBreak = false;
            }
            else
            {
                memBreak = true;
            }
        }

        private void BtnUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            memNewWeekNumber = 1;
            memOldWeekNumber = 1;
            if (dtpickOldStart.SelectedDate.HasValue)
            {
                memOldSemStart = dtpickOldStart.SelectedDate.Value;

                if (dtpickNewStart.SelectedDate.HasValue)
                {
                    memNewSemStart = dtpickNewStart.SelectedDate.Value;

                    if (IsInt(txtbxNumWeeks.Text))
                    {
                        memNumWeeks = Convert.ToInt32(txtbxNumWeeks.Text);
                        if (memBreak)
                        {
                            memNumWeeks++;
                        }
                        updateCalendar();
                    }
                }
                else
                {
                    MessageBox.Show("New Start Date must have a valid date.");
                }
            }
            else
            {
                MessageBox.Show("Old Start Date must have a valid date.");
            }


        }

        private bool IsInt(string input)
        {
            int numVal = 0;
            try
            {
                numVal = Convert.ToInt32(input);
                return true;
            }
            catch (FormatException e)
            {
                MessageBox.Show("Number of weeks needs to be a whole number.");
                return false;
            }
            catch (OverflowException e)
            {
                Console.WriteLine("Number of weeks is to large.");
                return false;
            }
        }

        private void updateCalendar()
        {
            int i;
            memNewSem = new Semester(memNewSemStart, memNumWeeks, memBreak);
            memOldSem = new Semester(memOldSemStart, memNumWeeks, memBreak);

            memNewSemList = memNewSem.GetWeek(memNewWeekNumber);
            memOldSemList = memOldSem.GetWeek(memOldWeekNumber);
            memNewSemDates = memNewSem.GetDates(memNewWeekNumber);
            memOldSemDates = memOldSem.GetDates(memOldWeekNumber);

            if(memOldSemList[0] == "Break")
            {
                memOldWeekNumber++;
                memOldSemList = memOldSem.GetWeek(memOldWeekNumber);
            }
            if(memNewSemList[0] == "Break")
            {
                memOldWeekNumber--;
                memOldSemList = memNewSem.GetWeek(memNewWeekNumber);
            }

            i = 0;
            foreach(TextBlock blk in OldSemester)
            {
                blk.Text = memOldSemDates[i].DayOfWeek.ToString();
                i++;
            }
            i = 0;
            foreach (TextBlock blk in NewSemester)
            {
                blk.Text = memNewSemDates[i].DayOfWeek.ToString();
                i++;
            }
            tblkOldMonday.Text = memOldSemList[0];
            tblkOldTuesday.Text = memOldSemList[1];
            tblkOldWednesday.Text = memOldSemList[2];
            tblkOldThursday.Text = memOldSemList[3];
            tblkOldFriday.Text = memOldSemList[4];
            tblkOldSaturday.Text = memOldSemList[5];
            tblkOldSunday.Text = memOldSemList[6];

            tblkNewMonday.Text = memNewSemList[0];
            tblkNewTuesday.Text = memNewSemList[1];
            tblkNewWednesday.Text = memNewSemList[2];
            tblkNewThursday.Text = memNewSemList[3];
            tblkNewFriday.Text = memNewSemList[4];
            tblkNewSaturday.Text = memNewSemList[5];
            tblkNewSunday.Text = memNewSemList[6];
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if(memNewWeekNumber < memNumWeeks && memOldWeekNumber < memNumWeeks)
            {
                memNewWeekNumber++;
                memOldWeekNumber++;
                updateCalendar();
            }
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if(memNewWeekNumber > 1 && memOldWeekNumber > 1)
            {
                memNewWeekNumber--;
                memOldWeekNumber--;
                updateCalendar();
            }
        }

    }
}
