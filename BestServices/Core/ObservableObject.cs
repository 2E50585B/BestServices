using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BestServices.Core
{
    /// <summary>
    /// Общий класс, реализующий оповещения при изменении свойст
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод оповещения об изменении свойства <paramref name="propertyName"/>
        /// </summary>
        /// <param name="propertyName">Название изменённого свойства</param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}