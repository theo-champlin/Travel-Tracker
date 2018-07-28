using System.Windows.Input;

namespace TravelTracker.ViewModels.Interfaces
{
   using Models.Interfaces;

   public interface ITravelTrackingViewModel
   {
      /// <summary>
      /// General information on the location selected by the user to track in this class.
      /// </summary>
      ILocation Location { get; }

      /// <summary>
      /// Representation of the local time in the tracked location.
      /// </summary>
      ITimer Timer { get; }

      /// <summary>
      /// Representation of the local weather in the tracked location.
      /// </summary>
      IWeather Weather { get; }

      /// <summary>
      /// A command that opens a Wikipedia page on the tracked location if one is available.
      /// </summary>
      ICommand NavigateToWiki { get; }
   }
}
