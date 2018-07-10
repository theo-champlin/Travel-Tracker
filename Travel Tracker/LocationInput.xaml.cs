using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Travel_Tracker
{
   public partial class LocationInput : Window
   {
      public static string Country = String.Empty;
      public static string City = String.Empty;

      public ObservableCollection<string> TypeaheadCountryList { get; set; }
      public ObservableCollection<string> TypeaheadCityList { get; set; }

      public string CountrySelectionText { get; set; }
      public string CitySelectionText { get; set; }

      private struct CityInfo
      {
         [JsonProperty("country")]
         public string Country;

         [JsonProperty("name")]
         public string Name;
      }

      private List<CityInfo> AllCities;

      public LocationInput()
      {
         InitializeComponent();

         using (var cityListfile = File.OpenText(@"../../CityLookup.json"))
         using (var jsonTextReader = new JsonTextReader(cityListfile))
         {
            AllCities = new JsonSerializer().Deserialize<IEnumerable<CityInfo>>(jsonTextReader).ToList();
         }
         TypeaheadCountryList = new ObservableCollection<string>(AllCities.Select(city => city.Country).Distinct());
         TypeaheadCityList = new ObservableCollection<string> { };

         CountrySelectionText = String.Empty;
         CitySelectionText = String.Empty;
      }

      private void OnCountrySet(object sender, RoutedEventArgs e)
      {
         Country = CountrySelectionText;
         AllCities.Where(city => city.Country == Country).ToList().ForEach(city => TypeaheadCityList.Add(city.Name));
      }

      private void SetLocationClick(object sender, RoutedEventArgs e)
      {
         Country = CountrySelectionText;
         City = CitySelectionText;
         Close();
      }
   }
}
