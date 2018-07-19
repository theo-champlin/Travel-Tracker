using System.IO;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace TravelTracker.Tests
{
   using Implementations;
   using Interfaces;

   [TestClass]
   public class LocationDetailsTest
   {
      [TestMethod]
      public void GetTimezoneOffSet_ParsesValidResponse()
      {
         const int expectedOffset = 2;
         const string validResponseStream =
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
            "<data>" +
               "<request>" +
                  "<type>City</type>" +
                  "<query>Paris,France</query>" +
               "</request>" +
               "<time_zone>" +
                  "<localtime>2018-07-11 23:47</localtime>" +
                  "<utcOffset>2.0</utcOffset>" +
                  "<zone>Europe/Paris</zone>" +
               "</time_zone>" +
            "</data>";

         var locationDetails = GetLocationDetailsWithMockedTimeZone(validResponseStream);
         var offset = locationDetails.GetTimezoneOffSet(string.Empty, string.Empty);
         Assert.AreEqual(offset, expectedOffset);
      }

      [TestMethod]
      [ExpectedException(
         typeof(LocationDetailsException),
         "Expected exception when time zone info is empty.")]
      public void GetTimezoneOffSet_ParsesEmptyResponse()
      {
         GetTimezoneOffSet_TestError(string.Empty);
      }

      [TestMethod]
      [ExpectedException(
         typeof(LocationDetailsException),
         "Expected exception when time zone information is not available.")]
      public void GetTimezoneOffSet_ParsesResponseMissingTimezone()
      {
         const string InvalidResponseStream = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><data/>";
         GetTimezoneOffSet_TestError(InvalidResponseStream);
      }

      [TestMethod]
      [ExpectedException(
         typeof(LocationDetailsException),
         "Expected exception when offset is not available.")]
      public void GetTimezoneOffSet_ParsesResponseMissingOffset()
      {
         const string InvalidResponseStream =
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
            "<data>" +
               "<time_zone>" +
                  "<localtime>2018-07-11 23:47</localtime>" +
                  "<zone>Europe/Paris</zone>" +
               "</time_zone>" +
            "</data>";
         GetTimezoneOffSet_TestError(InvalidResponseStream);
      }


      [TestMethod]
      public void GetLocalWeatherCode_ParsesValidResponse()
      {
         const int expectedWeatherCode = 389;
         string validResponseStream =
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
            "<data>" +
               "<request>" +
                  "<type>City</type>" +
                  "<query>Toulouse, France</query>" +
               "</request>" +
               "<current_condition>" +
                  "<observation_time>08:53 PM</observation_time>" +
                  "<temp_C>26</temp_C>" +
                  "<temp_F>79</temp_F>" +
                  $"<weatherCode>{expectedWeatherCode}</weatherCode>" +
                  "<weatherIconUrl>" +
                     "<![CDATA[http://sample.icon/image.png]]></weatherIconUrl>" +
                  "<weatherDesc>" +
                     "<![CDATA[Light Rain With Thunderstorm]]>" +
                  "</weatherDesc>" +
               "</current_condition>" +
            "</data>";

         var locationDetails = GetLocationDetailsWithMockedWeather(validResponseStream);
         var offset = locationDetails.GetLocalWeatherCode(string.Empty, string.Empty);
         Assert.AreEqual(offset, expectedWeatherCode);
      }

      [TestMethod]
      [ExpectedException(
         typeof(LocationDetailsException),
         "Expected exception when weather info is empty.")]
      public void GetLocalWeatherCode_ParsesEmptyResponse()
      {
         GetLocalWeatherCode_TestError(string.Empty);
      }

      [TestMethod]
      [ExpectedException(
         typeof(LocationDetailsException),
         "Expected exception when weather information is not available.")]
      public void GetLocalWeatherCode_ParsesResponseMissingTimezone()
      {
         const string InvalidResponseStream = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><data/>";
         GetLocalWeatherCode_TestError(InvalidResponseStream);
      }

      [TestMethod]
      [ExpectedException(
         typeof(LocationDetailsException),
         "Expected exception when weather code is not available.")]
      public void GetLocalWeatherCode_ParsesResponseMissingOffset()
      {
         const string InvalidResponseStream =
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
            "<data>" +
               "<request>" +
                  "<type>City</type>" +
                  "<query>Toulouse, France</query>" +
               "</request>" +
               "<current_condition></current_condition>" +
            "</data>";
         GetLocalWeatherCode_TestError(InvalidResponseStream);
      }

      private void GetTimezoneOffSet_TestError(string streamInput)
      {
         var locationDetails = GetLocationDetailsWithMockedTimeZone(streamInput);
         locationDetails.GetTimezoneOffSet(string.Empty, string.Empty);
      }

      private void GetLocalWeatherCode_TestError(string streamInput)
      {
         var locationDetails = GetLocationDetailsWithMockedWeather(streamInput);
         locationDetails.GetLocalWeatherCode(string.Empty, string.Empty);
      }

      private static ILocationDetailsService GetLocationDetailsWithMockedTimeZone(
         string mockResponse)
      {
         var detailFetcher = Substitute.For<ILocationDetailsFetcher>();

         detailFetcher.FetchClockDetailsForLocation(
            Arg.Any<string>(),
            Arg.Any<string>())
            .Returns(GetMockResponseStream(mockResponse));

         return new LocationDetailsService(detailFetcher);
      }

      private static ILocationDetailsService GetLocationDetailsWithMockedWeather(
         string mockResponse)
      {
         var detailFetcher = Substitute.For<ILocationDetailsFetcher>();

         detailFetcher.FetchWeatherDetailsForLocation(
            Arg.Any<string>(),
            Arg.Any<string>())
            .Returns(GetMockResponseStream(mockResponse));

         return new LocationDetailsService(detailFetcher);
      }

      private static Stream GetMockResponseStream(string expected)
      {
         var expectedBytes = Encoding.UTF8.GetBytes(expected);
         var responseStream = new MemoryStream();
         responseStream.Write(expectedBytes, 0, expectedBytes.Length);
         responseStream.Seek(0, SeekOrigin.Begin);
         return responseStream;
      }
   }
}
