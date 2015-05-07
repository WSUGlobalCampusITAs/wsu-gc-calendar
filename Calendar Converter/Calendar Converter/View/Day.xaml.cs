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

namespace Calendar_Converter.View
{
    /// <summary>
    /// Interaction logic for Day.xaml
    /// </summary>
    public partial class Day : UserControl
    {
        public Day()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseMove(object sender, MouseEventArgs e)
        {
            TextBlock item = sender as TextBlock;
            if (item != null && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(item,
                                    item.Text,
                                     DragDropEffects.Copy);
            }
        }
    }
}
