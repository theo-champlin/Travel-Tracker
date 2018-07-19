using System;
using System.Diagnostics;
using System.Windows.Input;

namespace TravelTracker.Commands
{
   public class NavigateToWeather : ICommand
   {
      public NavigateToWeather(string weatherAreaCode)
      {
         this.weatherAreaCode = weatherAreaCode;
      }

      public event EventHandler CanExecuteChanged
      {
         add
         {
            CommandManager.RequerySuggested += value;
         }
         remove
         {
            CommandManager.RequerySuggested -= value;
         }
      }

      public bool CanExecute(object parameter)
      {
         return !string.IsNullOrEmpty(weatherAreaCode);
      }

      public void Execute(object parameter)
      {
         Process.Start(
            "https://www.wunderground.com/q/zmw:" +
            weatherAreaCode);
      }

      private string weatherAreaCode;
   }
}
