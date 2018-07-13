using System;

namespace Travel_Tracker.Interfaces
{
   public interface ITimerUtility
   {
      /// <summary>
      /// Gets the time in the location the timer object represents.
      /// </summary>
      /// <param name="utcTime">The current UTC time</param>
      /// <returns>The time in the location to which the timer utility is set.</returns>
      DateTime GetLocationTime(DateTime utcTime);

      /// <summary>
      /// Formats the local time represented by the timer utility.
      /// </summary>
      /// <param name="utcTime">The current UTC time</param>
      /// <returns>
      /// A string representation of the time in the location to which the timer utility is set
      /// with the format "h:mm tt".
      /// </returns>
      string GetFormattedLocationTime(DateTime utcTime);

      /// <summary>
      /// Calculates how long it will be until the next minute passes.
      /// </summary>
      /// <param name="currentTime"></param>
      /// <returns></returns>
      TimeSpan GetMinuteUpdateInterval(DateTime currentTime);
   }
}
