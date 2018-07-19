using System;
using System.Windows.Input;
using System.Windows.Media;

namespace TravelTracker.Commands
{
   using Models;

   public class UpdateTheme : ICommand
   {
      public UpdateTheme(
         Theme currentDisplay,
         Color foreground,
         Color background)
      {
         this.currentDisplay = currentDisplay;
         this.foreground = foreground;
         this.background = background;
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
         currentDisplay.SetTheme(
            foreground,
            background);
      }

      private Theme currentDisplay;
      private Color foreground;
      private Color background;
   }
}
