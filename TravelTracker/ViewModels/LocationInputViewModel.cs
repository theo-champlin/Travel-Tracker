using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TravelTracker.ViewModels
{
   using Commands;
   using Services.Implementations;
   using Services.Interfaces;

   public class LocationInputViewModel
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

      private SetLocation _setLocation;
      public ICommand SetLocation
      {
         get
         {
            return _setLocation;
         }
      }

      public ObservableCollection<string> TypeaheadCountryList { get; set; }
         = new ObservableCollection<string> { };

      public ObservableCollection<string> TypeaheadCityList { get; set; }
         = new ObservableCollection<string> { };

      private string _countrySelectionText;
      public string CountrySelectionText
      {
         get
         {
            return _countrySelectionText;
         }
         set
         {
            _countrySelectionText = value;
            OnCountrySet();
         }
      }

      public string CitySelectionText { get; set; }
         = String.Empty;

      public LocationInputViewModel()
      {
         locationSetter.PopulateCountryCollection(TypeaheadCountryList);
         _setLocation = new SetLocation(this);
      }

      public void FinalizeLocation()
      {
         City = CitySelectionText;
      }

      public void OnCountrySet()
      {
         Country = CountrySelectionText;
         locationSetter.PopulateCityCollection(Country, TypeaheadCityList);
      }

      private ILocationSetService locationSetter = new LocationSetService(new CityFetchService());
   }
}
