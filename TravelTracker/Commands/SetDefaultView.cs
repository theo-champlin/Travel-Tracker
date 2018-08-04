using System;
using System.Windows;
using System.Windows.Input;

namespace TravelTracker.Commands
{
   using ViewModels;
   using Views;

   public class SetDefaultView : ICommand
   {
      public SetDefaultView(TravelTrackingContainer trackers)
      {
         this.trackers = trackers;
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
         return true;
      }

      public void Execute(object parameter)
      {
         var singleViewWindow = new TimerWindow(trackers);
         singleViewWindow.Show();

         ((Window)parameter).Close();
      }

      private TravelTrackingContainer trackers;
   }
}
