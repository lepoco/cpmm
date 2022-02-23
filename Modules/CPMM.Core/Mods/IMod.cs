// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

namespace CPMM.Core.Mods
{
    /// <summary>
    /// Represents instance of the modification.
    /// </summary>
    public interface IMod
    {
        public int Id { get; }

        public int Priority { get; }

        public bool IsValid { get; }

        public bool Enabled { get; }

        public bool Installed { get; }

        public string Name { get; }

        public string Author { get; }

        public string Website { get; }

        public string ArchiveName { get; }

        public string SourcePath { get; }

        public string SourceBackupPath { get; }

        public string TempPath { get; }

        public Version Version { get; }

        public Core.Mods.Location Location { get; }

        public Core.Mods.Category Category { get; }

        public DateTime DateInstalled { get; }

        public DateTime DateModified { get; }

        public IEnumerable<string> Files { get; }

        public IEnumerable<string> FilesOverridden { get; }
    }
}
