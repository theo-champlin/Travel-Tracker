using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace TravelTracker
{
   using Services.Implementations;
   using Services.Interfaces;

   public partial class LocationInput : Window
   {
      public string Country { get; set; }
         = String.Empty;
      public string City { get; set; }
         = String.Empty;
      public string WikiPageId
      {
         get
         {
            return locationSetter.GetWikiPageId(Country, City);
         }
      }
      public string WeatherAreaCode
      {
         get
         {
            return locationSetter.GetWeatherAreaCode(Country, City);
         }
      }

      public ObservableCollection<string> TypeaheadCountryList { get; set; }
         = new ObservableCollection<string> { };

      public ObservableCollection<string> TypeaheadCityList { get; set; }
         = new ObservableCollection<string> { };

      public string CountrySelectionText { get; set; }
         = String.Empty;
      public string CitySelectionText { get; set; }
         = String.Empty;

      public LocationInput()
      {
         InitializeComponent();
         this.DataContext = this;
         locationSetter.PopulateCountryCollection(TypeaheadCountryList);
      }

      private ILocationSetService locationSetter = new LocationSetService(new CityFetchService());

      private void OnCountrySet(object sender, RoutedEventArgs e)
      {
         Country = CountrySelectionText;
         locationSetter.PopulateCityCollection(Country, TypeaheadCityList);
      }

      private void SetLocationClick(object sender, RoutedEventArgs e)
      {
         City = CitySelectionText;
         Close();
      }
   }
}
