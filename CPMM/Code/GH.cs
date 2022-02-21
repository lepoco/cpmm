// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using CPMM.Views;
using System;
using System.Windows;

namespace CPMM.Code
{
    /// <summary>
    /// Static global hook.
    /// </summary>
    internal static class GH
    {
        public static Core.Settings.Manager Settings => (Application.Current as App)!.Middleware.Settings;

        public static void Navigate(string pageTag) =>
            (Application.Current.MainWindow as Container)?.RootNavigation.Navigate(pageTag);

        public static void Dispatch(Action callback) => Application.Current.Dispatcher.Invoke(callback);
    }
}
