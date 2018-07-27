using System.Windows.Input;

namespace TravelTracker.ViewModels
{
   using Commands;
   using Models.Interfaces;

   public class LocationErrorViewModel
   {
      #region Properties

      private CloseWindow _closeWindow;
      public ICommand CloseWindow
      {
         get
         {
            return _closeWindow;
         }
      }

      private SetExitMode _setExitMode;
      public ICommand SetExitMode
      {
         get
         {
            return _setExitMode;
         }
      }

      public bool ExitMode { get; set; }

      public ITheme Theme
      {
         get;
      }

      #endregion

      #region Public

      public LocationErrorViewModel(ITheme appliedTheme)
      {
         _closeWindow = new CloseWindow();
         _setExitMode = new SetExitMode(this);
         ExitMode = false;
         Theme = appliedTheme;
      }

      #endregion
   }
}
