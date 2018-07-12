using Newtonsoft.Json;

namespace Travel_Tracker.Models
{
   public class CityInfo
   {
      [JsonProperty("country")]
      public string Country = "";

      [JsonProperty("name")]
      public string Name = "";
   }
}
