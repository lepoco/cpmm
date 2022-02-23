// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using System.IO;
using System.Security.Cryptography;

namespace CPMM.Core.Common
{
    /// <summary>
    /// Facilitates directory and file management.
    /// </summary>
    internal static class IOExtensions
    {
        public static bool CreateOpenDirectory(string path)
        {
            if (Directory.Exists(path))
                return false;

            Directory.CreateDirectory(path);

            var directoryInfo = new DirectoryInfo(path);
            directoryInfo.Attributes &= ~FileAttributes.ReadOnly;

            var info = directoryInfo.GetFileSystemInfos("*", SearchOption.AllDirectories);

            foreach (var t in info)
                t.Attributes = FileAttributes.Normal;

            return true;
        }

        public static async Task<bool> CreateOpenDirectoryAsync(string path)
        {
            return await Task.Run(() => CreateOpenDirectory(path));
        }

        public static IEnumerable<string> GetAllFiles(string path)
        {
            if (!Directory.Exists(path))
                return new string[] { };

            var files = Directory.GetFiles(path).ToList();

            var directories = Directory.GetDirectories(path);

            foreach (var singleDirectory in directories)
                files.AddRange(GetAllFiles(singleDirectory));

            return files.ToArray();
        }

        public static async ValueTask<IEnumerable<string>> GetAllFilesAsync(string path)
        {
            return await Task.Run(() => GetAllFiles(path));
        }

        public static string ComputeHash(string filePath)
        {
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(filePath);

            var hash = md5.ComputeHash(stream);

            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        public static async Task<string> ComputeHashAsync(string filePath)
        {
            using var md5 = MD5.Create();
            await using var stream = File.OpenRead(filePath);

            var hash = await md5.ComputeHashAsync(stream);

            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}