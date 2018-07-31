using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace TravelTracker.Models.Implementations
{
   using Commands;
   using Factories.Interfaces;
   using Interfaces;
   using ViewModels.Interfaces;

   public class Navigator : INavigator
   {
      #region Properties

      private ITravelTrackingViewModel _currentTracker;
      public ITravelTrackingViewModel CurrentTracker
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

      public ObservableCollection<ITravelTrackingViewModel> TravelTrackingOptions { get; set; } =
         new ObservableCollection<ITravelTrackingViewModel>();

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
         TravelTrackingOptions.Add(trackerFactory.Generate(currentTheme));
         CurrentTracker = TravelTrackingOptions.Last();
      }

      public bool IsNotLast()
      {
         var indexOfCurrent = TravelTrackingOptions.IndexOf(CurrentTracker);
         return indexOfCurrent < TravelTrackingOptions.Count - 1;
      }

      public void SkipToNext()
      {
         var indexOfCurrent = TravelTrackingOptions.IndexOf(CurrentTracker);

         if (indexOfCurrent == TravelTrackingOptions.Count - 1)
         {
            return;
         }

         CurrentTracker = TravelTrackingOptions.ElementAt(indexOfCurrent + 1);
      }

      public bool IsNotFirst()
      {
         return TravelTrackingOptions.IndexOf(CurrentTracker) > 0;
      }

      public void SkipToPrevious()
      {
         var indexOfCurrent = TravelTrackingOptions.IndexOf(CurrentTracker);

         if (indexOfCurrent == 0)
         {
            return;
         }

         CurrentTracker = TravelTrackingOptions.ElementAt(indexOfCurrent - 1);
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

      ITravelTrackerFactory trackerFactory;

      #endregion
   }
}
