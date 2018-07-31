using System.Windows;

namespace TravelTracker.Views
{
   using ViewModels;

   /// <summary>
   /// Interaction logic for ExtendedTimers.xaml
   /// </summary>
   public partial class ExtendedTimers : Window
   {
      public ExtendedTimers(TravelTrackingContainer windowContext)
      {
         InitializeComponent();
         DataContext = windowContext;

         MouseLeftButtonDown += delegate { DragMove(); };
      }
   }
}
