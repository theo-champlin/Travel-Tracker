using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace Travel_Tracker.Services
{
   class LocationDetailsService
   {
      /// <summary>
      /// Attempts to fetch the offset from UTC of the given city.
      /// </summary>
      /// <returns>
      /// Integer offset from UTC of the supplied location
      /// </returns>
      public static int GetTimezoneOffSet(
         string country,
         string city)
      {
         var timeZoneResponse = FetchLocationInformation(
            country,
            city,
            TIMEZONE_SERVICE_PATH);

         return ParseOffsetFromServiceResponse(timeZoneResponse);
      }

      public static string GetWeatherIconUrl(
         string country,
         string city)
      {
         var weatherDataResponse = FetchLocationInformation(
            country,
            city,
            WEATHER_SERVICE_PATH,
            WEATHER_ADDITIONAL_ARGUMENTS);

         return ParseWeatherIconFromServiceResponse(weatherDataResponse);
      }

      private const string SERVICE_KEY = "2c69bdbf75324227bbb213129182906";
      private const string SERVICE_DOMAIN = "https://api.worldweatheronline.com/premium/v1/";
      private const string TIMEZONE_SERVICE_PATH = "tz.ashx";
      private const string WEATHER_SERVICE_PATH = "weather.ashx";
      private const string WEATHER_ADDITIONAL_ARGUMENTS = "&date=today&fx=no&cc=yes&show_comments=no&num_of_days=1&mca=no";

      private struct TimeZone
      {
         public string UTCOffset { get; set; }
      }

      private struct Weather
      {
         public string IconUrl { get; set; }
      }

      private static HttpWebResponse FetchLocationInformation(
         string country,
         string city,
         string service,
         string additionalArguments = "")
      {
         var location = (city + ',' + country).Replace(" ", "+");
         var url = SERVICE_DOMAIN + service + $"?q={location}&key={SERVICE_KEY}" + additionalArguments;
         var request = (HttpWebRequest)WebRequest.Create(url);
         return (HttpWebResponse)request.GetResponse();
      }

      private static int ParseOffsetFromServiceResponse(HttpWebResponse timeZoneResponse)
      {
         string timeZoneOffset;
         using (var xmlReader = new StreamReader(timeZoneResponse.GetResponseStream()))
         {
            var doc = XDocument.Load(xmlReader);
            timeZoneOffset = (from locationInfo in doc.Descendants("time_zone")
               select new TimeZone
               {
                  UTCOffset = locationInfo.Element("utcOffset").Value
               }).First().UTCOffset;
         }

         return Int32.Parse(timeZoneOffset, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
      }

      private static string ParseWeatherIconFromServiceResponse(HttpWebResponse localWeatherResponse)
      {
         using (var xmlReader = new StreamReader(localWeatherResponse.GetResponseStream()))
         {
            var doc = XDocument.Load(xmlReader);
            return (from locationInfo in doc.Descendants("current_condition")
               select new Weather
               {
                  IconUrl = locationInfo.Element("weatherIconUrl").Value
               }).First().IconUrl;
         }
      }
   }
}
