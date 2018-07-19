using System.IO;

namespace TravelTracker.Services.Interfaces
{
   public interface ILocationDetailsFetcher
   {
      /// <summary>
      /// Attempts to query the local weather of the given location.
      /// </summary>
      /// <param name="country"></param>
      /// <param name="city"></param>
      /// <returns>
      /// An XML stream with the retrieved weather details.
      /// </returns>
      Stream FetchWeatherDetailsForLocation(string country, string city);

      /// <summary>
      /// Attempts to query the local time of the given location.
      /// </summary>
      /// <param name="country"></param>
      /// <param name="city"></param>
      /// <returns>
      /// An XML stream with the retrieved timezone details.
      /// </returns>
      Stream FetchClockDetailsForLocation(string country, string city);
   }
}
