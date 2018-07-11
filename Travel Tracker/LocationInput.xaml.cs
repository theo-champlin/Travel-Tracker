using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Travel_Tracker
{
   using Services;

   public partial class LocationInput : Window
   {
      public static string Country = String.Empty;
      public static string City = String.Empty;

      public ObservableCollection<string> TypeaheadCountryList { get; set; }
         = new ObservableCollection<string> { };

      public ObservableCollection<string> TypeaheadCityList { get; set; }
         = new ObservableCollection<string> { };

      public string CountrySelectionText { get; set; }
         = String.Empty;
      public string CitySelectionText { get; set; }
         = String.Empty;

      public LocationInput()
      {
         InitializeComponent();
         LocationSetService.Instance.PopulateCountryCollection(TypeaheadCountryList);
      }

      private void OnCountrySet(object sender, RoutedEventArgs e)
      {
         Country = CountrySelectionText;
         LocationSetService.Instance.PopulateCityCollection(Country, TypeaheadCityList);
      }

      private void SetLocationClick(object sender, RoutedEventArgs e)
      {
         City = CitySelectionText;
         Close();
      }
   }
}
