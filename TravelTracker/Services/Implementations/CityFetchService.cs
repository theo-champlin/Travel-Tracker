using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json;

namespace TravelTracker.Services.Implementations
{
   using Interfaces;
   using Models.Implementations;
   using Serializations;

   public class CityFetchService : ICityFetchService
   {
      #region Public

      public List<CityInfo> GetCities()
      {
         using (var cityListfile = File.OpenText(Settings.AppSettings.CityLookupLocation))
         using (var jsonTextReader = new JsonTextReader(cityListfile))
         {
            return new JsonSerializer().Deserialize<IEnumerable<CityInfo>>(jsonTextReader).ToList();
         }
      }

      #endregion
   }
}
