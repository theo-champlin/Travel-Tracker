using System;
using System.Windows.Input;

namespace TravelTracker.ViewModels
{
   using Commands;
   using Implementations;
   using Interfaces;
   using Models;

   public class TravelTrackingViewModel
   {
      private Location _location;
      public Location Location
      {
         get
         {
            return _location;
         }
      }

      private Theme _theme;
      public Theme Theme
      {
         get
         {
            return _theme;
         }
      }

      private Timer _timer;
      public Timer Timer
      {
         get
         {
            return _timer;
         }
      }

      private Weather _weather;
      public Weather Weather
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

      private ExitApplication _exitApplication;
      public ICommand ExitApplication
      {
         get
         {
            if (_exitApplication == null)
            {
               _exitApplication = new ExitApplication();
            }

            return _exitApplication;
         }
      }

      public TravelTrackingViewModel()
      {
         _location = new Location();
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

         InitializeTheme();
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

      private void InitializeTheme()
      {
         _theme = new Theme();
         _theme.SetDarkTheme.Execute(null);
      }
   }
}
