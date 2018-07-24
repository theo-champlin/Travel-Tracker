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

      private ITheme _theme;
      public ITheme Theme
      {
         get
         {
            return _theme;
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
         InitializeTheme();

         _location = new Location(Theme);
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

      private void InitializeTheme()
      {
         _theme = new Theme();
         _theme.SetBlueTheme.Execute(null);
      }
   }
}
