using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace TravelTracker.Services
{
   using Interfaces;

   public class LocationDetailsService : ILocationDetailsService
   {
      public LocationDetailsService(ILocationDetailsFetcher locationDetails)
      {
         this.locationDetails = locationDetails;
      }

      public int GetTimezoneOffSet(
         string country,
         string city)
      {
         var timeZoneResponse = locationDetails.FetchClockDetailsForLocation(country, city);

         try
         {
            return ParseOffsetFromServiceResponse(timeZoneResponse);
         }
         catch (Exception)
         {
            throw new LocationDetailsException();
         }
      }

      public int GetLocalWeatherCode(
         string country,
         string city)
      {
         var weatherDataResponse = locationDetails.FetchWeatherDetailsForLocation(country, city);

         try
         {
            return ParseWeatherCodeFromServiceResponse(weatherDataResponse);
         }
         catch (Exception)
         {
            throw new LocationDetailsException();
         }
      }

      private ILocationDetailsFetcher locationDetails;

      private struct TimeZone
      {
         public string UTCOffset { get; set; }
      }

      private struct Weather
      {
         public int IconCode { get; set; }
      }

      private XDocument ParseStream(Stream rawTarget)
      {
         using (var xmlReader = new StreamReader(rawTarget))
         {
            return XDocument.Load(xmlReader);
         }
      }

      private int ParseOffsetFromServiceResponse(Stream timeZoneResponse)
      {
         XDocument parsedTimeData = ParseStream(timeZoneResponse);

         string timeZoneOffset =
            (from locationInfo in parsedTimeData.Descendants("time_zone")
            select new TimeZone
            {
               UTCOffset = locationInfo.Element("utcOffset").Value
            }).FirstOrDefault().UTCOffset;

         return Int32.Parse(
            timeZoneOffset,
            NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
      }

      private int ParseWeatherCodeFromServiceResponse(Stream localWeatherResponse)
      {
         var parsedWeatherData = ParseStream(localWeatherResponse);

         string weatherCode =
            (from locationInfo in parsedWeatherData.Descendants("current_condition")
             select new TimeZone
             {
                UTCOffset = locationInfo.Element("weatherCode").Value
             }).FirstOrDefault().UTCOffset;

         return Int32.Parse(
            weatherCode,
            NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
      }
   }
}
