using Calendar_Converter.DataAccess;
using Calendar_Converter.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calendar_Converter.Properties;
using System.Windows.Media;

namespace Calendar_Converter.ViewModel
{
    public class WeekViewModel : ViewModelBase
    {
        readonly SemesterLogic _semesters;
        private Brush _color;

        public ObservableCollection<Day> Days
        {
            get;
            private set;
        }

        public WeekViewModel(SemesterLogic Semesters, bool IsOldWeek)
        {   
            if (Semesters == null)
            {
                throw new ArgumentNullException("Semesters");
            }

            _semesters = Semesters;
            int OldOrNew = 0;
            
            if(!IsOldWeek)
            { OldOrNew = 1; }

            this.Days = new ObservableCollection<Day>(_semesters.Semesters[OldOrNew].Week(Settings.Default.CurrentWeek).Days);

            if(IsOldWeek)
            {
                this.Color = Settings.Default.OldSemesterColor;
            }
            else
            {
                this.Color = Settings.Default.NewSemesterColor;
            }
            

            
        }

        public Brush Color
        {
            get{return _color;}
            private set { _color = value; }
        }

        protected override void OnDispose()
        {
            this.Days.Clear();
        }
    }
}
