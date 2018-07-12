using System;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Travel_Tracker
{
   using Interfaces;
   using Services;

   /// <summary>
   /// Interaction logic for TimerWindow.xaml
   /// </summary>
   public partial class TimerWindow : Window
   {
      public string WeatherImageSource { get; set; }

      public TimerWindow()
      {
         InitializeComponent();

         LocationInput locationWindow = new LocationInput();
         locationWindow.ShowDialog();

         MouseLeftButtonDown += delegate { DragMove(); };

         location.Text = LocationInput.City + ", " + LocationInput.Country;

         offset = locationDetails.GetTimezoneOffSet(
            LocationInput.Country,
            LocationInput.City);

         WeatherImageSource = locationDetails.GetWeatherIconUrl(
            LocationInput.Country,
            LocationInput.City);
         WeatherIcon.GetBindingExpression(Image.SourceProperty).UpdateTarget();

         SetTime();

         timer = new DispatcherTimer(
            GetClockUpdateInterval(),
            DispatcherPriority.Normal,
            delegate { TimerElapsed(); },
            Dispatcher);
      }

      private DispatcherTimer timer;

      private int offset;

      private ILocationDetailsService locationDetails
         = new LocationDetailsService(new LocationDetailsFetcher());

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
