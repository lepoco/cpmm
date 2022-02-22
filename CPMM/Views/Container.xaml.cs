// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using System.Windows;

namespace CPMM.Views
{
    /// <summary>
    /// Interaction logic for Container.xaml
    /// </summary>
    public partial class Container : Window
    {
        public Container()
        {
            WPFUI.Background.Manager.Apply(this);

            InitializeComponent();

#if DEBUG
            // Debug hacking window
            new Hacking().Show();
#endif
        }

        private void RootNavigation_OnLoaded(object sender, RoutedEventArgs e)
        {
            RootNavigation.Navigate("dashboard");
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            // TRAY
        }
    }
}
