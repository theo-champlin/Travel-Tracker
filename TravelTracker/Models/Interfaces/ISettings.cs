using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelTracker.Models.Interfaces
{
   internal interface ISettings
   {
      /// <summary>
      /// The relative path to the CityLookup.json file.
      /// </summary>
      string CityLookupLocation
      {
         get;
      }

      /// <summary>
      /// The service key required in requests to the WorldWeatherOnline API.
      /// </summary>
      string LocationDetailsServiceKey
      {
         get;
      }
   }
}
