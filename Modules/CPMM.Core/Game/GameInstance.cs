// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using System.Diagnostics;
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
        public string Company { get; internal set; } = String.Empty;

        /// <inheritdoc />
        public string Copyright { get; internal set; } = String.Empty;

        /// <inheritdoc />
        public string Version { get; internal set; } = String.Empty;

        /// <inheritdoc />
        public string ProductVersion { get; internal set; } = String.Empty;

        /// <inheritdoc />
        public string ExecutablePath { get; internal set; } = String.Empty;

        /// <inheritdoc />
        public string BasePath { get; internal set; } = String.Empty;


        public void Fetch(string gameRootDir)
        {
            if (!Directory.Exists(gameRootDir))
                return;

            var executablePath = Path.Combine(
                gameRootDir,
                "bin\\x64\\Cyberpunk2077.exe"
            );

            if (!File.Exists(executablePath))
                return;

            var executableInfo = FileVersionInfo.GetVersionInfo(executablePath);

            Name = executableInfo.ProductName ?? "Cyberpunk 2077";
            Company = executableInfo.CompanyName ?? "CD PROJEKT S.A.";
            Copyright = executableInfo.LegalCopyright ?? "© 2020 CD PROJEKT S.A.";
            Version = executableInfo.FileVersion ?? "0.0.0";
            ProductVersion = executableInfo.ProductVersion ?? "0.0.0";
            BasePath = gameRootDir;
            ExecutablePath = executableInfo.FileName;
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
