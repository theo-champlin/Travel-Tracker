using System.ComponentModel;

namespace TravelTracker.Models
{
   public class Location : INotifyPropertyChanged
   {
      public string FullName
      {
         get
         {
            return City + ", " + Country;
         }
      }

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
