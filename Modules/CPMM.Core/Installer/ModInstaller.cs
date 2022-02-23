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
            var parsedMods = new List<IMod>();

            foreach (var singleResult in extractedMods)
                parsedMods.Add(await ParseModAsync(singleResult));

            return parsedMods;
        }

        public async Task<IMod> ParseModAsync(ExtractingResult extractedMod)
        {
            var parsedMod = new Mod();
            parsedMod.Name = Path.GetFileNameWithoutExtension(extractedMod.InPath);
            parsedMod.ArchiveName = Path.GetFileName(extractedMod.InPath);
            parsedMod.SourcePath = extractedMod.InPath;


            return parsedMod;
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