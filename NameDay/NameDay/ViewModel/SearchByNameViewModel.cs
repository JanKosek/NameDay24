using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Net;
using Newtonsoft.Json;

namespace NameDay.ViewModel
{
    class SearchByNameViewModel : Abstract.ViewModel
    {
        public string json;
        public NamedayData items;
        public Model.data mod;

        public Command todaysNameday { get; private set; }
        public Command onSearchByDate { get; private set; }
        public Command SearchByName { get; private set; }
        DateTime today;
        
       public SearchByNameViewModel()
        {
            today = DateTime.Today;
            mod = new Model.data();
            items = new NamedayData();
           
            onSearchByDate = new Command(onSearchByDateClicked);
            todaysNameday = new Command(onTodaysNamedayClicked);
            SearchByName = new Command(onSearchByName);
            //DaysBetween(today, Convert.ToDateTime(Month + "/" + Day + "/" + "2019"));

        } 
        private void getData()
        {
            json = new WebClient().DownloadString("https://api.abalin.net/get/getdate?name="+Name+"&calendar=cz");
            items = JsonConvert.DeserializeObject<NamedayData>(json);
        }
        private void onSearchByName()
        {
            getData();
            returnName();
        }
        private void returnName()
        {

         //   Day = items.udaje.Values.ElementAt(0);
           // Month = items.udaje.Values.ElementAt(1);
           // Name = items.udaje.Values.ElementAt(2);
            
        }
          
        int DaysBetween(DateTime d1, DateTime d2)
        {
            TimeSpan span = d2.Subtract(d1);
            return Math.Abs((int)span.TotalDays);
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
        public int daysBetween
        {
            get { return mod.DaysBetween; }
            set
            {
                mod.DaysBetween = value;
                this.OnPropertyChanged("daysBetween");
            }
        }
    }
}
