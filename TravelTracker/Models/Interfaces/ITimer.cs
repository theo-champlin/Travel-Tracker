using System.ComponentModel;

namespace TravelTracker.Models.Interfaces
{
   /// <summary>
   /// A class managing and making available the current time in a location specified by the user.
   /// A Timer should send a property changed notification when any public property is updated to
   /// support binding.
   /// </summary>
   public interface ITimer : INotifyPropertyChanged
   {
      /// <summary>
      /// A formatted string representing the time in a targeted location.
      /// </summary>
      string FormattedTime
      {
         get;
      }
   }
}
