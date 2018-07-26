using System;
using System.Windows.Input;

namespace TravelTracker.Commands
{
   using Models.Interfaces;
   using Models.Implementations;

   public class AddTracker : ICommand
   {
      public AddTracker(INavigator trackerNavigator)
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
         return true;
      }

      public void Execute(object parameter)
      {
         trackerNavigator.Add(parameter as Theme);
      }

      private INavigator trackerNavigator;
   }
}
