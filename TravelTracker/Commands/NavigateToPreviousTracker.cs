using System;
using System.Windows.Input;

namespace TravelTracker.Commands
{
   using Models.Interfaces;

   public class NavigateToPreviousTracker : ICommand
   {
      public NavigateToPreviousTracker(INavigator trackerNavigator)
      {
         this.trackerNavigator = trackerNavigator;
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
         return trackerNavigator.IsNotFirst();
      }

      public void Execute(object parameter)
      {
         trackerNavigator.SkipToPrevious();
      }

      private INavigator trackerNavigator;
   }
}
