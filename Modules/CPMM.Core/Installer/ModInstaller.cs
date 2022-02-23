// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

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

        public ModInstaller()
        {
        }

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

        public async Task<ExtractingResult> TryUnpackAsync(string sourcePath)
        {
            PrepareTempPath();

            var fileHash = await IOExtension.ComputeHashAsync(sourcePath);

            return await Archive.ExtractAsync(
                sourcePath,
                Path.Combine(
                    TemporaryPath,
                    fileHash
                ),
                fileHash
            );
        }

        public async Task<IEnumerable<IMod>> ParseModsAsync(IEnumerable<ExtractingResult> extractedMods)
        {
            var parsedMods = new List<Mod>();

            return parsedMods;
        }

        public bool Install(IMod modification)
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