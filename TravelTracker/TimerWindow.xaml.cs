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
      public TimerWindow()
      {
         InitializeComponent();
         this.DataContext = this;
#if DEBUG
         LocationInput locationWindow = new LocationInput
         {
            City = "Poland",
            Country = "Warsaw"
         };
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

      #region ViewModelProperty: BackgroundThemeColor
      private Brush backgroundThemeColor;
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
      #endregion

      #region ViewModelProperty: ForegroundThemeColor
      private Brush foregroundThemeColor;
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
      #endregion

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
         ForegroundThemeColor = new SolidColorBrush(foreground);
         BackgroundThemeColor = new SolidColorBrush(background) { Opacity = 0.75 };
      }

      private void OnClose(object sender, RoutedEventArgs e)
      {
         App.Current.Shutdown();
      }
   }
}
