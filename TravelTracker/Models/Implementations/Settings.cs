using System;
using System.IO;
using Newtonsoft.Json;

namespace TravelTracker.Models.Implementations
{
   using Interfaces;

   internal class Settings : ISettings
   {
      #region Properties

      public static ISettings AppSettings { get { return ControllerInstance.Value; } }
      public string CityLookupLocation { get; set; }
      public string LocationDetailsServiceKey { get; set; }

      #endregion

      #region Private

      private Settings() { }

      private static Settings Read(string filepath)
      {
         using (var settingsFile = File.OpenText(filepath))
         using (var jsonTextReader = new JsonTextReader(settingsFile))
         {
            return new JsonSerializer().Deserialize<Settings>(jsonTextReader);
         }
      }

      #endregion

      #region Members

      private static readonly Lazy<Settings> ControllerInstance =
        new Lazy<Settings>(() => Read(@"Res/appsettings.json"));

      #endregion
   }
}
