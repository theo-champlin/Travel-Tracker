using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Travel_Tracker_Tests
{
   using Travel_Tracker.Services;

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
         LocationSetService.Instance.PopulateCountryCollection(countries);

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
            "Yerres",
            "Toulouse",
            "Paris",
            "La Defense"
         };

         GenerateCityCollection_TestHelper("France", expectedCities);
      }

      [TestMethod]
      public void GenerateCityCollection_InvalidCountry_CollectionSizeIsCorrect()
      {
         GenerateCityCollection_TestHelper("Africa", new List<string> { });
      }

      private void GenerateCityCollection_TestHelper(
         string country,
         ICollection<string> expectedCities)
      {
         var cities = new List<string> { };
         LocationSetService.Instance.PopulateCityCollection(country, cities);

         Assert.AreEqual(cities.Count, expectedCities.Count);

         foreach (var city in expectedCities)
         {
            Assert.IsTrue(cities.Contains(city));
         }
      }
   }
}
