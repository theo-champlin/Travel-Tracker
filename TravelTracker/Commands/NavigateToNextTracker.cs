using System;
using System.Windows.Input;

namespace TravelTracker.Commands
{
   using Models.Interfaces;

   public class NavigateToNextTracker : ICommand
   {
      public NavigateToNextTracker(INavigator trackerNavigator)
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
         return trackerNavigator.IsNotLast();
      }

      public void Execute(object parameter)
      {
         trackerNavigator.SkipToNext();
      }

      private INavigator trackerNavigator;
   }
}
