using System.ComponentModel;

namespace CPMM.Code
{
    /// <summary>
    /// Base notifier.
    /// </summary>
    internal class ViewData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void UpdateProperty<T>(ref T property, object value, string propertyName)
        {
            if (property == null || property.Equals(value))
                return;

            property = (T)value;

            OnPropertyChanged(propertyName);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}