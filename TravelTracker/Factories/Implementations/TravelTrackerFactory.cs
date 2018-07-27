namespace TravelTracker.Factories.Implementations
{
   using Interfaces;
   using Models.Interfaces;
   using ViewModels;

   public class TravelTrackerFactory : ITravelTrackerFactory
   {
      #region Public

      public TravelTrackingViewModel Generate(ITheme currentTheme)
      {
         return new TravelTrackingViewModel(currentTheme);
      }

      #endregion
   }
}
