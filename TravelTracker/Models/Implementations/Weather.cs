using System;
using System.ComponentModel;
using System.Windows.Input;

namespace TravelTracker.Models.Implementations
{
   using Commands;
   using Interfaces;
   using Services.Implementations;
   using Services.Interfaces;

   public class Weather : IWeather
   {
      #region Properties

      object _icon;
      public object Icon
      {
         get
         {
            return _icon;
         }

         private set
         {
            _icon = value;
            OnPropertyChanged("Icon");
         }
      }

      NavigateToWeather _navigateToWeather;
      public ICommand NavigateToWeather
      {
         get
         {
            return _navigateToWeather;
         }

         private set
         {
            _navigateToWeather = value as NavigateToWeather;
            OnPropertyChanged("NavigateToWeather");
         }
      }

      #endregion

      #region Public

      public Weather(
         string country,
         string city,
         DateTime locationTime,
         string weatherAreaCode,
         ILocationDetailsService locationDetails)
      {
         var weatherCode = locationDetails.GetLocalWeatherCode(
           country,
           city);

         IResourceLookup weatherResourceLookup = new ResourceLookup();
         Icon = weatherResourceLookup.FindWeatherIcon(
            weatherCode,
            locationTime);

         NavigateToWeather = new NavigateToWeather(weatherAreaCode);
      }

      #endregion

      #region INotifyPropertyChanged

      public event PropertyChangedEventHandler PropertyChanged;

      private void OnPropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      #endregion
   }
}
