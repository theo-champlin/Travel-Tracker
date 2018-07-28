namespace TravelTracker.Factories.Implementations
{
   using Interfaces;
   using Models.Interfaces;
   using ViewModels;
   using ViewModels.Interfaces;

   public class TravelTrackerFactory : ITravelTrackerFactory
   {
      #region Public

      public ITravelTrackingViewModel Generate(ITheme currentTheme)
      {
         return new TravelTrackingViewModel(
            new LocationInputFactory(),
            currentTheme);
      }

      #endregion
   }
}
