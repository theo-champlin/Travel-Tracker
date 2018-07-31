using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace TravelTracker.Tests.Models
{
   using Factories.Interfaces;
   using System;
   using TravelTracker.Models.Implementations;
   using TravelTracker.Models.Interfaces;
   using ViewModels.Interfaces;

   /// <summary>
   /// Summary description for LocationTests
   /// </summary>
   [TestClass]
   public class LocationTests
   {
      [TestMethod]
      public void FullName_IsNotInitializedFromInvalidInput()
      {
         var location = new Location(
            GetMockedLocationInputFactory(),
            null);

         Assert.IsNull(location.FullName);
      }

      [TestMethod]
      public void FullName_IsFormattedOnValidInput()
      {
         var location = new Location(
            GetMockedLocationInputFactory(
               ValidLocation.Item1,
               ValidLocation.Item2),
            null);

         Assert.IsTrue(location.FullName == ValidLocation.Item2 + ", " + ValidLocation.Item1);
      }

      [TestMethod]
      public void WikiPageId_IsNotNullOnValidInput()
      {
         var location = new Location(
            GetMockedLocationInputFactory(
               ValidLocation.Item1,
               ValidLocation.Item2),
            null);

         Assert.IsNotNull(location.WikiPageId);
      }

      [TestMethod]
      public void WeatherAreaCode_IsNotNullOnValidInput()
      {
         var location = new Location(
            GetMockedLocationInputFactory(
               ValidLocation.Item1,
               ValidLocation.Item2),
            null);

         Assert.IsNotNull(location.WeatherAreaCode);
      }

      private readonly Tuple<string, string> ValidLocation = new Tuple<string, string>
      (
         "France",
         "Paris"
      );

      private ILocationInputFactory GetMockedLocationInputFactory()
      {
         var locationInputFactory = Substitute.For<ILocationInputFactory>();
         locationInputFactory.Generate(Arg.Any<ITheme>()).Returns((ILocationInputViewModel)null);
         return locationInputFactory;
      }

      private ILocationInputFactory GetMockedLocationInputFactory(
         string country,
         string city)
      {
         var locationInputFactory = Substitute.For<ILocationInputFactory>();

         var mockedLocationInput = Generate(country, city);
         locationInputFactory.Generate(Arg.Any<ITheme>()).Returns(mockedLocationInput);

         return locationInputFactory;
      }

      private static ILocationInputViewModel Generate(
         string country,
         string city)
      {
         var locationTarget = Substitute.For<ILocationInputFields>();
         locationTarget.CountryInput.Returns(country);
         locationTarget.CityInput.Returns(city);

         var locationInput = Substitute.For<ILocationInputViewModel>();
         locationInput.LocationInputFields.Returns(locationTarget);

         locationInput.WikiPageId.Returns("NonNullString");
         locationInput.WeatherAreaCode.Returns("NonNullString");

         locationInput.IsValidCitySet().Returns(true);

         return locationInput;
      }
   }
}
