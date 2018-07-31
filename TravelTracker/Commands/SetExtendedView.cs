using System;
using System.Windows;
using System.Windows.Input;

namespace TravelTracker.Commands
{
   using ViewModels;
   using Views;

   class SetExtendedView : ICommand
   {
      public SetExtendedView(TravelTrackingContainer trackers)
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
         var extendedWindow = new ExtendedTimers(trackers);
         extendedWindow.Show();

         ((Window)parameter).Close();
      }

      private TravelTrackingContainer trackers;
   }
}
