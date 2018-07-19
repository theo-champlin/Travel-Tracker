using System;
using System.ComponentModel;
using System.Windows.Input;

namespace TravelTracker.Models
{
   using Commands;
   using Services.Implementations;
   using Services.Interfaces;

   /// <summary>
   /// A representation of the weather in a given location. Weather will send a property changed
   /// notification when any public property is updated to support binding.
   /// </summary>
   public class Weather : INotifyPropertyChanged
   {
      /// <summary>
      /// A canvas object displaying a portrayal of the weather in the given location.
      /// </summary>
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

      /// <summary>
      /// A command that launches a web page with weather information on the given location if the
      /// page location is known.
      /// </summary>
      NavigateToWeather _navigateToWeather;
      public NavigateToWeather NavigateToWeather
      {
         get
         {
            return _navigateToWeather;
         }

         private set
         {
            _navigateToWeather = value;
            OnPropertyChanged("NavigateToWeather");
         }
      }

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

      #region INotifyPropertyChanged
      public event PropertyChangedEventHandler PropertyChanged;

      private void OnPropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
      #endregion
   }
}
