using System.ComponentModel;

namespace TravelTracker.Models.Interfaces
{
   public interface ILocationInputFields : INotifyPropertyChanged
   {
      string CountryInput
      {
         get;
         set;
      }

      string CityInput
      {
         get;
         set;
      }
   }
}
