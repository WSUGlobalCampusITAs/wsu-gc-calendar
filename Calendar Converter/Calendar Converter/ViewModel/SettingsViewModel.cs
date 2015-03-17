using Calendar_Converter.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calendar_Converter.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private ICommand _saveCommand;
        private ICommand _cancelCommand;


        public SettingsViewModel()
        {
            _saveCommand = new RelayCommand(Save);
            _cancelCommand = new RelayCommand(Cancel);
            Settings.Default.Reload();

        }

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

        public ICommand SaveCommand { get { return _saveCommand; } }
        public ICommand CancelCommand { get { return _cancelCommand; } }
    }
}
