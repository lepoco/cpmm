// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using System;
using System.Threading.Tasks;

namespace CPMM.Code
{
    internal class Middleware : IDisposable
    {
        private bool _disposed = false;

        public readonly Core.Settings.Manager Settings = new();

        ~Middleware()
        {
            Dispose();
        }

        public async Task<bool> InitializeAsync()
        {
            await Settings.ReadAsync();

            return true;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"INFO | {typeof(Middleware)} disposed, Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}", "CPMM");
#endif

            // Dispose middleware resources.
        }
    }
}
