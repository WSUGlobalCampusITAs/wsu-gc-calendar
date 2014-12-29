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
        private int memWeekNumber;

        public MainWindow()
        {
            memWeekNumber = 1;
            InitializeComponent();
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
            if (dtpickOldStart.SelectedDate.HasValue)
            {
                memOldSemStart = dtpickOldStart.SelectedDate.Value;

                if (dtpickNewStart.SelectedDate.HasValue)
                {
                    memNewSemStart = dtpickNewStart.SelectedDate.Value;

                    if (IsInt(txtbxNumWeeks.Text))
                    {
                        memNumWeeks = Convert.ToInt32(txtbxNumWeeks.Text);
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
            memNewSem = new Semester(memNewSemStart, memNumWeeks, memBreak);
            memOldSem = new Semester(memOldSemStart, memNumWeeks, memBreak);

            memNewSemList = memNewSem.GetWeek(memWeekNumber);
            memOldSemList = memOldSem.GetWeek(memWeekNumber);

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

    }
}
