// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using CPMM.Core.Common;
using CPMM.Core.Mods;
using System.IO;
using System.Text.RegularExpressions;

namespace CPMM.Core.Installer
{
    /// <summary>
    /// Analyzes mod structure.
    /// </summary>
    internal static class Parser
    {
        public static async Task<IEnumerable<IMod>> ParseModsAsync(IEnumerable<ExtractingResult> extractedMods,
            string gamePath)
        {
            var parsedMods = new List<IMod>();

            foreach (var singleResult in extractedMods)
                parsedMods.Add(await ParseModAsync(singleResult, gamePath));

            return parsedMods;
        }

        public static async Task<IMod> ParseModAsync(ExtractingResult extractedMod, string gamePath)
        {
            var fileName = Path.GetFileName(extractedMod.InPath);
            var fileList = await IOExtensions.GetAllFilesAsync(extractedMod.OutPath) as string[] ?? new string[] { };

            return new Mod
            {
                Name = GuessName(Path.GetFileNameWithoutExtension(extractedMod.InPath)),
                ArchiveName = fileName,
                Category = GuessCategory(Path.GetFileNameWithoutExtension(extractedMod.InPath)),
                Location = GuessLocation(fileList),
                Version = GuessVersion(fileName),
                SourcePath = extractedMod.InPath,
                Files = fileList,
                FilesOverridden = FindExistingFiles(extractedMod.OutPath, gamePath, fileList)
            };
        }

        private static IEnumerable<string> FindExistingFiles(string tempPath, string gamePath,
            IEnumerable<string> modFiles)
        {
            var existingFiles = new List<string> { };
            var relativePaths = new List<string> { };

            if (String.IsNullOrEmpty(gamePath) || !Directory.Exists(gamePath))
                return existingFiles.ToArray();

            foreach (var singleFile in modFiles)
                relativePaths.Add(singleFile.Replace(tempPath + "\\", ""));

            foreach (var singleFile in relativePaths)
            {
                var localFile = Path.Combine(
                    gamePath,
                    singleFile
                );

                if (File.Exists(localFile))
                    existingFiles.Add(singleFile);
            }

            return existingFiles.ToArray();
        }

        private static string GuessName(string archiveName)
        {
            Regex nameRegex = new Regex(@"[\d-+v_. ]+$");
            var name = nameRegex.Replace(archiveName, "");

            return name
                .Replace("_", "")
                .Replace("  ", " ")
                .Trim()
                .FirstCharToUpper();
        }

        private static Version GuessVersion(string archiveName)
        {
            //https://regex101.com/
            //@"\d+(\.\d+)+"
            //@"^(\*|\d+(\.\d+){0,2}(\.\*)?)$"
            //@"^(\d+\.)?(\d+\.)?(\*|\d+)$"
            //@"(\d+(\.\d+)+)|(\d+(\d))"
            Regex versionRegex = new Regex(@"(\d+(\.\d+)+)|(\d+(\d))", RegexOptions.Compiled);

            var rawVersion = versionRegex.Match(archiveName).Value;

            if (String.IsNullOrEmpty(rawVersion))
                return new Version(1, 0, 0);

            // Not valid
            //return Version.Parse(rawVersion);
            return new Version(1, 0, 0);
        }

        private static Mods.Location GuessLocation(IEnumerable<string> modFiles)
        {
            // TODO: Mod location
            return Mods.Location.Bin;
        }

        private static Mods.Category GuessCategory(string archiveName)
        {
            //It more or less suggests a good category
            string[] tweaksKeywords =
            {
                "fix", "patch", "improved", "tweak", "skip", "rework", "quest", "transla", " no ", "level", "swap",
                "zoom", "lore", "realist", "immersi", "balanc", "disable", "vehicle", "handling", "preset", "engine",
                "r6", "settings"
            };
            string[] graphicsKeywords =
            {
                "hd", "darker", "shadow", "graphic", "ultra", "qualit", "hair", "textur", "animati", "light", "eye",
                "preset", "effect", "fx", "design", "color", "vgx", "draw", "reshade", "bladerunner"
            };
            string[] utilitiesKeywords =
            {
                "utils", "utili", "imports", "manager", "registry", "strings", "utilit", "menu", "npc", "music",
                "camera", "rand", "boot", "toggle", "dynamic", "slot", "auto", "debug", "sort", "confi", "tool", "dlc",
                "releas"
            };
            string[] cheatsKeywords =
            {
                "naked", "genita", "cheat", "nude", "nudit", "exp", "ass", "breast", "tits", "loot", "increas", "fall",
                "money", "restrict", "boost"
            };

            if (graphicsKeywords.Any(archiveName.Contains))
                return Mods.Category.Graphics;

            if (tweaksKeywords.Any(archiveName.Contains))
                return Mods.Category.Tweaks;

            if (utilitiesKeywords.Any(archiveName.Contains))
                return Mods.Category.Utilities;

            if (cheatsKeywords.Any(archiveName.Contains))
                return Mods.Category.Cheats;

            return Mods.Category.Other;
        }
    }
}