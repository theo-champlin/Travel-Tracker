using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace TravelTracker.Tests
{
   using Implementations;
   using Interfaces;
   using Models;

   [TestClass]
   public class LocationSetTests
   {
      [TestMethod]
      public void GenerateCountryCollection_CollectionSizeIsCorrect()
      {
         var expectedCountryList = new List<string>
         {
            "Australia",
            "France",
            "United States",
            "South Africa",
            "Zimbabwe"
         };

         var countries = new List<string> { };
         locationSetter.PopulateCountryCollection(countries);

         Assert.AreEqual(countries.Count, expectedCountryList.Count);

         foreach (var country in expectedCountryList)
         {
            Assert.IsTrue(countries.Contains(country));
         }
      }

      [TestMethod]
      public void GenerateCityCollection_ValidCountry_CollectionSizeIsCorrect()
      {
         var expectedCities = new List<string>
         {
            "Grand Rapids",
            "San Francisco",
            "Las Vegas",
            "Anchorage"
         };

         GenerateCityCollection_TestHelper("United States", expectedCities);
      }

      [TestMethod]
      public void GenerateCityCollection_InvalidCountry_CollectionSizeIsCorrect()
      {
         GenerateCityCollection_TestHelper("Africa", new List<string> { });
      }

      private ILocationSetService locationSetter = GetLocationSetter();

      private static ILocationSetService GetLocationSetter()
      {
         var citySubset = new List<CityInfo>
         {
            new CityInfo { Country = "Australia", Name = "Perth" },
            new CityInfo { Country = "France", Name = "Toulouse" },
            new CityInfo { Country = "South Africa", Name = "Wolmaransstad" },
            new CityInfo { Country = "Zimbabwe", Name = "Epworth" },
            new CityInfo { Country = "United States", Name = "Grand Rapids" },
            new CityInfo { Country = "United States", Name = "San Francisco" },
            new CityInfo { Country = "United States", Name = "Las Vegas" },
            new CityInfo { Country = "United States", Name = "Anchorage" }
         };
         var cityFetcher = Substitute.For<ICityFetchService>();
         cityFetcher.GetCities().Returns(citySubset);

         return new LocationSetService(cityFetcher);
      }

      private void GenerateCityCollection_TestHelper(
         string country,
         ICollection<string> expectedCities)
      {
         var cities = new List<string> { };
         locationSetter.PopulateCityCollection(country, cities);

         Assert.AreEqual(cities.Count, expectedCities.Count);

         foreach (var city in expectedCities)
         {
            Assert.IsTrue(cities.Contains(city));
         }
      }
   }
}
