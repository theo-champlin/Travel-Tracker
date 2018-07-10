using System;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace Travel_Tracker
{
   /// <summary>
   /// Interaction logic for TimerWindow.xaml
   /// </summary>
   public partial class TimerWindow : Window
   {
      const string SERVICE_KEY = "2c69bdbf75324227bbb213129182906";
      const string SERVICE_DOMAIN = "https://api.worldweatheronline.com/premium/v1/";
      const string TIMEZONE_SERVICE_PATH = "tz.ashx";
      const string WEATHER_SERVICE_PATH = "weather.ashx";
      const string WEATHER_ADDITIONAL_ARGUMENTS = "&date=today&fx=no&cc=yes&show_comments=no&num_of_days=1&mca=no";

      public string URI_IMAGE_SOURCE { get; set; }

      private DispatcherTimer timer;
      private int offset;

      public TimerWindow()
      {
         InitializeComponent();

         LocationInput locationWindow = new LocationInput();
         locationWindow.ShowDialog();

         var timeZoneResponse = FetchLocationInformation(LocationInput.City, LocationInput.Country, TIMEZONE_SERVICE_PATH);
         SetTimeZoneFromResponse(timeZoneResponse);

         var localWeatherResponse = FetchLocationInformation(LocationInput.City, LocationInput.Country, WEATHER_SERVICE_PATH, WEATHER_ADDITIONAL_ARGUMENTS);
         SetWeatherIcon(localWeatherResponse);

         SetTime();

         timer = new DispatcherTimer(
            GetClockUpdateInterval(),
            DispatcherPriority.Normal,
            delegate { TimerElapsed(); },
            Dispatcher);
      }

      private HttpWebResponse FetchLocationInformation(string city, string country, string service, string additionalArguments = "")
      {
         var location = (city + ',' + country).Replace(" ", "+");
         var url = SERVICE_DOMAIN + service + $"?q={location}&key={SERVICE_KEY}" + additionalArguments;
         var request = (HttpWebRequest)WebRequest.Create(url);
         return (HttpWebResponse)request.GetResponse();
      }

      public struct TimeZone
      {
         public string UTCOffset { get; set; }
      }

      private void SetTimeZoneFromResponse(HttpWebResponse timeZoneResponse)
      {
         using (var xmlReader = new StreamReader(timeZoneResponse.GetResponseStream()))
         {
            var doc = XDocument.Load(xmlReader);
            var value = (from locationInfo in doc.Descendants("time_zone")
               select new TimeZone
               {
                  UTCOffset = locationInfo.Element("utcOffset").Value
               }).First().UTCOffset;

            offset = Int32.Parse(value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
         }
      }

      public struct Weather
      {
         public string IconUrl { get; set; }
      }

      private void SetWeatherIcon(HttpWebResponse localWeatherResponse)
      {
         using (var xmlReader = new StreamReader(localWeatherResponse.GetResponseStream()))
         {
            var doc = XDocument.Load(xmlReader);
            var value = (from locationInfo in doc.Descendants("current_condition")
               select new Weather
               {
                  IconUrl = locationInfo.Element("weatherIconUrl").Value
               }).First().IconUrl;

            URI_IMAGE_SOURCE = value;
            WeatherIcon.GetBindingExpression(Image.SourceProperty).UpdateTarget();
         }
      }

      private void SetTime()
      {
         dateText.Text = DateTime.UtcNow.AddHours(offset).ToString("h:mm tt");
      }

      private void TimerElapsed()
      {
         timer.Interval = GetClockUpdateInterval();
         timer.Start();
         SetTime();
      }

      private TimeSpan GetClockUpdateInterval()
      {
         const int SECONDS_IN_MINUTE = 60;
         const int MILLISECOND_IN_SECOND = 1000;

         // To ensure the update happens after the minute updates, not right before.
         const int UPDATE_DELAY_WINDOW = 15;

         var now = DateTime.Now;
         var millisecondsUntilUpdate =
            (SECONDS_IN_MINUTE - now.Second)
            * MILLISECOND_IN_SECOND - now.Millisecond
            + UPDATE_DELAY_WINDOW;

         return TimeSpan.FromMilliseconds(millisecondsUntilUpdate);
      }
   }
}
