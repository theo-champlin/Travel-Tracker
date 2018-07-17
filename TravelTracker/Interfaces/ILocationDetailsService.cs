using System;

namespace TravelTracker.Interfaces
{
   public class LocationDetailsException : Exception
   {
      public LocationDetailsException()
         : base(ErrorMessage)
      {
      }

      private const string ErrorMessage = "Unable to retrieve location details.";
   }

   public interface ILocationDetailsService
   {
      /// <summary>
      /// Attempts to fetch the offset from UTC of the given city.
      /// </summary>
      /// <returns>
      /// Integer offset from UTC of the supplied location
      /// </returns>
      /// <exception cref="LocationDetailsException">
      /// Throws if there is an error retrieving or parsing location details.
      /// </exception>
      int GetTimezoneOffSet(
         string country,
         string city);

      /// <summary>
      /// Attempts to fetch the weather code representing the current weather for the given city.
      /// </summary>
      /// <returns>
      /// Integer code for the current local weather conditions
      /// </returns>
      /// <exception cref="LocationDetailsException">
      /// Throws if there is an error retrieving or parsing location details.
      /// </exception>
      int GetLocalWeatherCode(
         string country,
         string city);
   }
}
