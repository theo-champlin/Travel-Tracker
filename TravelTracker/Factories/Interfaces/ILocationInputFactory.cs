namespace TravelTracker.Factories.Interfaces
{
   using Models.Interfaces;
   using ViewModels;

   public interface ILocationInputFactory
   {
      LocationInputViewModel Generate(ITheme currentTheme);
   }
}
