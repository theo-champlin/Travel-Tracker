using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Tracker.Interfaces
{
   using Models;

   public interface ICityFetchService
   {
      /// <returns>
      /// A list of all country and city pairs that this application considers valid.
      /// </returns>
      List<CityInfo> GetCities();
   }
}
