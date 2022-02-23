// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

namespace CPMM.Core.Settings
{
    /// <summary>
    /// Provides a representation of all application options with default values.
    /// </summary>
    [Serializable]
    public class Options
    {
        public bool Initialized { get; set; } = false;

        public bool UseMica { get; set; } = true;

        public bool UseTray { get; set; } = true;

        public bool KeepModsBackup { get; set; } = false;

        public bool UseCustomBackupPath { get; set; } = false;

        public bool ShowHackingIntro { get; set; } = false;

        public bool RunElevated { get; set; } = false;

        public int Theme { get; set; } = 0;

        public string Language { get; set; } = "en_US";

        public string StartArguments { get; set; } = String.Empty;

        public string GameRootDirectory { get; set; } = String.Empty;

        public string GameSavesDirectory { get; set; } = String.Empty;

        public string GameSettingsDirectory { get; set; } = String.Empty;

        public string CustomBackupPath { get; set; } = String.Empty;
    }
}
