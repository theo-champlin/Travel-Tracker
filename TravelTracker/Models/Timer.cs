using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace TravelTracker.Models
{
   using Services.Interfaces;

   /// <summary>
   /// A class managing and making available the current time in a location specified by the user.
   /// Timer will send a property changed notification when any public property is updated to
   /// support binding.
   /// </summary>
   public class Timer : INotifyPropertyChanged
   {
      /// <summary>
      /// A string of format "h:mm tt" representing the local time.
      /// </summary>
      private string _time;
      public string FormattedTime
      {
         get
         {
            return _time;
         }

         private set
         {
            _time = value;
            OnPropertyChanged("FormattedTime");
         }
      }

      public Timer(ITimerUtility timerUtil)
      {
         this.timerUtil = timerUtil;
         
         FormattedTime = timerUtil.GetFormattedLocationTime(DateTime.UtcNow);

         intervalTimer = new DispatcherTimer(
            timerUtil.GetMinuteUpdateInterval(DateTime.Now),
            DispatcherPriority.Normal,
            delegate { TimerElapsed(); },
            Dispatcher.CurrentDispatcher);
      }

      #region INotifyPropertyChanged
      public event PropertyChangedEventHandler PropertyChanged;

      private void OnPropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
      #endregion

      private DispatcherTimer intervalTimer;
      private ITimerUtility timerUtil;

      private void TimerElapsed()
      {
         FormattedTime = timerUtil.GetFormattedLocationTime(DateTime.UtcNow);

         intervalTimer.Interval = timerUtil.GetMinuteUpdateInterval(DateTime.Now);
         intervalTimer.Start();
      }
   }
}
