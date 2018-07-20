using System;
using System.Windows.Input;
using System.Windows.Media;

namespace TravelTracker.Commands
{
   using Models.Interfaces;

   public class UpdateTheme : ICommand
   {
      public UpdateTheme(
         ITheme currentDisplay,
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

      private ITheme currentDisplay;
      private Color foreground;
      private Color background;
   }
}
