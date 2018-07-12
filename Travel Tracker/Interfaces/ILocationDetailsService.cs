namespace Travel_Tracker.Interfaces
{
   public interface ILocationDetailsService
   {
      /// <summary>
      /// Attempts to fetch the offset from UTC of the given city.
      /// </summary>
      /// <returns>
      /// Integer offset from UTC of the supplied location
      /// </returns>
      int GetTimezoneOffSet(
         string country,
         string city);

      /// <summary>
      /// Attempts to fetch the path to an icon representing the current weather for the given
      /// city.
      /// </summary>
      /// <returns>
      /// Path to an icon representing the current weather of the supplied location
      /// </returns>
      string GetWeatherIconUrl(
         string country,
         string city);
   }
}
