using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Travel_Tracker.Services
{
   using Interfaces;

   public class LocationDetailsException : Exception
   {
      public LocationDetailsException()
         : base(ErrorMessage)
      {
      }

      private const string ErrorMessage = "Unable to retrieve location details.";
   }

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

      public string GetWeatherIconUrl(
         string country,
         string city)
      {
         var weatherDataResponse = locationDetails.FetchWeatherDetailsForLocation(country, city);

         try
         {
            return ParseWeatherIconFromServiceResponse(weatherDataResponse);
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
         public string IconUrl { get; set; }
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

      private string ParseWeatherIconFromServiceResponse(Stream localWeatherResponse)
      {
         var parsedWeatherData = ParseStream(localWeatherResponse);

         return (from locationInfo in parsedWeatherData.Descendants("current_condition")
            select new Weather
            {
               IconUrl = locationInfo.Element("weatherIconUrl").Value
            }).First().IconUrl;
      }
   }
}
