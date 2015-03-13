using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Calendar_Converter.DataAccess;

namespace Calendar_Converter.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        readonly SemesterLogic _semesters;
        private ViewModelBase _controls;
        ObservableCollection<ViewModelBase> _singleWeeksViewModels;

        public MainWindowViewModel()
        {
            _semesters = new SemesterLogic();
            //create an instance of our viewmodel add it to our collection
        }

        public ObservableCollection<ViewModelBase> SingleWeeksViewModels
        {
            get
            {
                if(_singleWeeksViewModels == null)
                {
                    _singleWeeksViewModels = new ObservableCollection<ViewModelBase>();
                }
                return _singleWeeksViewModels;
            }
        }

        public ViewModelBase Controls
        {
            get
            {
                if(_controls == null)
                {
                    _controls = new ControlsViewModel();
                }
                return _controls;
            }
        }
    }
}
