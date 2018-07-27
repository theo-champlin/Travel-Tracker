using System.ComponentModel;
using System.Windows;

namespace TravelTracker.Models.Implementations
{
   using Factories.Interfaces;
   using Interfaces;
   using Views;
   using ViewModels;

   public class Location : ILocation
   {
      #region Properties

      public string FullName
      {
         get
         {
            return City + ", " + Country;
         }
      }

      private string _country;
      public string Country
      {
         get
         {
            return _country;
         }

         private set
         {
            _country = value;
            OnPropertyChanged("Country");
            OnPropertyChanged("FullName");
         }
      }

      private string _city;
      public string City
      {
         get
         {
            return _city;
         }

         private set
         {
            _city = value;
            OnPropertyChanged("City");
            OnPropertyChanged("FullName");
         }
      }

      private string _wikiPageId;
      public string WikiPageId
      {
         get
         {
            return _wikiPageId;
         }

         private set
         {
            _wikiPageId = value;
            OnPropertyChanged("WikiPageId");
         }
      }

      private string _weatherAreaCode;
      public string WeatherAreaCode
      {
         get
         {
            return _weatherAreaCode;
         }

         private set
         {
            _weatherAreaCode = value;
            OnPropertyChanged("WeatherAreaCode");
         }
      }

      #endregion

      #region Public

      public Location(
         ILocationInputFactory locationFactory,
         ITheme appliedTheme)
      {
#if NDEBUG
         var locationWindowControl = new LocationInputViewModel(appliedTheme)
         {
            LocationInputFields = new LocationInputFields(() => { })
            {
               CityInput = "Paris",
               CountryInput = "France"
            }
         };
#else
         var locationWindowControl = locationFactory.Generate(appliedTheme);
         if (locationWindowControl == null)
         {
            return;
         }
#endif

         Country = locationWindowControl.LocationInputFields.CountryInput;
         City = locationWindowControl.LocationInputFields.CityInput;

         WikiPageId = locationWindowControl.WikiPageId;
         WeatherAreaCode = locationWindowControl.WeatherAreaCode;
      }

      #endregion

      #region INotifyPropertyChanged
      public event PropertyChangedEventHandler PropertyChanged;

      private void OnPropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
      #endregion

      #region Private

      private bool SetLocation(
         LocationInputViewModel locationWindowControl,
         ITheme appliedTheme)
      {
         do
         {
            locationWindowControl.ClearInput();
            DisplayLocationInputWindow(locationWindowControl);
         } while (!locationWindowControl.IsValidCitySet()
            && ShouldContinueLocationSelection(appliedTheme));

         return locationWindowControl.IsValidCitySet();
      }

      private bool ShouldContinueLocationSelection(ITheme appliedTheme)
      {
         var errorWindowControl = new LocationErrorViewModel(appliedTheme);
         var errorWindow = new LocationError(errorWindowControl);
         errorWindow.ShowDialog();

         if (errorWindowControl.ExitMode)
         {
            Application.Current.Shutdown();
            return false;
         }

         return true;
      }

      private void DisplayLocationInputWindow(LocationInputViewModel locationWindowControl)
      {
         var locationWindow = new Views.LocationInput(locationWindowControl);
         locationWindow.ShowDialog();
      }

      #endregion
   }
}
