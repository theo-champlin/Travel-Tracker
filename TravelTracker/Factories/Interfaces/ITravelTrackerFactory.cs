namespace TravelTracker.Factories.Interfaces
{
   using Models.Interfaces;
   using ViewModels.Interfaces;

   public interface ITravelTrackerFactory
   {
      ITravelTrackingViewModel Generate(ITheme currentTheme);
   }
}
