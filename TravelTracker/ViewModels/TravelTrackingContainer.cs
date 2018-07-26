using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace TravelTracker.ViewModels
{
   using Commands;
   using Models.Implementations;
   using Models.Interfaces;

   public class TravelTrackingContainer : INotifyPropertyChanged
   {
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

      private ExitApplication _exitApplication;
      public ICommand ExitApplication
      {
         get
         {
            if (_exitApplication == null)
            {
               _exitApplication = new ExitApplication();
            }

            return _exitApplication;
         }
      }

      public ICommand AddTracker { get; private set; }

      public NavigateToNextTracker NavigateToNextTracker { get; private set; }

      public NavigateToPreviousTracker NavigateToPreviousTracker { get; private set; }

      public ITheme Theme { get; private set; }

      public TravelTrackingContainer()
      {
         InitializeTheme();

         NavigateToNextTracker = new NavigateToNextTracker(this);
         NavigateToPreviousTracker = new NavigateToPreviousTracker(this);

         AddTracker = new AddTracker(this);
         AddTracker.Execute(null);
      }

      public void Add()
      {
         travelTrackingOptions.Add(new TravelTrackingViewModel(Theme));
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

      #region INotifyPropertyChanged
      public event PropertyChangedEventHandler PropertyChanged;

      private void OnPropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
      #endregion

      private IList<TravelTrackingViewModel> travelTrackingOptions =
         new List<TravelTrackingViewModel>();

      private void InitializeTheme()
      {
         Theme = new Theme();
         Theme.SetBlueTheme.Execute(null);
      }
   }
}
