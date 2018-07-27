using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace TravelTracker.Models.Implementations
{
   using Commands;
   using Factories.Interfaces;
   using Interfaces;
   using ViewModels;

   public class Navigator : INavigator
   {
      #region Properties

      private TravelTrackingViewModel _currentTracker;
      public TravelTrackingViewModel CurrentTracker
      {
         get
         {
            return _currentTracker;
         }

         private set
         {
            _currentTracker = value;
            OnPropertyChanged("CurrentTracker");
         }
      }

      private AddTracker _addTracker;
      public ICommand AddTracker
      {
         get
         {
            return _addTracker;
         }

         private set
         {
            _addTracker = (AddTracker)value;
            OnPropertyChanged("AddTracker");
         }
      }

      private NavigateToNextTracker _navigateToNextTracker;
      public ICommand NavigateToNextTracker
      {
         get
         {
            return _navigateToNextTracker;
         }

         private set
         {
            _navigateToNextTracker = (NavigateToNextTracker)value;
            OnPropertyChanged("NavigateToNextTracker");
         }
      }

      private NavigateToPreviousTracker _navigateToPreviousTracker;
      public ICommand NavigateToPreviousTracker
      {
         get
         {
            return _navigateToPreviousTracker;
         }

         private set
         {
            _navigateToPreviousTracker = (NavigateToPreviousTracker)value;
            OnPropertyChanged("NavigateToPreviousTracker");
         }
      }

      #endregion

      #region Public

      public Navigator(
         ITravelTrackerFactory trackerFactory,
         ITheme currentTheme)
      {
         this.trackerFactory = trackerFactory;

         NavigateToNextTracker = new NavigateToNextTracker(this);
         NavigateToPreviousTracker = new NavigateToPreviousTracker(this);

         AddTracker = new AddTracker(this);
         AddTracker.Execute(currentTheme);
      }

      public void Add(ITheme currentTheme)
      {
         travelTrackingOptions.Add(trackerFactory.Generate(currentTheme));
         CurrentTracker = travelTrackingOptions.Last();
      }

      public bool IsNotLast()
      {
         var indexOfCurrent = travelTrackingOptions.IndexOf(CurrentTracker);
         return indexOfCurrent < travelTrackingOptions.Count - 1;
      }

      public void SkipToNext()
      {
         var indexOfCurrent = travelTrackingOptions.IndexOf(CurrentTracker);

         if (indexOfCurrent == travelTrackingOptions.Count - 1)
         {
            return;
         }

         CurrentTracker = travelTrackingOptions.ElementAt(indexOfCurrent + 1);
      }

      public bool IsNotFirst()
      {
         return travelTrackingOptions.IndexOf(CurrentTracker) > 0;
      }

      public void SkipToPrevious()
      {
         var indexOfCurrent = travelTrackingOptions.IndexOf(CurrentTracker);

         if (indexOfCurrent == 0)
         {
            return;
         }

         CurrentTracker = travelTrackingOptions.ElementAt(indexOfCurrent - 1);
      }

      #endregion

      #region INotifyPropertyChanged

      public event PropertyChangedEventHandler PropertyChanged;

      private void OnPropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      #endregion

      #region Members

      private IList<TravelTrackingViewModel> travelTrackingOptions =
         new List<TravelTrackingViewModel>();

      ITravelTrackerFactory trackerFactory;

      #endregion
   }
}
