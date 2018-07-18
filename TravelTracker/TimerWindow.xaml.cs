using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;

namespace TravelTracker
{
   using Interfaces;
   using Services;
   using System.Windows.Input;

   /// <summary>
   /// Interaction logic for TimerWindow.xaml
   /// </summary>
   public partial class TimerWindow : Window
   {
      public TimerWindow()
      {
         InitializeComponent();

#if NDEBUG
         LocationInput.City = "Poland";
         LocationInput.Country = "Warsaw";
#else
         LocationInput locationWindow = new LocationInput();
         locationWindow.ShowDialog();
#endif
         location.Text = locationWindow.City + ", " + locationWindow.Country;
         weatherLocationTarget = locationWindow.WeatherAreaCode;

         StartTimeTracking(
            locationWindow.Country,
            locationWindow.City);

         // This needs to happen after the time is set because the local time is used to decide
         // which weather icon to display.
         SetWeatherIcon(
            locationWindow.Country,
            locationWindow.City);

         MouseLeftButtonDown += delegate { DragMove(); };
      }

      private DispatcherTimer timer;

      private ITimerUtility timerUtil;

      private ILocationDetailsService locationDetails
         = new LocationDetailsService(new LocationDetailsFetcher());

      private string weatherLocationTarget = string.Empty;

      private void StartTimeTracking(
         string country,
         string city)
      {
         timerUtil = new TimerUtility(
            locationDetails,
            country,
            city);

         SetTime();

         timer = new DispatcherTimer(
            timerUtil.GetMinuteUpdateInterval(DateTime.Now),
            DispatcherPriority.Normal,
            delegate { TimerElapsed(); },
            Dispatcher);
      }

      private void SetTime()
      {
         dateText.Text = timerUtil.GetFormattedLocationTime(DateTime.UtcNow);
      }

      private void TimerElapsed()
      {
         SetTime();

         timer.Interval = timerUtil.GetMinuteUpdateInterval(DateTime.Now);
         timer.Start();
      }

      private void SetWeatherIcon(
         string country,
         string city)
      {
         var weatherCode = locationDetails.GetLocalWeatherCode(
            country,
            city);

         IResourceLookup weatherResourceLookup = new ResourceLookup();
         var iconResource = weatherResourceLookup.FindWeatherIcon(
            weatherCode,
            timerUtil.GetLocationTime(DateTime.UtcNow));

         var binding = new Binding
         {
            Source = iconResource
         };

         WeatherIcon.SetBinding(ContentProperty, binding);
      }

      private void OpenWeatherPage(
         object sender,
         MouseButtonEventArgs e)
      {
         if (string.IsNullOrEmpty(weatherLocationTarget))
         {
            return;
         }

         System.Diagnostics.Process.Start(
            "https://www.wunderground.com/q/zmw:" +
            weatherLocationTarget);
         e.Handled = true;
      }

      private void SetDarkTheme(object sender, RoutedEventArgs e)
      {
         UpdateTheme(Colors.Black, Colors.Gray);
      }

      private void SetLightTheme(object sender, RoutedEventArgs e)
      {
         UpdateTheme(Colors.DeepSkyBlue, Colors.White);
      }

      private void UpdateTheme(Color foreground, Color background)
      {
         var foregroundBrush = new SolidColorBrush(foreground);
         BorderBrush = foregroundBrush;
         location.Foreground = foregroundBrush;
         dateText.Foreground = foregroundBrush;

         Background = new SolidColorBrush(background) { Opacity = 0.75 };
      }

      private void OnClose(object sender, RoutedEventArgs e)
      {
         App.Current.Shutdown();
      }
   }
}
