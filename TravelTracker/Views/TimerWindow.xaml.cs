using System.Windows;

namespace TravelTracker.Views
{
   using ViewModels;

   /// <summary>
   /// Interaction logic for TimerWindow.xaml
   /// </summary>
   public partial class TimerWindow : Window
   {
      public TimerWindow()
      {
         InitializeComponent();
         DataContext = new TravelTrackingContainer();

         MouseLeftButtonDown += delegate { DragMove(); };
      }
   }
}
