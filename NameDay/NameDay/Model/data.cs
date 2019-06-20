using System;
using System.Collections.Generic;
using System.Text;

namespace NameDay.Model
{
    class data
    {
        private string day = "den";
        public string Day
        {
            get { return day; }
            set
            {
                day = value;
            }
        }
        private string month ="měsíc";
        public string Month
        {
            get { return month; }
            set
            {
                month = value;
            }
        }
        
        private string name = "Jméno";
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }
        private int daysBetween = 0;
        public int DaysBetween
        {
            get { return daysBetween; }
            set
            {
                daysBetween = value;
            }
        }
    }
}
