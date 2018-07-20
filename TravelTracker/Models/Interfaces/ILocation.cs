using System.ComponentModel;

namespace TravelTracker.Models.Interfaces
{
   /// <summary>
   /// A manager and provider the locally maintained location information of the place for which
   /// the user has targeted. Location will send a property changed notification when any public
   /// property is updated to support binding.
   /// </summary>
   public interface ILocation : INotifyPropertyChanged
   {
      /// <summary>
      /// A formatted string representation of the target location. The name will be of the style
      /// "{City}, {Country}" eg. "Paris, France".
      /// </summary>
      string FullName
      {
         get;
      }

      /// <summary>
      /// The name of the current country the user has selected.
      /// </summary>
      string Country
      {
         get;
      }

      /// <summary>
      /// The name of the current city the user has selected.
      /// </summary>
      string City
      {
         get;
      }

      /// <summary>
      /// The page id that Wikipedia uses to uniquely identify the page corresponding to the
      /// location chosen by the user, if we have one on record.
      /// </summary>
      string WikiPageId
      {
         get;
      }

      /// <summary>
      /// The area id that Weather Underground uses to uniquely identify the page corresponding to
      /// the location chosen by the user, if we have one on record.
      /// </summary>
      string WeatherAreaCode
      {
         get;
      }
   }
}
