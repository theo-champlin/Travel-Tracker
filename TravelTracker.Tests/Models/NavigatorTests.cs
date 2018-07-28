using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace TravelTracker.Tests.Models
{
   using Factories.Interfaces;
   using TravelTracker.Models.Implementations;
   using TravelTracker.Models.Interfaces;
   using ViewModels.Interfaces;

   [TestClass]
   public class NavigatorTests
   {
      [TestMethod]
      public void Navigator_InitializesWithSingleTracker()
      {
         var navigator = new Navigator(GetMockedTrackerFactory(), null);

         Assert.IsNotNull(navigator.CurrentTracker);
         Assert.IsTrue(navigator.CurrentTracker == PossibleLocations.ElementAt(0));
      }

      [TestMethod]
      public void Add_NavigatesToLastTracker()
      {
         var navigator = new Navigator(GetMockedTrackerFactory(), null);
         Assert.IsTrue(navigator.CurrentTracker == PossibleLocations.ElementAt(0));

         navigator.Add(null);
         Assert.IsTrue(navigator.CurrentTracker == PossibleLocations.ElementAt(1));

         navigator.Add(null);
         Assert.IsTrue(navigator.CurrentTracker == PossibleLocations.ElementAt(2));
      }

      [TestMethod]
      public void AddTracker_NavigatesToLastTracker()
      {
         var navigator = new Navigator(GetMockedTrackerFactory(), null);
         Assert.IsTrue(navigator.CurrentTracker == PossibleLocations.ElementAt(0));

         navigator.AddTracker.Execute(null);
         Assert.IsTrue(navigator.CurrentTracker == PossibleLocations.ElementAt(1));

         navigator.AddTracker.Execute(null);
         Assert.IsTrue(navigator.CurrentTracker == PossibleLocations.ElementAt(2));
      }

      [TestMethod]
      public void SkipToPrevious_HasNoEffectWithSingleTracker()
      {
         var navigator = new Navigator(GetMockedTrackerFactory(), null);

         navigator.SkipToPrevious();
         Assert.IsTrue(navigator.CurrentTracker == PossibleLocations.ElementAt(0));
      }

      [TestMethod]
      public void SkipToPrevious_UpdatesTrackerWhenNotFirst()
      {
         var navigator = new Navigator(GetMockedTrackerFactory(), null);

         navigator.Add(null);
         navigator.SkipToPrevious();

         Assert.IsTrue(navigator.CurrentTracker == PossibleLocations.ElementAt(0));
      }

      [TestMethod]
      public void HasNoEffectWhenFirst()
      {
         var navigator = new Navigator(GetMockedTrackerFactory(), null);

         navigator.Add(null);
         navigator.SkipToPrevious();
         navigator.SkipToPrevious();

         Assert.IsTrue(navigator.CurrentTracker == PossibleLocations.ElementAt(0));
      }

      [TestMethod]
      public void SkipToNext_HasNoEffectWithSingleTracker()
      {
         var navigator = new Navigator(GetMockedTrackerFactory(), null);

         navigator.SkipToNext();
         Assert.IsTrue(navigator.CurrentTracker == PossibleLocations.ElementAt(0));
      }

      [TestMethod]
      public void SkipToNext_UpdatesTrackerWhenNotLast()
      {
         var navigator = new Navigator(GetMockedTrackerFactory(), null);

         navigator.Add(null);
         navigator.SkipToPrevious();

         navigator.SkipToNext();
         Assert.IsTrue(navigator.CurrentTracker == PossibleLocations.ElementAt(1));
      }

      [TestMethod]
      public void SkipToNext_HasNoEffectWhenLast()
      {
         var navigator = new Navigator(GetMockedTrackerFactory(), null);
         navigator.Add(null);

         navigator.SkipToNext();
         Assert.IsTrue(navigator.CurrentTracker == PossibleLocations.ElementAt(1));
      }

      [TestMethod]
      public void IsNotLast_ReturnsFalseWithSingleTracker()
      {
         var navigator = new Navigator(GetMockedTrackerFactory(), null);
         Assert.IsFalse(navigator.IsNotLast());
      }

      [TestMethod]
      public void IsNotLast_ReturnsFalseAfterAdd()
      {
         var navigator = new Navigator(GetMockedTrackerFactory(), null);

         navigator.Add(null);
         Assert.IsFalse(navigator.IsNotLast());

         navigator.SkipToPrevious();
         Assert.IsTrue(navigator.IsNotLast());

         navigator.Add(null);
         Assert.IsFalse(navigator.IsNotLast());
      }

      [TestMethod]
      public void IsNotFirst_ReturnsFalseWithSingleTracker()
      {
         var navigator = new Navigator(GetMockedTrackerFactory(), null);
         Assert.IsFalse(navigator.IsNotFirst());
      }

      [TestMethod]
      public void IsNotFirst_ReturnsTrueAfterAdd()
      {
         var navigator = new Navigator(GetMockedTrackerFactory(), null);

         navigator.Add(null);
         Assert.IsTrue(navigator.IsNotFirst());

         navigator.SkipToPrevious();
         Assert.IsFalse(navigator.IsNotFirst());

         navigator.Add(null);
         Assert.IsTrue(navigator.IsNotFirst());
      }

      [TestMethod]
      public void NavigateToNextTracker_IsDiabledWithSingleTracker()
      {
         var navigator = new Navigator(GetMockedTrackerFactory(), null);
         Assert.IsFalse(navigator.NavigateToNextTracker.CanExecute(null));
      }

      [TestMethod]
      public void NavigateToNextTracker_IsDiabledWhenLast()
      {
         var navigator = new Navigator(GetMockedTrackerFactory(), null);
         navigator.Add(null);

         Assert.IsFalse(navigator.NavigateToNextTracker.CanExecute(null));
      }

      [TestMethod]
      public void NavigateToNextTracker_IsEnabledWhenNotLast()
      {
         var navigator = new Navigator(GetMockedTrackerFactory(), null);
         navigator.Add(null);
         navigator.SkipToPrevious();

         Assert.IsTrue(navigator.NavigateToNextTracker.CanExecute(null));
      }

      private static readonly List<ITravelTrackingViewModel> PossibleLocations
         = new List<ITravelTrackingViewModel>
      {
         Generate("Japan", "Tokyo"),
         Generate("France", "Toulouse"),
         Generate("United States", "Las Vegas"),
         Generate("United Kingdom", "London")
      };

      private ITravelTrackerFactory GetMockedTrackerFactory()
      {
         var trackerFactory = Substitute.For<ITravelTrackerFactory>();

         trackerFactory.Generate(Arg.Any<ITheme>()).Returns(
            PossibleLocations.ElementAt(0),
            PossibleLocations.ElementAt(1),
            PossibleLocations.ElementAt(2),
            PossibleLocations.ElementAt(3));

         return trackerFactory;
      }

      private static ITravelTrackingViewModel Generate(string country, string city)
      {
         var locationTarget = Substitute.For<ILocation>();
         locationTarget.Country.Returns(country);
         locationTarget.City.Returns(city);

         var trackerViewModel = Substitute.For<ITravelTrackingViewModel>();
         trackerViewModel.Location.Returns(locationTarget);

         return trackerViewModel;
      }
   }
}
