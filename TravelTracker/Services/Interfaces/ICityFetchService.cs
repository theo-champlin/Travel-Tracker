using System.Collections.Generic;

namespace TravelTracker.Services.Interfaces
{
   using Serializations;

   public interface ICityFetchService
   {
      /// <returns>
      /// A list of all country and city pairs that this application considers valid.
      /// </returns>
      List<CityInfo> GetCities();
   }
}
