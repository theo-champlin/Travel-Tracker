using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace TravelTracker
{
   using Interfaces;
   using Services;

   /// <summary>
   /// Interaction logic for TimerWindow.xaml
   /// </summary>
   public partial class TimerWindow : Window, INotifyPropertyChanged
   {
      public Brush BackgroundThemeColor
      {
         get
         {
            return backgroundThemeColor;
         }

         set
         {
            backgroundThemeColor = value;
            OnPropertyChanged("BackgroundThemeColor");
         }
      }

      public Brush ForegroundThemeColor
      {
         get
         {
            return foregroundThemeColor;
         }

         set
         {
            foregroundThemeColor = value;
            OnPropertyChanged("ForegroundThemeColor");
         }
      }

      public string CurrentTime
      {
         get
         {
            return time;
         }

         set
         {
            time = value;
            OnPropertyChanged("CurrentTime");
         }
      }

      public string Location
      {
         get
         {
            return location;
         }

         set
         {
            location = value;
            OnPropertyChanged("Location");
         }
      }

      public object WeatherIcon
      {
         get
         {
            return weather;
         }

         set
         {
            weather = value;
            OnPropertyChanged("WeatherIcon");
         }
      }

      public TimerWindow()
      {
         InitializeComponent();
         DataContext = this;
#if DEBUG
         LocationInput locationWindow = new LocationInput
         {
            City = "Paris",
            Country = "France"
         };
#else
         LocationInput locationWindow = new LocationInput();
         locationWindow.ShowDialog();
#endif
         Location = locationWindow.City + ", " + locationWindow.Country;
         weatherLocationTarget = locationWindow.WeatherAreaCode;

         StartTimeTracking(
            locationWindow.Country,
            locationWindow.City);

         // This needs to happen after the time is set because the local time is used to decide
         // which weather icon to display.
         SetWeatherIcon(
            locationWindow.Country,
            locationWindow.City);

         UpdateTheme(Colors.Black, Colors.Gray);

         MouseLeftButtonDown += delegate { DragMove(); };
      }

      #region INotifiedProperty Block
      public event PropertyChangedEventHandler PropertyChanged;

      protected void OnPropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
      #endregion

      private DispatcherTimer timer;

      private ITimerUtility timerUtil;

      private ILocationDetailsService locationDetails
         = new LocationDetailsService(new LocationDetailsFetcher());

      private string weatherLocationTarget = string.Empty;

      private Brush backgroundThemeColor;
      private Brush foregroundThemeColor;

      private string time;
      private string location;

      private object weather;

      private void StartTimeTracking(
         string country,
         string city)
      {
         timerUtil = new TimerUtility(
            locationDetails,
            country,
            city);

         CurrentTime = timerUtil.GetFormattedLocationTime(DateTime.UtcNow);

         timer = new DispatcherTimer(
            timerUtil.GetMinuteUpdateInterval(DateTime.Now),
            DispatcherPriority.Normal,
            delegate { TimerElapsed(); },
            Dispatcher);
      }

      private void TimerElapsed()
      {
         CurrentTime = timerUtil.GetFormattedLocationTime(DateTime.UtcNow);

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
         WeatherIcon = weatherResourceLookup.FindWeatherIcon(
            weatherCode,
            timerUtil.GetLocationTime(DateTime.UtcNow));
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
         ForegroundThemeColor = new SolidColorBrush(foreground);
         BackgroundThemeColor = new SolidColorBrush(background) { Opacity = 0.75 };
      }

      private void OnClose(object sender, RoutedEventArgs e)
      {
         App.Current.Shutdown();
      }
   }
}
