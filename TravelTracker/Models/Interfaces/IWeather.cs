using System.ComponentModel;
using System.Windows.Input;

namespace TravelTracker.Models.Interfaces
{
   /// <summary>
   /// A representation of the weather in a given location. Weather will send a property changed
   /// notification when any public property is updated to support binding.
   /// </summary>
   public interface IWeather : INotifyPropertyChanged
   {
      /// <summary>
      /// A canvas object displaying a portrayal of the weather in the given location.
      /// </summary>
      object Icon
      {
         get;
      }

      /// <summary>
      /// A command that launches a web page with weather information on the given location if the
      /// page id is known.
      /// </summary>
      ICommand NavigateToWeather
      {
         get;
      }
   }
}
