using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TravelTracker
{
   using Implementations;
   using Interfaces;
   using Models;

   /// <summary>
   /// Interaction logic for TimerWindow.xaml
   /// </summary>
   public partial class TimerWindow : Window, INotifyPropertyChanged
   {
      private Theme _theme;
      public Theme Theme
      {
         get
         {
            return _theme;
         }
      }

      private Timer _timer;
      public Timer Timer
      {
         get
         {
            return _timer;
         }
      }

      private Weather _weather;
      public Weather Weather
      {
         get
         {
            return _weather;
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

      public string InfoUrl
      {
         get
         {
            return infoUrl;
         }

         set
         {
            infoUrl = "https://en.wikipedia.org/wiki/?curid=" + value;
            OnPropertyChanged("InfoUrl");
         }
      }

      public ICommand GetAdditionalInfo;

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
         InfoUrl = locationWindow.WikiPageId;

         StartTimeTracking(
            locationWindow.Country,
            locationWindow.City);

         // This needs to happen after the time is set because the local time is used to decide
         // which weather icon to display.
         SetWeatherIcon(
            locationWindow.Country,
            locationWindow.City);

         _theme = new Theme(Colors.Black, Colors.Gray);

         MouseLeftButtonDown += delegate { DragMove(); };
      }

      #region INotifiedProperty Block
      public event PropertyChangedEventHandler PropertyChanged;

      protected void OnPropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
      #endregion

      private ITimerUtility timerUtil;

      private ILocationDetailsService locationDetails
         = new LocationDetailsService(new LocationDetailsFetcher());

      private string weatherLocationTarget = string.Empty;

      private string location;
      private string infoUrl;

      private void StartTimeTracking(
         string country,
         string city)
      {
         timerUtil = new TimerUtility(
            locationDetails,
            country,
            city);

         _timer = new Timer(timerUtil);
      }

      private void SetWeatherIcon(
         string country,
         string city)
      {
         _weather = new Weather(
            country,
            city,
            timerUtil.GetLocationTime(DateTime.UtcNow),
            locationDetails);
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
         _theme.SetTheme(Colors.Black, Colors.Gray);
      }

      private void SetLightTheme(object sender, RoutedEventArgs e)
      {
         _theme.SetTheme(Colors.DeepSkyBlue, Colors.White);
      }

      private void OnClose(object sender, RoutedEventArgs e)
      {
         App.Current.Shutdown();
      }
   }
}
