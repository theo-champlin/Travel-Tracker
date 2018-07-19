using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelTracker.Services.Implementations
{
   using Models;
   using Interfaces;

   public class LocationSetService : ILocationSetService
   {
      public LocationSetService(ICityFetchService cityFetcher)
      {
         AllCities = cityFetcher.GetCities();
      }

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

      public void PopulateCityCollection(string country, ICollection<string> cities)
      {
         if (cities == null)
         {
            throw new ArgumentNullException("\"cities\" argument must not be null");
         }

         var cityCollection = AllCities
            .Where(city => city.Country == country)
            .Select(city => city.Name);
         foreach (var city in cityCollection)
         {
            cities.Add(city);
         }
      }

      public string GetWikiPageId(string country, string city)
      {
         return AllCities
            .Where(cityInfo => cityInfo.Name == city && cityInfo.Country == country)
            .Select(cityInfo => cityInfo.WikiPageId)
            .FirstOrDefault();
      }

      public string GetWeatherAreaCode(string country, string city)
      {
         return AllCities
            .Where(cityInfo =>
               String.Compare(
                  cityInfo.Country,
                  country,
                  StringComparison.OrdinalIgnoreCase) == 0
               && String.Compare(
                  cityInfo.Name,
                  city,
                  StringComparison.OrdinalIgnoreCase) == 0)
            .Select(cityInfo => cityInfo.WeatherAreaCode)
            .FirstOrDefault();
      }

      private List<CityInfo> AllCities;
   }
}
