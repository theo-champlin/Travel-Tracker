using System;
using System.Collections.Generic;

namespace TravelTracker.Implementations
{
   using Interfaces;

   class ResourceLookup : IResourceLookup
   {
      public object FindWeatherIcon(
         int weatherCode,
         DateTime localTime)
      {
         var weatherIconNames = weatherCodeLookup[weatherCode];
         if (localTime.TimeOfDay.Ticks < 6 * TimeSpan.TicksPerHour ||
            localTime.TimeOfDay.Ticks > 20 * TimeSpan.TicksPerHour)
         {
            return App.Current.FindResource(weatherIconNames.NightImage);
         }

         return App.Current.FindResource(weatherIconNames.DayImage);
      }

      private class WeatherConditionIcon
      {
         public string DayImage;
         public string NightImage;

         public static WeatherConditionIcon SnowImage
            = new WeatherConditionIcon("snow_image");

         public static WeatherConditionIcon Rain
            = new WeatherConditionIcon("rain_image");

         public static WeatherConditionIcon ThunderStorm
            = new WeatherConditionIcon("thunder_storm_image");

         public static WeatherConditionIcon Cloudy
            = new WeatherConditionIcon(
               "cloudy_image",
               "night_cloudy_image");

         public static WeatherConditionIcon PartlyCloudy
            = new WeatherConditionIcon(
               "partly_cloudy_image",
               "night_cloudy_image");

         public static WeatherConditionIcon Clear
            = new WeatherConditionIcon(
               "sunny_image",
               "night_clear_image");

         private WeatherConditionIcon(string dayImage, string nightImage)
         {
            DayImage = dayImage;
            NightImage = nightImage;
         }

         private WeatherConditionIcon(string image)
         {
            DayImage = image;
            NightImage = image;
         }
      }

      private static IReadOnlyDictionary<int, WeatherConditionIcon> weatherCodeLookup =
         new Dictionary<int, WeatherConditionIcon>()
         {
            { 395, WeatherConditionIcon.SnowImage },
            { 392, WeatherConditionIcon.SnowImage },
            { 389, WeatherConditionIcon.ThunderStorm },
            { 386, WeatherConditionIcon.SnowImage },
            { 377, WeatherConditionIcon.SnowImage },
            { 374, WeatherConditionIcon.SnowImage },
            { 371, WeatherConditionIcon.SnowImage },
            { 368, WeatherConditionIcon.SnowImage },
            { 365, WeatherConditionIcon.SnowImage },
            { 362, WeatherConditionIcon.SnowImage },
            { 359, WeatherConditionIcon.Rain },
            { 356, WeatherConditionIcon.Rain },
            { 353, WeatherConditionIcon.Rain },
            { 350, WeatherConditionIcon.SnowImage },
            { 338, WeatherConditionIcon.SnowImage },
            { 335, WeatherConditionIcon.SnowImage },
            { 332, WeatherConditionIcon.SnowImage },
            { 329, WeatherConditionIcon.SnowImage },
            { 326, WeatherConditionIcon.SnowImage },
            { 323, WeatherConditionIcon.SnowImage },
            { 320, WeatherConditionIcon.SnowImage },
            { 317, WeatherConditionIcon.SnowImage },
            { 314, WeatherConditionIcon.SnowImage },
            { 311, WeatherConditionIcon.Rain },
            { 308, WeatherConditionIcon.Rain },
            { 305, WeatherConditionIcon.Rain },
            { 302, WeatherConditionIcon.Rain },
            { 299, WeatherConditionIcon.Rain },
            { 296, WeatherConditionIcon.Rain },
            { 293, WeatherConditionIcon.Rain },
            { 284, WeatherConditionIcon.Rain },
            { 281, WeatherConditionIcon.Rain },
            { 266, WeatherConditionIcon.Rain },
            { 363, WeatherConditionIcon.Rain },
            { 260, WeatherConditionIcon.Cloudy },
            { 248, WeatherConditionIcon.Cloudy },
            { 230, WeatherConditionIcon.SnowImage },
            { 227, WeatherConditionIcon.SnowImage },
            { 200, WeatherConditionIcon.ThunderStorm },
            { 185, WeatherConditionIcon.Rain },
            { 182, WeatherConditionIcon.SnowImage },
            { 179, WeatherConditionIcon.SnowImage },
            { 176, WeatherConditionIcon.Rain },
            { 143, WeatherConditionIcon.Cloudy },
            { 122, WeatherConditionIcon.Cloudy },
            { 119, WeatherConditionIcon.Cloudy },
            { 116, WeatherConditionIcon.PartlyCloudy },
            { 113, WeatherConditionIcon.Clear }
         };
   }
}
