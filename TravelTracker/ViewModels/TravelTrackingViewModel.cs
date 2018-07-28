using System;
using System.Windows.Input;

namespace TravelTracker.ViewModels
{
   using Commands;
   using Factories.Interfaces;
   using Interfaces;
   using Models.Implementations;
   using Models.Interfaces;
   using Services.Implementations;
   using Services.Interfaces;

   public class TravelTrackingViewModel : ITravelTrackingViewModel
   {
      #region Properties

      public ILocation Location { get; private set; }

      public ITimer Timer { get; private set; }

      public IWeather Weather { get; private set; }

      public ICommand NavigateToWiki { get; private set; }

      #endregion

      #region Public

      public TravelTrackingViewModel(
         ILocationInputFactory locationFactory,
         ITheme currentTheme)
      {
         Location = new Location(
            locationFactory,
            currentTheme);

         if (string.IsNullOrEmpty(Location.City))
         {
            return;
         }

         NavigateToWiki = new NavigateToWiki(Location.WikiPageId);

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

      #endregion

      #region Members

      private ITimerUtility timerUtil;

      private ILocationDetailsService locationDetails
         = new LocationDetailsService(new LocationDetailsFetcher());

      #endregion

      #region Private

      private void StartTimeTracking(
         string country,
         string city)
      {
         timerUtil = new TimerUtility(
            locationDetails,
            country,
            city);

         Timer = new Timer(timerUtil);
      }

      private void StartWeatherTracking(
         string country,
         string city,
         string weatherAreaCode)
      {
         Weather = new Weather(
            country,
            city,
            timerUtil.GetLocationTime(DateTime.UtcNow),
            weatherAreaCode,
            locationDetails);
      }

      #endregion
   }
}
