using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace TravelTracker.Models.Interfaces
{
   /// <summary>
   /// A class representing the current color scheme of the application.
   /// </summary>
   public interface ITheme : INotifyPropertyChanged
   {
      /// <summary>
      /// The suggested color of foreground elements for the current theme.
      /// </summary>
      Brush Foreground
      {
         get;
      }

      /// <summary>
      /// The suggested color of background elements for the current theme.
      /// </summary>
      Brush Background
      {
         get;
      }

      /// <summary>
      /// A command telling the theme to update to a lighter, blue color scheme.
      /// </summary>
      ICommand SetBlueTheme
      {
         get;
      }

      /// <summary>
      /// A command telling the theme to update to a darker color scheme.
      /// </summary>
      ICommand SetDarkTheme
      {
         get;
      }

      /// <summary>
      /// Set a custom color scheme.
      /// </summary>
      /// <param name="foreground"></param>
      /// <param name="background"></param>
      void SetTheme(Color foreground, Color background);
   }
}
