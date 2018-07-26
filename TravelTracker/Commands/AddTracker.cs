using System;
using System.Windows.Input;

namespace TravelTracker.Commands
{
   using ViewModels;

   public class AddTracker : ICommand
   {
      public AddTracker(TravelTrackingContainer trackingContainer)
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
         return true;
      }

      public void Execute(object parameter)
      {
         trackingContainer.Add();
      }

      private TravelTrackingContainer trackingContainer;
   }
}
