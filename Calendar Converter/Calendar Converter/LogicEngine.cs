using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Calendar_Converter
{
    /// <summary>
    /// This class implements the functionality of navigating weeks, and populating semesters. 
    /// This is added for the sake of seperating the user interface from the logic of the code. 
    /// </summary>
    class LogicEngine : INotifyPropertyChanged
    {
        //Member Variable Declarations. 
        private Week memOldWeek;
        private Week memNewWeek;
        private Semester memOldSemester;
        private Semester memNewSemester;
        private int memCurrentWeek;
        private int memNumWeeks;

        //Event for user interface to subscribe to. 
        public event PropertyChangedEventHandler PropertyChanged;

        //Method for implementing the PropertyChanged Event. 
        void LogicEngine_SubscriptionUpdate(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        /// <summary>
        /// Default contructor for the LogicEngine class;
        /// </summary>
        public LogicEngine()
        {
            memOldWeek = new Week();
            memNewWeek = new Week();
            memCurrentWeek = 1;
            memNumWeeks = 0;
        }

        //Setter for the Semesters
        public void Semesters(DateTime OldSemesterStart, DateTime NewSemesterStart,  int NumWeeks, bool Breaks)
        {
            memCurrentWeek = 1;
            memOldSemester = new Semester(OldSemesterStart, NumWeeks, Breaks);
            memNewSemester = new Semester(NewSemesterStart, NumWeeks, Breaks);
            AdjustBreaks();
            memOldWeek = memOldSemester.Weeks[memCurrentWeek - 1];
            memNewWeek = memNewSemester.Weeks[memCurrentWeek - 1];
            memNumWeeks = memNewSemester.NumWeeks;
            LogicEngine_SubscriptionUpdate(this, new PropertyChangedEventArgs("Semester Update"));
        }

        public Semester OldSemester
        { get { return memOldSemester; } }

        public Semester NewSemester
        { get { return memNewSemester; } }

        public Week OldWeek
        {
            get { return memOldWeek; }
        }

        public Week NewWeek
        {
            get { return memNewWeek; }
        }

        public void Next()
        {
            if(memCurrentWeek < memNumWeeks)
            {
                memCurrentWeek++;
                memNewWeek = memNewSemester.Weeks[memCurrentWeek - 1];
                memOldWeek = memOldSemester.Weeks[memCurrentWeek - 1];
                LogicEngine_SubscriptionUpdate(this, new PropertyChangedEventArgs("Next"));
            }
           
        }

        public void Previous()
        {
            if (memCurrentWeek > 1)
            {
                memCurrentWeek--;
                memNewWeek = memNewSemester.Weeks[memCurrentWeek - 1];
                memOldWeek = memOldSemester.Weeks[memCurrentWeek - 1];
                LogicEngine_SubscriptionUpdate(this, new PropertyChangedEventArgs("Previous"));
            }
        }

        private void AdjustBreaks()
        {
            Week toBeRemoved = new Week();

            foreach(Week week in memOldSemester.Weeks)
            {
                if(week.IsBreak)
                {
                    toBeRemoved = week;
                }
            }

                memOldSemester.Weeks.Remove(toBeRemoved);
            

            toBeRemoved = new Week();
            foreach(Week week in memNewSemester.Weeks)
            {
                if(week.IsBreak)
                {
                    toBeRemoved = week;  
                }
            }

            if (toBeRemoved.WeekStart.Year > 1985)
            {
                memOldSemester.Weeks.Insert(memNewSemester.Weeks.IndexOf(toBeRemoved), toBeRemoved);
            }
            
        }

    }
}
