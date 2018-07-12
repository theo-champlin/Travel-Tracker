using System.IO;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Travel_Tracker_Tests
{
   using Travel_Tracker.Interfaces;
   using Travel_Tracker.Services;

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
      public void GetTimezoneOffSet_ParsesEmptyResponse()
      {
         const int expectedOffset = 0;
         string validResponseStream = string.Empty;

         var locationDetails = GetLocationDetailsWithMockedTimeZone(validResponseStream);
         var offset = locationDetails.GetTimezoneOffSet(string.Empty, string.Empty);
         Assert.AreEqual(offset, expectedOffset);
      }

      private static ILocationDetailsService GetLocationDetailsWithMockedWeather(
         string mockResponse)
      {
         var detailFetcher = Substitute.For<ILocationDetailsFetcher>();

         detailFetcher.FetchWeatherDetailsForLocation(
            Arg.Any<string>(),
            Arg.Any<string>()).Returns(GetMockResponseStream(mockResponse));

         return new LocationDetailsService(detailFetcher);
      }

      private static ILocationDetailsService GetLocationDetailsWithMockedTimeZone(
         string mockResponse)
      {
         var detailFetcher = Substitute.For<ILocationDetailsFetcher>();

         detailFetcher.FetchClockDetailsForLocation(
            Arg.Any<string>(),
            Arg.Any<string>()).Returns(GetMockResponseStream(mockResponse));

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
