using System;

namespace TravelTracker.Services
{
   using Interfaces;

   public class TimerUtility : ITimerUtility
   {
      public TimerUtility(
         ILocationDetailsService locationDetails,
         string country,
         string city)
      {
         offset = locationDetails.GetTimezoneOffSet(
            country,
            city);
      }

      public DateTime GetLocationTime(DateTime utcTime)
      {
         return utcTime.AddHours(offset);
      }

      public string GetFormattedLocationTime(DateTime utcTime)
      {
         return GetLocationTime(utcTime).ToString("h:mm tt");
      }

      public TimeSpan GetMinuteUpdateInterval(DateTime currentTime)
      {
         const int SecondsInMinute = 60;
         const int MillisecondsInSecond = 1000;

         // To ensure the update happens after the minute updates, not right before.
         const int UpdateDelayWindow = 5;

         var millisecondsUntilUpdate =
            (SecondsInMinute - currentTime.Second)
            * MillisecondsInSecond - currentTime.Millisecond
            + UpdateDelayWindow;

         return TimeSpan.FromMilliseconds(millisecondsUntilUpdate);
      }

      private int offset;
   }
}
