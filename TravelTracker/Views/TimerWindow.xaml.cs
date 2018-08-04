using System.Windows;

namespace TravelTracker.Views
{
   using ViewModels;

   /// <summary>
   /// Interaction logic for TimerWindow.xaml
   /// </summary>
   public partial class TimerWindow : Window
   {
      public TimerWindow(TravelTrackingContainer windowContext = null)
      {
         InitializeComponent();
         DataContext = windowContext ?? new TravelTrackingContainer();

         MouseLeftButtonDown += delegate { DragMove(); };
      }
   }
}
