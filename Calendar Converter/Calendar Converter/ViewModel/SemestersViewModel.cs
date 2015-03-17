using Calendar_Converter.DataAccess;
using Calendar_Converter.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calendar_Converter.ViewModel
{
    public class SemestersViewModel : ViewModelBase
    {
        readonly SemesterLogic _semesters;
        private ObservableCollection<ViewModelBase> _weeks;
        private readonly ICommand _closeCalendarCommand;

        public ObservableCollection<ViewModelBase> Weeks
        {
            get
            {
                if (_weeks == null)
                {
                    _weeks = new ObservableCollection<ViewModelBase>();
                    
                }
                return _weeks;
            }
            private set { _weeks = value; }
        }

        public SemestersViewModel(SemesterLogic Semesters)
        {
            _closeCalendarCommand = new RelayCommand(CloseCalendar);

            if (Semesters == null)
            {
                throw new ArgumentNullException("Semesters");
            }

            
            _semesters = Semesters;

            List<ViewModelBase> weeks = new List<ViewModelBase>();

            int i = 0; 

            foreach(Week _week in _semesters.Semesters[0].Weeks)
            {
                weeks.Add(new WeekViewModel(_semesters, true, i));
                weeks.Add(new WeekViewModel(_semesters, false, i));
                i++;
            }

            this.Weeks = new ObservableCollection<ViewModelBase>(weeks);
        }

        public void CloseCalendar(object obj)
        {
            this.OnPropertyChanged("FullCalendarClose");
        }

        protected override void OnDispose()
        {
            this.Weeks.Clear();
        }

        public ICommand CloseCalendarCommand { get { return _closeCalendarCommand; } }
    }
}
