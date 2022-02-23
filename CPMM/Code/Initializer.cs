// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using CPMM.Core.Game;
using CPMM.Views;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CPMM.Code
{
    /// <summary>
    /// Initializes some parts of the manager.
    /// </summary>
    internal static class Initializer
    {
        public static void Run()
        {
            AutoLocateGameDir();
            AutoLocateGameAdditionalDirs();
            CheckSupportedLanguages();

            MarkAsInitialized();


            // Show initial hacking console if user is not holding one of the shift keys
#if RELEASE
            if (!(System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift) || System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightShift)))
                ShowHackingConsole();
#endif
        }

        private static void ShowHackingConsole()
        {
            if (Application.Current.MainWindow == null)
                return;

            Application.Current.MainWindow.Hide();

            new Hacking
            {
                Config = new Hacking.Configuration
                {
                    LockUserControl = true,
                    ShowFullIntro = true,
                    TopMost = true
                },
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                OnFinished = OnHackingFinished
            }.Show();
        }

        private static void MarkAsInitialized()
        {
            GH.Settings.Options.Initialized = true;
            GH.Settings.Save();
        }

        private static void CheckSupportedLanguages()
        {
            // CultureInfo.CurrentUICulture.Name <- system language, not necessarily the user's favorite language
            // Windows.System.UserProfile.GlobalizationPreferences.Languages <- The first language in this list should be the user's favorite language

            var detectedLanguage = String.Empty;

            if (Environment.OSVersion.Version.Build >= 10240)
            {
                try
                {
                    var userLanguages = Windows.System.UserProfile.GlobalizationPreferences.Languages;

                    if (userLanguages?.Count > 0)
                    {
                        detectedLanguage = userLanguages.First().ToLower().Trim();
                    }
                }
                catch (Exception e)
                {
                    try
                    {
                        detectedLanguage = CultureInfo.CurrentUICulture.Name.ToLower().Trim();
                    }
                    catch (Exception ex)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine($"INFO | {typeof(Initializer)} could not detect system language | {ex}, Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}", "CPMM");
#endif
                    }
#if DEBUG
                    System.Diagnostics.Debug.WriteLine($"INFO | {typeof(Initializer)} could not detect system language | {e}, Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}", "CPMM");
#endif
                }
            }
            else
            {
                try
                {
                    detectedLanguage = CultureInfo.CurrentUICulture.Name.ToLower().Trim();
                }
                catch (Exception e)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine($"INFO | {typeof(Initializer)} could not detect system language | {e}, Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}", "CPMM");
#endif
                }
            }

            if (String.IsNullOrEmpty(detectedLanguage) || detectedLanguage.Length < 2)
                return;

            detectedLanguage = detectedLanguage.Substring(0, 2);

            // TODO: Use Languages class here

            switch (detectedLanguage)
            {
                case "pl":
                    GH.Settings.Options.Language = "pl_PL";
                    break;

                case "ru":
                    GH.Settings.Options.Language = "ru_RU";
                    break;

                case "cs":
                    GH.Settings.Options.Language = "cs_CZ";
                    break;

                case "de":
                    GH.Settings.Options.Language = "de_DE";
                    break;

                case "es":
                    GH.Settings.Options.Language = "es_ES";
                    break;

                case "fr":
                    GH.Settings.Options.Language = "fr_FR";
                    break;

                case "hu":
                    GH.Settings.Options.Language = "hu_HU";
                    break;

                case "it":
                    GH.Settings.Options.Language = "it_IT";
                    break;

                case "tr":
                    GH.Settings.Options.Language = "tr_TR";
                    break;

                case "ja":
                case "jp":
                    GH.Settings.Options.Language = "ja_JP";
                    break;

                case "pt":
                case "br":
                    GH.Settings.Options.Language = "pt_BR";
                    break;

                case "zh":
                case "cn":
                    GH.Settings.Options.Language = "zh_CN";
                    break;
            }
        }

        private static void AutoLocateGameDir()
        {
            var gameLocation = GameInstance.TryToLocate();

            if (String.IsNullOrEmpty(gameLocation))
                return;

            GH.Settings.Options.GameRootDirectory = gameLocation;
        }

        private static void AutoLocateGameAdditionalDirs()
        {
            var userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\";

            var settingsPath = Path.Combine(
                userPath,
                Core.Game.Locations.SettingsSuffix
            );

            if (Directory.Exists(settingsPath))
                GH.Settings.Options.GameSettingsDirectory = settingsPath;

            System.Diagnostics.Debug.WriteLine("INFO | User " + userPath);
            System.Diagnostics.Debug.WriteLine("INFO | " + settingsPath);

            var savesPath = Path.Combine(
                userPath,
                Core.Game.Locations.SavesSuffix
            );

            if (Directory.Exists(savesPath))
                GH.Settings.Options.GameSavesDirectory = savesPath;

            System.Diagnostics.Debug.WriteLine("INFO | " + savesPath);
        }

        private static async Task OnHackingFinished(object sender)
        {
            if (Application.Current.MainWindow == null)
                return;

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"INFO | {typeof(Hacking)} finished, Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}", "CPMM");
#endif
            await GH.DispatchAsync(() =>
            {
                if (Application.Current.MainWindow == null)
                    return;

                Application.Current.MainWindow.Show();
                Application.Current.MainWindow.Activate();

                Application.Current.MainWindow.Topmost = true;
                Application.Current.MainWindow.Topmost = false;

                Application.Current.MainWindow.Focus();
            });
        }
    }
}
