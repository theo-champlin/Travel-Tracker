using System.ComponentModel;

namespace TravelTracker.Models.Implementations
{
   using Interfaces;

   public class LocationInputFields : ILocationInputFields
   {
      private string _countryInput = string.Empty;
      public string CountryInput
      {
         get
         {
            return _countryInput;
         }
         set
         {
            _countryInput = value;
            OnPropertyChanged("CountryInput");
            onCountrySet();
         }
      }

      private string _cityInput = string.Empty;
      public string CityInput
      {
         get
         {
            return _cityInput;
         }
         set
         {
            _cityInput = value;
            OnPropertyChanged("CityInput");
         }
      }

      public delegate void CountrySetCallback();

      public LocationInputFields(CountrySetCallback onCountrySet)
      {
         this.onCountrySet = onCountrySet;
      }

      #region INotifyPropertyChanged
      public event PropertyChangedEventHandler PropertyChanged;

      private void OnPropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
      #endregion

      private CountrySetCallback onCountrySet;
   }
}
