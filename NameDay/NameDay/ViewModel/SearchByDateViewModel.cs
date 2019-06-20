using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net;
using System.Linq;
using NameDay.Model;

namespace NameDay.ViewModel
{
    class SearchByDateViewModel : Abstract.ViewModel
    {
        private int theDaysBetween;
        private Model.data mod;
        public string json;
        public NamedayData items;
        DateTime today;

        public Command todaysNameday { get; private set; }
        public Command onSearchByName { get; private set; }
        public Command onSearchByDate { get; private set; }
        public SearchByDateViewModel()
        {
            theDaysBetween = 0;
            today = DateTime.Today;
            mod = new Model.data();
            items = new NamedayData();
            onSearchByName = new Command(onSearchByNameClicked);
            todaysNameday = new Command(onTodaysNamedayClicked);
            onSearchByDate = new Command(onSearchByDateClicked);
            

        }
        int DaysBetween(DateTime d1, DateTime d2)
        {
            TimeSpan span = d2.Subtract(d1);
           
            if (((int)span.TotalDays) < 0)
            {
                d2 = Convert.ToDateTime(Day + "/" + Month + "/" + ((int)today.Year + 1) + " 00:00:00");
                span = d2.Subtract(d1);
            }
            return (int)span.TotalDays;
        }
        private void getData()
        {
            json = new WebClient().DownloadString("https://api.abalin.net/get/namedays?day=" + Day + "&month=" + Month + "&country=cz");
            items = JsonConvert.DeserializeObject<NamedayData>(json);
        }
        private void onSearchByDateClicked()
        {
            getData();
            returnName();
            if (Day.Length < 2)
            {
                Day = "0" + Day;
            }
            TheDaysBetween = DaysBetween(today, Convert.ToDateTime(Day + "/" + Month + "/" + "2019" + " 00:00:00"));
        }
        private void returnName()
        {
            Day = items.data.Values.ElementAt(0);
            Month = items.data.Values.ElementAt(1);
            Name = items.data.Values.ElementAt(2);
        }
        private void onTodaysNamedayClicked(object sender)
        {
            Application.Current.MainPage = new NavigationPage(new View.TodaysNameDay());
        }
        private void onSearchByNameClicked(object sender)
        {
            Application.Current.MainPage = new NavigationPage(new View.SearchByNameView());
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
