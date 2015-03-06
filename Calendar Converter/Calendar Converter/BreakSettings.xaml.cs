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
	public partial class BreakSettings
	{
		public BreakSettings()
		{
			this.InitializeComponent();
            ckbxAutoCalc.IsChecked = Properties.Settings.Default.AutoCalcBreaks;
            
		}

        private void ckbxAutoCalc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.AutoCalcBreaks = (bool)ckbxAutoCalc.IsChecked;
            }
            catch (InvalidOperationException) //Probably not necessary, but better safe than sorry. 
            {
                Properties.Settings.Default.AutoCalcBreaks = false;
            }
        }


	}
}