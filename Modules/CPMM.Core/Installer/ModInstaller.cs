// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using CPMM.Core.Common;
using CPMM.Core.Mods;
using System.IO;

namespace CPMM.Core.Installer
{
    /// <summary>
    /// Provides functionality for installing mods.
    /// </summary>
    public class ModInstaller
    {
        public string TemporaryPath { get; set; } = String.Empty;

        public string GameRootDirectory { get; set; } = String.Empty;

        public async Task<IEnumerable<IMod>> ParseModsAsync(IEnumerable<ExtractingResult> extractedMods) =>
            await Parser.ParseModsAsync(extractedMods, GameRootDirectory);

        public async Task<IMod> ParseModAsync(ExtractingResult extractedMod) =>
            await Parser.ParseModAsync(extractedMod, GameRootDirectory);

        /// <summary>
        /// Clears temporary directory.
        /// </summary>
        public async Task<bool> ClearTemps()
        {
            PrepareTempPath();

            if (!Directory.Exists(TemporaryPath))
                return true;

            return await Task.Run(() =>
            {
                Directory.Delete(TemporaryPath, true);
                return true;
            });
        }

        /// <summary>
        /// Tries to unpack provided archive.
        /// </summary>
        /// <param name="sourcePath"></param>
        public async Task<ExtractingResult> TryUnpackAsync(string sourcePath)
        {
            PrepareTempPath();

            var fileHash = await IOExtensions.ComputeHashAsync(sourcePath);

            return await Archive.ExtractAsync(
                sourcePath,
                Path.Combine(
                    TemporaryPath,
                    fileHash
                ),
                fileHash
            );
        }

        public bool Install(IMod modification)
        {
            return false;
        }

        public async Task<bool> InstallAsync(IMod modification)
        {
            return false;
        }

        private void PrepareTempPath()
        {
            if (String.IsNullOrEmpty(TemporaryPath) || !Directory.Exists(TemporaryPath))
                TemporaryPath = Path.Combine(
                    Path.GetTempPath(),
                    "cpmm\\mods"
                );
        }
    }
}