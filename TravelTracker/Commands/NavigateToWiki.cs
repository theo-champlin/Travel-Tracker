using System;
using System.Diagnostics;
using System.Windows.Input;

namespace TravelTracker.Commands
{
   public class NavigateToWiki : ICommand
   {
      public NavigateToWiki(string wikiPageId)
      {
         this.wikiPageId = wikiPageId;
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
         return !string.IsNullOrEmpty(wikiPageId);
      }

      public void Execute(object parameter)
      {
         Process.Start(
            "https://en.wikipedia.org/wiki/?curid=" +
            wikiPageId);
      }

      private string wikiPageId;
   }
}
