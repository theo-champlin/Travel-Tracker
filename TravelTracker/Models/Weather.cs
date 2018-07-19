using System;
using System.ComponentModel;

namespace TravelTracker.Models
{
   using Implementations;
   using Interfaces;

   public class Weather : INotifyPropertyChanged
   {
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

      public Weather(
         string country,
         string city,
         DateTime locationTime,
         ILocationDetailsService locationDetails)
      {
         var weatherCode = locationDetails.GetLocalWeatherCode(
           country,
           city);

         IResourceLookup weatherResourceLookup = new ResourceLookup();
         Icon = weatherResourceLookup.FindWeatherIcon(
            weatherCode,
            locationTime);
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
