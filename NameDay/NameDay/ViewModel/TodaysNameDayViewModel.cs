using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using NameDay.ViewModel;
using System.Net;
using Newtonsoft.Json;

namespace NameDay.ViewModel
{
    
    class TodaysNameDayViewModel : Abstract.ViewModel
    {
        public string json;
        public NamedayData items;
        public Model.data mod;
        public Command onSearchByName { get; private set; }
        public Command onSearchByDate { get; private set; }
        public Command searchToday { get; private set; }
        public TodaysNameDayViewModel()
        {
            mod = new Model.data();
            items = new NamedayData();

            onSearchToday();
          
            onSearchByDate = new Command(onSearchByDateClicked);
            onSearchByName = new Command(onSearchByNameClicked);
            searchToday = new Command(onSearchToday);
            
        }
      private void returnName()
        {
            Day = items.data.Values.ElementAt(0);
            Month = items.data.Values.ElementAt(1);
            Name = items.data.Values.ElementAt(2);
        }
        private void getData()
        {
            json = new WebClient().DownloadString("https://api.abalin.net/get/today?country=cz");
            items = JsonConvert.DeserializeObject<NamedayData>(json);
        }
        private void onSearchToday()
        {
            getData();
            returnName();
        }
        private void onSearchByNameClicked()
        {
            Application.Current.MainPage = new NavigationPage(new View.SearchByNameView());
        }
        
    private void onSearchByDateClicked()
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




    }
}
