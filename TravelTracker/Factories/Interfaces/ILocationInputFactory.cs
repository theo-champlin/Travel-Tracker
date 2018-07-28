namespace TravelTracker.Factories.Interfaces
{
   using Models.Interfaces;
   using ViewModels.Interfaces;

   public interface ILocationInputFactory
   {
      ILocationInputViewModel Generate(ITheme currentTheme);
   }
}
