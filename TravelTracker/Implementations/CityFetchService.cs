using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json;

namespace TravelTracker.Services
{
   using Interfaces;
   using Models;
   using System.Text;

   public class CityFetchService : ICityFetchService
   {
      public List<CityInfo> GetCities()
      {
         using (var cityListfile = File.OpenText(Settings.AppSettings.CityLookupLocation))
         using (var jsonTextReader = new JsonTextReader(cityListfile))
         {
            return new JsonSerializer().Deserialize<IEnumerable<CityInfo>>(jsonTextReader).ToList();
         }
      }
   }
}
