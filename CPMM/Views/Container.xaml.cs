// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using CPMM.Code;
using System.Threading.Tasks;
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

            // Debug hacking window
            // Show on first run, OR skip if shift key pressed
            //Hide();

            //new Hacking
            //{
            //    Config = new Hacking.Configuration
            //    {
            //        LockUserControl = true,
            //        ShowFullIntro = true,
            //        TopMost = true
            //    },
            //    WindowStartupLocation = WindowStartupLocation.CenterScreen,
            //    OnFinished = OnHackingFinished
            //}.Show();
        }

        private async Task OnHackingFinished(object sender)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"INFO | {typeof(Hacking)} finished, Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}", "CPMM");
#endif
            await GH.DispatchAsync(() =>
            {
                Show();
                Activate();
                Topmost = true;
                Topmost = false;

                Focus();

            });
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
