using System.Windows.Input;

namespace TravelTracker.ViewModels
{
   using Commands;
   using Factories.Implementations;
   using Models.Implementations;
   using Models.Interfaces;

   public class TravelTrackingContainer
   {
      #region Properties

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

      private ICommand _setExtendedView;
      public ICommand SetExtendedView
      {
         get
         {
            if (_setExtendedView == null)
            {
               _setExtendedView = new SetExtendedView(this);
            }

            return _setExtendedView;
         }
      }

      public INavigator Navigator { get; private set; }

      public ITheme Theme { get; private set; }

      #endregion

      #region Public

      public TravelTrackingContainer()
      {
         InitializeTheme();
         Navigator = new Navigator(
            new TravelTrackerFactory(),
            Theme);
      }

      #endregion

      #region Private

      private void InitializeTheme()
      {
         Theme = new Theme();
         Theme.SetBlueTheme.Execute(null);
      }

      #endregion
   }
}
