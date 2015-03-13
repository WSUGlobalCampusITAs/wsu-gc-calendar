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

using System.Windows;
using Calendar_Converter.View;

namespace Calendar_Converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml, this file has had the program logic seperated from
    /// it into the logic engine file to improve flexibility and maintenance. This seperation includes
    /// specifying an interface for the output and input of data from the logic engine. The current setup uses 
    /// the Week class as the input object type, and outputs using 2 DateTime objects, a boolean, and an int. 
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Default Constructor for MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            frmControls.Navigate(new Controls());
        }
    }
	
	
}
