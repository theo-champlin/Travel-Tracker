using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TravelTracker.ViewModels
{
   using Commands;
   using Interfaces;
   using Models.Implementations;
   using Models.Interfaces;
   using Services.Implementations;
   using Services.Interfaces;

   public class LocationInputViewModel : ILocationInputViewModel
   {
      #region Properties

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

      public ICommand CloseWindow { get; private set; }

      public ITheme Theme { get; private set; }

      public ObservableCollection<string> TypeaheadCountryList { get; set; }
         = new ObservableCollection<string> { };

      public ObservableCollection<string> TypeaheadCityList { get; set; }
         = new ObservableCollection<string> { };

      #endregion

      #region Public

      public LocationInputViewModel(ITheme appliedTheme)
      {
         locationSetter.PopulateCountryCollection(TypeaheadCountryList);
         _locationInputFields = new LocationInputFields(OnCountrySet);
         CloseWindow = new CloseWindow();
         Theme = appliedTheme;
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

      public void OnCountrySet()
      {
         locationSetter.PopulateCityCollection(
            LocationInputFields.CountryInput,
            TypeaheadCityList);
      }

      #endregion

      #region Members

      private ILocationSetService locationSetter = new LocationSetService(new CityFetchService());

      #endregion
   }
}
