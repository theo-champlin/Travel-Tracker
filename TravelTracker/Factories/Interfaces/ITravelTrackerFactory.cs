namespace TravelTracker.Factories.Interfaces
{
   using Models.Interfaces;
   using ViewModels;

   public interface ITravelTrackerFactory
   {
      TravelTrackingViewModel Generate(ITheme currentTheme);
   }
}
