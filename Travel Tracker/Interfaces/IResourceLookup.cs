using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Tracker.Interfaces
{
   public interface IResourceLookup
   {
      object FindWeatherIcon(
         int weatherCode,
         DateTime localTime);
   }
}
