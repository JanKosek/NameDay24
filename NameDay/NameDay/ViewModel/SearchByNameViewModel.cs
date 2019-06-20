using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Net;
using Newtonsoft.Json;
using System.Linq;
using NameDay.Model;

namespace NameDay.ViewModel
{
    class SearchByNameViewModel : Abstract.ViewModel
    {
        private int theDaysBetween;
        public string json;
        public SearchByNameData items;
        public Model.data mod;

        public Command todaysNameday { get; private set; }
        public Command onSearchByDate { get; private set; }
        public Command SearchByName { get; private set; }
        DateTime today;
        
       public SearchByNameViewModel()
        {
            theDaysBetween = 0;
            today = DateTime.Today;
            mod = new Model.data();
            items = new SearchByNameData();
           
            onSearchByDate = new Command(onSearchByDateClicked);
            todaysNameday = new Command(onTodaysNamedayClicked);
            SearchByName = new Command(onSearchByName);
            

        } 
        private void getData()
        {
            json = new WebClient().DownloadString("https://api.abalin.net/get/getdate?name="+Name+"&calendar=cz");
            items = JsonConvert.DeserializeObject<SearchByNameData>(json);
            
        }
        private void onSearchByName()
        {
            getData();
            returnName();
            if (Day.Length <2)
            {
                Day = "0" + Day;
            }
            TheDaysBetween = DaysBetween(today, Convert.ToDateTime(Day + "/" + Month + "/" + today.Year +" 00:00:00"));
        }
        private void returnName()
        {
            for (int i =0; i < items.results.Count; i++)
            {
                if (items.results[i].Name.ToUpper() == Name.ToUpper())
                {
                    Day = items.results[i].Day;
                    Month = items.results[i].Month;
                }
                
            }
            
        }
          
        int DaysBetween(DateTime d1, DateTime d2)
        {
            TimeSpan span = d2.Subtract(d1);
            if (((int)span.TotalDays) < 0)
            {
                d2 = Convert.ToDateTime(Day + "/" + Month + "/" + ((int)today.Year+1)+ " 00:00:00");
                span = d2.Subtract(d1);
            }
            return (int)span.TotalDays;
            
        }
        private void onTodaysNamedayClicked(object sender)
        { 
            Application.Current.MainPage = new NavigationPage(new View.TodaysNameDay());
        }
        private void onSearchByDateClicked(object sender)
        {
            Application.Current.MainPage = new NavigationPage(new View.SearchByDateView());
        }
        

        public string Name
        {
            get { return mod.Name; }
            set
            {
                mod.Name = value;
                this.OnPropertyChanged("Name");
            }
        }
        public string Day
        {
            get { return mod.Day; }
            set
            {
                mod.Day = value;
                this.OnPropertyChanged("Day");
            }
        }

        public string Month
        {
            get { return mod.Month; }
            set
            {
                mod.Month = value;
                this.OnPropertyChanged("Month");
            }
        }
        public int TheDaysBetween
        {
            get { return theDaysBetween; }
            set
            {
                theDaysBetween = value;
                this.OnPropertyChanged("TheDaysBetween");
            }
        }
    }
}
