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

        private List<TextBlock> OldSemester;
        private List<TextBlock> NewSemester;
        private List<TextBlock> OldDates;
        private List<TextBlock> NewDates;
        private LogicEngine memEngine;
        private bool memBreak;
        /// <summary>
        /// Default Constructor for MainWindow
        /// </summary>
        public MainWindow()
        {
            memEngine = new LogicEngine();
            memBreak = false;

            memEngine.PropertyChanged += memEngine_PropertyChanged;

            InitializeComponent();

            OldSemester = new List<TextBlock>();
            NewSemester = new List<TextBlock>();
            NewDates = new List<TextBlock>();
            OldDates = new List<TextBlock>();

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

            NewDates.Add(tblkNewMonday);
            NewDates.Add(tblkNewTuesday);
            NewDates.Add(tblkNewWednesday);
            NewDates.Add(tblkNewThursday);
            NewDates.Add(tblkNewFriday);
            NewDates.Add(tblkNewSaturday);
            NewDates.Add(tblkNewSunday);

            OldDates.Add(tblkOldMonday);
            OldDates.Add(tblkOldTuesday);
            OldDates.Add(tblkOldWednesday);
            OldDates.Add(tblkOldThursday);
            OldDates.Add(tblkOldFriday);
            OldDates.Add(tblkOldSaturday);
            OldDates.Add(tblkOldSunday);


        }

        //Event handler for updates to the calendar. 
        void memEngine_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            int i = 0;
            foreach (TextBlock blk in NewDates)
            {
                blk.Text = memEngine.NewWeek.Dates[i];
                i++;
            }
            i = 0;
            foreach (TextBlock blk in OldDates)
            {
                blk.Text = memEngine.OldWeek.Dates[i];
                i++;
            }
            i = 0;
            foreach (TextBlock blk in NewSemester)
            {
                blk.Text = memEngine.NewWeek.DateList[i].DayOfWeek.ToString();
                i++;
            }
            i = 0;
            foreach (TextBlock blk in OldSemester)
            {
                blk.Text = memEngine.OldWeek.DateList[i].DayOfWeek.ToString();
                i++;
            }
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
            DateTime memOldSemStart = new DateTime(), memNewSemStart = new DateTime();
            int memNumWeeks;
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
                        memEngine.Semesters(memOldSemStart, memNewSemStart, memNumWeeks, memBreak);

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



        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            memEngine.Next();
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            memEngine.Previous();
        }

    }
}
