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

        public async Task<ExtractingResult> TryUnpackAsync(string sourcePath)
        {
            if (String.IsNullOrEmpty(TemporaryPath))
                TemporaryPath = Path.Combine(
                    Path.GetTempPath(),
                    "cpmm\\mods"
                );

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

        public bool Install(IMod modification)
        {
            return false;
        }
    }
}