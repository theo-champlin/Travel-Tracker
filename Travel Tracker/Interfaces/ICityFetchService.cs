﻿using System.Collections.Generic;

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
