using System.Windows;

namespace TravelTracker
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
         DataContext = new TravelTrackingViewModel();

         MouseLeftButtonDown += delegate { DragMove(); };
      }
   }
}
