// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

namespace CPMM.Core.Installer
{
    /// <summary>
    /// Represents the result of the unpacking operation.
    /// </summary>
    public struct ExtractingResult
    {
        /// <summary>
        /// Represents the status of the unpacking operation.
        /// </summary>
        public enum ExtractingStatus
        {
            Unknown,
            Success,
            Failure,
            FileNotExist,
            Unsupported,
            PasswordProtected
        }

        public ExtractingStatus Status = ExtractingStatus.Unknown;

        public string SourceHash = String.Empty;

        public string InPath = String.Empty;

        public string OutPath = String.Empty;

        public ExtractingResult()
        {
        }
    }
}
