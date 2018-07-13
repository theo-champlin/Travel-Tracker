﻿using System;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Travel_Tracker
{
   using Interfaces;
   using Services;
   using System.Windows.Data;

   /// <summary>
   /// Interaction logic for TimerWindow.xaml
   /// </summary>
   public partial class TimerWindow : Window
   {
      public TimerWindow()
      {
         InitializeComponent();

         LocationInput locationWindow = new LocationInput();
         locationWindow.ShowDialog();
         location.Text = LocationInput.City + ", " + LocationInput.Country;

         StartTimeTracking();

         // This needs to happen after the time is set because the local time is used to decide
         // which weather icon to display.
         SetWeatherIcon();

         MouseLeftButtonDown += delegate { DragMove(); };
      }

      private DispatcherTimer timer;

      private ITimerUtility timerUtil;

      private ILocationDetailsService locationDetails
         = new LocationDetailsService(new LocationDetailsFetcher());

      private void StartTimeTracking()
      {
         timerUtil = new TimerUtility(
            locationDetails,
            LocationInput.Country,
            LocationInput.City);

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

      private void SetWeatherIcon()
      {
         var weatherCode = locationDetails.GetLocalWeatherCode(
           LocationInput.Country,
           LocationInput.City);

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
   }
}
