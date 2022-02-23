// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

namespace CPMM.Core.Mods
{
    /// <summary>
    /// Represents instance of the modification.
    /// </summary>
    [Serializable]
    public class Mod : IMod
    {
        public int Id { get; internal set; } = 0;

        public int Priority { get; internal set; } = 0;

        public bool IsValid { get; internal set; } = false;

        public bool Enabled { get; internal set; } = false;

        public bool Installed { get; internal set; } = false;

        public string Name { get; internal set; } = String.Empty;

        public string Author { get; internal set; } = String.Empty;

        public string Website { get; internal set; } = String.Empty;

        public string ArchiveName { get; internal set; } = String.Empty;

        public string SourcePath { get; internal set; } = String.Empty;

        public string SourceBackupPath { get; internal set; } = String.Empty;

        public string TempPath { get; internal set; } = String.Empty;

        public Version Version { get; internal set; } = new(1, 0, 0);

        public Core.Mods.Location Location { get; internal set; } = Core.Mods.Location.Unknown;

        public Core.Mods.Category Category { get; internal set; } = Core.Mods.Category.Unknown;

        public DateTime DateInstalled { get; internal set; } = DateTime.Now;

        public DateTime DateModified { get; internal set; } = DateTime.Now;

        public IEnumerable<string> Files { get; internal set; } = new string[] { };

        public IEnumerable<string> FilesOverridden { get; internal set; } = new string[] { };
    }
}
