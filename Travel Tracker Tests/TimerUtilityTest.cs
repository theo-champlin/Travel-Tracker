using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Travel_Tracker_Tests
{
   using Travel_Tracker.Interfaces;
   using Travel_Tracker.Services;

   [TestClass]
   public class TimerUtilityTest
   {
      [TestMethod]
      public void GetLocationTime_NoOffset()
      {
         const int TargetLocationOffset = 0;
         const string UTCInitialTime = "2005-01-01T12:00:00";
         GetLocationTime_TestHelper(
            TargetLocationOffset,
            UTCInitialTime,
            UTCInitialTime);
      }

      [TestMethod]
      public void GetLocationTime_DayOverflow()
      {
         const int TargetLocationOffset = 5;
         const string UTCInitialTime = "2005-04-30T20:00:00";
         const string ExpectedFormattedResult = "2005-05-01T01:00:00";
         GetLocationTime_TestHelper(
            TargetLocationOffset,
            UTCInitialTime,
            ExpectedFormattedResult);
      }

      [TestMethod]
      public void GetLocationTime_DayUnderflow()
      {
         const int TargetLocationOffset = -8;
         const string UTCInitialTime = "2005-01-01T06:00:00";
         const string ExpectedFormattedResult = "2004-12-31T22:00:00";
         GetLocationTime_TestHelper(
            TargetLocationOffset,
            UTCInitialTime,
            ExpectedFormattedResult);
      }

      [TestMethod]
      public void GetFormattedLocationTime_NoOffset()
      {
         const int TargetLocationOffset = 0;
         const string UTCInitialTime = "2005-01-01T12:00:00";
         const string ExpectedFormattedResult = "12:00 PM";
         GetFormattedLocationTime_TestHelper(
            TargetLocationOffset,
            UTCInitialTime,
            ExpectedFormattedResult);
      }

      [TestMethod]
      public void GetFormattedLocationTime_DayOverflow()
      {
         const int TargetLocationOffset = 5;
         const string UTCInitialTime = "2005-01-01T20:00:00";
         const string ExpectedFormattedResult = "1:00 AM";
         GetFormattedLocationTime_TestHelper(
            TargetLocationOffset,
            UTCInitialTime,
            ExpectedFormattedResult);
      }

      [TestMethod]
      public void GetFormattedLocationTime_DayUnderflow()
      {
         const int TargetLocationOffset = -8;
         const string UTCInitialTime = "2005-01-01T06:00:00";
         const string ExpectedFormattedResult = "10:00 PM";
         GetFormattedLocationTime_TestHelper(
            TargetLocationOffset,
            UTCInitialTime,
            ExpectedFormattedResult);
      }

      [TestMethod]
      public void GetMinuteUpdateInterval_StandardInput()
      {
         GetMinuteUpdateInterval_TestHelper(
            "2005-01-01T06:00:35.458",
            245420000);
      }

      [TestMethod]
      public void GetMinuteUpdateInterval_MaximumInterval()
      {
         GetMinuteUpdateInterval_TestHelper(
            "2005-01-01T06:00:00",
            TimeSpan.FromMinutes(1).Ticks);
      }

      [TestMethod]
      public void GetMinuteUpdateInterval_MinimalInterval()
      {
         GetMinuteUpdateInterval_TestHelper(
            "2005-01-01T06:00:59.999",
            TimeSpan.FromMilliseconds(1).Ticks);
      }

      private void GetLocationTime_TestHelper(
         int offset,
         string formattedInputTime,
         string expectedFormattedResult)
      {
         ITimerUtility timerInTest = GetInitializedTimer(offset);

         var inputDateTime = DateTime.Parse(formattedInputTime);
         var targetLocationTime = timerInTest.GetLocationTime(inputDateTime);

         var expectedTargetLocationTime = DateTime.Parse(expectedFormattedResult);
         Assert.AreEqual(expectedTargetLocationTime, targetLocationTime);
      }

      private void GetFormattedLocationTime_TestHelper(
         int offset,
         string formattedInputTime,
         string expectedFormattedResult)
      {
         ITimerUtility timerInTest = GetInitializedTimer(offset);

         var inputDateTime = DateTime.Parse(formattedInputTime);
         var formattedTime = timerInTest.GetFormattedLocationTime(inputDateTime);
         Assert.AreEqual(expectedFormattedResult, formattedTime);
      }

      private ITimerUtility GetInitializedTimer(int offset)
      {
         var mockedLocationDetails = Substitute.For<ILocationDetailsService>();
         mockedLocationDetails.GetTimezoneOffSet(
            Arg.Any<string>(),
            Arg.Any<string>())
            .Returns(offset);

         return new TimerUtility(
            mockedLocationDetails,
            string.Empty,
            string.Empty);
      }

      private void GetMinuteUpdateInterval_TestHelper(
         string formattedDatetime,
         long expectedTicksUntilNextMinute)
      {
         ITimerUtility timerInTest = new TimerUtility(
            Substitute.For<ILocationDetailsService>(),
            string.Empty,
            string.Empty);

         var timeUntilNextMinute = timerInTest.GetMinuteUpdateInterval(
            DateTime.Parse(formattedDatetime));
         var ticksUntilNextMinute = timeUntilNextMinute.Ticks;
         Assert.IsTrue(ticksUntilNextMinute >= expectedTicksUntilNextMinute);

         const long acceptableDelta = 5 * TimeSpan.TicksPerMillisecond;
         Assert.IsTrue(ticksUntilNextMinute - expectedTicksUntilNextMinute <= acceptableDelta);
      }
   }
}
