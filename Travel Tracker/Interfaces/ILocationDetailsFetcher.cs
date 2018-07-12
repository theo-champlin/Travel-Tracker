using System.IO;

namespace Travel_Tracker.Interfaces
{
   public interface ILocationDetailsFetcher
   {
      Stream FetchWeatherDetailsForLocation(string country, string city);

      Stream FetchClockDetailsForLocation(string country, string city);
   }
}
