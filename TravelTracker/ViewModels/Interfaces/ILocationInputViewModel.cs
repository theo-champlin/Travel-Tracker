using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TravelTracker.ViewModels.Interfaces
{
   using Models.Interfaces;

   public interface ILocationInputViewModel
   {
      /// <summary>
      /// Representation of the country and city given by the user.
      /// </summary>
      ILocationInputFields LocationInputFields { get; set; }

      /// <summary>
      /// The page id that Wikipedia uses to uniquely identify the page corresponding to the
      /// location chosen by the user, if we have one on record.
      /// </summary>
      string WikiPageId { get; }

      /// <summary>
      /// The area id that Weather Underground uses to uniquely identify the page corresponding to
      /// the location chosen by the user, if we have one on record.
      /// </summary>
      string WeatherAreaCode { get; }

      /// <summary>
      /// A command that closes the passed in argument, if that argument is a window.
      /// </summary>
      ICommand CloseWindow { get; }

      /// <summary>
      /// A representation the current color scheme of the application.
      /// </summary>
      ITheme Theme { get; }

      /// <summary>
      /// A collection of all countries this considered as valid input.
      /// </summary>
      ObservableCollection<string> TypeaheadCountryList { get; set; }

      /// <summary>
      /// A collection of all cities this considered as valid input for the currently set country.
      /// </summary>
      ObservableCollection<string> TypeaheadCityList { get; set; }

      /// <summary>
      /// Checks whether the city and country stored in the location input are recognized.
      /// </summary>
      /// <returns>True if the stored city and country are </returns>
      bool IsValidCitySet();

      /// <summary>
      /// Resets the city and country input fields.
      /// </summary>
      void ClearInput();

      /// <summary>
      /// A callback function for when the country input field has been set that initializes the
      /// collection of valid cities.
      /// </summary>
      void OnCountrySet();
   }
}
