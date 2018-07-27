using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace TravelTracker.Models.Implementations
{
   using Interfaces;
   using Services.Interfaces;

   public class Timer : ITimer
   {
      #region Properties

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

      #endregion

      #region Public

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

      #endregion

      #region INotifyPropertyChanged

      public event PropertyChangedEventHandler PropertyChanged;

      private void OnPropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      #endregion

      #region Members

      private DispatcherTimer intervalTimer;
      private ITimerUtility timerUtil;

      #endregion

      #region Private

      private void TimerElapsed()
      {
         FormattedTime = timerUtil.GetFormattedLocationTime(DateTime.UtcNow);

         intervalTimer.Interval = timerUtil.GetMinuteUpdateInterval(DateTime.Now);
         intervalTimer.Start();
      }

      #endregion
   }
}
