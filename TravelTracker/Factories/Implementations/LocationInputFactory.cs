using System.Windows;

namespace TravelTracker.Factories.Implementations
{
   using Interfaces;
   using Models.Interfaces;
   using ViewModels;
   using Views;

   public class LocationInputFactory : ILocationInputFactory
   {
      #region Public

      public LocationInputViewModel Generate(ITheme currentTheme)
      {
         var locationWindowControl = new LocationInputViewModel(currentTheme);

         if (!SetLocation(
            locationWindowControl,
            currentTheme))
         {
            return null;
         }

         return locationWindowControl;
      }

      #endregion

      #region Private

      private bool SetLocation(
         LocationInputViewModel locationWindowControl,
         ITheme appliedTheme)
      {
         do
         {
            locationWindowControl.ClearInput();
            DisplayLocationInputWindow(locationWindowControl);
         } while (!locationWindowControl.IsValidCitySet()
            && ShouldContinueLocationSelection(appliedTheme));

         return locationWindowControl.IsValidCitySet();
      }

      private bool ShouldContinueLocationSelection(ITheme appliedTheme)
      {
         var errorWindowControl = new LocationErrorViewModel(appliedTheme);
         var errorWindow = new LocationError(errorWindowControl);
         errorWindow.ShowDialog();

         if (errorWindowControl.ExitMode)
         {
            Application.Current.Shutdown();
            return false;
         }

         return true;
      }

      private void DisplayLocationInputWindow(LocationInputViewModel locationWindowControl)
      {
         var locationWindow = new LocationInput(locationWindowControl);
         locationWindow.ShowDialog();
      }

      #endregion
   }
}
