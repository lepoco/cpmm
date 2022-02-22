// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using SevenZip;

namespace CPMM.Core.Installer
{
    /// <summary>
    /// Represents the result of the unpacking operation.
    /// </summary>
    internal struct ExtractingResult
    {
        /// <summary>
        /// Represents the status of the unpacking operation.
        /// </summary>
        public enum ExtractingStatus
        {
            Unknown,
            Success,
            Failure,
            Unsupported,
            PasswordProtected
        }

        public ExtractingStatus Status = ExtractingStatus.Unknown;

        public string InPath = String.Empty;

        public string OutPath = String.Empty;
    }

    /// <summary>
    /// Provides functions for unpacking the archives with the help of LZMA and 7Z.
    /// </summary>
    internal static class Archive
    {
        public static async Task<ExtractingResult> ExtractAsync(string input, string output)
        {
            using var extractor = new SevenZipExtractor(input);

            if (!extractor.Check())
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine($"WARNING | {input} is password protected (or another error has occurred)., Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}", "CPMM.Core");
#endif
                return new ExtractingResult
                {
                    InPath = input,
                    OutPath = output,
                    Status = ExtractingResult.ExtractingStatus.PasswordProtected
                };
            }

            if (
                !(extractor.Format == InArchiveFormat.Tar ||
                  extractor.Format == InArchiveFormat.Rar ||
                  extractor.Format == InArchiveFormat.Zip ||
                  extractor.Format == InArchiveFormat.SevenZip)
            )
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine($"WARNING | {input} archive is unsupported., Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}", "CPMM.Core");
#endif
                return new ExtractingResult
                {
                    InPath = input,
                    OutPath = output,
                    Status = ExtractingResult.ExtractingStatus.Unsupported
                };
            }

            await extractor.ExtractArchiveAsync(output);

            return new ExtractingResult
            {
                InPath = input,
                OutPath = output,
                Status = ExtractingResult.ExtractingStatus.Success
            };
        }
    }
}
