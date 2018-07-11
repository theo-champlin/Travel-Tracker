using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Travel_Tracker.Services
{
   public class LocationSetService
   {
      public static LocationSetService Instance { get { return ControllerInstance.Value; } }

      /// <summary>
      /// Populates the "countries" argument with a collection of all countries this application
      /// considers valid.
      /// </summary>
      /// <param name="countries"></param>
      public void PopulateCountryCollection(ICollection<string> countries)
      {
         if (countries == null)
         {
            throw new ArgumentNullException("\"countries\" argument must not be null");
         }

         var countryCollection = AllCities.Select(city => city.Country).Distinct();
         foreach (var country in countryCollection)
         {
            countries.Add(country);
         }
      }

      /// <summary>
      /// Populates the "cities" argument with a collection of all cities in the given country this
      /// application considers valid.
      /// </summary>
      /// <param name="country">
      /// The country for which we want to retrieve a collection of contained cities.
      /// </param>
      /// <param name="cities"></param>
      public void PopulateCityCollection(string country, ICollection<string> cities)
      {
         if (cities == null)
         {
            throw new ArgumentNullException("\"cities\" argument must not be null");
         }

         var cityCollection = AllCities.Where(city => city.Country == country).Select(city => city.Name);
         foreach (var city in cityCollection)
         {
            cities.Add(city);
         }
      }

      private class CityInfo
      {
         [JsonProperty("country")]
         public string Country = "";

         [JsonProperty("name")]
         public string Name = "";
      }

      private LocationSetService()
      {
         using (var cityListfile = File.OpenText(Settings.AppSettings.CityLookupLocation))
         using (var jsonTextReader = new JsonTextReader(cityListfile))
         {
            AllCities = new JsonSerializer().Deserialize<IEnumerable<CityInfo>>(jsonTextReader).ToList();
         }
      }

      private List<CityInfo> AllCities;

      private static readonly Lazy<LocationSetService> ControllerInstance =
        new Lazy<LocationSetService>(() => new LocationSetService());
   }
}
