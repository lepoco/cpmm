// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using System;
using System.Reflection;
using System.Windows;

namespace CPMM.Code
{
    /// <summary>
    /// A class to separate additional functions from <see cref="Application"/>.
    /// </summary>
    internal class Middleware : IDisposable
    {
        private bool _disposed = false;

        /// <summary>
        /// Application settings.
        /// </summary>
        public readonly Core.Settings.Manager Settings = new();

        /// <summary>
        /// Custom shortcuts.
        /// </summary>
        public readonly Core.Input.Shortcuts Shortcuts = new();

        /// <summary>
        /// Game instance.
        /// </summary>
        public readonly Core.Game.GameInstance GameInstance = new();

        ~Middleware()
        {
            Dispose();
        }

        public bool Initialize()
        {
            SetLanguage(Settings.Options.Language);

            Shortcuts.Initialize(Application.Current.MainWindow!);

            if (!String.IsNullOrEmpty(Settings.Options.GameRootDirectory))
                GameInstance.Fetch(Settings.Options.GameRootDirectory);

            return true;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

#if DEBUG
            System.Diagnostics.Debug.WriteLine(
                $"INFO | {typeof(Middleware)} disposed, Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}",
                "CPMM");
#endif
            Lepo.i18n.Translator.Flush();
            // Dispose middleware resources.
        }

        public void SetLanguage(string language)
        {
            language = language.Trim();

            // Validate
            switch (language)
            {
                default:
                    Lepo.i18n.Translator.SetLanguage(
                        Assembly.GetExecutingAssembly(),
                        "en_US",
                        "CPMM.Assets.Strings.en_US.yml",
                        false
                    );
                    break;
            }
        }
    }
}