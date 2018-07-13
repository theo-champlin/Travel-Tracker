using System;

namespace Travel_Tracker.Interfaces
{
   public interface IResourceLookup
   {
      /// <summary>
      /// Attempts to retrieve the static resource mapped to from the given weather code. Some
      /// resources will differ depending on whether it is night or day.
      /// </summary>
      /// <param name="weatherCode">Integer code representing a weather condition.</param>
      /// <param name="localTime">Time in the location to which the weather code belongs.</param>
      /// <returns>
      /// A canvas resource that represents the weather identified by the input weather code.
      /// </returns>
      object FindWeatherIcon(
         int weatherCode,
         DateTime localTime);
   }
}
