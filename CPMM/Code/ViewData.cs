// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using System.ComponentModel;

namespace CPMM.Code
{
    /// <summary>
    /// Base notifier.
    /// </summary>
    internal class ViewData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Updates property by reference if value changed.
        /// </summary>
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