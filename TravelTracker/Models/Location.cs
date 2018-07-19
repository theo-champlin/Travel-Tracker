using System.ComponentModel;

namespace TravelTracker.Models
{
   /// <summary>
   /// A class managing and providing the locally maintained location information of the place
   /// for which the user has targeted. Location will send a property changed notification when any
   /// public property is updated to support binding.
   /// </summary>
   public class Location : INotifyPropertyChanged
   {
      /// <summary>
      /// A formatted string representation of the target location. The name will be of the style
      /// "{City}, {Country}" eg. "Paris, France".
      /// </summary>
      public string FullName
      {
         get
         {
            return City + ", " + Country;
         }
      }

      /// <summary>
      /// The name of the current country the user has selected.
      /// </summary>
      private string _country;
      public string Country
      {
         get
         {
            return _country;
         }

         private set
         {
            _country = value;
            OnPropertyChanged("Country");
            OnPropertyChanged("FullName");
         }
      }

      /// <summary>
      /// The name of the current city the user has selected.
      /// </summary>
      private string _city;
      public string City
      {
         get
         {
            return _city;
         }

         private set
         {
            _city = value;
            OnPropertyChanged("City");
            OnPropertyChanged("FullName");
         }
      }

      /// <summary>
      /// The page id that Wikipedia uses to uniquely identify the page corresponding to the
      /// location chosen by the user, if we have one on record.
      /// </summary>
      private string _wikiPageId;
      public string WikiPageId
      {
         get
         {
            return _wikiPageId;
         }

         private set
         {
            _wikiPageId = value;
            OnPropertyChanged("WikiPageId");
         }
      }

      /// <summary>
      /// The area id that Weather Underground uses to uniquely identify the page corresponding to
      /// the location chosen by the user, if we have one on record.
      /// </summary>
      private string _weatherAreaCode;
      public string WeatherAreaCode
      {
         get
         {
            return _weatherAreaCode;
         }

         private set
         {
            _weatherAreaCode = value;
            OnPropertyChanged("WeatherAreaCode");
         }
      }

      public Location()
      {
#if DEBUG
         LocationInput locationWindow = new LocationInput
         {
            City = "Paris",
            Country = "France"
         };
#else
         LocationInput locationWindow = new LocationInput();
         locationWindow.ShowDialog();
#endif

         Country = locationWindow.Country;
         City = locationWindow.City;

         WikiPageId = locationWindow.WikiPageId;
         WeatherAreaCode = locationWindow.WeatherAreaCode;
      }

      #region INotifyPropertyChanged
      public event PropertyChangedEventHandler PropertyChanged;

      private void OnPropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
      #endregion
   }
}
