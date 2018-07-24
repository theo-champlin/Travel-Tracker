using System;
using System.Collections.Generic;

namespace TravelTracker.Services.Interfaces
{
   public interface ILocationSetService
   {
      /// <summary>
      /// Populates the "countries" argument with a collection of all countries this application
      /// considers valid.
      /// </summary>
      /// <param name="countries"></param>
      void PopulateCountryCollection(ICollection<string> countries);

      /// <summary>
      /// Populates the "cities" argument with a collection of all cities in the given country this
      /// application considers valid.
      /// </summary>
      /// <param name="country">
      /// The country for which we want to retrieve a collection of contained cities.
      /// </param>
      /// <param name="cities"></param>
      /// <exception cref="ArgumentNullException"></exception>
      void PopulateCityCollection(string country, ICollection<string> cities);

      /// <summary>
      /// Retrieves the Wikipedia page id for the city identified by the given city and country
      /// names. The Wikipedia page id is an identifier used in the service URL to uniquely
      /// identify the page for this location.
      /// </summary>
      /// <param name="country"></param>
      /// <param name="city"></param>
      /// <returns>
      /// A string representing the page id for the given location or an empty string if the page
      /// id can not be found.
      /// </returns>
      string GetWikiPageId(string country, string city);

      /// <summary>
      /// Retrieves the weather code for the city identified by the given city and country
      /// names. The weather code is a string used by weather underground to direct to the local
      /// weather for the given location.
      /// </summary>
      /// <param name="country"></param>
      /// <param name="city"></param>
      /// <returns>
      /// A string representing the weather code for the given location or an empty string if the
      /// weather code can not be found.
      /// </returns>
      string GetWeatherAreaCode(string country, string city);

      /// <summary>
      /// Determines whether the given city-country combination is one of those built into the
      /// program.
      /// </summary>
      /// <param name="country"></param>
      /// <param name="city"></param>
      /// <returns>True if the city is recognized, false otherwise.</returns>
      bool IsRecognizedCity(string country, string city);
   }
}
