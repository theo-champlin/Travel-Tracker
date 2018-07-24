using System;
using System.Windows;
using System.Windows.Input;

namespace TravelTracker.Commands
{
   using ViewModels;

   class SetExitMode : ICommand
   {
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
         errorWindowControl.ExitMode = true;
         ((Window)parameter).Close();
      }

      public SetExitMode(LocationErrorViewModel errorWindowControl)
      {
         this.errorWindowControl = errorWindowControl;
      }

      private LocationErrorViewModel errorWindowControl;
   }
}
