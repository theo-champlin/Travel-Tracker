using System.IO;
using System.Net;

namespace TravelTracker.Services.Implementations
{
   using Interfaces;
   using Models.Implementations;

   public class LocationDetailsFetcher : ILocationDetailsFetcher
   {
      public Stream FetchWeatherDetailsForLocation(string country, string city)
      {
         return FetchLocationInformation(
            country,
            city,
            WEATHER_SERVICE_PATH,
            WEATHER_ADDITIONAL_ARGUMENTS).GetResponseStream();
      }

      public Stream FetchClockDetailsForLocation(string country, string city)
      {
         return FetchLocationInformation(
            country,
            city,
            TIMEZONE_SERVICE_PATH).GetResponseStream();
      }

      private static string ServiceKey = Settings.AppSettings.LocationDetailsServiceKey;
      private const string SERVICE_DOMAIN = "https://api.worldweatheronline.com/premium/v1/";
      private const string TIMEZONE_SERVICE_PATH = "tz.ashx";
      private const string WEATHER_SERVICE_PATH = "weather.ashx";
      private const string WEATHER_ADDITIONAL_ARGUMENTS =
         "&date=today" +
         "&fx=no" +
         "&cc=yes" +
         "&show_comments=no" +
         "&num_of_days=1" +
         "&mca=no";

      private HttpWebResponse FetchLocationInformation(
         string country,
         string city,
         string service,
         string additionalArguments = "")
      {
         var location = (city + ',' + country).Replace(" ", "+");
         var url = SERVICE_DOMAIN + service + $"?q={location}&key={ServiceKey}" + additionalArguments;
         var request = (HttpWebRequest)WebRequest.Create(url);
         return (HttpWebResponse)request.GetResponse();
      }
   }
}
