using System.Collections.Generic;

namespace Travel_Tracker.Interfaces
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
      void PopulateCityCollection(string country, ICollection<string> cities);
   }
}
