using System;
using System.Windows;
using System.Windows.Input;


namespace TravelTracker.Commands
{
   using ViewModels;

   public class SetLocation : ICommand
   {
      public SetLocation(LocationInputViewModel currentInputControl)
      {
         this.currentInputControl = currentInputControl;
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
         currentInputControl.FinalizeLocation();
         ((Window)parameter).Close();
      }

      private LocationInputViewModel currentInputControl;
   }
}
