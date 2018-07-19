using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace TravelTracker.Models
{
   using Interfaces;

   public class Timer : INotifyPropertyChanged
   {
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
