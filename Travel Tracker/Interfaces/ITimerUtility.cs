using System;

namespace Travel_Tracker.Interfaces
{
   public interface ITimerUtility
   {
      DateTime GetLocationTime(DateTime utcTime);

      string GetFormattedLocationTime(DateTime utcTime);

      TimeSpan GetMinuteUpdateInterval(DateTime currentTime);
   }
}
