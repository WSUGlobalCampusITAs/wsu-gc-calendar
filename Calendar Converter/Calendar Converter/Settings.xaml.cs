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
	/// <summary>
	/// Interaction logic for Settings.xaml
	/// </summary>
	public partial class winSettings : Window
	{
        private List<Page> memSettingsPages;
        private List<TreeViewItem> memTVSettingsPages;

		public winSettings()
		{
			this.InitializeComponent();
            this.Visibility = System.Windows.Visibility.Visible;
            this.Focus();
            memSettingsPages[0] = new BreakSettings();
            frmSettings.Navigate(memSettingsPages[0]); //Default starting page. 

			foreach(Page settingpage in memSettingsPages)
            {
                TreeViewItem newItem = new TreeViewItem();
                newItem.Header = settingpage.Title;
                newItem.Tag = settingpage;
                memTVSettingsPages.Add(newItem);
            }
		}

		private void OK_Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Properties.Settings.Default.Save();
            this.Close();
		}

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Properties.Settings.Default.Reload();

        }
	}
}