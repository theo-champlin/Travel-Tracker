using System;
using System.IO;
using Newtonsoft.Json;

namespace TravelTracker.Models.Implementations
{
   using Interfaces;

   internal class Settings : ISettings
   {
      public static ISettings AppSettings { get { return ControllerInstance.Value; } }
      public string CityLookupLocation { get; set; }
      public string LocationDetailsServiceKey { get; set; }

      private Settings() { }

      private static Settings Read(string filepath)
      {
         using (var settingsFile = File.OpenText(filepath))
         using (var jsonTextReader = new JsonTextReader(settingsFile))
         {
            return new JsonSerializer().Deserialize<Settings>(jsonTextReader);
         }
      }

      private static readonly Lazy<Settings> ControllerInstance =
        new Lazy<Settings>(() => Read(@"Res/appsettings.json"));
   }
}
