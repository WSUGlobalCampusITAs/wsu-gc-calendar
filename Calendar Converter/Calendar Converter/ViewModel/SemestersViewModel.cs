using Calendar_Converter.DataAccess;
using Calendar_Converter.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Converter.ViewModel
{
    public class SemestersViewModel : ViewModelBase
    {
        readonly SemesterLogic _semesters;
        private ObservableCollection<ViewModelBase> _oldWeeks;
        private ObservableCollection<ViewModelBase> _newWeeks;

        public ObservableCollection<ViewModelBase> OldWeeks
        {
            get
            {
                if (_oldWeeks == null)
                {
                    _oldWeeks = new ObservableCollection<ViewModelBase>();
                    
                }
                return _oldWeeks;
            }
            private set { _oldWeeks = value; }
        }

        public ObservableCollection<ViewModelBase> NewWeeks
        {
            get
            {
                if (_newWeeks == null)
                {
                    _newWeeks = new ObservableCollection<ViewModelBase>();
                }
                return _newWeeks;
            }
            private set { _newWeeks = value; }
        }

        public SemestersViewModel(SemesterLogic Semesters)
        {   
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
                i++;
            }

            this.OldWeeks = new ObservableCollection<ViewModelBase>(weeks);

            weeks.Clear();

            foreach (Week _week in _semesters.Semesters[1].Weeks)
            {
                weeks.Add(new WeekViewModel(_semesters, false, i));
                i++;
            }

            this.NewWeeks = new ObservableCollection<ViewModelBase>(weeks);
            
        }

        protected override void OnDispose()
        {
            this.NewWeeks.Clear();
            this.OldWeeks.Clear();
        }
    }
}
