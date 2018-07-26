using System.Windows.Input;

namespace TravelTracker.ViewModels
{
   using Commands;
   using Models.Implementations;
   using Models.Interfaces;

   public class TravelTrackingContainer
   {
      private ExitApplication _exitApplication;
      public ICommand ExitApplication
      {
         get
         {
            if (_exitApplication == null)
            {
               _exitApplication = new ExitApplication();
            }

            return _exitApplication;
         }
      }

      public INavigator Navigator { get; private set; }

      public ITheme Theme { get; private set; }

      public TravelTrackingContainer()
      {
         InitializeTheme();
         Navigator = new Navigator(Theme);
      }

      private void InitializeTheme()
      {
         Theme = new Theme();
         Theme.SetBlueTheme.Execute(null);
      }
   }
}
