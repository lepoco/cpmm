// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

namespace CPMM.Core.Settings
{
    public class Manager
    {
        public Options Options { get; internal set; }

        public async Task<bool> SaveAsync()
        {
            return true;
        }

        public async Task<bool> ReadAsync()
        {
            return true;
        }
    }
}
