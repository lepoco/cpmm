// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

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
    }
}
