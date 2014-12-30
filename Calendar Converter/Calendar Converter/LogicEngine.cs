using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Calendar_Converter
{
    /// <summary>
    /// This class implements the functionality of navigating weeks, and populating the semesters. 
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
            memOldSemester = new Semester(OldSemesterStart, NumWeeks, Breaks);
            memNewSemester = new Semester(NewSemesterStart, NumWeeks, Breaks);
            memOldWeek = memOldSemester.Weeks[memCurrentWeek];
            memNewWeek = memNewSemester.Weeks[memCurrentWeek];
            LogicEngine_SubscriptionUpdate(this, new PropertyChangedEventArgs("Semester Update"));
        }

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
                memNewWeek = memNewSemester.Weeks[memCurrentWeek];
                memOldWeek = memOldSemester.Weeks[memCurrentWeek];
                LogicEngine_SubscriptionUpdate(this, new PropertyChangedEventArgs("Next"));
            }
           
        }

        public void Previous()
        {
            if (memCurrentWeek > 1)
            {
                memCurrentWeek--;
                memNewWeek = memNewSemester.Weeks[memCurrentWeek];
                memOldWeek = memOldSemester.Weeks[memCurrentWeek];
                LogicEngine_SubscriptionUpdate(this, new PropertyChangedEventArgs("Previous"));
            }
        }

        //Items Needed - Collection of Weeks
        //Break Class that inherits from Week
        //Pass weeks to userinterface to allow containers for the weeks that are efficiently passed to 
        //the User Interface
        //Use events to pass updates to the user interface. 



    }
}
