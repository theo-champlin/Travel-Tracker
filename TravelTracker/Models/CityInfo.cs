using Newtonsoft.Json;

namespace TravelTracker.Models
{
   public class CityInfo
   {
      [JsonProperty("country")]
      public string Country = "";

      [JsonProperty("name")]
      public string Name = "";

      [JsonProperty("wc")]
      public string WeatherAreaCode = "";

      [JsonProperty("pid")]
      public string WikiPageId = "";
   }
}
