using System;
using System.Windows.Input;

namespace TravelTracker.Commands
{
   using ViewModels;

   public class NavigateToPreviousTracker : ICommand
   {
      public NavigateToPreviousTracker(TravelTrackingContainer trackingContainer)
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
         return trackingContainer.IsNotFirst();
      }

      public void Execute(object parameter)
      {
         trackingContainer.SkipToPrevious();
      }

      private TravelTrackingContainer trackingContainer;
   }
}
