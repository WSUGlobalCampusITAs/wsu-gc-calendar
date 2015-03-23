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

//   Supervisor             Celisse Ellis
//   Design Lead            David Lystad
//   Programming Lead       David Lystad

using Calendar_Converter.Properties;
using MVVMObjectLibrary;
using System.Windows.Input;

namespace Calendar_Converter.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        #region Member Variables

        private ICommand _saveCommand;
        private ICommand _cancelCommand;

        #endregion

        #region Constructors

        public SettingsViewModel()
        {
            _saveCommand = new RelayCommand(Save);
            _cancelCommand = new RelayCommand(Cancel);
            Settings.Default.Reload();

        }

        #endregion

        #region Properties

        public ICommand SaveCommand { get { return _saveCommand; } }
        public ICommand CancelCommand { get { return _cancelCommand; } }

        #endregion

        #region Member Methods

        private void Save(object obj)
        {
            Settings.Default.Save();
            this.OnPropertyChanged("Save Settings");
        }

        private void Cancel(object obj)
        {
            Settings.Default.Reload();
            this.OnPropertyChanged("Cancel Settings");
        }

        #endregion
    }
}
