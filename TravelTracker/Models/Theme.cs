using System.ComponentModel;
using System.Windows.Media;

namespace TravelTracker.Models
{
   using Commands;

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

      private UpdateTheme _setBlueTheme;
      public UpdateTheme SetBlueTheme
      {
         get
         {
            return _setBlueTheme;
         }

         private set
         {
            _setBlueTheme = value;
            OnPropertyChanged("SetBlueTheme");
         }
      }

      private UpdateTheme _setDarkTheme;
      public UpdateTheme SetDarkTheme
      {
         get
         {
            return _setDarkTheme;
         }

         private set
         {
            _setDarkTheme = value;
            OnPropertyChanged("SetDarkTheme");
         }
      }

      public Theme()
      {
         SetBlueTheme = new UpdateTheme(
            this,
            Colors.DeepSkyBlue,
            Colors.White);

         SetDarkTheme = new UpdateTheme(
            this,
            Colors.Black,
            Colors.Gray);
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
