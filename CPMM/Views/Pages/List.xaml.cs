// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using CPMM.Code;
using System.Threading.Tasks;
using WPFUI.Controls.Interfaces;

namespace CPMM.Views.Pages
{
    /// <summary>
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class List : INavigable
    {
        public List()
        {
            InitializeComponent();
        }

        public void OnNavigationRequest(INavigation sender, object current)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(
                $"INFO | {typeof(List)} navigated, Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}",
                "CPMM");
#endif
            // If list null
            Task.Run(async () =>
            {
                await Task.Delay(50);

                GH.Dispatch(() =>
                {
                    GH.Navigate("install");
                });
            });
        }
    }
}
