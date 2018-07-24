using System.Windows;

namespace TravelTracker.Views
{
   using ViewModels;

   /// <summary>
   /// Interaction logic for LocationError.xaml
   /// </summary>
   public partial class LocationError : Window
   {
      public LocationError(LocationErrorViewModel errorViewModel)
      {
         InitializeComponent();
         DataContext = errorViewModel;

         MouseLeftButtonDown += delegate { DragMove(); };
      }
   }
}
