using System.Windows;

namespace TravelTracker.Views
{
   using ViewModels;

   public partial class LocationInput : Window
   {
      public LocationInput(LocationInputViewModel attachedViewModel)
      {
         InitializeComponent();
         DataContext = attachedViewModel;

         MouseLeftButtonDown += delegate { DragMove(); };
      }
   }
}
