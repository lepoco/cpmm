using System.IO;
using System.Security.Cryptography;

namespace CPMM.Core.Installer
{
    /// <summary>
    /// Facilitates directory and file management.
    /// </summary>
    internal static class IOExtension
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