using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace TravelTracker.Models.Interfaces
{
   using ViewModels.Interfaces;

   public interface INavigator : INotifyPropertyChanged
   {
      /// <summary>
      /// The current location set to be tracked.
      /// </summary>
      ITravelTrackingViewModel CurrentTracker
      {
         get;
      }

      ObservableCollection<ITravelTrackingViewModel> TravelTrackingOptions { get; }

      /// <summary>
      /// Gives the user an opportunity to track a new location. Once the location is set, the
      /// recently added tracker is set to be active.
      /// </summary>
      ICommand AddTracker
      {
         get;
      }

      /// <summary>
      /// Updates the currently active tracker to point to the next tracker in the set. This
      /// command is only available if the current tracker is not the last tracker in the set.
      /// </summary>
      ICommand NavigateToNextTracker
      {
         get;
      }

      /// <summary>
      /// Updates the currently active tracker to point to the previous tracker in the set. This
      /// command is only available if the current tracker is not the first tracker in the set.
      /// </summary>
      ICommand NavigateToPreviousTracker
      {
         get;
      }

      /// <summary>
      /// Adds an additional location tracker to the set of available trackers and sets the new
      /// tracker to be the currently tracked location.
      /// </summary>
      /// <param name="currentTheme"></param>
      void Add(ITheme currentTheme);

      /// <summary>
      /// Indicates whether the currently active tracker is the most recently added tracked
      /// location.
      /// </summary>
      /// <returns>
      /// True if the active tracker is any but the last added tracked location.
      /// </returns>
      bool IsNotLast();

      /// <summary>
      /// Updates the currently active tracker to point to the next tracker in the set or does
      /// nothing if the currently active tracker is the most recent tracker added.
      /// </summary>
      void SkipToNext();

      /// <summary>
      /// Indicates whether the currently active tracker is the initial tracked location.
      /// </summary>
      /// <returns>
      /// True if the active tracker is any but the first added tracked location.
      /// </returns>
      bool IsNotFirst();

      /// <summary>
      /// Updates the currently active tracker to point to the previous tracker in the set or does
      /// nothing if the currently active tracker is the first tracker added.
      /// </summary>
      void SkipToPrevious();
   }
}
