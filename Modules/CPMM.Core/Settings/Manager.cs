// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using System.IO;

namespace CPMM.Core.Settings
{
    /// <summary>
    /// Stores application settings and allows to save and read them.
    /// </summary>
    public class Manager
    {
        /// <summary>
        /// Main settings directory.
        /// </summary>
        private readonly string _directory;

        /// <summary>
        /// All application options.
        /// </summary>
        public Options Options { get; internal set; } = new();

        /// <summary>
        /// Path to the application settings file.
        /// </summary>
        public string Path { get; }

        public Manager()
        {
            _directory = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "lepo_co\\cpmm"
            );

            Path = System.IO.Path.Combine(
                _directory,
                "settings.json"
            );

            Task.Run(Prepare);
        }

        /// <summary>
        /// Checks directory, creates it if needed. Saves default options.
        /// </summary>
        /// <returns></returns>
        private async Task Prepare()
        {
            if (!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);

                var directoryInfo = new DirectoryInfo(_directory);
                directoryInfo.Attributes &= ~FileAttributes.ReadOnly;

                var info = directoryInfo.GetFileSystemInfos("*", SearchOption.AllDirectories);

                foreach (var t in info)
                    t.Attributes = FileAttributes.Normal;
            }

            if (!File.Exists(Path) || new FileInfo(Path).Length == 0)
                await SaveAsync();
            else
                await ReadAsync();
        }


        public async Task SaveAsync()
        {
            if (String.IsNullOrEmpty(Path))
                throw new NullReferenceException(
                    $"ERROR | The write path must be defined in the {typeof(Manager)} constructor.");

            await Task.Run(() => JsonData.Write<Options>(Path, Options));
        }

        public async Task ReadAsync()
        {
            if (String.IsNullOrEmpty(Path))
                throw new NullReferenceException(
                    $"ERROR | The write path must be defined in the {typeof(Manager)} constructor.");

            await Task.Run(() => Options = JsonData.Read<Options>(Path));
        }
    }
}