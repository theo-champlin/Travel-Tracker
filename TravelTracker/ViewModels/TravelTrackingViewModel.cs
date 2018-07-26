using System;
using System.Windows.Input;

namespace TravelTracker.ViewModels
{
   using Commands;
   using Models.Implementations;
   using Models.Interfaces;
   using Services.Implementations;
   using Services.Interfaces;

   public class TravelTrackingViewModel
   {
      private ILocation _location;
      public ILocation Location
      {
         get
         {
            return _location;
         }
      }

      private ITimer _timer;
      public ITimer Timer
      {
         get
         {
            return _timer;
         }
      }

      private IWeather _weather;
      public IWeather Weather
      {
         get
         {
            return _weather;
         }
      }

      private NavigateToWiki _navigateToWiki;
      public ICommand NavigateToWiki
      {
         get
         {
            return _navigateToWiki;
         }
      }

      public TravelTrackingViewModel(ITheme currentTheme)
      {
         _location = new Location(currentTheme);
         if (string.IsNullOrEmpty(Location.City))
         {
            return;
         }
         _navigateToWiki = new NavigateToWiki(Location.WikiPageId);

         StartTimeTracking(
            Location.Country,
            Location.City);

         // This needs to happen after the time is set because the local time is used to decide
         // which weather icon to display.
         StartWeatherTracking(
            Location.Country,
            Location.City,
            Location.WeatherAreaCode);
      }

      private ITimerUtility timerUtil;

      private ILocationDetailsService locationDetails
         = new LocationDetailsService(new LocationDetailsFetcher());

      private void StartTimeTracking(
         string country,
         string city)
      {
         timerUtil = new TimerUtility(
            locationDetails,
            country,
            city);

         _timer = new Timer(timerUtil);
      }

      private void StartWeatherTracking(
         string country,
         string city,
         string weatherAreaCode)
      {
         _weather = new Weather(
            country,
            city,
            timerUtil.GetLocationTime(DateTime.UtcNow),
            weatherAreaCode,
            locationDetails);
      }
   }
}
