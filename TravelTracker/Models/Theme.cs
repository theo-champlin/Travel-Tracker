using System.ComponentModel;
using System.Windows.Media;

namespace TravelTracker.Models
{
   public class Theme : INotifyPropertyChanged
   {
      private Brush _foreground;
      public Brush Foreground
      {
         get
         {
            return _foreground;
         }

         private set
         {
            _foreground = value;
            OnPropertyChanged("Foreground");
         }
      }

      private Brush _background;
      public Brush Background
      {
         get
         {
            return _background;
         }

         private set
         {
            _background = value;
            OnPropertyChanged("Background");
         }
      }

      public Theme(Color foreground, Color background)
      {
         SetTheme(foreground, background);
      }

      public void SetTheme(Color foreground, Color background)
      {
         Foreground = new SolidColorBrush(foreground);
         Background = new SolidColorBrush(background) { Opacity = 0.75 };
      }

      #region INotifyPropertyChanged
      public event PropertyChangedEventHandler PropertyChanged;

      private void OnPropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
      #endregion
   }
}
