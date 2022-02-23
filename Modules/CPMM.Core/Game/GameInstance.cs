// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using System.IO;

namespace CPMM.Core.Game
{
    /// <summary>
    /// Represents information about the game on the computer.
    /// </summary>
    public class GameInstance : IGame
    {
        /// <inheritdoc />
        public string Name { get; internal set; } = String.Empty;

        /// <inheritdoc />
        public string Version { get; internal set; } = String.Empty;

        /// <inheritdoc />
        public string ProductVersion { get; internal set; } = String.Empty;

        /// <inheritdoc />
        public string ExecutablePath { get; internal set; } = String.Empty;

        /// <inheritdoc />
        public string BasePath { get; internal set; } = String.Empty;

        /// <inheritdoc />
        public string SettingsPath { get; internal set; } = String.Empty;

        /// <inheritdoc />
        public string SavesPath { get; internal set; } = String.Empty;

        public static async Task<IGame> FetchAsync()
        {
            throw new NotImplementedException();
        }

        public static string TryToLocate()
        {
            if (Directory.Exists(Locations.GogDefault))
                return Locations.GogDefault;

            if (Directory.Exists(Locations.GogAlternative))
                return Locations.GogAlternative;

            if (Directory.Exists(Locations.SteamDefault))
                return Locations.SteamDefault;

            if (Directory.Exists(Locations.SteamAlternative))
                return Locations.SteamAlternative;

            if (Directory.Exists(Locations.UserPrimary))
                return Locations.UserPrimary;

            if (Directory.Exists(Locations.UserSecondary))
                return Locations.UserSecondary;

            return String.Empty;
        }
    }
}
