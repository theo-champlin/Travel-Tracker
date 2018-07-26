using System;
using System.Windows.Input;

namespace TravelTracker.Commands
{
   using ViewModels;

   public class NavigateToNextTracker : ICommand
   {
      public NavigateToNextTracker(TravelTrackingContainer trackingContainer)
      {
         this.trackingContainer = trackingContainer;
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
         return trackingContainer.IsNotLast();
      }

      public void Execute(object parameter)
      {
         trackingContainer.SkipToNext();
      }

      private TravelTrackingContainer trackingContainer;
   }
}
