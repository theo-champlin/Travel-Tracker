using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TravelTracker.ViewModels
{
   using Commands;
   using Models.Implementations;
   using Models.Interfaces;
   using Services.Implementations;
   using Services.Interfaces;

   public class LocationInputViewModel
   {
      public string WikiPageId
      {
         get
         {
            return locationSetter.GetWikiPageId(
               LocationInputFields.CountryInput,
               LocationInputFields.CityInput);
         }
      }
      public string WeatherAreaCode
      {
         get
         {
            return locationSetter.GetWeatherAreaCode(
               LocationInputFields.CountryInput,
               LocationInputFields.CityInput);
         }
      }

      public bool IsValidCitySet()
      {
         return locationSetter.IsRecognizedCity(
            LocationInputFields.CountryInput,
            LocationInputFields.CityInput);
      }

      public void ClearInput()
      {
         LocationInputFields.CountryInput = string.Empty;
         LocationInputFields.CityInput = string.Empty;
      }

      private CloseWindow _closeWindow;
      public ICommand CloseWindow
      {
         get
         {
            return _closeWindow;
         }
      }

      public ITheme Theme
      {
         get;
      }

      public ObservableCollection<string> TypeaheadCountryList { get; set; }
         = new ObservableCollection<string> { };

      public ObservableCollection<string> TypeaheadCityList { get; set; }
         = new ObservableCollection<string> { };

      private LocationInputFields _locationInputFields;
      public ILocationInputFields LocationInputFields
      {
         get
         {
            return _locationInputFields;
         }
#if DEBUG
         set
         {
            _locationInputFields = (LocationInputFields)value;
         }
#endif
      }

      public LocationInputViewModel(ITheme appliedTheme)
      {
         locationSetter.PopulateCountryCollection(TypeaheadCountryList);
         _locationInputFields = new LocationInputFields(OnCountrySet);
         _closeWindow = new CloseWindow();
         Theme = appliedTheme;
      }

      public void OnCountrySet()
      {
         locationSetter.PopulateCityCollection(
            LocationInputFields.CountryInput,
            TypeaheadCityList);
      }

      private ILocationSetService locationSetter = new LocationSetService(new CityFetchService());
   }
}
