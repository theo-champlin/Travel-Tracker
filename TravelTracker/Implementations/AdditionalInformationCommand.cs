using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TravelTracker.Implementations
{
   class AdditionalInformationCommand : ICommand
   {
#pragma warning disable 67
      public event EventHandler CanExecuteChanged
      {
         add { CommandManager.RequerySuggested += value; }
         remove { CommandManager.RequerySuggested -= value; }
      }
#pragma warning restore 67

      public AdditionalInformationCommand(string pageLocation)
      {
         this.pageLocation = pageLocation;
      }

      public bool CanExecute(object parameter)
      {
         return true;
      }

      public void Execute(object parameter)
      {
         System.Diagnostics.Process.Start(
            "https://en.wikipedia.org/wiki/?curid=" +
            pageLocation);
      }

      private string pageLocation;
   }
}
