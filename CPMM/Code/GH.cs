// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using CPMM.Views;
using System;
using System.Windows;
using System.Windows.Threading;

namespace CPMM.Code
{
    /// <summary>
    /// Static global hook.
    /// </summary>
    internal static class GH
    {
        /// <summary>
        /// Main application version.
        /// </summary>
        public static string Version =>
            System.Reflection.Assembly.GetExecutingAssembly().GetName().Version!.ToString() ?? "1.0.0";

        /// <summary>
        /// Application settings.
        /// </summary>
        public static Core.Settings.Manager Settings => (Application.Current as App)!.Middleware.Settings;

        /// <summary>
        /// Returns the instance of the current <see cref="System.Windows.Application"/>.
        /// </summary>
        public static App App => System.Windows.Application.Current as App ??
                                 throw new NullReferenceException(
                                     "ERROR | The main application instance does not exist.");

        /// <summary>
        /// Changes <see cref="Lepo.i18n.Translator.Current"/> language.
        /// </summary>
        /// <param name="language">Language code.</param>
        public static void SetLanguage(string language) =>
            (Application.Current as App)!.Middleware.SetLanguage(language);

        /// <summary>
        /// Navigates to specific page.
        /// </summary>
        /// <param name="pageTag">Page tag.</param>
        public static void Navigate(string pageTag) =>
            (Application.Current.MainWindow as Container)?.RootNavigation.Navigate(pageTag);

        /// <summary>
        /// Synchronously executes delegated action on UI thread.
        /// </summary>
        /// <param name="callback">An Action delegate to invoke through the dispatcher.</param>
        public static void Dispatch(Action callback) => Application.Current.Dispatcher.Invoke(callback);

        /// <summary>
        /// Asynchronously executes delegated action on UI thread.
        /// </summary>
        /// <param name="callback">An Action delegate to invoke through the dispatcher.</param>
        public static DispatcherOperation DispatchAsync(Action callback) => Application.Current.Dispatcher.InvokeAsync(callback);
    }
}