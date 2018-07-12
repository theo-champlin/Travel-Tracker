using System;

namespace Travel_Tracker.Interfaces
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
      /// Attempts to fetch the path to an icon representing the current weather for the given
      /// city.
      /// </summary>
      /// <returns>
      /// Path to an icon representing the current weather of the supplied location
      /// </returns>
      /// <exception cref="LocationDetailsException">
      /// Throws if there is an error retrieving or parsing location details.
      /// </exception>
      string GetWeatherIconUrl(
         string country,
         string city);
   }
}
