using Newtonsoft.Json;

namespace TravelTracker.Models
{
   public class CityInfo
   {
      [JsonProperty("country")]
      public string Country = "";

      [JsonProperty("name")]
      public string Name = "";
   }
}
